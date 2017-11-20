using System.Collections.Generic;

namespace CPUZ
{
    public static class Cache
    {
        public static Dictionary<int, string> GCahce = new Dictionary<int, string>();

        public static string GetGName(int actorId)
        {
            if (GCahce.TryGetValue(actorId, out var gname)) return gname;
            gname = KReader.getGNameFromId(actorId);
            GCahce.Add(actorId, gname);
            return gname;
        }

        public static string GetPlayerName(int playerId)
        {
            if (PlayerCache.TryGetValue(playerId, out var playerName)) return playerName;
            playerName = KReader.getGNameFromId(playerId);
            PlayerCache.Add(playerId, playerName);
            return playerName;
        }

        public static void ClearPlayerCache()
        {
            PlayerCache.Clear();
        }



        public static Dictionary<int, string> PlayerCache = new Dictionary<int, string>();

    }
}