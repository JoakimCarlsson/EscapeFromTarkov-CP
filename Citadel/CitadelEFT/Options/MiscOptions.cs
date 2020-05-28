using UnityEngine;

namespace Citadel.Options
{
    static class MiscOptions
    {
        public static KeyCode ToggleMenu = KeyCode.Insert;

        public static KeyCode ThermalVisonKey = KeyCode.F6;
        public static KeyCode NightVisonKey = KeyCode.F7;

        public static bool ThermalVison = false;
        public static bool NoVisor = true;
        public static bool NightVison = false;

        //Weapon
        public static bool NoRecoil = true;
        public static bool NoSway = true;
        public static bool BulletPenetration = false;
        public static bool DontMoveWeaponCloser = true;

        public static float SpeedHackValue = 0f;
        public static bool DrawHud = true;
        public static bool ForceLight = false;
    }
}
