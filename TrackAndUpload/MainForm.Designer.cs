namespace TrackAndUpload
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnSelectPath = new System.Windows.Forms.Button();
            this.lTrackedPath = new System.Windows.Forms.Label();
            this.eTrackedPath = new System.Windows.Forms.TextBox();
            this.lChangesLog = new System.Windows.Forms.Label();
            this.eChangesLog = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lTracking = new System.Windows.Forms.Label();
            this.eTracking = new System.Windows.Forms.TextBox();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // btnSelectPath
            // 
            this.btnSelectPath.Location = new System.Drawing.Point(12, 403);
            this.btnSelectPath.Name = "btnSelectPath";
            this.btnSelectPath.Size = new System.Drawing.Size(128, 35);
            this.btnSelectPath.TabIndex = 0;
            this.btnSelectPath.Text = "Select folder";
            this.btnSelectPath.UseVisualStyleBackColor = true;
            this.btnSelectPath.Click += new System.EventHandler(this.btnSelectPath_Click);
            // 
            // lTrackedPath
            // 
            this.lTrackedPath.AutoSize = true;
            this.lTrackedPath.Location = new System.Drawing.Point(12, 9);
            this.lTrackedPath.Name = "lTrackedPath";
            this.lTrackedPath.Size = new System.Drawing.Size(135, 15);
            this.lTrackedPath.TabIndex = 1;
            this.lTrackedPath.Text = "Currently tracked folder:";
            // 
            // eTrackedPath
            // 
            this.eTrackedPath.Enabled = false;
            this.eTrackedPath.Location = new System.Drawing.Point(146, 6);
            this.eTrackedPath.Name = "eTrackedPath";
            this.eTrackedPath.ReadOnly = true;
            this.eTrackedPath.Size = new System.Drawing.Size(642, 23);
            this.eTrackedPath.TabIndex = 2;
            // 
            // lChangesLog
            // 
            this.lChangesLog.AutoSize = true;
            this.lChangesLog.Location = new System.Drawing.Point(64, 43);
            this.lChangesLog.Name = "lChangesLog";
            this.lChangesLog.Size = new System.Drawing.Size(76, 15);
            this.lChangesLog.TabIndex = 1;
            this.lChangesLog.Text = "Changes log:";
            // 
            // eChangesLog
            // 
            this.eChangesLog.Enabled = false;
            this.eChangesLog.Location = new System.Drawing.Point(146, 40);
            this.eChangesLog.Multiline = true;
            this.eChangesLog.Name = "eChangesLog";
            this.eChangesLog.ReadOnly = true;
            this.eChangesLog.Size = new System.Drawing.Size(642, 302);
            this.eChangesLog.TabIndex = 2;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(526, 403);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(128, 35);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start tracking";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(660, 403);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(128, 35);
            this.btnStop.TabIndex = 0;
            this.btnStop.Text = "Stop tracking";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lTracking
            // 
            this.lTracking.AutoSize = true;
            this.lTracking.Location = new System.Drawing.Point(526, 351);
            this.lTracking.Name = "lTracking";
            this.lTracking.Size = new System.Drawing.Size(76, 15);
            this.lTracking.TabIndex = 1;
            this.lTracking.Text = "Currently on:";
            // 
            // eTracking
            // 
            this.eTracking.BackColor = System.Drawing.Color.Red;
            this.eTracking.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.eTracking.Enabled = false;
            this.eTracking.Location = new System.Drawing.Point(608, 348);
            this.eTracking.Name = "eTracking";
            this.eTracking.ReadOnly = true;
            this.eTracking.Size = new System.Drawing.Size(180, 23);
            this.eTracking.TabIndex = 2;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Track and Update";
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.eTracking);
            this.Controls.Add(this.lTracking);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.eChangesLog);
            this.Controls.Add(this.lChangesLog);
            this.Controls.Add(this.eTrackedPath);
            this.Controls.Add(this.lTrackedPath);
            this.Controls.Add(this.btnSelectPath);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Track and Upload";
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectPath;
        private System.Windows.Forms.Label lTrackedPath;
        private System.Windows.Forms.TextBox eTrackedPath;
        private System.Windows.Forms.Label lChangesLog;
        private System.Windows.Forms.TextBox eChangesLog;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lTracking;
        private System.Windows.Forms.TextBox eTracking;
        private System.Windows.Forms.NotifyIcon notifyIcon;
    }
}

