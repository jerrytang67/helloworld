namespace CPUZ
{
    public static class Setting
    {
        public static bool 车辆显示 { get; set; } = true;
        public static bool 物品显示  { get; set; } = false;
        public static bool 距离和血量  { get; set; } = true;
        public static bool 线条  { get; set; } = true;
        public static bool 雷达 { get; set; } = false;

        public static bool 一键大跳 { get; set; } = false;

        public static int JSON_STATE { get; set; } = 0;

        public static bool Web端 { get; set; } = false;


        public static ulong UWorldOffset { get; } = 0x37E6988;
        public static ulong GNameOffset { get; } = 0x36E9790;
    }
}