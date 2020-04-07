using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using EFT.UI;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features.ESP
{
    class ExtractEsp : MonoBehaviour
    {
        private IEnumerator _coroutineUpdate;
        private readonly List<ExfiltrationPoint> exfiltrationPoints = new List<ExfiltrationPoint>();
        private void Start()
        {
            _coroutineUpdate = Updates(2.5f);
            StartCoroutine(_coroutineUpdate);
        }

        private IEnumerator Updates(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);

                try
                {
                    if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && MiscOptions.ExtractEsp)
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

                            exfiltrationPoints.Add(exfiltrationPoint);
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
                GUI.Label(new Rect(20f, 20f, 500f, 500f), $"Extract Debug text: {exfiltrationPoints.Count}" );
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && MiscOptions.ExtractEsp)
                {
                    foreach (ExfiltrationPoint exfiltrationPoint in exfiltrationPoints)
                    {
                        Vector3 screenPosition = GameUtils.WorldPointToScreenPoint(exfiltrationPoint.transform.position);
                        bool isOnScreen = GameUtils.IsScreenPointVisible(screenPosition);
                        float num = GameUtils.InPoint(Main.Camera.transform.position, exfiltrationPoint.transform.position);
                        if (isOnScreen)
                        {
                            string exfiltrationPointText = $"{exfiltrationPoint.Settings.Name} [{((int)Math.Sqrt(num)).ToString()} m]";

                            Render.DrawTextOutline(screenPosition, exfiltrationPointText, Color.black, MiscOptions.ExtractColor, 12);
                        }
                    }
                }
            }
            catch { }
        }
    }
}
