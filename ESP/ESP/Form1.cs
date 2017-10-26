using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ESP.Model;
using Newtonsoft.Json;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DXGI;
using SharpDX.Mathematics.Interop;

using Vector2 = ESP.Model.Vector2;
using Vector3 = ESP.Model.Vector3;

namespace ESP
{
    public partial class Form1 : Form
    {



        public Form1()
        {
            InitializeComponent();
            //Make the window's border completely transparant

            // ANTI BATTLEYE SIG SCAN ;)
            this.Text = Guid.NewGuid().ToString().Replace("-", "");

            // TRANSPARENCY KEY
            this.BackColor = System.Drawing.Color.Black;

            // SETTINGS
            this.FormBorderStyle = FormBorderStyle.None;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;

            // MAKE WINDOW TRANSPARENT
            Win32.SetWindowLong(this.Handle, Win32.GWL_EXSTYLE, (IntPtr)(Win32.GetWindowLong(this.Handle, Win32.GWL_EXSTYLE) ^ Win32.WS_EX_LAYERED ^ Win32.WS_EX_TRANSPARENT));

            // MAKE WINDOW SOLID
            Win32.SetLayeredWindowAttributes(this.Handle, 0, 255, Win32.LWA_ALPHA);

            var targetProperties = new HwndRenderTargetProperties
            {
                Hwnd = this.Handle,
                PixelSize = new Size2(this.Bounds.Right - this.Bounds.Left, this.Bounds.Bottom - this.Bounds.Top),
                PresentOptions = PresentOptions.Immediately
            };

            var prop = new RenderTargetProperties(RenderTargetType.Hardware, new PixelFormat(Format.B8G8R8A8_UNorm, SharpDX.Direct2D1.AlphaMode.Premultiplied), 0, 0, RenderTargetUsage.None, FeatureLevel.Level_DEFAULT);

            var d3dFactory = new SharpDX.Direct2D1.Factory();

            var device = new WindowRenderTarget(d3dFactory, prop, targetProperties)
            {
                TextAntialiasMode = TextAntialiasMode.Cleartype,
                AntialiasMode = AntialiasMode.Aliased
            };

            var url = "http://127.0.0.1:8888/api/values/5";
            var dxthread = new Thread(() =>
            {

                var brushWhite = new SolidColorBrush(device, RawColorFromColor(Color.White));
                var brushBlack = new SolidColorBrush(device, RawColorFromColor(Color.Black));
                var brushGreen = new SolidColorBrush(device, RawColorFromColor(Color.Green));
                var brushRed = new SolidColorBrush(device, RawColorFromColor(Color.Red));
                var brushPurple = new SolidColorBrush(device, RawColorFromColor(Color.Purple));


                var fontFactory = new SharpDX.DirectWrite.Factory();

                var fontConsolas = new SharpDX.DirectWrite.TextFormat(fontFactory, "Consolas", 15);
                var fontESP = new SharpDX.DirectWrite.TextFormat(fontFactory, "Consolas", 12);

                JSON_DATA json_data = null;

                using (var w = new WebClient())
                {
                    while (true)
                    {
                        // attempt to download JSON data as a string
                        try
                        {
                            var str = w.DownloadString(url);
                            json_data = JsonConvert.DeserializeObject<JSON_DATA>(str);
                        }
                        catch (Exception)
                        {

                        }
                        finally
                        {

                            device.BeginDraw();
                            device.Clear(null);

                            #region RADAR

                            int radarX = 20;
                            int radarY = 140;
                            int radarSize = 200;
                            int radarBorder = 1;
                            Vector2 centerpoint = new Vector2(radarX + radarSize / 2, radarY + radarSize / 2);

                            // TODO: INTEGRATE INTO MINIMAP
                            if (false)
                            {
                                var radarOuterRectangle = new RawRectangleF(radarX, radarY, radarX + radarSize,
                                    radarY + radarSize);
                                var radarRectangle = new RawRectangleF(radarX + radarBorder, radarY + radarBorder,
                                    radarX + radarSize - radarBorder, radarY + radarSize - radarBorder);

                                var radarCenterRectangle = new RoundedRectangle()
                                {
                                    RadiusX = 4,
                                    RadiusY = 4,
                                    Rect = new RawRectangleF(centerpoint.X, centerpoint.Y, centerpoint.X + 4,
                                        centerpoint.Y + 4)
                                };

                                device.FillRectangle(radarRectangle, brushBlack);
                                device.DrawRectangle(radarRectangle, brushWhite);

                                device.FillRoundedRectangle(radarCenterRectangle, brushGreen);

                            }

                            #endregion


                            var vecLocalLocation = new Model.Vector3
                            {
                                X = json_data.camera[1].X,
                                Y = json_data.camera[1].Y,
                                Z = json_data.camera[1].Z
                            };

                            var PlayerCameraManager = new APlayerCameraManager
                            {
                                CameraCache = new FCameraCacheEntry
                                {
                                    POV = new FMinimalViewInfo
                                    {
                                        Fov = json_data.camera[2].X,
                                        Location = new Model.Vector3
                                        {
                                            X = json_data.camera[1].X,
                                            Y = json_data.camera[1].Y,
                                            Z = json_data.camera[1].Z
                                        },
                                        Rotation = new Model.FRotator
                                        {
                                            Pitch = json_data.camera[0].X,
                                            Yaw = json_data.camera[0].Y,
                                            Roll = json_data.camera[0].Z
                                        }
                                    }
                                }
                            };

                            #region 车

                            if (true)
                            {
                                foreach (var v in json_data.vehicles)
                                {
                                    var vecActorLocation = new Vector3 { X = v.rx, Y = v.ry, Z = v.rz };
                                    var vecRelativePos = vecLocalLocation - vecActorLocation;
                                    var lDeltaInMeters = vecRelativePos.Length / 100;

                                    Vector2 screenlocation;
                                    WorldToScreen(vecActorLocation, PlayerCameraManager, out screenlocation);
                                    
                                    DrawText($"[{v.v}] {(int)lDeltaInMeters}m", (int)screenlocation.X,
                                        (int)screenlocation.Y, brushRed, fontFactory, fontESP, device);

                                }
                            }

                            #endregion

                            #region 人物
                            var playerList = new List<Player>();

                            foreach (var player in json_data.players)
                            {
                                if (player.health > 0)
                                {
                                    var vecPlayerLocation = new Vector3 { X = player.rx, Y = player.ry, Z = player.rz };
                                    var vecRelativePos = vecLocalLocation - vecPlayerLocation;
                                    var lDeltaInMeters = vecRelativePos.Length / 100;

                                    #region Line ESP

                                    // 线
                                    if (lDeltaInMeters <= 200 && lDeltaInMeters > 5 && json_data.players.Count<=50)
                                    {
                                        Vector2 screenlocation;
                                        WorldToScreen(vecPlayerLocation, PlayerCameraManager, out screenlocation);

                                        device.DrawLine(new RawVector2(1920 / 2, 1080),
                                            new RawVector2(screenlocation.X, screenlocation.Y), brushWhite);
                                    }

                                    #endregion

                                    #region Distance ESP

                                    // if (CheatSettings.DistanceESP)
                                    if (lDeltaInMeters > 5)
                                    {
                                        Vector2 screenlocation;
                                        WorldToScreen(vecPlayerLocation, PlayerCameraManager, out screenlocation);

                                        DrawText($"HP：{(int)player.health} {(int)lDeltaInMeters}m",
                                            (int)screenlocation.X,
                                            (int)screenlocation.Y, lDeltaInMeters >= 200 ? brushGreen : brushRed,
                                            fontFactory, fontESP, device);
                                    }

                                    #endregion

                                    #region Radar

                                    if (false)
                                    {
                                        if (lDeltaInMeters <= radarSize / 2 /*DISTANCE FROM CENTER TO EDGE*/)
                                        {
                                            Vector2 screenpos = centerpoint + vecRelativePos.To2D() / 100;

                                            var radarPlayerRectangle = new RoundedRectangle() 
                                            {
                                                RadiusX = 4,
                                                RadiusY = 4,
                                                Rect = new RawRectangleF(screenpos.X, screenpos.Y, screenpos.X + 5,
                                                    screenpos.Y + 5)
                                            };

                                            // DRAW ENEMY
                                            device.FillRoundedRectangle(radarPlayerRectangle, brushRed);
                                        }
                                    }

                                    #endregion
                                }
                            }
                        }

                        #endregion


                        // DRAW END
                        device.EndDraw();

                        Thread.Sleep(1000 / 60);

                    }

                }

            });

            dxthread.IsBackground = true;
            dxthread.Start();

            var marg = new Win32.Margins();
            marg.Left = 0;
            marg.Top = 0;
            marg.Right = this.Width;
            marg.Bottom = this.Height;
            Win32.DwmExtendFrameIntoClientArea(this.Handle, ref marg);
        }


