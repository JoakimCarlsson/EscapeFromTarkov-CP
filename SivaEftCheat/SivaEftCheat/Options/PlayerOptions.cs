using UnityEngine;

namespace SivaEftCheat.Options
{
    class PlayerOptions
    {
        public static Color PlayerColor = Color.red;
        public static Color Scav = Color.yellow;
        public static Color PlayerScav = Color.magenta;
        public static Color FriendColor = Color.green;

        public static float DrawPlayerRange = 300f;
        public static float DrawPlayerSkeletonRange = 100f;

        public static bool DrawPlayers = true;
        public static bool DrawPlayerHealth = true;
        public static bool DrawPlayerHealthBar = true;
        public static bool DrawPlayerName = true;
        public static bool DrawPlayerLevel = true;
        public static bool DrawPlayerWeapon = true;
        public static bool DrawPlayerSnapLine = true;
        public static bool DrawPlayerBox = true;
        public static bool DrawPlayerSkeleton = true;
        public static bool DrawPlayerDistance = true;

        public static float DrawScavsRange = 300f;
        public static float DrawScavsSkeletonRange = 100f;
        public static bool DrawScavs = true;
        public static bool DrawScavHealth = true;
        public static bool DrawScavHealthBar = true;
        public static bool DrawScavName = true;
        public static bool DrawScavWeapon = true;
        public static bool DrawScavSnapLine = true;
        public static bool DrawScavBox = true;
        public static bool DrawScavSkeleton = true;
        public static bool DrawScavDistance = true;
    }
}
