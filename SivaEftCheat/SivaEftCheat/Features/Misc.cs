using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT.UI;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features
{
    class Misc : MonoBehaviour
    {
        private void FixedUpdate()
        {

        }

        private void OnGUI()
        {
            if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive)
            {
                try
                {
                    DrawCrosshair();
                }
                catch { }

            }
        }

        private static void DrawCrosshair()
        {
            if (MiscVisualsOptions.DrawCrossHair)
            {
                Render.DrawLine(new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2 - 9)),
                    new Vector2((float) (Screen.width / 2), (float) (Screen.height / 2 + 9)), MiscOptions.CorsshairColor, 0.5f,
                    true);
                Render.DrawLine(new Vector2((float) (Screen.width / 2 - 9), (float) (Screen.height / 2)),
                    new Vector2((float) (Screen.width / 2 + 9), (float) (Screen.height / 2)), MiscOptions.CorsshairColor, 0.5f,
                    true);
            }
        }
    }
}