        public bool WorldToScreen(Vector3 WorldLocation, APlayerCameraManager CameraManager, out Vector2 Screenlocation)
        {
            Screenlocation = new Vector2(0, 0);

            var POV = CameraManager.CameraCache.POV;
            FRotator Rotation = POV.Rotation;

            Vector3 vAxisX, vAxisY, vAxisZ;
            Rotation.GetAxes(out vAxisX, out vAxisY, out vAxisZ);

            Vector3 vDelta = WorldLocation - POV.Location;
            Vector3 vTransformed = new Vector3(Vector3.DotProduct(vDelta, vAxisY), Vector3.DotProduct(vDelta, vAxisZ), Vector3.DotProduct(vDelta, vAxisX));

            if (vTransformed.Z < 1f)
                vTransformed.Z = 1f;

            float FovAngle = POV.Fov;
            float ScreenCenterX = 1920 / 2;
            float ScreenCenterY = 1080 / 2;

            Screenlocation.X = ScreenCenterX + vTransformed.X * (ScreenCenterX / (float)Math.Tan(FovAngle * (float)Math.PI / 360)) / vTransformed.Z;
            Screenlocation.Y = ScreenCenterY - vTransformed.Y * (ScreenCenterX / (float)Math.Tan(FovAngle * (float)Math.PI / 360)) / vTransformed.Z;

            return true;
        }

