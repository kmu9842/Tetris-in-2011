namespace Tetris
{
    partial class frm_main
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BlockDownTimer = new System.Windows.Forms.Timer(this.components);
            this.MainPanel = new System.Windows.Forms.Panel();
            this.lbl_게임오버 = new System.Windows.Forms.Label();
            this.lbl_포즈 = new System.Windows.Forms.Label();
            this.FallingDownTimer = new System.Windows.Forms.Timer(this.components);
            this.lbl_점수 = new System.Windows.Forms.Label();
            this.ScoreLabel = new System.Windows.Forms.Label();
            this.LevelLabel = new System.Windows.Forms.Label();
            this.lbl_속도 = new System.Windows.Forms.Label();
            this.SpeedLabel = new System.Windows.Forms.Label();
            this.StatusPanel = new System.Windows.Forms.Panel();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_레벨 = new System.Windows.Forms.Label();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.filetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.endgametoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.savetoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadtoolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.설정CToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pvp1 = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.FilledLineDelete = new System.Windows.Forms.Timer(this.components);
            this.pvp2 = new System.Windows.Forms.Panel();
            this.MFTimer = new System.Windows.Forms.Timer(this.components);
            this.MainPanel.SuspendLayout();
            this.StatusPanel.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // BlockDownTimer
            // 
            this.BlockDownTimer.Interval = 500;
            this.BlockDownTimer.Tick += new System.EventHandler(this.BlockDownTimer_Tick);
            // 
            // MainPanel
            // 
            this.MainPanel.BackColor = System.Drawing.Color.White;
            this.MainPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MainPanel.Controls.Add(this.lbl_게임오버);
            this.MainPanel.Controls.Add(this.lbl_포즈);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.MainPanel.Location = new System.Drawing.Point(0, 24);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(206, 408);
            this.MainPanel.TabIndex = 1;
            this.MainPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.MainPanel_Paint);
            // 
            // lbl_게임오버
            // 
            this.lbl_게임오버.AutoSize = true;
            this.lbl_게임오버.BackColor = System.Drawing.Color.Transparent;
            this.lbl_게임오버.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_게임오버.ForeColor = System.Drawing.Color.Red;
            this.lbl_게임오버.Location = new System.Drawing.Point(37, 169);
            this.lbl_게임오버.Name = "lbl_게임오버";
            this.lbl_게임오버.Size = new System.Drawing.Size(141, 21);
            this.lbl_게임오버.TabIndex = 1;
            this.lbl_게임오버.Text = "-Game Over-";
            this.lbl_게임오버.Visible = false;
            // 
            // lbl_포즈
            // 
            this.lbl_포즈.AutoSize = true;
            this.lbl_포즈.BackColor = System.Drawing.Color.Transparent;
            this.lbl_포즈.Font = new System.Drawing.Font("굴림", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lbl_포즈.ForeColor = System.Drawing.Color.Black;
            this.lbl_포즈.Location = new System.Drawing.Point(54, 169);
            this.lbl_포즈.Name = "lbl_포즈";
            this.lbl_포즈.Size = new System.Drawing.Size(101, 21);
            this.lbl_포즈.TabIndex = 0;
            this.lbl_포즈.Text = "-PAUSE-";
            this.lbl_포즈.Visible = false;
            // 
            // 
            // FallingDownTimer
            // 
            this.FallingDownTimer.Interval = 5;
            this.FallingDownTimer.Tick += new System.EventHandler(this.FallingDownTimer_Tick);
            // 
            // lbl_점수
            // 
            this.lbl_점수.AutoSize = true;
            this.lbl_점수.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_점수.Location = new System.Drawing.Point(37, 117);
            this.lbl_점수.Name = "lbl_점수";
            this.lbl_점수.Size = new System.Drawing.Size(38, 18);
            this.lbl_점수.TabIndex = 0;
            this.lbl_점수.Text = "점수";
            // 
            // ScoreLabel
            // 
            this.ScoreLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ScoreLabel.Location = new System.Drawing.Point(8, 138);
            this.ScoreLabel.Name = "ScoreLabel";
            this.ScoreLabel.Size = new System.Drawing.Size(96, 20);
            this.ScoreLabel.TabIndex = 1;
            this.ScoreLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LevelLabel
            // 
            this.LevelLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.LevelLabel.Location = new System.Drawing.Point(8, 97);
            this.LevelLabel.Name = "LevelLabel";
            this.LevelLabel.Size = new System.Drawing.Size(96, 20);
            this.LevelLabel.TabIndex = 3;
            this.LevelLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_속도
            // 
            this.lbl_속도.AutoSize = true;
            this.lbl_속도.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_속도.Location = new System.Drawing.Point(37, 158);
            this.lbl_속도.Name = "lbl_속도";
            this.lbl_속도.Size = new System.Drawing.Size(38, 18);
            this.lbl_속도.TabIndex = 4;
            this.lbl_속도.Text = "속도";
            // 
            // SpeedLabel
            // 
            this.SpeedLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SpeedLabel.Location = new System.Drawing.Point(7, 181);
            this.SpeedLabel.Name = "SpeedLabel";
            this.SpeedLabel.Size = new System.Drawing.Size(96, 20);
            this.SpeedLabel.TabIndex = 5;
            this.SpeedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StatusPanel
            // 
            this.StatusPanel.BackColor = System.Drawing.Color.White;
            this.StatusPanel.Controls.Add(this.txt_name);
            this.StatusPanel.Controls.Add(this.label1);
            this.StatusPanel.Controls.Add(this.lbl_레벨);
            this.StatusPanel.Controls.Add(this.SpeedLabel);
            this.StatusPanel.Controls.Add(this.lbl_속도);
            this.StatusPanel.Controls.Add(this.LevelLabel);
            this.StatusPanel.Controls.Add(this.ScoreLabel);
            this.StatusPanel.Controls.Add(this.lbl_점수);
            this.StatusPanel.Location = new System.Drawing.Point(205, 222);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(108, 210);
            this.StatusPanel.TabIndex = 0;
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(8, 55);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(95, 21);
            this.txt_name.TabIndex = 7;
            this.txt_name.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_name_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(37, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "이름";
            // 
            // lbl_레벨
            // 
            this.lbl_레벨.AutoSize = true;
            this.lbl_레벨.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_레벨.Location = new System.Drawing.Point(37, 79);
            this.lbl_레벨.Name = "lbl_레벨";
            this.lbl_레벨.Size = new System.Drawing.Size(38, 18);
            this.lbl_레벨.TabIndex = 2;
            this.lbl_레벨.Text = "레벨";
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filetoolStripMenuItem,
            this.helpToolStripMenuItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(311, 24);
            this.MainMenu.TabIndex = 2;
            this.MainMenu.Text = "menuStrip1";
            // 
            // filetoolStripMenuItem
            // 
            this.filetoolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.endgametoolStripMenuItem,
            this.savetoolStripMenuItem,
            this.loadtoolStripMenuItem,
            this.exitToolStripMenuItem,
            this.설정CToolStripMenuItem});
            this.filetoolStripMenuItem.Name = "filetoolStripMenuItem";
            this.filetoolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.filetoolStripMenuItem.Text = "메인(&M)";
            this.filetoolStripMenuItem.DropDownOpened += new System.EventHandler(this.filetoolStripMenuItem_DropDownOpened);
            this.filetoolStripMenuItem.DropDownClosed += new System.EventHandler(this.filetoolStripMenuItem_DropDownClosed);
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.newGameToolStripMenuItem.Text = "새 게임(&N)";
            this.newGameToolStripMenuItem.Click += new System.EventHandler(this.newGameToolStripMenuItem_Click);
            // 
            // endgametoolStripMenuItem
            // 
            this.endgametoolStripMenuItem.Name = "endgametoolStripMenuItem";
            this.endgametoolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.endgametoolStripMenuItem.Text = "게임 중단(&A)";
            this.endgametoolStripMenuItem.Click += new System.EventHandler(this.endgametoolStripMenuItem_Click);
            // 
            // savetoolStripMenuItem
            // 
            this.savetoolStripMenuItem.Name = "savetoolStripMenuItem";
            this.savetoolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.savetoolStripMenuItem.Text = "저장(&S)";
            this.savetoolStripMenuItem.Click += new System.EventHandler(this.savetoolStripMenuItem_Click);
            // 
            // loadtoolStripMenuItem
            // 
            this.loadtoolStripMenuItem.Name = "loadtoolStripMenuItem";
            this.loadtoolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.loadtoolStripMenuItem.Text = "불러오기(&L)";
            this.loadtoolStripMenuItem.Click += new System.EventHandler(this.loadtoolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.exitToolStripMenuItem.Text = "종료(&X)";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // 설정CToolStripMenuItem
            // 
            this.설정CToolStripMenuItem.Name = "설정CToolStripMenuItem";
            this.설정CToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.설정CToolStripMenuItem.Text = "설정(&C)";
            this.설정CToolStripMenuItem.Click += new System.EventHandler(this.설정CToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.helpToolStripMenuItem.Text = "도움말(&H)";
            this.helpToolStripMenuItem.DropDownOpened += new System.EventHandler(this.helpToolStripMenuItem_DropDownOpened);
            this.helpToolStripMenuItem.DropDownClosed += new System.EventHandler(this.helpToolStripMenuItem_DropDownClosed);
            // 
            // HToolStripMenuItem
            // 
            this.HToolStripMenuItem.Name = "HToolStripMenuItem";
            this.HToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
            this.HToolStripMenuItem.Text = "도움말(&H)";
            this.HToolStripMenuItem.Click += new System.EventHandler(this.HelpToolStripMenuItem_Click);
            // 
            // pvp1
            // 
            this.pvp1.BackColor = System.Drawing.Color.White;
            this.pvp1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pvp1.Location = new System.Drawing.Point(205, 25);
            this.pvp1.Name = "pvp1";
            this.pvp1.Size = new System.Drawing.Size(103, 95);
            this.pvp1.TabIndex = 3;
            this.pvp1.Paint += new System.Windows.Forms.PaintEventHandler(this.PreveiwPanel_Paint);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "*.bdttts";
            this.openFileDialog1.Filter = "공식 세이브 파일 (*.bdttts)|*.bdttts|모든 파일 (*.*)|*.*\"";
            this.openFileDialog1.ReadOnlyChecked = true;
            this.openFileDialog1.Title = "세이브 파일 로드";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "*.bdttts";
            this.saveFileDialog1.Filter = "공식 세이브 파일 (*.bdttts)|*.bdttts|모든 파일 (*.*)|*.*\"";
            this.saveFileDialog1.SupportMultiDottedExtensions = true;
            this.saveFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFileDialog1_FileOk);
            // 
            // FilledLineDelete
            // 
            this.FilledLineDelete.Interval = 500;
            this.FilledLineDelete.Tick += new System.EventHandler(this.FilledLineDelete_Tick);
            // 
            // pvp2
            // 
            this.pvp2.BackColor = System.Drawing.Color.White;
            this.pvp2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pvp2.Location = new System.Drawing.Point(205, 121);
            this.pvp2.Name = "pvp2";
            this.pvp2.Size = new System.Drawing.Size(103, 95);
            this.pvp2.TabIndex = 4;
            this.pvp2.Paint += new System.Windows.Forms.PaintEventHandler(this.pvp2_Paint);
            // 
            // MFTimer
            // 
            this.MFTimer.Enabled = true;
            this.MFTimer.Interval = 1000;
            // 
            // frm_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(311, 432);
            this.Controls.Add(this.pvp2);
            this.Controls.Add(this.pvp1);
            this.Controls.Add(this.StatusPanel);
            this.Controls.Add(this.MainPanel);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "frm_main";
            this.Text = "Tetris";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Move += new System.EventHandler(this.frm_main_Move);
            this.LocationChanged += new System.EventHandler(this.frm_main_LocationChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            this.StatusPanel.ResumeLayout(false);
            this.StatusPanel.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer BlockDownTimer;
        internal System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.Timer FallingDownTimer;
        private System.Windows.Forms.Label lbl_점수;
        private System.Windows.Forms.Label ScoreLabel;
        private System.Windows.Forms.Label LevelLabel;
        private System.Windows.Forms.Label lbl_속도;
        private System.Windows.Forms.Label SpeedLabel;
        internal System.Windows.Forms.Panel StatusPanel;
        internal System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem filetoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        internal System.Windows.Forms.Panel pvp1;
        private System.Windows.Forms.ToolStripMenuItem HToolStripMenuItem;
        internal System.Windows.Forms.Label lbl_포즈;
        internal System.Windows.Forms.Label lbl_게임오버;
        private System.Windows.Forms.ToolStripMenuItem savetoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadtoolStripMenuItem;
        private System.Windows.Forms.Label lbl_레벨;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem endgametoolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 설정CToolStripMenuItem;
        private System.Windows.Forms.Timer FilledLineDelete;
        internal System.Windows.Forms.Panel pvp2;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer MFTimer;
    }
}

