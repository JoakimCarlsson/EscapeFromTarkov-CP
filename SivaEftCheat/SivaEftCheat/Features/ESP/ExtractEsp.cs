using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comfort.Common;
using EFT;
using EFT.Interactive;
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
            _coroutineUpdate = Updates(10f);
            StartCoroutine(_coroutineUpdate);
        }

        private IEnumerator Updates(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);

                try
                {
                    int scavMask = 0;
                    if (Main.LocalPlayer is ClientPlayer player)
                        scavMask = player.ScavExfilMask;

                    ExfiltrationPoint[] points;

                    if (Main.LocalPlayer.Profile.Info.Side == EPlayerSide.Savage)
                        points = Main.GameWorld.ExfiltrationController.ScavExfiltrationClaim(scavMask, Main.LocalPlayer.Profile.Id);
                    else
                        points = Main.GameWorld.ExfiltrationController.EligiblePoints(Main.LocalPlayer.Profile);

                    foreach (ExfiltrationPoint exfiltrationPoint in points)
                    {
                        if (exfiltrationPoint == null)
                            continue;

                        exfiltrationPoints.Add(exfiltrationPoint);
                    }
                }
                catch { }
            }
        }

        private void OnGUI()
        {
            try
            {
                foreach (ExfiltrationPoint exfiltrationPoint in exfiltrationPoints)
                {
                    Vector3 screenPosition = GameUtils.WorldPointToScreenPoint(exfiltrationPoint.transform.position);
                    bool isOnScreen = GameUtils.IsScreenPointVisible(screenPosition);
                    float num = GameUtils.InPoint(Main.Camera.transform.position, exfiltrationPoint.transform.position);
                    if (isOnScreen)
                    {
                        string exfiltrationPointText = $"{exfiltrationPoint.Settings.Name} [{((int)Math.Sqrt(num)).ToString()} m]";

                        Render.DrawTextOutline(screenPosition, exfiltrationPointText, Color.black, Color.white, 12);
                    }
                }
            }
            catch { }
        }
    }
}
