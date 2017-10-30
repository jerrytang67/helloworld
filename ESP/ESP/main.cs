using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace CPUZ
{
    public partial class mainFrom : MaterialForm
    {
        public static bool STATE = false;
        public mainFrom()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        private void main_Load(object sender, EventArgs e)
        {
            this.timer1.Enabled = true;
            this.timer1.Interval = 500;
            this.MinimumSize = new Size(100, 100);

            this.chk1.Checked = Setting.车辆显示;
            this.chk2.Checked = Setting.物品显示;
            this.chkHealth.Checked = Setting.距离和血量;
            this.chkLine.Checked = Setting.线条;

            new Cpuz().Show();

        }

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            Setting.车辆显示 = chk1.Checked;
        }

        private void materialCheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            Setting.物品显示 = chk2.Checked;

        }

        private void main_Leave(object sender, EventArgs e)
        {
            KReader.Close();
            Application.Exit();

        }

        private void chkHealth_CheckedChanged(object sender, EventArgs e)
        {
            Setting.距离和血量 = chkHealth.Checked;
        }

        private void chkLine_CheckedChanged(object sender, EventArgs e)
        {
            Setting.线条 = chkLine.Checked;

        }

        private void chkRadar_CheckedChanged(object sender, EventArgs e)
        {
            Setting.雷达 = chkRadar.Checked;
        }


        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Visible)
                this.Hide();
            else
                this.Show();
        }

        private void chkJump_CheckedChanged(object sender, EventArgs e)
        {
            Setting.一键大跳 = chkJump.Checked;
        }

        private void materialFlatButton1_Click_1(object sender, EventArgs e)
        {
            this.Hide();

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if(STATE)
            {
                materialLabel1.Text = "正常";
            }
            else
            {
                materialLabel1.Text = "无数据";
            }
        }

        private void chkWebMap_CheckedChanged(object sender, EventArgs e)
        {
            Setting.Web端 = chkWebMap.Checked;

        }
    }
}
