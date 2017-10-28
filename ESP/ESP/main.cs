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
    }
}
