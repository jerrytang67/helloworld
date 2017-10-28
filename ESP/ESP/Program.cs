using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Device;

namespace CPUZ
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        /// 
        /// 
        private static mainFrom _mainForm;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            HotKeyManager.RegisterHotKey(Keys.F1, KeyModifiers.Alt);
            HotKeyManager.HotKeyPressed += HotKeyManager_HotKeyPressed;


            _mainForm = new mainFrom();
            Application.Run(_mainForm);

        }


        //注册热键

        static void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            if (_mainForm.Visible)
            {
                _mainForm.Hide();
            }
            else
            {
                _mainForm.Show();
            }
        }
    }
}
