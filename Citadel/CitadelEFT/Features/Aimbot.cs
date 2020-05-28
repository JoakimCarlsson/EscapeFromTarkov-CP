using System;
using System.Collections.Generic;
using System.Linq;
using Citadel.Data;
using Citadel.Options;
using Citadel.Utils;
using EFT;
using EFT.Ballistics;
using EFT.InventoryLogic;
using EFT.UI;
using UnityEngine;

namespace Citadel.Features
{
    class Aimbot : MonoBehaviour
    {
        public static bool NotHooked = true;
        public static TestHook CreateShotHook;
        public static GamePlayer Target;
        private static float _nextShot;

        private void FixedUpdate()
        {
            try
            {
                if (Main.CanUpdate && Main.LocalPlayer.HandsController.Item is Weapon)
                {
                    if (AimbotOptions.SilentAim && NotHooked)
                    {
                        CreateShotHook = new TestHook();
                        CreateShotHook.Init(typeof(BallisticsCalculator).GetMethod("CreateShot"), typeof(HookObject).GetMethod("SilentAimHook"));
                        CreateShotHook.Hook();
                        NotHooked = false;
                    }

                    //Target = Main.Players.Where(p => p.DistanceFromCenter <= AimbotOptions.AimbotFov && p.Distance <= AimbotOptions.Distnace && p.IsOnScreen).OrderBy(p => p.DistanceFromCenter).First();
                    //if (Target.DistanceFromCenter > AimbotOptions.AimbotFov || !GameUtils.IsPlayerAlive(Target.Player))
                    //    Target = null;

                    Target = GetTarget();

                    DoAimbot();
                    AutoShoot();

                }
            }
            catch
            {
            }
        }

        private static void AutoShoot()
        {
            if (Target != null && AimbotOptions.AutoShoot)
            {
                if (!Main.LocalPlayer.IsInventoryOpened || Main.LocalPlayer.Weapon != null)
                {
                    if (_nextShot < Time.time && RayCast.IsBodyPartVisible(Target.Player, 132))
                    {
                        Main.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(true);
                        _nextShot = Time.time + 0.064f;
                        Main.LocalPlayer.GetComponent<Player.FirearmController>().SetTriggerPressed(false);
                    }
                }
            }

        }

        private GamePlayer GetTarget()
        {
            Dictionary<GamePlayer, int> dictionary = new Dictionary<GamePlayer, int>();
            foreach (var player in Main.Players)
            {
                if (GameUtils.IsFriend(player.Player))
                    continue;

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
                if (Main.CanUpdate)
                {
                    Render.DrawString(new Vector2(20, 100), Target != null ? $"Target: {Target.Player.Profile.Info.Nickname}" : $"Target: None", Color.white, false);
                    TargetSnapLine();
                    DrawFov();
                }
            }
            catch { }
        }

        private void DoAimbot()
        {
            if (AimbotOptions.Aimbot && Input.GetKey(AimbotOptions.AimbotKey))
            {
                if (Target == null)
                    return;

                Vector3 aimPosition = Vector3.zero;
                Vector3 headPosition = GameUtils.FinalVector(Target.Player, AimbotOptions.AimbotBone) + new Vector3(0f, 0.07246377f, 0f);

                Weapon weapon = Main.LocalPlayer.Weapon;
                if (weapon != null)
                {
                    if (Main.LocalPlayer.Weapon.CurrentAmmoTemplate != null)
                    {
                        float travelTime = Target.Distance / Main.LocalPlayer.Weapon.CurrentAmmoTemplate.InitialSpeed;
                        headPosition.x += Target.Player.Velocity.x * travelTime;
                        headPosition.y += Target.Player.Velocity.y * travelTime;
                    }

                    aimPosition = headPosition;
                }

                if (aimPosition != Vector3.zero)
                    AimAtPos(aimPosition);
            }
        }

        private void TargetSnapLine()
        {
            if (AimbotOptions.TargetSnapLine && AimbotOptions.Aimbot)
            {
                if (Target == null || !GameUtils.IsPlayerAlive(Target.Player))
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
            if (AimbotOptions.DrawAimbotFov)
            {
                Render.DrawCircle(GameUtils.ScreenCenter, AimbotOptions.AimbotFov, Color.white, 0.5f, true, 40);
            }
        }
    }
}
