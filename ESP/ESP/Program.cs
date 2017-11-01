using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Device;
using System.Threading;

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

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        [DllImport("user32.dll")]
        static extern byte MapVirtualKey(byte wCode, int wMap);

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);



            HotKeyManager.RegisterHotKey(Keys.F1, KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.NumPad7, KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.NumPad8, KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.NumPad9, KeyModifiers.Alt);

            HotKeyManager.HotKeyPressed += HotKeyManager_HotKeyPressed;

            //HotKeyManager.RegisterHotKey(Keys.Space, 0);
            //HotKeyManager.HotKeyPressed += HotKeyManager_HotKeyPressed2;

            
            _mainForm = new mainFrom();
            Application.Run(_mainForm);
            Application.ThreadExit += (a, b) =>
            {
                KReader.Close();
            };
        }


        //注册热键

        static void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            if (e.Key == Keys.F1 && e.Modifiers == KeyModifiers.Alt)
                if (_mainForm.Visible)
                {
                    _mainForm.Hide();
                }
                else
                {
                    _mainForm.Show();

                }
            if (e.Key == Keys.NumPad7 && e.Modifiers == KeyModifiers.Alt)
            {
                Setting.车辆显示 = !Setting.车辆显示;
            }
            if (e.Key == Keys.NumPad8 && e.Modifiers == KeyModifiers.Alt)
            {
                Setting.物品显示 = !Setting.物品显示;
            }
            if (e.Key == Keys.NumPad9 && e.Modifiers == KeyModifiers.Alt)
            {
                Setting.雷达 = !Setting.雷达;
            }
        }
        static void HotKeyManager_HotKeyPressed2(object sender, HotKeyEventArgs e)
        {

            if (Setting.一键大跳 && e.Key == Keys.Space)
            {
                keybd_event(32, MapVirtualKey(32, 0), 0x2, 0);//空格　
                Thread.Sleep(100);
                keybd_event(67, MapVirtualKey(67, 0), 0, 0); //C
                keybd_event(32, MapVirtualKey(32, 0), 0x2, 0);//放開 空格
                keybd_event(67, MapVirtualKey(67, 0), 0x2, 0);//放開C　
            }
        }
        
    }
}
