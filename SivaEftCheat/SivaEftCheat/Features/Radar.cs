using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BehaviourMachine;
using EFT.UI;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features
{


    class Radar : MonoBehaviour
    {


        private void Update()
        {
            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && MiscVisualsOptions.DrawRadar && Main.Camera != null && Menu.Visible)
                {
                    if (Input.GetKey(KeyCode.UpArrow))
                        MiscVisualsOptions.RadarY -= 0.5f;
                    if (Input.GetKey(KeyCode.DownArrow))
                        MiscVisualsOptions.RadarY += 0.5f;
                    if (Input.GetKey(KeyCode.RightArrow))
                        MiscVisualsOptions.RadarX += 0.5f;
                    if (Input.GetKey(KeyCode.LeftArrow))
                        MiscVisualsOptions.RadarX -= 0.5f;
                }
            }
            catch { }

        }
        private void OnGUI()
        {
            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && MiscVisualsOptions.DrawRadar && Main.Camera != null)
                {
                    Render.BoxRect(new Rect(MiscVisualsOptions.RadarX + MiscVisualsOptions.RadarSize / 2f - 3f, MiscVisualsOptions.RadarY + MiscVisualsOptions.RadarSize / 2f - 3f, 6f, 6f), Color.white);

                    foreach (var player in Main.Players)
                    {
                        if ((!player.IsAI || MiscVisualsOptions.DrawScavs) && (player.IsAI || MiscVisualsOptions.DrawPlayers))
                        {
                            float y = Main.LocalPlayer.Transform.position.x - player.Player.Transform.position.x;
                            float x = Main.LocalPlayer.Transform.position.z - player.Player.Transform.position.z;

                            float atan = Mathf.Atan2(y, x) * 57.29578f - 270 - Main.LocalPlayer.Transform.eulerAngles.y;

                            float num3 = player.Distance * Mathf.Cos(atan * 0.0174532924f);
                            float num4 = player.Distance * Mathf.Sin(atan * 0.0174532924f);

                            num3 = num3 * (MiscVisualsOptions.RadarSize / MiscVisualsOptions.RadarRange) / 2f;
                            num4 = num4 * (MiscVisualsOptions.RadarSize / MiscVisualsOptions.RadarRange) / 2f;

                            if (player.Distance <= MiscVisualsOptions.RadarRange)
                            {
                                Render.BoxRect(new Rect(MiscVisualsOptions.RadarX + MiscVisualsOptions.RadarSize / 2f + num3 - 3f, MiscVisualsOptions.RadarY + MiscVisualsOptions.RadarSize / 2f + num4 - 3f, 6f, 6f), player.PlayerColor);

                                if (MiscVisualsOptions.DrawWealth)
                                    Render.DrawString(new Vector2(MiscVisualsOptions.RadarX + MiscVisualsOptions.RadarSize / 2f + num3 - 3f, MiscVisualsOptions.RadarY + MiscVisualsOptions.RadarSize / 2f + num4 - 3f), player.FormattedValue, Color.white, true, 9, FontStyle.Bold);
                            }


                        }
                    }

                    //To Render Our background
                    Render.DrawRadarBackground(new Rect(MiscVisualsOptions.RadarX, MiscVisualsOptions.RadarY, MiscVisualsOptions.RadarSize, MiscVisualsOptions.RadarSize));
                }

            }
            catch { }
        }

    }
}