        public SharpDX.DirectWrite.TextLayout TextLayout(string szText, SharpDX.DirectWrite.Factory factory, SharpDX.DirectWrite.TextFormat font) =>
            new SharpDX.DirectWrite.TextLayout(factory, szText, font, float.MaxValue, float.MaxValue);

        public void DrawText(string szText, int x, int y, SharpDX.Direct2D1.Brush foregroundBroush, SharpDX.DirectWrite.Factory fontFactory, SharpDX.DirectWrite.TextFormat font, WindowRenderTarget device)
        {
            var tempTextLayout = TextLayout(szText, fontFactory, font);

            device.DrawTextLayout(new RawVector2(x, y), tempTextLayout, foregroundBroush, DrawTextOptions.NoSnap);

            tempTextLayout.Dispose();
        }

        public RawColor4 RawColorFromColor(Color color) => new RawColor4(color.R, color.G, color.B, color.A);//color.ToArgb() >> 16 & 255L, color.ToArgb() >> 8 & 255L, (byte)color.ToArgb() & 255L, color.ToArgb() >> 24 & 255L);


        private static class Win32
        {
            #region Definitions
            public const int GWL_EXSTYLE = -20;

            public const int WS_EX_LAYERED = 0x80000;

            public const int WS_EX_TRANSPARENT = 0x20;

            public const int LWA_ALPHA = 0x2;

            public const int LWA_COLORKEY = 0x1;
            #endregion

            #region Functions
            [DllImport("user32.dll", SetLastError = true)]
            public static extern uint GetWindowLong(IntPtr hWnd, int nIndex);

            [DllImport("user32.dll")]
            public static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

            [DllImport("user32.dll")]
            public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

            [DllImport("dwmapi.dll")]
            public static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMargins);
            #endregion

            #region Structs
            internal struct Margins
            {
                public int Left, Right, Top, Bottom;
            }
            #endregion
        }

        private void OverlayForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
