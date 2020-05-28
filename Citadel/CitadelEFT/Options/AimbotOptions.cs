using UnityEngine;

namespace Citadel.Options
{
    class AimbotOptions
    {
        public static bool Aimbot = true;
        public static float Distnace = 200f;
        public static KeyCode AimbotKey = KeyCode.LeftControl;
        public static float AimbotFov = 500f;
        public static bool DrawAimbotFov = true;
        public static bool TargetSnapLine = true;
        public static int AimbotBone = 132;

        //133 Head.
        //132 Neck.
        //120 Body
    }
}
