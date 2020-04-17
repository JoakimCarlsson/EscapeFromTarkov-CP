using System;
using System.Collections;
using System.Collections.Generic;
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
        private IEnumerator _coroutineUpdate;
        private readonly List<GameExtractPoint> exfiltrationPoints = new List<GameExtractPoint>();
        private void Start()
        {
            _coroutineUpdate = Updates(2.5f);
            StartCoroutine(_coroutineUpdate);
        }

        private void FixedUpdate()
        {
            foreach (var exfiltrationPoint in exfiltrationPoints)
                exfiltrationPoint.RecalculateDynamics();
        }

        private IEnumerator Updates(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);

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
        }

        private void OnGUI()
        {
            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && MiscVisualsOptions.DrawExtractEsp && Main.Camera != null)
                {
                    foreach (GameExtractPoint exfiltrationPoint in exfiltrationPoints)
                    {
                        if (exfiltrationPoint.IsOnScreen)
                        {
                            string exfiltrationPointText = $"{exfiltrationPoint.Name} [{exfiltrationPoint.FormattedDistance}]";

                            Render.DrawString(exfiltrationPoint.ScreenPosition, exfiltrationPointText, MiscVisualsOptions.ExtractColor);
                        }
                    }
                }
            }
            catch { }
        }
    }
}
