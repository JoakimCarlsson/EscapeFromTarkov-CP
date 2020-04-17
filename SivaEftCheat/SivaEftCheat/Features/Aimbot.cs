using System;
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
        public static TestHook CreateShotHook;
        public static GamePlayer Target;
        private static float NextMouseClick;

        private void Update()
        {
            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive)
                {
                    if (AimbotOptions.SilentAim && NotHooked)
                    {
                        CreateShotHook = new TestHook();
                        CreateShotHook.Init(typeof(BallisticsCalculator).GetMethod("CreateShot"), typeof(HookObject).GetMethod("SilentAimHook"));
                        CreateShotHook.Hook();
                        NotHooked = false;
                    }

                    Target = GetTarget();
                    AutoShoot();
                }
            }
            catch { }
        }

        private static void AutoShoot()
        {
            if (Target != null && AimbotOptions.AutoShoot)
            {
                if (NextMouseClick < Time.time && RayCast.IsBodyPartVisible(Target.Player, 132))
                {
                    MouseOperations.MouseEvent(
                        MouseOperations.MouseEventFlags.LeftUp | MouseOperations.MouseEventFlags.LeftDown);
                    NextMouseClick = Time.time + 0.064f;
                }
            }
        }

        private GamePlayer GetTarget()
        {
            Dictionary<GamePlayer, int> dictionary = new Dictionary<GamePlayer, int>();
            foreach (var player in Main.Players)
            {
                Vector3 vector2 = player.Player.Transform.position - Main.Camera.transform.position;
                if (player.Distance <= AimbotOptions.Distnace && player.DistanceFromCenter <= AimbotOptions.AimbotFov && Vector3.Dot(Main.Camera.transform.TransformDirection(Vector3.forward), vector2) > 0f)
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
                    GamePlayer player = Target;

                    Vector3 headPosition = player.Player.PlayerBones.Head.position;

                    Weapon weapon = Main.LocalPlayer.Weapon;
                    if (weapon != null)
                    {
                        float travelTime = Target.Distance / Main.LocalPlayer.Weapon.CurrentAmmoTemplate.InitialSpeed;
                        headPosition.x += player.Player.Velocity.x * travelTime;
                        headPosition.y += player.Player.Velocity.y * travelTime;
                        aimPosition = headPosition;
                        //Render.DrawString(new Vector2(bottomPosition.x, (float)Screen.height - bottomPosition.y + 20f), "This dude is about to die", Color.red, true, 12, FontStyle.Bold, 3);
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

                if (Target == null)
                    return;

                Weapon weapon = Main.LocalPlayer.Weapon;

                if (weapon != null)
                {
                    Render.DrawLine(GameUtils.ScreenCenter, Target.HeadScreenPosition, MiscVisualsOptions.CrossHairColor, 0.5f, true);
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
