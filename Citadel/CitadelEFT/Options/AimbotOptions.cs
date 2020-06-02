using UnityEngine;

namespace Citadel.Options
{
    class AimbotOptions
    {
        public static bool Aimbot = true;
        public static float Distance = 200f;
        public static KeyCode AimbotKey = KeyCode.LeftControl;
        public static float AimbotFov = 500f;
        public static bool DrawAimbotFov = false;
        public static bool SilentAim = false;
        public static bool TargetSnapLine = true;
        public static bool AutoShoot = false;
        public static int AimbotBone = 132;

        //133 Head.
        //132 Neck.
        //120 Body
    }
}
