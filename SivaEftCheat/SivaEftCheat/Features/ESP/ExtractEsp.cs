using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using EFT;
using EFT.Interactive;
using EFT.UI;
using SivaEftCheat.Data;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features.ESP
{
    class ExtractEsp : MonoBehaviour
    {
        private readonly List<GameExtractPoint> exfiltrationPoints = new List<GameExtractPoint>();

        private float _nextListCacheTime;
        private static readonly float _cacheListInterval = 5f;



        private void FixedUpdate()
        {
            if (Time.time >= _nextListCacheTime)
            {
                Updates();
                _nextListCacheTime = (Time.time + _cacheListInterval);
            }

            foreach (var exfiltrationPoint in exfiltrationPoints)
                exfiltrationPoint.RecalculateDynamics();
        }

        private void Updates()
        {
            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && MiscVisualsOptions.DrawExtractEsp && Main.Camera != null)
                {
                    int scavMask = 0;
                    if (Main.LocalPlayer is ClientPlayer player)
                        scavMask = player.ScavExfilMask;

                    ExfiltrationPoint[] points;

                    if (Main.LocalPlayer.Profile.Info.Side == EPlayerSide.Savage)
                        points = Main.GameWorld.ExfiltrationController.ScavExfiltrationClaim(scavMask,
                            Main.LocalPlayer.Profile.Id);
                    else
                        points = Main.GameWorld.ExfiltrationController.EligiblePoints(Main.LocalPlayer.Profile);

                    exfiltrationPoints.Clear();
                    foreach (ExfiltrationPoint exfiltrationPoint in points)
                    {
                        if (exfiltrationPoint == null)
                            continue;

                        exfiltrationPoints.Add(new GameExtractPoint(exfiltrationPoint));
                    }
                }
            }
            catch { }
        }

        private void OnGUI()
        {
            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && MiscVisualsOptions.DrawExtractEsp && Main.Camera != null)
                {
                    //int y = 20;
                    foreach (GameExtractPoint exfiltrationPoint in exfiltrationPoints)
                    {
                        if (exfiltrationPoint.ScreenPosition.z > 0.01)
                        {
                            string exfiltrationPointText1 = $"{exfiltrationPoint.Name} [{exfiltrationPoint.FormattedDistance}]";
                            string exfiltrationPointText2 = $"{TypeOfExfiltration(exfiltrationPoint.ExfiltrationPoint.Status)}";

                            if (MiscVisualsOptions.DrawExtractEspSwitches)
                            {
                                Switch exfiltrationPointSwitch = exfiltrationPoint.ExfiltrationPoint.Switch;

                                while (exfiltrationPointSwitch != null && exfiltrationPointSwitch.PreviousSwitch != null)
                                {
                                    exfiltrationPointSwitch = exfiltrationPointSwitch.PreviousSwitch;
                                    Vector3 switchScreenPosition = GameUtils.WorldPointToScreenPoint(exfiltrationPointSwitch.transform.position);
                                    Render.DrawString(switchScreenPosition, exfiltrationPointSwitch.name, Color.white);
                                    Render.DrawLine(GameUtils.WorldPointToScreenPoint(exfiltrationPointSwitch.transform.position), exfiltrationPoint.ScreenPosition, 1f, Color.white);
                                }
                            }

                            Render.DrawString(exfiltrationPoint.ScreenPosition, exfiltrationPointText1, MiscVisualsOptions.ExtractColor);
                            Render.DrawString(exfiltrationPoint.ScreenPosition + new Vector3(0, 10, 0), exfiltrationPointText2, MiscVisualsOptions.ExtractColor);
                        }

                    }
                }
            }
            catch { }
        }

        public static string TypeOfExfiltration(EExfiltrationStatus status)
        {
            switch (status)
            {
                case EExfiltrationStatus.AwaitsManualActivation:
                    return "Activate";
                case EExfiltrationStatus.Countdown:
                    return "Timer";
                case EExfiltrationStatus.NotPresent:
                    return "Closed";
                case EExfiltrationStatus.Pending:
                    return "Pending";
                case EExfiltrationStatus.RegularMode:
                    return "Open";
                case EExfiltrationStatus.UncompleteRequirements:
                    return "Requirement";
                default:
                    return "";
            }
        }
    }
}
