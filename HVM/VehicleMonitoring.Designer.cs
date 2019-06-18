namespace HVM
{
    partial class txtDate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Button btnDetect;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(txtDate));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtTime = new System.Windows.Forms.TextBox();
            this.txtCamera = new System.Windows.Forms.TextBox();
            this.txtToday = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblCameraPosition = new System.Windows.Forms.Label();
            this.videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
            this.grpEventDetector = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.textStatus = new System.Windows.Forms.RichTextBox();
            this.objectsCountLabel = new System.Windows.Forms.Label();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnPause = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.pictureBox1 = new AForge.Controls.PictureBox();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.txtIncident = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblIncident = new System.Windows.Forms.Label();
            this.grpVehicleMonitoring = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox2 = new AForge.Controls.PictureBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.alarmTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            btnDetect = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.grpEventDetector.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.grpVehicleMonitoring.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDetect
            // 
            btnDetect.Location = new System.Drawing.Point(42, 40);
            btnDetect.Name = "btnDetect";
            btnDetect.Size = new System.Drawing.Size(142, 23);
            btnDetect.TabIndex = 0;
            btnDetect.Text = "Detect";
            btnDetect.UseVisualStyleBackColor = true;
            btnDetect.Click += new System.EventHandler(this.btnDetect_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtTime);
            this.groupBox2.Controls.Add(this.txtCamera);
            this.groupBox2.Controls.Add(this.txtToday);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.lblTime);
            this.groupBox2.Controls.Add(this.lblCameraPosition);
            this.groupBox2.Location = new System.Drawing.Point(753, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(219, 138);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data";
            // 
            // txtTime
            // 
            this.txtTime.Location = new System.Drawing.Point(78, 59);
            this.txtTime.Name = "txtTime";
            this.txtTime.Size = new System.Drawing.Size(128, 20);
            this.txtTime.TabIndex = 7;
            // 
            // txtCamera
            // 
            this.txtCamera.Location = new System.Drawing.Point(78, 91);
            this.txtCamera.Name = "txtCamera";
            this.txtCamera.Size = new System.Drawing.Size(128, 20);
            this.txtCamera.TabIndex = 6;
            // 
            // txtToday
            // 
            this.txtToday.Location = new System.Drawing.Point(78, 26);
            this.txtToday.Name = "txtToday";
            this.txtToday.Size = new System.Drawing.Size(128, 20);
            this.txtToday.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(16, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Date";
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(14, 61);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(30, 13);
            this.lblTime.TabIndex = 3;
            this.lblTime.Text = "Time";
            // 
            // lblCameraPosition
            // 
            this.lblCameraPosition.AutoSize = true;
            this.lblCameraPosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCameraPosition.Location = new System.Drawing.Point(14, 93);
            this.lblCameraPosition.Name = "lblCameraPosition";
            this.lblCameraPosition.Size = new System.Drawing.Size(43, 13);
            this.lblCameraPosition.TabIndex = 2;
            this.lblCameraPosition.Text = "Camera";
            // 
            // videoSourcePlayer
            // 
            this.videoSourcePlayer.Location = new System.Drawing.Point(17, 21);
            this.videoSourcePlayer.Name = "videoSourcePlayer";
            this.videoSourcePlayer.Size = new System.Drawing.Size(348, 300);
            this.videoSourcePlayer.TabIndex = 0;
            this.videoSourcePlayer.Text = "videoSourcePlayer";
            this.videoSourcePlayer.VideoSource = null;
            this.videoSourcePlayer.Click += new System.EventHandler(this.videoSourcePlayer_Click);
            this.videoSourcePlayer.NewFrame += new AForge.Controls.VideoSourcePlayer.NewFrameHandler(this.videoSourcePlayer_NewFrame);
            // 
            // grpEventDetector
            // 
            this.grpEventDetector.Controls.Add(this.button1);
            this.grpEventDetector.Controls.Add(this.btnClear);
            this.grpEventDetector.Controls.Add(this.textStatus);
            this.grpEventDetector.Controls.Add(this.objectsCountLabel);
            this.grpEventDetector.Controls.Add(this.btnStop);
            this.grpEventDetector.Controls.Add(this.btnPause);
            this.grpEventDetector.Controls.Add(this.btnOpen);
            this.grpEventDetector.Controls.Add(this.pictureBox1);
            this.grpEventDetector.Controls.Add(this.videoSourcePlayer);
            this.grpEventDetector.Location = new System.Drawing.Point(12, 12);
            this.grpEventDetector.Name = "grpEventDetector";
            this.grpEventDetector.Size = new System.Drawing.Size(726, 374);
            this.grpEventDetector.TabIndex = 24;
            this.grpEventDetector.TabStop = false;
            this.grpEventDetector.Text = "Event Detector";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(371, 332);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 29;
            this.button1.Text = "Settings";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(538, 332);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 30);
            this.btnClear.TabIndex = 28;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // textStatus
            // 
            this.textStatus.Location = new System.Drawing.Point(467, 338);
            this.textStatus.Name = "textStatus";
            this.textStatus.Size = new System.Drawing.Size(42, 23);
            this.textStatus.TabIndex = 27;
            this.textStatus.Text = "";
            this.textStatus.Visible = false;
            // 
            // objectsCountLabel
            // 
            this.objectsCountLabel.AutoSize = true;
            this.objectsCountLabel.Location = new System.Drawing.Point(639, 341);
            this.objectsCountLabel.Name = "objectsCountLabel";
            this.objectsCountLabel.Size = new System.Drawing.Size(35, 13);
            this.objectsCountLabel.TabIndex = 26;
            this.objectsCountLabel.Text = "label1";
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(290, 331);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 30);
            this.btnStop.TabIndex = 25;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(208, 331);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(75, 30);
            this.btnPause.TabIndex = 24;
            this.btnPause.Text = "Pause";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(14, 332);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(125, 30);
            this.btnOpen.TabIndex = 21;
            this.btnOpen.Text = "Open Video File";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = null;
            this.pictureBox1.Location = new System.Drawing.Point(371, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(348, 300);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(119, 131);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(174, 62);
            this.txtMessage.TabIndex = 9;
            this.txtMessage.TabStop = false;
            // 
            // txtIncident
            // 
            this.txtIncident.Location = new System.Drawing.Point(119, 41);
            this.txtIncident.Multiline = true;
            this.txtIncident.Name = "txtIncident";
            this.txtIncident.Size = new System.Drawing.Size(174, 58);
            this.txtIncident.TabIndex = 8;
            this.txtIncident.TabStop = false;
            this.txtIncident.TextChanged += new System.EventHandler(this.txtIncident_TextChanged);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(14, 134);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(50, 13);
            this.lblMessage.TabIndex = 4;
            this.lblMessage.Text = "Message";
            // 
            // lblIncident
            // 
            this.lblIncident.AutoSize = true;
            this.lblIncident.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIncident.Location = new System.Drawing.Point(14, 44);
            this.lblIncident.Name = "lblIncident";
            this.lblIncident.Size = new System.Drawing.Size(45, 13);
            this.lblIncident.TabIndex = 3;
            this.lblIncident.Text = "Incident";
            // 
            // grpVehicleMonitoring
            // 
            this.grpVehicleMonitoring.Controls.Add(this.txtMessage);
            this.grpVehicleMonitoring.Controls.Add(this.txtIncident);
            this.grpVehicleMonitoring.Controls.Add(this.lblMessage);
            this.grpVehicleMonitoring.Controls.Add(this.lblIncident);
            this.grpVehicleMonitoring.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpVehicleMonitoring.Location = new System.Drawing.Point(12, 392);
            this.grpVehicleMonitoring.Name = "grpVehicleMonitoring";
            this.grpVehicleMonitoring.Size = new System.Drawing.Size(349, 258);
            this.grpVehicleMonitoring.TabIndex = 23;
            this.grpVehicleMonitoring.TabStop = false;
            this.grpVehicleMonitoring.Text = "Vehicle Monitoring";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Location = new System.Drawing.Point(367, 392);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(371, 258);
            this.groupBox1.TabIndex = 26;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Frame Detected";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Image = null;
            this.pictureBox2.Location = new System.Drawing.Point(16, 19);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(335, 230);
            this.pictureBox2.TabIndex = 21;
            this.pictureBox2.TabStop = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(19, 393);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(168, 56);
            this.listBox1.TabIndex = 0;
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listBox1);
            this.groupBox3.Controls.Add(this.richTextBox1);
            this.groupBox3.Controls.Add(btnDetect);
            this.groupBox3.Location = new System.Drawing.Point(753, 166);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(219, 475);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Non Moving Vehicles";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(19, 87);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(187, 267);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // timer1
            // 
            this.timer1.Interval = 2500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1700;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // txtDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 662);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.grpEventDetector);
            this.Controls.Add(this.grpVehicleMonitoring);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "txtDate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vehicle Monitoring";
            this.Load += new System.EventHandler(this.VehicleMonitoring_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VehicleMonitoring_FormClosing);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpEventDetector.ResumeLayout(false);
            this.grpEventDetector.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.grpVehicleMonitoring.ResumeLayout(false);
            this.grpVehicleMonitoring.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private AForge.Controls.VideoSourcePlayer videoSourcePlayer;
        private System.Windows.Forms.GroupBox grpEventDetector;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.TextBox txtIncident;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblIncident;
        private System.Windows.Forms.GroupBox grpVehicleMonitoring;
        private AForge.Controls.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnPause;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.Timer alarmTimer;
        private System.Windows.Forms.TextBox txtCamera;
        private System.Windows.Forms.TextBox txtToday;
        private System.Windows.Forms.Label lblCameraPosition;
        private System.Windows.Forms.Label objectsCountLabel;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox textStatus;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ListBox listBox1;
        private AForge.Controls.PictureBox pictureBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
    }
}