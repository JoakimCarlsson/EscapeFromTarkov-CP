using System;
using System.Collections;
using System.Collections.Generic;
using EFT.UI;
using SivaEftCheat.Data;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features.ESP
{
    class GrenadeEsp : MonoBehaviour
    {
        private List<GameThrowable> _grenades = new List<GameThrowable>();
        private float _nextThorwableCacheTime;
        private static readonly float _cacheThrowableInterval = 1f;

        private void FixedUpdate()
        {
            if (Time.time >= _nextThorwableCacheTime)
            {
                GetGrenades();
                _nextThorwableCacheTime = (Time.time + _cacheThrowableInterval);
            }

            foreach (var grenade in _grenades)
                grenade.RecalculateDynamics();
        }

        private void GetGrenades()
        {

            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && MiscVisualsOptions.DrawGrenadeEsp)
                {
                    _grenades.Clear();
                    var e = Main.GameWorld.Grenades.GetValuesEnumerator().GetEnumerator();
                    while (e.MoveNext())
                    {
                        var grenade = e.Current;

                        if (grenade == null)
                            continue;

                        _grenades.Add(new GameThrowable(grenade));
                    }
                }
            }
            catch { }
        }

        private void OnGUI()
        {
            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && MiscVisualsOptions.DrawGrenadeEsp)
                {
                    foreach (GameThrowable grenade in _grenades)
                    {
                        if (!grenade.IsOnScreen)
                            return;

                        string name = ThrowableName(grenade.Grenade.name.Localized());
                        Render.DrawCircle(grenade.ScreenPosition, 20f, Color.red, 0.5f, true, 40);
                        Render.DrawString(grenade.ScreenPosition, name, Color.red);

                        if (grenade.Distance < 10f)
                        {
                            Render.DrawString(new Vector2(1024, Screen.height - 56), $"{name} close: {grenade.FormattedDistance}", Color.white, false, 20);

                        }
                    }
                }
            }
            catch { }
        }
        public static string ThrowableName(string name)
        {
            switch (name)
            {
                case "weapon_rgd5_world(Clone)":
                    return "RGD5";
                case "weapon_grenade_f1_world(Clone)":
                    return "F1";
                case "weapon_rgd2_world(Clone)":
                    return "Smoke";
                case "weapon_m67_world(Clone)":
                    return "M67";
                case "weapon_zarya_world(Clone)":
                    return "Flash Bang";
                default:
                    return name.Replace("weapon_", "").Replace("_world(Clone)", "").Replace("grenade_", "");
            }
        }
    }
}
