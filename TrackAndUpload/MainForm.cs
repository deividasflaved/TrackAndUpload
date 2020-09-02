using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Google.Apis.Upload;
using Google.Apis.YouTube.v3.Data;

namespace TrackAndUpload
{
    public partial class MainForm : Form
    {
        private bool _exit;
        private bool _running;

        public MainForm()
        {
            InitializeComponent();
            UpdateControls();
        }

        private void btnSelectPath_Click(object sender, EventArgs e)
        {
            using (var f = new FolderBrowserDialog())
            {
                if (f.ShowDialog() != DialogResult.OK)
                    return;

                eTrackedPath.Text = f.SelectedPath;
            }

            UpdateControls();
        }

        private void TrackChanges()
        {
            try
            {
                ClearLog();
                _exit = false;
                _running = true;
                UpdateControls();

                using (var watcher = new FileSystemWatcher())
                {
                    watcher.Path = eTrackedPath.Text;

                    // Watch for changes in LastAccess and LastWrite times, and
                    // the renaming of files or directories.
                    watcher.NotifyFilter = NotifyFilters.LastAccess
                                           | NotifyFilters.LastWrite
                                           | NotifyFilters.FileName
                                           | NotifyFilters.DirectoryName;

                    // Only watch text files.
                    watcher.Filter = "*.mp4";
                    watcher.IncludeSubdirectories = true;

                    // Add event handlers.
                    watcher.Changed += OnChanged;
                    watcher.Created += OnChanged;
                    //watcher.Deleted += OnChanged;
                    watcher.Renamed += OnRenamed;

                    // Begin watching.
                    watcher.EnableRaisingEvents = true;

                    // Wait for the user to quit the program.
                    while (!_exit)
                    {
                    }
                }
            }
            finally
            {
                _running = false;
                UpdateControls();
            }
        }

        private void OnRenamed(object sender, RenamedEventArgs e)
        {
            //UpdateLog($"File: {e.OldFullPath} renamed to {e.FullPath}");
            if(File.Exists(e.FullPath) && new FileInfo(e.FullPath).Length > 25000000)
                Run(e.FullPath);
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            //UpdateLog($"File: {e.FullPath} {e.ChangeType}");
            if (File.Exists(e.FullPath) && new FileInfo(e.FullPath).Length > 25000000)
                Run(e.FullPath);
        }

        private void btnStop_Click(object sender, EventArgs e) => _exit = true;

        private void btnStart_Click(object sender, EventArgs e)
        {
            var thread = new Thread(TrackChanges)
            {
                IsBackground = true
            };
            thread.Start();
        }

        private delegate void UpdateControlsDelegate();

        private void UpdateControls()
        {
            if (Disposing) return;

            if (InvokeRequired)
            {
                var d = new UpdateControlsDelegate(UpdateControls);
                Invoke(d);
            }
            else
            {
                eTracking.BackColor = _running ? Color.Green : Color.Red;
                btnStop.Enabled = !string.IsNullOrEmpty(eTrackedPath.Text) && _running;
                btnStart.Enabled = !string.IsNullOrEmpty(eTrackedPath.Text) && !_running;
                btnSelectPath.Enabled = !_running;
            }
        }

        private delegate void UpdateLogDelegate(string line);

        private void UpdateLog(string line)
        {
            if (Disposing) return;

            if (InvokeRequired)
            {
                var d = new UpdateLogDelegate(UpdateLog);
                Invoke(d, line);
            }
            else
            {
                eChangesLog.Text += line;
                eChangesLog.Text += Environment.NewLine;
            }
        }

        private delegate void ClearLogDelegate();

        private void ClearLog()
        {
            if (Disposing) return;

            if (InvokeRequired)
            {
                var d = new ClearLogDelegate(ClearLog);
                Invoke(d);
            }
            else
            {
                eChangesLog.Text = string.Empty;
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized) return;

            Hide();
            notifyIcon.Visible = true;
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }

        private async Task Run(string filePath)
        {
            UserCredential credential;
            using (var stream = new FileStream("..\\..\\..\\client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    // This OAuth 2.0 access scope allows an application to upload files to the
                    // authenticated user's YouTube channel, but doesn't allow other types of access.
                    new[] { YouTubeService.Scope.YoutubeUpload },
                    "user",
                    CancellationToken.None
                );
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = Assembly.GetExecutingAssembly().GetName().Name
            });

            var video = new Video
            {
                Snippet = new VideoSnippet
                {
                    Title = Path.GetFileName(filePath), Description = ""
                },
                Status = new VideoStatus {PrivacyStatus = "unlisted"}
            };
            //video.Snippet.Tags = new string[] { "tag1", "tag2" };
            //video.Snippet.CategoryId = "20"; // See https://developers.google.com/youtube/v3/docs/videoCategories/list
            // or "private" or "public"

            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var videosInsertRequest = youtubeService.Videos.Insert(video, "snippet,status", fileStream, "video/*");
                videosInsertRequest.ProgressChanged += videosInsertRequest_ProgressChanged;
                videosInsertRequest.ResponseReceived += videosInsertRequest_ResponseReceived;

                await videosInsertRequest.UploadAsync();
            }
        }

        void videosInsertRequest_ProgressChanged(Google.Apis.Upload.IUploadProgress progress)
        {
            switch (progress.Status)
            {
                case UploadStatus.Uploading:
                    Debug.WriteLine("{0} bytes sent.", progress.BytesSent);
                    break;

                case UploadStatus.Failed:
                    Debug.WriteLine("An error prevented the upload from completing.\n{0}", progress.Exception);
                    break;
            }
        }

        void videosInsertRequest_ResponseReceived(Video video)
        {
            UpdateLog($"Video id '{video.Id}' was successfully uploaded.");
        }
    }

}