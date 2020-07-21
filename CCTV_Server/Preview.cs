using log4net;
using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CCTV_Server
{
    public class Preview : Form
    {
        int[] PtzBandera = new int[80];

        private string a = "label";
        private int x = 0, y = 0;
        private Label[,] labels = new Label[3, 81];
        private Panel[] paneles = new Panel[81];
        private Button[] buttons = new Button[81];
        SqlConnection Conexion = SqlConnect.ConexionSQL();
        private uint iLastErr = 0;
        private Int32 m_lUserID = -1;
        private bool m_bInitSDK = false;
        private bool m_bRecord = false;
        private bool m_bTalk = false;
        private Int32 m_lRealHandle = -1;
        private Int32 m_lDownHandle = -1;

        private int lVoiceComHandle = -1;
        private string str;
        CHCNetSDK.REALDATACALLBACK RealData = null;
        public CHCNetSDK.NET_DVR_PTZPOS m_struPtzCfg;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnPreview;
        private System.Windows.Forms.PictureBox RealPlayWnd;
        private TextBox textBoxIP;
        private TextBox textBoxPort;
        private TextBox textBoxUserName;
        private TextBox textBoxPassword;
        private Label label9;
        private Button btnBMP;
        private Button btnJPEG;
        private Label label11;
        private Label label12;
        private Label label13;
        private TextBox textBoxChannel;
        private Button btnRecord;
        private Label label14;
        private Button btn_Exit;
        private Button btnVioceTalk;
        private Label label16;
        private Label label17;
        private TextBox textBoxID;
        private Label label19;
        private Label label20;
        private Label label21;
        private Label label22;
        private Button PreSet;
        private Label label23;
        private Panel panel11;
        private Panel pnlBase;
        private Label lblCuentaBase;
        private Button btnBase;
        private Label lblNombreBase;
        private Label lblStaBase;
        private ToolTip MyToolTip;
        private Label label10;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Panel panelStatus;
        private Label labelDB;
        private Label labelNVR;
        private Label labelClock;
        private Label lbalStatus;
        private Timer timerClock;
        private Timer tmr20s;
        private IContainer components;
        public static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Timer tmr5s;
        private Label lbl95;
        private Label lbl96;
        private Button button1;
        private Timer timerProgress;
        private Button btnVideosDAI;
        private Timer tmrVideosInci;
        private Button button2;
        private int xx = 0;


        public Preview()
        {
            InitializeComponent();
            m_bInitSDK = CHCNetSDK.NET_DVR_Init();
            if (m_bInitSDK == false)
            {
                //// .Show("NET_DVR_Init error!");
                return;
            }
            else
            {
                CHCNetSDK.NET_DVR_SetLogToFile(3, "C:\\SdkLog\\", true);
            }
            Organizar();
            //   var sVideoPre = ConfigurationManager.AppSettings["sVideoPre"];
            // var sVideoDur = ConfigurationManager.AppSettings["sVideoDur"];

        }
        protected override void Dispose(bool disposing)
        {
            if (m_lRealHandle >= 0)
            {
                CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle);
            }
            if (m_lUserID >= 0)
            {
                CHCNetSDK.NET_DVR_Logout(m_lUserID);
            }
            if (m_bInitSDK == true)
            {
                CHCNetSDK.NET_DVR_Cleanup();
            }
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Preview));
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.RealPlayWnd = new System.Windows.Forms.PictureBox();
            this.textBoxIP = new System.Windows.Forms.TextBox();
            this.textBoxPort = new System.Windows.Forms.TextBox();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnBMP = new System.Windows.Forms.Button();
            this.btnJPEG = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.textBoxChannel = new System.Windows.Forms.TextBox();
            this.btnRecord = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.btnVioceTalk = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBoxID = new System.Windows.Forms.TextBox();
            this.PreSet = new System.Windows.Forms.Button();
            this.label23 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlBase = new System.Windows.Forms.Panel();
            this.lblCuentaBase = new System.Windows.Forms.Label();
            this.btnBase = new System.Windows.Forms.Button();
            this.lblNombreBase = new System.Windows.Forms.Label();
            this.lblStaBase = new System.Windows.Forms.Label();
            this.MyToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.panelStatus = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnVideosDAI = new System.Windows.Forms.Button();
            this.lbl95 = new System.Windows.Forms.Label();
            this.lbl96 = new System.Windows.Forms.Label();
            this.labelDB = new System.Windows.Forms.Label();
            this.labelNVR = new System.Windows.Forms.Label();
            this.labelClock = new System.Windows.Forms.Label();
            this.lbalStatus = new System.Windows.Forms.Label();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.tmr20s = new System.Windows.Forms.Timer(this.components);
            this.tmr5s = new System.Windows.Forms.Timer(this.components);
            this.timerProgress = new System.Windows.Forms.Timer(this.components);
            this.tmrVideosInci = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.RealPlayWnd)).BeginInit();
            this.panel11.SuspendLayout();
            this.pnlBase.SuspendLayout();
            this.panelStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(269, 3);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(65, 20);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "Login";
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.Location = new System.Drawing.Point(3, 188);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(63, 32);
            this.btnPreview.TabIndex = 7;
            this.btnPreview.Text = "Live View";
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // RealPlayWnd
            // 
            this.RealPlayWnd.BackColor = System.Drawing.SystemColors.WindowText;
            this.RealPlayWnd.Location = new System.Drawing.Point(3, 29);
            this.RealPlayWnd.Name = "RealPlayWnd";
            this.RealPlayWnd.Size = new System.Drawing.Size(177, 81);
            this.RealPlayWnd.TabIndex = 4;
            this.RealPlayWnd.TabStop = false;
            // 
            // textBoxIP
            // 
            this.textBoxIP.Location = new System.Drawing.Point(3, 3);
            this.textBoxIP.Name = "textBoxIP";
            this.textBoxIP.Size = new System.Drawing.Size(95, 20);
            this.textBoxIP.TabIndex = 2;
            this.textBoxIP.Text = "192.168.25.155";
            // 
            // textBoxPort
            // 
            this.textBoxPort.Location = new System.Drawing.Point(104, 3);
            this.textBoxPort.Name = "textBoxPort";
            this.textBoxPort.Size = new System.Drawing.Size(32, 20);
            this.textBoxPort.TabIndex = 3;
            this.textBoxPort.Text = "8000";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(142, 3);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(69, 20);
            this.textBoxUserName.TabIndex = 4;
            this.textBoxUserName.Text = "visualizacion";
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBoxPassword.Location = new System.Drawing.Point(217, 3);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '*';
            this.textBoxPassword.Size = new System.Drawing.Size(46, 20);
            this.textBoxPassword.TabIndex = 5;
            this.textBoxPassword.Text = "sier2019*";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 169);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "preview";
            // 
            // btnBMP
            // 
            this.btnBMP.Location = new System.Drawing.Point(81, 189);
            this.btnBMP.Name = "btnBMP";
            this.btnBMP.Size = new System.Drawing.Size(65, 32);
            this.btnBMP.TabIndex = 8;
            this.btnBMP.Text = "Capture BMP ";
            this.btnBMP.UseVisualStyleBackColor = true;
            this.btnBMP.Click += new System.EventHandler(this.btnBMP_Click);
            // 
            // btnJPEG
            // 
            this.btnJPEG.Location = new System.Drawing.Point(162, 188);
            this.btnJPEG.Name = "btnJPEG";
            this.btnJPEG.Size = new System.Drawing.Size(81, 32);
            this.btnJPEG.TabIndex = 9;
            this.btnJPEG.Text = "Capture JPEG";
            this.btnJPEG.UseVisualStyleBackColor = true;
            this.btnJPEG.Click += new System.EventHandler(this.btnJPEG_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(83, 169);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(69, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "BMP capture";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(167, 169);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(73, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "JPEG capture";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(3, 142);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(126, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "preview/capture channel";
            // 
            // textBoxChannel
            // 
            this.textBoxChannel.Location = new System.Drawing.Point(131, 138);
            this.textBoxChannel.Name = "textBoxChannel";
            this.textBoxChannel.Size = new System.Drawing.Size(46, 20);
            this.textBoxChannel.TabIndex = 6;
            this.textBoxChannel.Text = "1";
            // 
            // btnRecord
            // 
            this.btnRecord.Location = new System.Drawing.Point(255, 188);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(83, 32);
            this.btnRecord.TabIndex = 10;
            this.btnRecord.Text = "Start Record";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(256, 169);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 13);
            this.label14.TabIndex = 22;
            this.label14.Text = "client recording";
            // 
            // btn_Exit
            // 
            this.btn_Exit.Location = new System.Drawing.Point(354, 229);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(62, 30);
            this.btn_Exit.TabIndex = 11;
            this.btn_Exit.Tag = "";
            this.btn_Exit.Text = "Exit";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btnVioceTalk
            // 
            this.btnVioceTalk.Location = new System.Drawing.Point(4, 253);
            this.btnVioceTalk.Name = "btnVioceTalk";
            this.btnVioceTalk.Size = new System.Drawing.Size(62, 32);
            this.btnVioceTalk.TabIndex = 25;
            this.btnVioceTalk.Text = "Start Talk";
            this.btnVioceTalk.UseVisualStyleBackColor = true;
            this.btnVioceTalk.Click += new System.EventHandler(this.btnVioceTalk_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(4, 235);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 13);
            this.label16.TabIndex = 26;
            this.label16.Text = "TwoWayAudio";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(187, 141);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(52, 13);
            this.label17.TabIndex = 27;
            this.label17.Text = "stream ID";
            // 
            // textBoxID
            // 
            this.textBoxID.Location = new System.Drawing.Point(239, 137);
            this.textBoxID.Name = "textBoxID";
            this.textBoxID.Size = new System.Drawing.Size(164, 20);
            this.textBoxID.TabIndex = 28;
            // 
            // PreSet
            // 
            this.PreSet.Location = new System.Drawing.Point(85, 253);
            this.PreSet.Name = "PreSet";
            this.PreSet.Size = new System.Drawing.Size(81, 31);
            this.PreSet.TabIndex = 31;
            this.PreSet.Text = "PTZ Control";
            this.PreSet.UseVisualStyleBackColor = true;
            this.PreSet.Click += new System.EventHandler(this.PreSet_Click);
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(88, 235);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(63, 13);
            this.label23.TabIndex = 32;
            this.label23.Text = "PTZ control";
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel11.Controls.Add(this.textBoxIP);
            this.panel11.Controls.Add(this.label23);
            this.panel11.Controls.Add(this.textBoxPort);
            this.panel11.Controls.Add(this.label10);
            this.panel11.Controls.Add(this.PreSet);
            this.panel11.Controls.Add(this.label1);
            this.panel11.Controls.Add(this.textBoxUserName);
            this.panel11.Controls.Add(this.label2);
            this.panel11.Controls.Add(this.textBoxID);
            this.panel11.Controls.Add(this.label3);
            this.panel11.Controls.Add(this.label4);
            this.panel11.Controls.Add(this.label17);
            this.panel11.Controls.Add(this.textBoxPassword);
            this.panel11.Controls.Add(this.label16);
            this.panel11.Controls.Add(this.btnLogin);
            this.panel11.Controls.Add(this.btnVioceTalk);
            this.panel11.Controls.Add(this.RealPlayWnd);
            this.panel11.Controls.Add(this.btn_Exit);
            this.panel11.Controls.Add(this.btnPreview);
            this.panel11.Controls.Add(this.label14);
            this.panel11.Controls.Add(this.label9);
            this.panel11.Controls.Add(this.btnRecord);
            this.panel11.Controls.Add(this.btnBMP);
            this.panel11.Controls.Add(this.textBoxChannel);
            this.panel11.Controls.Add(this.btnJPEG);
            this.panel11.Controls.Add(this.label13);
            this.panel11.Controls.Add(this.label11);
            this.panel11.Controls.Add(this.label12);
            this.panel11.Location = new System.Drawing.Point(668, 12);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(429, 348);
            this.panel11.TabIndex = 38;
            this.panel11.Visible = false;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(270, 305);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 21);
            this.label10.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(273, 305);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 21);
            this.label1.TabIndex = 34;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(248, 294);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 32);
            this.label2.TabIndex = 35;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(187, 262);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 64);
            this.label3.TabIndex = 36;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(234, 279);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(131, 47);
            this.label4.TabIndex = 37;
            // 
            // pnlBase
            // 
            this.pnlBase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.pnlBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlBase.Controls.Add(this.lblCuentaBase);
            this.pnlBase.Controls.Add(this.btnBase);
            this.pnlBase.Controls.Add(this.lblNombreBase);
            this.pnlBase.Controls.Add(this.lblStaBase);
            this.pnlBase.Location = new System.Drawing.Point(907, 518);
            this.pnlBase.Name = "pnlBase";
            this.pnlBase.Size = new System.Drawing.Size(300, 28);
            this.pnlBase.TabIndex = 39;
            this.pnlBase.Visible = false;
            // 
            // lblCuentaBase
            // 
            this.lblCuentaBase.AutoSize = true;
            this.lblCuentaBase.ForeColor = System.Drawing.SystemColors.Window;
            this.lblCuentaBase.Location = new System.Drawing.Point(267, 7);
            this.lblCuentaBase.Name = "lblCuentaBase";
            this.lblCuentaBase.Size = new System.Drawing.Size(25, 13);
            this.lblCuentaBase.TabIndex = 3;
            this.lblCuentaBase.Text = "300";
            // 
            // btnBase
            // 
            this.btnBase.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnBase.Location = new System.Drawing.Point(232, 4);
            this.btnBase.Margin = new System.Windows.Forms.Padding(0);
            this.btnBase.Name = "btnBase";
            this.btnBase.Size = new System.Drawing.Size(30, 18);
            this.btnBase.TabIndex = 2;
            this.btnBase.Text = "P1";
            this.btnBase.UseVisualStyleBackColor = false;
            this.btnBase.Click += new System.EventHandler(this.button3_Click);
            // 
            // lblNombreBase
            // 
            this.lblNombreBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreBase.ForeColor = System.Drawing.Color.Gray;
            this.lblNombreBase.Location = new System.Drawing.Point(39, 4);
            this.lblNombreBase.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lblNombreBase.Name = "lblNombreBase";
            this.lblNombreBase.Size = new System.Drawing.Size(193, 18);
            this.lblNombreBase.TabIndex = 1;
            this.lblNombreBase.Text = "cr 64 call 54 suramericana ";
            this.lblNombreBase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStaBase
            // 
            this.lblStaBase.BackColor = System.Drawing.Color.LimeGreen;
            this.lblStaBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblStaBase.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStaBase.ForeColor = System.Drawing.Color.Gray;
            this.lblStaBase.Location = new System.Drawing.Point(3, 5);
            this.lblStaBase.Name = "lblStaBase";
            this.lblStaBase.Size = new System.Drawing.Size(30, 18);
            this.lblStaBase.TabIndex = 0;
            this.lblStaBase.Text = "STA";
            // 
            // panelStatus
            // 
            this.panelStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(49)))), ((int)(((byte)(54)))));
            this.panelStatus.Controls.Add(this.button2);
            this.panelStatus.Controls.Add(this.button1);
            this.panelStatus.Controls.Add(this.btnVideosDAI);
            this.panelStatus.Controls.Add(this.lbl95);
            this.panelStatus.Controls.Add(this.lbl96);
            this.panelStatus.Controls.Add(this.labelDB);
            this.panelStatus.Controls.Add(this.labelNVR);
            this.panelStatus.Controls.Add(this.labelClock);
            this.panelStatus.Controls.Add(this.lbalStatus);
            this.panelStatus.Location = new System.Drawing.Point(7, 638);
            this.panelStatus.Name = "panelStatus";
            this.panelStatus.Size = new System.Drawing.Size(1205, 28);
            this.panelStatus.TabIndex = 40;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(765, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 42;
            this.button2.Text = "Grabar_incidentes";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(851, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 22);
            this.button1.TabIndex = 41;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnVideosDAI
            // 
            this.btnVideosDAI.Location = new System.Drawing.Point(930, 3);
            this.btnVideosDAI.Name = "btnVideosDAI";
            this.btnVideosDAI.Size = new System.Drawing.Size(75, 22);
            this.btnVideosDAI.TabIndex = 8;
            this.btnVideosDAI.Text = "Videos DAI";
            this.btnVideosDAI.UseVisualStyleBackColor = true;
            this.btnVideosDAI.Click += new System.EventHandler(this.btnVideosDAI_Click);
            // 
            // lbl95
            // 
            this.lbl95.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl95.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl95.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl95.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbl95.Location = new System.Drawing.Point(1007, 5);
            this.lbl95.Margin = new System.Windows.Forms.Padding(0);
            this.lbl95.Name = "lbl95";
            this.lbl95.Size = new System.Drawing.Size(26, 18);
            this.lbl95.TabIndex = 7;
            this.lbl95.Text = "95";
            this.lbl95.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl96
            // 
            this.lbl96.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl96.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl96.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl96.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbl96.Location = new System.Drawing.Point(1036, 5);
            this.lbl96.Margin = new System.Windows.Forms.Padding(0);
            this.lbl96.Name = "lbl96";
            this.lbl96.Size = new System.Drawing.Size(26, 18);
            this.lbl96.TabIndex = 6;
            this.lbl96.Text = "96";
            this.lbl96.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDB
            // 
            this.labelDB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelDB.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelDB.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelDB.Location = new System.Drawing.Point(1064, 5);
            this.labelDB.Margin = new System.Windows.Forms.Padding(0);
            this.labelDB.Name = "labelDB";
            this.labelDB.Size = new System.Drawing.Size(26, 18);
            this.labelDB.TabIndex = 5;
            this.labelDB.Text = "DB";
            this.labelDB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelNVR
            // 
            this.labelNVR.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelNVR.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelNVR.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNVR.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelNVR.Location = new System.Drawing.Point(1092, 5);
            this.labelNVR.Margin = new System.Windows.Forms.Padding(0);
            this.labelNVR.Name = "labelNVR";
            this.labelNVR.Size = new System.Drawing.Size(34, 18);
            this.labelNVR.TabIndex = 4;
            this.labelNVR.Text = "NVR";
            this.labelNVR.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelClock
            // 
            this.labelClock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelClock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelClock.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelClock.ForeColor = System.Drawing.SystemColors.GrayText;
            this.labelClock.Location = new System.Drawing.Point(1128, 5);
            this.labelClock.Margin = new System.Windows.Forms.Padding(0);
            this.labelClock.Name = "labelClock";
            this.labelClock.Size = new System.Drawing.Size(74, 18);
            this.labelClock.TabIndex = 3;
            this.labelClock.Text = "01:02:03";
            this.labelClock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbalStatus
            // 
            this.lbalStatus.AutoSize = true;
            this.lbalStatus.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lbalStatus.Location = new System.Drawing.Point(6, 8);
            this.lbalStatus.Name = "lbalStatus";
            this.lbalStatus.Size = new System.Drawing.Size(37, 13);
            this.lbalStatus.TabIndex = 0;
            this.lbalStatus.Text = "Status";
            // 
            // timerClock
            // 
            this.timerClock.Enabled = true;
            this.timerClock.Interval = 400;
            this.timerClock.Tick += new System.EventHandler(this.timerCloclk_Tick);
            // 
            // tmr20s
            // 
            this.tmr20s.Enabled = true;
            this.tmr20s.Interval = 15000;
            this.tmr20s.Tick += new System.EventHandler(this.tmr20s_Tick);
            // 
            // tmr5s
            // 
            this.tmr5s.Enabled = true;
            this.tmr5s.Interval = 5000;
            this.tmr5s.Tick += new System.EventHandler(this.tmr1s_Tick);
            // 
            // timerProgress
            // 
            this.timerProgress.Interval = 1000;
            this.timerProgress.Tick += new System.EventHandler(this.timerProgress_Tick);
            // 
            // tmrVideosInci
            // 
            this.tmrVideosInci.Interval = 60000;
            this.tmrVideosInci.Tick += new System.EventHandler(this.tmrVideosInci_Tick);
            // 
            // Preview
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(1215, 670);
            this.Controls.Add(this.panelStatus);
            this.Controls.Add(this.pnlBase);
            this.Controls.Add(this.panel11);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1231, 709);
            this.MinimumSize = new System.Drawing.Size(1231, 709);
            this.Name = "Preview";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestor de Video - Server";
            this.Load += new System.EventHandler(this.Preview_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RealPlayWnd)).EndInit();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.pnlBase.ResumeLayout(false);
            this.pnlBase.PerformLayout();
            this.panelStatus.ResumeLayout(false);
            this.panelStatus.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion
        //[STAThread]
        //static void Main()
        //{
        //    Application.Run(new Preview());
        //}
        private void btnLogin_Click(object sender, System.EventArgs e)
        {
            if (textBoxIP.Text == "" || textBoxPort.Text == "" ||
                textBoxUserName.Text == "" || textBoxPassword.Text == "")
            {
                //// .Show("Please input IP, Port, User name and Password!");
                return;
            }
            if (m_lUserID < 0)
            {
                string DVRIPAddress = textBoxIP.Text; //设备IP地址或者域名
                Int16 DVRPortNumber = Int16.Parse(textBoxPort.Text);//设备服务端口号
                string DVRUserName = textBoxUserName.Text;//设备登录用户名
                string DVRPassword = textBoxPassword.Text;//设备登录密码
                CHCNetSDK.NET_DVR_DEVICEINFO_V30 DeviceInfo = new CHCNetSDK.NET_DVR_DEVICEINFO_V30();
                //登录设备 Login the device
                m_lUserID = CHCNetSDK.NET_DVR_Login_V30(DVRIPAddress, DVRPortNumber, DVRUserName, DVRPassword, ref DeviceInfo);
                if (m_lUserID < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_Login_V30 failed, error code= " + iLastErr; //登录失败，输出错误号
                    log.Error(str);                                                          //    // .Show(str);
                                                                                             //   // .Show("Login NVR error");

                    return;
                }
                else
                {
                    //登录成功
                    //   // .Show("Login NVR exitoso");
                    btnLogin.Text = "Logout";
                }
            }
            else
            {
                //注销登录 Logout the device
                if (m_lRealHandle >= 0)
                {
                    //// .Show("Please stop live view firstly");
                    return;
                }
                if (!CHCNetSDK.NET_DVR_Logout(m_lUserID))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_Logout failed, error code= " + iLastErr;
                    // .Show(str);
                    return;
                }
                m_lUserID = -1;
                btnLogin.Text = "Login";
            }
            return;
        }
        private void btnPreview_Click(object sender, System.EventArgs e)
        {
            if (m_lUserID < 0)
            {
                //// .Show("Please login the device firstly");
                return;
            }
            if (m_lRealHandle < 0)
            {
                CHCNetSDK.NET_DVR_PREVIEWINFO lpPreviewInfo = new CHCNetSDK.NET_DVR_PREVIEWINFO();
                lpPreviewInfo.hPlayWnd = RealPlayWnd.Handle;//预览窗口
                lpPreviewInfo.lChannel = Int16.Parse(textBoxChannel.Text);//预te览的设备通道
                lpPreviewInfo.dwStreamType = 0;//码流类型：0-主码流，1-子码流，2-码流3，3-码流4，以此类推
                lpPreviewInfo.dwLinkMode = 0;//连接方式：0- TCP方式，1- UDP方式，2- 多播方式，3- RTP方式，4-RTP/RTSP，5-RSTP/HTTP 
                lpPreviewInfo.bBlocked = true; //0- 非阻塞取流，1- 阻塞取流
                lpPreviewInfo.dwDisplayBufNum = 1; //播放库播放缓冲区最大缓冲帧数
                lpPreviewInfo.byProtoType = 0;
                lpPreviewInfo.byPreviewMode = 0;
                if (textBoxID.Text != "")
                {
                    lpPreviewInfo.lChannel = -1;
                    byte[] byStreamID = System.Text.Encoding.Default.GetBytes(textBoxID.Text);
                    lpPreviewInfo.byStreamID = new byte[32];
                    byStreamID.CopyTo(lpPreviewInfo.byStreamID, 0);
                }
                if (RealData == null)
                {
                    RealData = new CHCNetSDK.REALDATACALLBACK(RealDataCallBack);//预览实时流回调函数
                }
                IntPtr pUser = new IntPtr();//用户数据
                //打开预览 Start live view 
                m_lRealHandle = CHCNetSDK.NET_DVR_RealPlay_V40(m_lUserID, ref lpPreviewInfo, null/*RealData*/, pUser);
                if (m_lRealHandle < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_RealPlay_V40 failed, error code= " + iLastErr; //预览失败，输出错误号
                    // .Show(str);
                    return;
                }
                else
                {
                    //预览成功
                    btnPreview.Text = "Stop Live View";
                }
            }
            else
            {
                //停止预览 Stop live view 
                if (!CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopRealPlay failed, error code= " + iLastErr;
                    // .Show(str);
                    return;
                }
                m_lRealHandle = -1;
                btnPreview.Text = "Live View";
            }
            return;
        }
        public void RealDataCallBack(Int32 lRealHandle, UInt32 dwDataType, IntPtr pBuffer, UInt32 dwBufSize, IntPtr pUser)
        {
            if (dwBufSize > 0)
            {
                byte[] sData = new byte[dwBufSize];
                Marshal.Copy(pBuffer, sData, 0, (Int32)dwBufSize);
                string str = "实时流数据.ps";
                FileStream fs = new FileStream(str, FileMode.Create);
                int iLen = (int)dwBufSize;
                fs.Write(sData, 0, iLen);
                fs.Close();
            }
        }
        private void btnBMP_Click(object sender, EventArgs e)
        {
            string sBmpPicFileName;
            //图片保存路径和文件名 the path and file name to save
            sBmpPicFileName = "BMP_test.bmp";
            //BMP抓图 Capture a BMP picture
            if (!CHCNetSDK.NET_DVR_CapturePicture(m_lRealHandle, sBmpPicFileName))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_CapturePicture failed, error code= " + iLastErr;
                // .Show(str);
                return;
            }
            else
            {
                str = "Successful to capture the BMP file and the saved file is " + sBmpPicFileName;
                // .Show(str);
            }
            return;
        }
        private void btnJPEG_Click(object sender, EventArgs e)
        {
            string sJpegPicFileName;
            //图片保存路径和文件名 the path and file name to save
            sJpegPicFileName = "JPEG_test.jpg";
            int lChannel = Int16.Parse(textBoxChannel.Text); //通道号 Channel number
            CHCNetSDK.NET_DVR_JPEGPARA lpJpegPara = new CHCNetSDK.NET_DVR_JPEGPARA();
            lpJpegPara.wPicQuality = 0; //图像质量 Image quality
            lpJpegPara.wPicSize = 0xff; //抓图分辨率 Picture size: 2- 4CIF，0xff- Auto(使用当前码流分辨率)，抓图分辨率需要设备支持，更多取值请参考SDK文档
            //JPEG抓图 Capture a JPEG picture
            if (!CHCNetSDK.NET_DVR_CaptureJPEGPicture(m_lUserID, lChannel, ref lpJpegPara, sJpegPicFileName))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_CaptureJPEGPicture failed, error code= " + iLastErr;
                // .Show(str);
                return;
            }
            else
            {
                str = "Successful to capture the JPEG file and the saved file is " + sJpegPicFileName;
                // .Show(str);
            }
            return;
        }
        private void btnRecord_Click(object sender, EventArgs e)
        {
            //录像保存路径和文件名 the path and file name to save
            string sVideoFileName;
            sVideoFileName = "Record_test.mp4";
            if (m_bRecord == false)
            {
                //强制I帧 Make a I frame
                int lChannel = Int16.Parse(textBoxChannel.Text); //通道号 Channel number
                CHCNetSDK.NET_DVR_MakeKeyFrame(m_lUserID, lChannel);
                //开始录像 Start recording
                if (!CHCNetSDK.NET_DVR_SaveRealData(m_lRealHandle, sVideoFileName))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_SaveRealData failed, error code= " + iLastErr;
                    // .Show(str);
                    return;
                }
                else
                {
                    btnRecord.Text = "Stop Record";
                    m_bRecord = true;
                }
            }
            else
            {
                //停止录像 Stop recording
                if (!CHCNetSDK.NET_DVR_StopSaveRealData(m_lRealHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopSaveRealData failed, error code= " + iLastErr;
                    // .Show(str);
                    return;
                }
                else
                {
                    str = "Successful to stop recording and the saved file is " + sVideoFileName;
                    // .Show(str);
                    btnRecord.Text = "Start Record";
                    m_bRecord = false;
                }
            }
            return;
        }
        private void btn_Exit_Click(object sender, EventArgs e)
        {
            //停止预览 Stop live view 
            if (m_lRealHandle >= 0)
            {
                CHCNetSDK.NET_DVR_StopRealPlay(m_lRealHandle);
                m_lRealHandle = -1;
            }
            //注销登录 Logout the device
            if (m_lUserID >= 0)
            {
                CHCNetSDK.NET_DVR_Logout(m_lUserID);
                m_lUserID = -1;
            }
            CHCNetSDK.NET_DVR_Cleanup();
            Application.Exit();
        }
        private void btnPTZ_Click(object sender, EventArgs e)
        {

        }
        public void VoiceDataCallBack(int lVoiceComHandle, IntPtr pRecvDataBuffer, uint dwBufSize, byte byAudioFlag, System.IntPtr pUser)
        {
            byte[] sString = new byte[dwBufSize];
            Marshal.Copy(pRecvDataBuffer, sString, 0, (Int32)dwBufSize);
            if (byAudioFlag == 0)
            {
                //将缓冲区里的音频数据写入文件 save the data into a file
                string str = "PC采集音频文件.pcm";
                FileStream fs = new FileStream(str, FileMode.Create);
                int iLen = (int)dwBufSize;
                fs.Write(sString, 0, iLen);
                fs.Close();
            }
            if (byAudioFlag == 1)
            {
                //将缓冲区里的音频数据写入文件 save the data into a file
                string str = "设备音频文件.pcm";
                FileStream fs = new FileStream(str, FileMode.Create);
                int iLen = (int)dwBufSize;
                fs.Write(sString, 0, iLen);
                fs.Close();
            }
        }
        private void btnVioceTalk_Click(object sender, EventArgs e)
        {
            if (m_bTalk == false)
            {
                //开始语音对讲 Start two-way talk
                CHCNetSDK.VOICEDATACALLBACKV30 VoiceData = new CHCNetSDK.VOICEDATACALLBACKV30(VoiceDataCallBack);//预览实时流回调函数
                lVoiceComHandle = CHCNetSDK.NET_DVR_StartVoiceCom_V30(m_lUserID, 1, true, VoiceData, IntPtr.Zero);
                //bNeedCBNoEncData [in]需要回调的语音数据类型：0- 编码后的语音数据，1- 编码前的PCM原始数据
                if (lVoiceComHandle < 0)
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StartVoiceCom_V30 failed, error code= " + iLastErr;
                    // .Show(str);
                    return;
                }
                else
                {
                    btnVioceTalk.Text = "Stop Talk";
                    m_bTalk = true;
                }
            }
            else
            {
                //停止语音对讲 Stop two-way talk
                if (!CHCNetSDK.NET_DVR_StopVoiceCom(lVoiceComHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopVoiceCom failed, error code= " + iLastErr;
                    // .Show(str);
                    return;
                }
                else
                {
                    btnVioceTalk.Text = "Start Talk";
                    m_bTalk = false;
                }
            }
        }
        private void Preview_Load(object sender, EventArgs e)
        {
            log.Info("Arrancando App");
            // btnLogin.PerformClick();
            int timerVideos = (Convert.ToInt32(ConfigurationManager.AppSettings["PeriodoVideos"])) * 60000;
            tmrVideosInci.Interval = timerVideos;
            for (int a = 0; a < 80; a++)
            {
                Bandera_citi[a] = false;
            }
            btnLogin_Click(null, null);
            //  // .Show(Dns.GetHostName());
        }
        private void PreSet_Click(object sender, EventArgs e)
        {
            PreSet dlg = new PreSet();
            dlg.m_lUserID = m_lUserID;
            dlg.m_lChannel = 1;
            dlg.m_lRealHandle = m_lRealHandle;
            dlg.ShowDialog();

        }
        private void button3_Click(object sender, EventArgs e)
        {
            Button btnPisado = (Button)sender;
            //   // .Show("Sender:" + sender.ToString() + "  Tag:" + btnPisado.Tag);
            log.Info("Preset " + btnPisado.Tag.ToString());
            int iChannel = int.Parse(btnPisado.Tag.ToString()) + 1;
            labels[2, int.Parse(btnPisado.Tag.ToString())].Text = "0";
            int codigo = 0;
            string query = "UPDATE [COVIANDES_ADAP_NKF].[dbo].[CAM_EQUIVALENCIA] SET BANDERA=0, CONTEO=0 WHERE CANAL=" + iChannel.ToString();// Preset 1 // Button = 0..79
            Conexion.Open();
            SqlCommand comando2 = new SqlCommand(query, Conexion);
            comando2.ExecuteNonQuery();
            query = "select codigo_hermes from [COVIANDES_ADAP_NKF].[dbo].[CAM_EQUIVALENCIA] WHERE CANAL=" + iChannel.ToString();
            comando2.CommandText = query;
            SqlDataReader dr = comando2.ExecuteReader();
            while (dr.Read())
            {
                codigo = dr.GetInt32(0);
            }
            Conexion.Close();
            Citilog.mover_home(codigo);

            if (!CHCNetSDK.NET_DVR_PTZPreset_Other(m_lUserID, iChannel, CHCNetSDK.GOTO_PRESET, 1))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_PTZPreset_Other failed, error code= " + iLastErr;
                // .Show(str + " - Error al ejecutar el Preset");
                log.Info("Error al ejecutar el Preset: Canal " + btnPisado.Tag.ToString());
            }
            else
            {
                //  // .Show("Llamada Exitosa");
                log.Info("Ejecuci髇 exitosa del Preset: Canal " + btnPisado.Tag.ToString());
            }
            // // .Show("please input the PtrPreSetNo");
        }
        private void timerCloclk_Tick(object sender, EventArgs e)
        {
            labelClock.Text = DateTime.Now.ToString("HH:mm:ss");
        }
        public bool[] Bandera_citi = new bool[80];
        private void tmr20s_Tick(object sender, EventArgs e)
        {
            try
            {
                Conexion.Open();
                labelDB.BackColor = Color.DarkGreen;   // Pintar estado de conectividad
                log.Info("Conectando a Bases de Datos");
                string query, tipo;
                query = "SELECT [CANAL],[BANDERA], [CONTEO],[CODIGO_HERMES] FROM [COVIANDES_ADAP_NKF].[dbo].[CAM_EQUIVALENCIA]";
                SqlCommand comando = new SqlCommand(query, Conexion);
                SqlCommand comando2 = new SqlCommand(query, Conexion);
                log.Info("Se realiza consulta");
                SqlDataReader dr = comando.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                Conexion.Close();



                string s = "";
                //while (dr.Read())
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //               log.Info("se apaga el panel " + dr.GetString(dr.GetOrdinal("O2")));
                    s += dt.Rows[i][0].ToString();
                    int iIdCode = Convert.ToInt32(dt.Rows[i][0].ToString()) - 1;  // CANAL = 1..80
                    int iBand = Convert.ToInt32(dt.Rows[i][1].ToString());        // BANDERA
                    int iCont = Convert.ToInt32(dt.Rows[i][2].ToString());        // CUENTA 
                    try
                    {
                        if (iCont > 0)
                        {
                            if (Bandera_citi[i] == false)
                            {
                                log.Info("Se envia salida de conteo a Citilog de la Camara " + iIdCode);
                                Citilog.mover_ptz(Convert.ToInt32(dt.Rows[i][3].ToString()));
                                Bandera_citi[i] = true;
                                //enviamos ptz a citilog
                            }
                        }

                        if ((iIdCode >= 0) && (iIdCode < 80))
                        {
                            labels[2, iIdCode].Text = iCont.ToString();
                            labels[2, iIdCode].Refresh();

                            PtzBandera[iIdCode] = iBand;
                            // Verificar las banderas
                            if (iBand == 1)
                            {
                                log.Info(iIdCode.ToString());
                                if ((iIdCode >= 0) && (iIdCode < 80))
                                    labels[2, iIdCode].Text = "0";

                                query = "UPDATE [COVIANDES_ADAP_NKF].[dbo].[CAM_EQUIVALENCIA] SET BANDERA=0 WHERE CANAL=" + (iIdCode + 1).ToString();// Preset 1 // Button = 0..79
                                Conexion.Open();
                                comando2.CommandText = query;
                                comando2.ExecuteNonQuery();
                                log.Info("Se entrada de conteo a Citilog de la Camara " + iIdCode);
                                Citilog.mover_home(Convert.ToInt32(dt.Rows[i][3].ToString()));
                                Conexion.Close();
                                Bandera_citi[i] = false;
                                buttons[iIdCode].PerformClick();//ENVIAMOS CONFIRMACION A CITILOG
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        log.Info("Catch : " + iIdCode.ToString());

                    }
                }
                //dr.Close();
                //for (int iCanal = 0; iCanal < 80; iCanal++)
                //{
                //    if (PtzBandera[iCanal] == 1)
                //    {

                //        query = "UPDATE [COVIANDES_ADAP_NKF].[dbo].[CAM_EQUIVALENCIA] SET BANDERA=0 WHERE CANAL=" + (iCanal + 1).ToString();
                //        comando.CommandText = query;
                //        comando.ExecuteNonQuery();

                //    }
                //}
                //Conexion.Close();
            }
            catch (SqlException)
            {
                Conexion.Close();
                labelDB.BackColor = Color.DarkRed;     // Pintar estado de conectividad
            }
        }
        private void tmr1s_Tick(object sender, EventArgs e)
        {
            for (int iIndice = 0; iIndice < 80; iIndice++)
            {
                // Actualizar los conteos
                int iCuenta = int.Parse(labels[2, iIndice].Text.ToString());
                iCuenta -= 5;
                if (iCuenta > 0)
                    labels[2, iIndice].Text = iCuenta.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tomarVideo("2020071517110000-UNO", 11, "prueba");
            tomarVideo("2020071517110000-DOS", 12, "prueba");
            tomarVideo("2020071517110000-TRES", 13, "prueba");
            tomarVideo("2020071517110000-CUATRO", 14, "prueba");

            //  tomarVideo("20200629140000", 1);



        }
        private DateTime FromUnixTime(long unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }
        private long ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalSeconds);
        }
        private void tomarVideo(string sIncidencia, int iCanal, string tipoInci)
        {
            uint uiCanal = (uint)(iCanal);
            DateTime dtStart, dtEnd, dtIncidencia;

            if (m_lDownHandle >= 0)
            {
                //// .Show("Downloading, please stop firstly!");//Please stop downloading
                return;
            }

            dtIncidencia = DateTime.ParseExact(sIncidencia.Substring(0, 14), "yyyyMMddHHmmss", null);
            int TInicial = Convert.ToInt32(ConfigurationManager.AppSettings["VideoPre"]), Tduracion = Convert.ToInt32(ConfigurationManager.AppSettings["VideoDur"]);


            dtStart = FromUnixTime(ToUnixTime(dtIncidencia) - TInicial); // -10 segundos
            dtEnd = FromUnixTime(ToUnixTime(dtStart) + Tduracion);       // 30 Duraci髇

            //  // .Show("Incidencia : " + dtIncidencia.ToString("yyyy.MM.dd HH:mm:ss"));
            //  // .Show("Inicio : " + dtStart.ToString("yyyy.MM.dd HH:mm:ss"));
            //  // .Show("Fin : " + dtEnd.ToString("yyyy.MM.dd HH:mm:ss"));


            //  return;

            CHCNetSDK.NET_DVR_PLAYCOND struDownPara = new CHCNetSDK.NET_DVR_PLAYCOND();
            struDownPara.dwChannel = uiCanal;            //Channel number  

            //   dtStart = DateTime.Now;
            // dtEnd = DateTime.Now;

            //Set the starting time
            struDownPara.struStartTime.dwYear = (uint)dtStart.Year;
            struDownPara.struStartTime.dwMonth = (uint)dtStart.Month;
            struDownPara.struStartTime.dwDay = (uint)dtStart.Day;
            struDownPara.struStartTime.dwHour = (uint)dtStart.Hour;
            struDownPara.struStartTime.dwMinute = (uint)dtStart.Minute;
            struDownPara.struStartTime.dwSecond = (uint)dtStart.Second;

            //Set the stopping time
            struDownPara.struStopTime.dwYear = (uint)dtEnd.Year;
            struDownPara.struStopTime.dwMonth = (uint)dtEnd.Month;
            struDownPara.struStopTime.dwDay = (uint)dtEnd.Day;
            struDownPara.struStopTime.dwHour = (uint)dtEnd.Hour;
            struDownPara.struStopTime.dwMinute = (uint)dtEnd.Minute;
            struDownPara.struStopTime.dwSecond = (uint)dtEnd.Second;

            string sVideoFileName;  //the path and file name to save      
            sVideoFileName = ConfigurationManager.AppSettings["RutaVideos"] + sIncidencia + "-" + tipoInci + ".mp4";

            //Download by time
            m_lDownHandle = CHCNetSDK.NET_DVR_GetFileByTime_V40(m_lUserID, sVideoFileName, ref struDownPara);
            if (m_lDownHandle < 0)
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_GetFileByTime_V40 failed, error code= " + iLastErr;
                log.Error(str);
                // // .Show(str);
                return;
            }

            uint iOutValue = 0;
            if (!CHCNetSDK.NET_DVR_PlayBackControl_V40(m_lDownHandle, CHCNetSDK.NET_DVR_PLAYSTART, IntPtr.Zero, 0, IntPtr.Zero, ref iOutValue))
            {
                iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                str = "NET_DVR_PLAYSTART failed, error code= " + iLastErr; //Download controlling failed,print error code
                log.Error(str);
                return;
            }
            timerProgress.Enabled = true;
            do
            {
                Application.DoEvents();
            } while (timerProgress.Enabled);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void timerProgress_Tick(object sender, EventArgs e)
        {
            // DownloadProgressBar.Maximum = 100;
            // DownloadProgressBar.Minimum = 0;
            int iPos = 0;
            //Get downloading process
            iPos = CHCNetSDK.NET_DVR_GetDownloadPos(m_lDownHandle);
            //if ((iPos > DownloadProgressBar.Minimum) && (iPos < DownloadProgressBar.Maximum))
            //{
            //    DownloadProgressBar.Value = iPos;
            //}
            if (iPos == 100)  //Finish downloading
            {
                //DownloadProgressBar.Value = iPos;
                if (!CHCNetSDK.NET_DVR_StopGetFile(m_lDownHandle))
                {
                    iLastErr = CHCNetSDK.NET_DVR_GetLastError();
                    str = "NET_DVR_StopGetFile failed, error code= " + iLastErr; //Download controlling failed,print error code
                                                                                 //   // .Show(str);
                    return;
                }
                m_lDownHandle = -1;
                timerProgress.Stop();
                //  // .Show("Descarga completa");
            }
            if (iPos == 200) //Network abnormal,download failed
            {
                //// .Show("The downloading is abnormal for the abnormal network!");
                timerProgress.Stop();
            }

        }
        private void btnVideosDAI_Click(object sender, EventArgs e)
        {
            VideosDai frm = new VideosDai();
            frm.Show();
        }

        private void GrabarIncidentes()
        {
            log.Info("Consulta de incidentes DAI");
            string query = @"SELECT  VD.*,
      GHA.[HIST_CODIGO_INT] as GHA_CODIGO_INCI
      ,GHA.[HIST_INCI_FECHA_INI] as GHA_FECHA_INI

	   ,GHA.HIST_INCI_FECHA_FIN as GHA_FECHA_FIN
      , GI.[INCI_ALIAS] AS GSI_ALIAS
      , TEG.ELEM_GEN_ALIAS as TEG_ALIAS
	  ,ce.ALIAS AS CE_ALIAS
	  ,CE.CANAL as CE_CANAL
 --     ,cast([HIST_INCI_PK] as int) as pk
	  ,CE.[CODIGO_HERMES] as CE_COD_HERMES

  FROM[MEDELLIN_GIP].[dbo].[GIP_HISTORICO_ACTUACIONES] as GHA
  INNER JOIN[MEDELLIN_GIP].[dbo].[GIP_C_ELEM_TV_IND] as ETV on cast([HIST_INCI_PK] as int)= cast(PK_INITIAL as int)
  INNER JOIN medellin_conf..TUN_ELEMENTO_GENERICO as TEG on LOGIC_CODE = ELEM_GEN_COD_LOG
  INNER JOIN[MEDELLIN_GIP].[dbo].[GIP_SGI_INCIDENTES] as GSI on CODIGO_UNICO = HIST_CODIGO_INT
  INNER JOIN[MEDELLIN_GIP].[dbo].[GIP_INCIDENCIA] as GI on INCI_CODIGO = CODIGO_INCIDENCIA
  INNER JOIN[COVIANDES_ADAP_NKF].[dbo].[CAM_EQUIVALENCIA] as CE on CE.[CODIGO_HERMES]= etv.TV_CODE
  left join medellin_HIST..videos_dai as VD on VD.INCIDENTE = GHA.HIST_CODIGO_INT--
  where ORIG_CODIGO = 0
  and gha.HIST_INCI_FECHA_INI > dateadd(hour, -8, getdate())
  AND VD.INCIDENTE IS NULL

  --order by  HIST_INCI_FECHA_INI desc";
            Conexion.Open();
            SqlCommand comando = new SqlCommand(query, Conexion);
            SqlCommand comando2 = new SqlCommand(query, Conexion);
            SqlDataReader dr = comando.ExecuteReader();
            log.Info("Se encontraron " + dr.FieldCount + " incidentes");
            DataTable dt = new DataTable();
            dt.Load(dr);
            Conexion.Close();
            if (dt.Rows.Count > 0)
            {
                log.Info("Se inicia descarga de videos");
                for (int i = 0; i < dt.Rows.Count; i++)

                {

                    log.Info("Descargando video incidente " + dt.Rows[i][3] + " cámara " + dt.Rows[i][8] + " tipo " + dt.Rows[i][6]);
                    int canal = Convert.ToInt32(dt.Rows[i][9].ToString());
                    tomarVideo(dt.Rows[i][3].ToString(), canal, dt.Rows[i][6].ToString());
                    query = @"INSERT INTO MEDELLIN_HIST..VIDEOS_DAI
           ([INCIDENTE]
           ,[CANAL]
           ,[VIDEO])
     VALUES
           ('" + dt.Rows[i][3].ToString() + "'," + dt.Rows[i][9].ToString() + ",1)";
                    Conexion.Open();
                    comando2.CommandText = query;
                    comando2.ExecuteNonQuery();
                    Conexion.Close();

                }
                log.Info("Descarga finalizada");
            }

            // Conexion.Close();


        }

        private void tmrVideosInci_Tick(object sender, EventArgs e)
        {
            GrabarIncidentes();


        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            GrabarIncidentes();
        }

        private void tmrStatus_Tick(object sender, EventArgs e)
        {
            try
            {
                Conexion.Open();
                labelDB.BackColor = Color.DarkGreen;
            }
            catch (SqlException)
            {
                log.Error("Error de conexion con base de datos");
                labelDB.BackColor = Color.DarkRed;
            }
        }

        private void Organizar()
        {
            //conta.panel1[] UC = new conta.panel1[80];
            string[] nombres = new string[81];
            int i = 0;
            string query = "SELECT *  FROM [COVIANDES_ADAP_NKF].[dbo].[CAM_EQUIVALENCIA]";
            Conexion.Open();
            SqlCommand comando = new SqlCommand(query, Conexion);
            SqlDataReader dr = comando.ExecuteReader();
            while (dr.Read())
            {
                nombres[i] = dr.GetString(1);
                i++;
            }

            x = 3;
            y = 5;
            Conexion.Close();
            for (int ac = 0; ac < 80; ac++)
            {
                Point punto = new Point(x, y);
                string c = a + ac;
                paneles[ac] = new Panel();
                labels[0, ac] = new Label();
                labels[1, ac] = new Label();
                labels[2, ac] = new Label();
                buttons[ac] = new Button();
                //Panel
                paneles[ac].Location = punto;
                paneles[ac].Size = pnlBase.Size;
                paneles[ac].BackColor = pnlBase.BackColor;
                paneles[ac].BorderStyle = pnlBase.BorderStyle;
                paneles[ac].Controls.Add(labels[0, ac]);
                paneles[ac].Controls.Add(labels[1, ac]);
                paneles[ac].Controls.Add(labels[2, ac]);
                paneles[ac].Controls.Add(buttons[ac]);
                // Label STA
                labels[0, ac].Size = lblStaBase.Size;
                labels[0, ac].ForeColor = lblStaBase.ForeColor;
                labels[0, ac].BorderStyle = lblStaBase.BorderStyle;
                labels[0, ac].Font = lblStaBase.Font;
                labels[0, ac].Location = lblStaBase.Location;
                labels[0, ac].Text = "STA";
                labels[0, ac].BackColor = Color.DarkGreen;
                if (nombres[ac].Contains("Comunica"))
                //if ((ac == 24) || (ac == 73) || (ac == 74) || (ac == 75) || (ac == 76) || (ac == 78) || (ac == 79) || (ac == 80))
                {
                    labels[0, ac].BackColor = Color.DarkRed;
                }
                // Label Nombre
                labels[1, ac].Size = lblNombreBase.Size;
                labels[1, ac].ForeColor = lblNombreBase.ForeColor;
                labels[1, ac].Location = lblNombreBase.Location;
                labels[1, ac].TextAlign = lblNombreBase.TextAlign;
                labels[1, ac].Text = nombres[ac];
                // Label Cuenta
                labels[2, ac].Size = lblCuentaBase.Size;
                labels[2, ac].ForeColor = lblCuentaBase.ForeColor;
                labels[2, ac].BorderStyle = lblCuentaBase.BorderStyle;
                labels[2, ac].Font = lblCuentaBase.Font;
                labels[2, ac].Location = lblCuentaBase.Location;
                labels[2, ac].Text = "0";
                // Bot髇
                buttons[ac].Size = btnBase.Size;
                buttons[ac].Tag = ac.ToString();
                buttons[ac].BackColor = btnBase.BackColor;
                buttons[ac].Font = btnBase.Font;
                buttons[ac].Margin = btnBase.Margin;
                buttons[ac].Text = "P1"; // + ac.ToString();
                buttons[ac].Click += new EventHandler(this.button3_Click);
                buttons[ac].Location = btnBase.Location;
                this.Controls.Add(paneles[ac]);
                if (ac != 0)
                {
                    y = y + paneles[ac].Height + 3;
                    if ((ac + 1) % 20 == 0)
                    {
                        y = 5;
                        x = x + paneles[ac - 1].Width + 3;
                    }
                }
                else
                {
                    y = y + paneles[ac].Height + 3;
                }
                //    // .Show(ac + " / " + paneles[ac].Location.X + "," + paneles[ac].Location.Y);
            } // for
              //this.Refresh();
            labelNVR.BackColor = Color.DarkGreen;
            labelDB.BackColor = Color.DarkGreen;
        }
    }
}
