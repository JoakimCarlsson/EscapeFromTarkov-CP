using UnityEngine;

namespace SivaEftCheat.Options
{
    static class PlayerOptions
    {
        public static Color PlayerColor = Color.red;
        public static Color ScavColor = Color.yellow;
        public static Color PlayerScavColor = Color.magenta;
        public static Color FriendColor = Color.green;
        public static Color BossColor = Color.red;


        public static bool DrawCorpses = true;
        public static float DrawPlayerRange = 300f;
        public static bool DrawPlayers = true;
        public static bool DrawPlayerHealth = true;
        public static bool DrawPlayerHealthBar = true;
        public static bool DrawPlayerName = true;
        public static bool DrawPlayerLevel = true;
        public static bool DrawPlayerWeapon = true;
        public static bool DrawPlayerSnapLine = true;
        public static bool DrawPlayerCornerBox = true;
        public static bool DrawPlayerSkeleton = true;
        public static bool DrawPlayerDistance = true;
        public static bool DrawPlayerAimPos = true;
        public static bool DrawPlayerValue = true;

        public static float DrawScavsRange = 300f;
        public static bool DrawScavs = true;
        public static bool DrawScavHealth = true;
        public static bool DrawScavHealthBar = true;
        public static bool DrawScavName = true;
        public static bool DrawScavWeapon = true;
        public static bool DrawScavSnapLine = true;
        public static bool DrawScavCornerBox = true;
        public static bool DrawScavSkeleton = true;
        public static bool DrawScavDistance = true;
        public static bool DrawScavAimPos = true;
        public static bool DrawScavValue = true;
    }
}
