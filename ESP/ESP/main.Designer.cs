namespace CPUZ
{
    partial class mainFrom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainFrom));
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.materialLabel2 = new MaterialSkin.Controls.MaterialLabel();
            this.chkWebMap = new MaterialSkin.Controls.MaterialCheckBox();
            this.chkRadar = new MaterialSkin.Controls.MaterialCheckBox();
            this.chkLine = new MaterialSkin.Controls.MaterialCheckBox();
            this.chkHealth = new MaterialSkin.Controls.MaterialCheckBox();
            this.chk2 = new MaterialSkin.Controls.MaterialCheckBox();
            this.chk1 = new MaterialSkin.Controls.MaterialCheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.materialFlatButton1 = new MaterialSkin.Controls.MaterialFlatButton();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.chkJump = new MaterialSkin.Controls.MaterialCheckBox();
            this.materialLabel1 = new MaterialSkin.Controls.MaterialLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.materialTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // materialTabControl1
            // 
            this.materialTabControl1.Controls.Add(this.tabPage1);
            this.materialTabControl1.Controls.Add(this.tabPage2);
            this.materialTabControl1.Depth = 0;
            this.materialTabControl1.Location = new System.Drawing.Point(0, 63);
            this.materialTabControl1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialTabControl1.Name = "materialTabControl1";
            this.materialTabControl1.SelectedIndex = 0;
            this.materialTabControl1.Size = new System.Drawing.Size(301, 238);
            this.materialTabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.materialLabel2);
            this.tabPage1.Controls.Add(this.chkWebMap);
            this.tabPage1.Controls.Add(this.chkRadar);
            this.tabPage1.Controls.Add(this.chkLine);
            this.tabPage1.Controls.Add(this.chkHealth);
            this.tabPage1.Controls.Add(this.chk2);
            this.tabPage1.Controls.Add(this.chk1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(293, 212);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // materialLabel2
            // 
            this.materialLabel2.AutoSize = true;
            this.materialLabel2.Depth = 0;
            this.materialLabel2.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel2.Location = new System.Drawing.Point(149, 198);
            this.materialLabel2.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel2.Name = "materialLabel2";
            this.materialLabel2.Size = new System.Drawing.Size(64, 18);
            this.materialLabel2.TabIndex = 6;
            this.materialLabel2.Text = "pubbase";
            // 
            // chkWebMap
            // 
            this.chkWebMap.AutoSize = true;
            this.chkWebMap.Depth = 0;
            this.chkWebMap.Font = new System.Drawing.Font("Roboto", 10F);
            this.chkWebMap.Location = new System.Drawing.Point(196, 12);
            this.chkWebMap.Margin = new System.Windows.Forms.Padding(0);
            this.chkWebMap.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkWebMap.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkWebMap.Name = "chkWebMap";
            this.chkWebMap.Ripple = true;
            this.chkWebMap.Size = new System.Drawing.Size(87, 30);
            this.chkWebMap.TabIndex = 5;
            this.chkWebMap.Text = "Web地图";
            this.chkWebMap.UseVisualStyleBackColor = true;
            this.chkWebMap.CheckedChanged += new System.EventHandler(this.chkWebMap_CheckedChanged);
            // 
            // chkRadar
            // 
            this.chkRadar.AutoSize = true;
            this.chkRadar.Depth = 0;
            this.chkRadar.Font = new System.Drawing.Font("Roboto", 10F);
            this.chkRadar.Location = new System.Drawing.Point(17, 173);
            this.chkRadar.Margin = new System.Windows.Forms.Padding(0);
            this.chkRadar.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkRadar.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkRadar.Name = "chkRadar";
            this.chkRadar.Ripple = true;
            this.chkRadar.Size = new System.Drawing.Size(60, 30);
            this.chkRadar.TabIndex = 4;
            this.chkRadar.Text = "雷达";
            this.chkRadar.UseVisualStyleBackColor = true;
            this.chkRadar.CheckedChanged += new System.EventHandler(this.chkRadar_CheckedChanged);
            // 
            // chkLine
            // 
            this.chkLine.AutoSize = true;
            this.chkLine.Depth = 0;
            this.chkLine.Font = new System.Drawing.Font("Roboto", 10F);
            this.chkLine.Location = new System.Drawing.Point(17, 134);
            this.chkLine.Margin = new System.Windows.Forms.Padding(0);
            this.chkLine.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkLine.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkLine.Name = "chkLine";
            this.chkLine.Ripple = true;
            this.chkLine.Size = new System.Drawing.Size(60, 30);
            this.chkLine.TabIndex = 3;
            this.chkLine.Text = "线条";
            this.chkLine.UseVisualStyleBackColor = true;
            this.chkLine.CheckedChanged += new System.EventHandler(this.chkLine_CheckedChanged);
            // 
            // chkHealth
            // 
            this.chkHealth.AutoSize = true;
            this.chkHealth.Depth = 0;
            this.chkHealth.Font = new System.Drawing.Font("Roboto", 10F);
            this.chkHealth.Location = new System.Drawing.Point(17, 95);
            this.chkHealth.Margin = new System.Windows.Forms.Padding(0);
            this.chkHealth.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkHealth.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkHealth.Name = "chkHealth";
            this.chkHealth.Ripple = true;
            this.chkHealth.Size = new System.Drawing.Size(121, 30);
            this.chkHealth.TabIndex = 2;
            this.chkHealth.Text = "人物距离血量";
            this.chkHealth.UseVisualStyleBackColor = true;
            this.chkHealth.CheckedChanged += new System.EventHandler(this.chkHealth_CheckedChanged);
            // 
            // chk2
            // 
            this.chk2.AutoSize = true;
            this.chk2.Depth = 0;
            this.chk2.Font = new System.Drawing.Font("Roboto", 10F);
            this.chk2.Location = new System.Drawing.Point(17, 54);
            this.chk2.Margin = new System.Windows.Forms.Padding(0);
            this.chk2.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chk2.MouseState = MaterialSkin.MouseState.HOVER;
            this.chk2.Name = "chk2";
            this.chk2.Ripple = true;
            this.chk2.Size = new System.Drawing.Size(60, 30);
            this.chk2.TabIndex = 1;
            this.chk2.Text = "物品";
            this.chk2.UseVisualStyleBackColor = true;
            this.chk2.CheckedChanged += new System.EventHandler(this.materialCheckBox2_CheckedChanged);
            // 
            // chk1
            // 
            this.chk1.AutoSize = true;
            this.chk1.Depth = 0;
            this.chk1.Font = new System.Drawing.Font("Roboto", 10F);
            this.chk1.Location = new System.Drawing.Point(17, 12);
            this.chk1.Margin = new System.Windows.Forms.Padding(0);
            this.chk1.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chk1.MouseState = MaterialSkin.MouseState.HOVER;
            this.chk1.Name = "chk1";
            this.chk1.Ripple = true;
            this.chk1.Size = new System.Drawing.Size(60, 30);
            this.chk1.TabIndex = 0;
            this.chk1.Text = "车辆";
            this.chk1.UseVisualStyleBackColor = true;
            this.chk1.CheckedChanged += new System.EventHandler(this.materialCheckBox1_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(293, 212);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // materialFlatButton1
            // 
            this.materialFlatButton1.AutoSize = true;
            this.materialFlatButton1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.materialFlatButton1.Depth = 0;
            this.materialFlatButton1.Location = new System.Drawing.Point(245, 306);
            this.materialFlatButton1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.materialFlatButton1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialFlatButton1.Name = "materialFlatButton1";
            this.materialFlatButton1.Primary = false;
            this.materialFlatButton1.Size = new System.Drawing.Size(42, 36);
            this.materialFlatButton1.TabIndex = 5;
            this.materialFlatButton1.Text = "隐藏";
            this.materialFlatButton1.UseVisualStyleBackColor = true;
            this.materialFlatButton1.Click += new System.EventHandler(this.materialFlatButton1_Click_1);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "CPU-Z";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // chkJump
            // 
            this.chkJump.AutoSize = true;
            this.chkJump.Depth = 0;
            this.chkJump.Font = new System.Drawing.Font("Roboto", 10F);
            this.chkJump.Location = new System.Drawing.Point(9, 306);
            this.chkJump.Margin = new System.Windows.Forms.Padding(0);
            this.chkJump.MouseLocation = new System.Drawing.Point(-1, -1);
            this.chkJump.MouseState = MaterialSkin.MouseState.HOVER;
            this.chkJump.Name = "chkJump";
            this.chkJump.Ripple = true;
            this.chkJump.Size = new System.Drawing.Size(90, 30);
            this.chkJump.TabIndex = 6;
            this.chkJump.Text = "一键大跳";
            this.chkJump.UseVisualStyleBackColor = true;
            this.chkJump.CheckedChanged += new System.EventHandler(this.chkJump_CheckedChanged);
            // 
            // materialLabel1
            // 
            this.materialLabel1.AutoSize = true;
            this.materialLabel1.BackColor = System.Drawing.Color.Transparent;
            this.materialLabel1.Depth = 0;
            this.materialLabel1.Font = new System.Drawing.Font("Roboto", 11F);
            this.materialLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialLabel1.Location = new System.Drawing.Point(233, 36);
            this.materialLabel1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialLabel1.Name = "materialLabel1";
            this.materialLabel1.Size = new System.Drawing.Size(56, 18);
            this.materialLabel1.TabIndex = 7;
            this.materialLabel1.Text = "无数据";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // mainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 380);
            this.Controls.Add(this.materialLabel1);
            this.Controls.Add(this.chkJump);
            this.Controls.Add(this.materialFlatButton1);
            this.Controls.Add(this.materialTabControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "mainFrom";
            this.ShowInTaskbar = false;
            this.Sizable = false;
            this.Text = "CPU-Z 1.0.0";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.main_Load);
            this.Leave += new System.EventHandler(this.main_Leave);
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private MaterialSkin.Controls.MaterialCheckBox chk2;
        private MaterialSkin.Controls.MaterialCheckBox chk1;
        private MaterialSkin.Controls.MaterialCheckBox chkLine;
        private MaterialSkin.Controls.MaterialCheckBox chkHealth;
        private MaterialSkin.Controls.MaterialCheckBox chkRadar;
        private MaterialSkin.Controls.MaterialFlatButton materialFlatButton1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private MaterialSkin.Controls.MaterialCheckBox chkJump;
        private MaterialSkin.Controls.MaterialLabel materialLabel1;
        private System.Windows.Forms.Timer timer1;
        private MaterialSkin.Controls.MaterialCheckBox chkWebMap;
        private MaterialSkin.Controls.MaterialLabel materialLabel2;
    }
}