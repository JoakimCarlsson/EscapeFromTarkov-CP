using UnityEngine;

namespace Citadel.Options
{
    static class MiscOptions
    {
        public static KeyCode ToggleMenu = KeyCode.Insert;

        public static KeyCode ThermalVisonKey = KeyCode.F6;
        public static KeyCode NightVisonKey = KeyCode.F7;
        public static KeyCode DoorUnlockerKey = KeyCode.Home;
        public static KeyCode FlyHackKey = KeyCode.F8;

        public static bool MaxSkills = true;
        public static bool DoorUnlocker = true;
        public static bool InfiniteStamina = true;
        public static bool SpeedHack = true;
        public static bool ThermalVison = false;
        public static bool NoVisor = true;
        public static bool NightVison = false;
        public static bool FlyHack = false;

        //Weapon
        public static bool NoRecoil = true;
        public static bool NoSway = true;
        public static bool AlwaysAutomatic = false;
        public static bool BulletPenetration = false;
        public static bool InstantHit = false;
        public static bool DontMoveWeaponCloser = true;

        public static float SpeedHackValue = 0f;
        public static bool DrawHud = true;
        public static bool ForceLight = false;
        public static bool AlwaysSprint = true;
        public static bool SuperJump = true;
        public static float SuperJumpValue = 0f;
    }
}
