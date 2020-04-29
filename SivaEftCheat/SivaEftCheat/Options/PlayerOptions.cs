using UnityEngine;

namespace Citadel.Options
{
    static class PlayerOptions
    {
        public static Color PlayerColor = new Color(0.75f, 0f, 0f);
        public static Color ScavColor = new Color(1f, 0.81f, 0f);
        public static Color PlayerScavColor = new Color(0.9f, 0.09f, 0.9f);
        public static Color FriendColor = new Color(0f, 0.7f, 1f);
        public static Color BossColor = Color.red;


        public static bool DrawCorpses = true;
        public static float DrawPlayerRange = 300f;
        public static bool DrawPlayers = true;
        public static bool DrawPlayerHealth = true;
        public static bool DrawPlayerHealthBar = true;
        public static bool DrawPlayerName = true;
        public static bool DrawPlayerLevel = true;
        public static bool DrawPlayerWeapon = true;
        public static bool DrawPlayerCornerBox = true;
        public static bool DrawPlayerSkeleton = false;
        public static bool DrawPlayerDistance = true;
        public static bool DrawPlayerAimPos = false;
        public static bool DrawPlayerValue = true;
        public static bool PlayerChams = false;

        public static float DrawScavsRange = 300f;
        public static bool DrawScavs = true;
        public static bool DrawScavHealth = true;
        public static bool DrawScavHealthBar = true;
        public static bool DrawScavName = true;
        public static bool DrawScavWeapon = true;
        public static bool DrawScavCornerBox = true;
        public static bool DrawScavSkeleton = false;
        public static bool DrawScavDistance = true;
        public static bool DrawScavAimPos = false;
        public static bool DrawScavValue = true;
        public static bool ScavChams = true;
    }
}
