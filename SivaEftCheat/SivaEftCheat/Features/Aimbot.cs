using EFT.Ballistics;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features
{
    class Aimbot : MonoBehaviour
    {
        public static string _hud = string.Empty;
        public static bool NotHooked = true;
        public static TestHook createShotHook;

        private void Update()
        {
            if (AimbotOptions.SilentAim && NotHooked)
            {
                createShotHook = new TestHook();
                createShotHook.Init(typeof(BallisticsCalculator).GetMethod("CreateShot"), typeof(SilentAim).GetMethod("SilentAimHook"));
                createShotHook.Hook();
                NotHooked = false;
            }
        }
        private void OnGUI()
        {
            try
            {
            }
            catch
            {
            }
        }
    }
}
