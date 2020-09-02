using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

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
                    watcher.Filter = "*.txt";

                    // Add event handlers.
                    watcher.Changed += OnChanged;
                    watcher.Created += OnChanged;
                    watcher.Deleted += OnChanged;
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
            UpdateLog($"File: {e.OldFullPath} renamed to {e.FullPath}");
        }

        private void OnChanged(object sender, FileSystemEventArgs e)
        {
            UpdateLog($"File: {e.FullPath} {e.ChangeType}");
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
    }
}