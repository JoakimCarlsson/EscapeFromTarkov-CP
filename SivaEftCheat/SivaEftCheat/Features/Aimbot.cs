using System.Collections.Generic;
using System.Linq;
using EFT;
using EFT.Ballistics;
using EFT.InventoryLogic;
using EFT.UI;
using SivaEftCheat.Data;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features
{
    class Aimbot : MonoBehaviour
    {
        public static bool NotHooked = true;
        public static TestHook createShotHook;
        private GamePlayer _target;

        private void Update()
        {
            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive)
                {
                    if (AimbotOptions.SilentAim && NotHooked)
                    {
                        createShotHook = new TestHook();
                        createShotHook.Init(typeof(BallisticsCalculator).GetMethod("CreateShot"), typeof(HookObject).GetMethod("SilentAimHook"));
                        createShotHook.Hook();
                        NotHooked = false;
                    }

                    _target = GetTarget();
                }
            }
            catch { }
        }

        private GamePlayer GetTarget()
        {
            Dictionary<GamePlayer, int> dictionary = new Dictionary<GamePlayer, int>();
            Vector3 zero = Vector3.zero;
            foreach (var player in Main.Players)
            {
                Vector3 vector2 = player.Player.Transform.position - Main.Camera.transform.position;
                if (player.Distance <= AimbotOptions.AimbotDistnace && player.DistanceFromCenter <= AimbotOptions.AimbotFov && Vector3.Dot(Main.Camera.transform.TransformDirection(Vector3.forward), vector2) > 0f)
                {
                    dictionary.Add(player, (int)player.DistanceFromCenter);
                }
            }

            if (dictionary.Count > 0.01)
            {
                dictionary = (from pair in dictionary orderby pair.Value select pair).ToDictionary(pair => pair.Key, pair => pair.Value);
                return dictionary.Keys.First();
            }

            return null;
        }
        private void OnGUI()
        {
            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive)
                {
                    TargetSnapLine();
                    DoAimbot();
                    DrawFov();
                }
            }
            catch { }
        }

        private void DoAimbot()
        {
            if (AimbotOptions.Aimbot && Input.GetKey(AimbotOptions.AimbotKey))
            {
                Vector3 aimPosition = Vector3.zero;
                {
                    GamePlayer player = _target;

                    Vector3 headPosition = player.Player.PlayerBones.Head.position;

                    Weapon weapon = Main.LocalPlayer.Weapon;
                    if (weapon != null)
                    {
                        float travelTime = _target.Distance / Main.LocalPlayer.Weapon.CurrentAmmoTemplate.InitialSpeed;
                        headPosition.x += player.Player.Velocity.x * travelTime;
                        headPosition.y += player.Player.Velocity.y * travelTime;
                        aimPosition = headPosition;
                        //Render.DrawString1(new Vector2(bottomPosition.x, (float)Screen.height - bottomPosition.y + 20f), "This dude is about to die", Color.red, true, 12, FontStyle.Bold, 3);
                    }

                    if (aimPosition != Vector3.zero)
                        AimAtPos(aimPosition);
                }
            }
        }

        private void TargetSnapLine()
        {
            if (AimbotOptions.TargetSnapLine && AimbotOptions.Aimbot)
            {

                if (_target == null)
                    return;

                Weapon weapon = Main.LocalPlayer.Weapon;

                if (_target.HeadScreenPosition.z > 0.01 && weapon != null)
                {
                    Render.DrawLine(GameUtils.ScreenCenter, _target.HeadScreenPosition, MiscVisualsOptions.CrossHairColor, 0.5f, true);
                }

            }
        }

        public static void AimAtPos(Vector3 position)
        {
            if (Main.LocalPlayer.Weapon != null)
            {
                Vector3 eulerAngles = Quaternion.LookRotation((position - Main.LocalPlayer.Fireport.position).normalized).eulerAngles;
                if (eulerAngles.x > 180f)
                    eulerAngles.x -= 360f;

                Main.LocalPlayer.MovementContext.Rotation = new Vector2(eulerAngles.y, eulerAngles.x);
            }
        }

        private void DrawFov()
        {
            if (AimbotOptions.DrawAimbotFov && AimbotOptions.Aimbot)
            {
                Render.DrawCircle(GameUtils.ScreenCenter, AimbotOptions.AimbotFov, Color.white, 0.5f, true, 40);
            }
            if (AimbotOptions.DrawSilentFov && AimbotOptions.SilentAim)
            {
                Render.DrawCircle(GameUtils.ScreenCenter, AimbotOptions.SilentAimFov, Color.white, 0.5f, true, 40);
            }
        }
    }
}
