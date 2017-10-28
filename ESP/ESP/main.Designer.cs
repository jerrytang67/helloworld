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
            this.materialTabControl1 = new MaterialSkin.Controls.MaterialTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.chk2 = new MaterialSkin.Controls.MaterialCheckBox();
            this.chk1 = new MaterialSkin.Controls.MaterialCheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.chkHealth = new MaterialSkin.Controls.MaterialCheckBox();
            this.chkLine = new MaterialSkin.Controls.MaterialCheckBox();
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
            this.tabPage2.Size = new System.Drawing.Size(293, 102);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
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
            // chkLine
            // 
            this.chkLine.AutoSize = true;
            this.chkLine.Depth = 0;
            this.chkLine.Font = new System.Drawing.Font("Roboto", 10F);
            this.chkLine.Location = new System.Drawing.Point(17, 137);
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
            // mainFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.materialTabControl1);
            this.Name = "mainFrom";
            this.Text = "CPU-Z 1.0.0";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.main_Load);
            this.Leave += new System.EventHandler(this.main_Leave);
            this.materialTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private MaterialSkin.Controls.MaterialTabControl materialTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private MaterialSkin.Controls.MaterialCheckBox chk2;
        private MaterialSkin.Controls.MaterialCheckBox chk1;
        private MaterialSkin.Controls.MaterialCheckBox chkLine;
        private MaterialSkin.Controls.MaterialCheckBox chkHealth;
    }
}