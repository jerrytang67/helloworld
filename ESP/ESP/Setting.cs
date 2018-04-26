namespace CPUZ
{
    public static class Setting
    {


        public static bool 车辆显示 { get; set; } = true;
        public static bool 物品显示 { get; set; } = false;
        public static bool 距离和血量 { get; set; } = true;
        public static bool 线条 { get; set; } = true;
        public static bool 雷达 { get; set; } = false;

        public static bool 一键大跳 { get; set; } = false;

        public static int JSON_STATE { get; set; } = 0;

        public static bool Web端 { get; set; } = false;


        public static ulong UWorldOffset { get; } = 0x3A4F1A8;
        public static ulong GNameOffset { get; } = 0x3951F90;

        public static ScreenType Screen { get; } = new ScreenType { Width = 2560, Height = 1440 };

        public static 模式 模式 { get; set; } = 模式.模式二;
    }

    public class ScreenType
    {
        public int Width { get; set; }
        public int Height { get; set; }
    }
    
    public enum 模式
    {
        模式一 = 1,
        模式二 = 2
    }
}