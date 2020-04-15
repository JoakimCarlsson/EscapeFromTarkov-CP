using System;
using System.Collections.Generic;
using System.Linq;
using BSG.CameraEffects;
using EFT;
using EFT.Interactive;
using EFT.InventoryLogic;
using EFT.UI;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features
{
    class Misc : MonoBehaviour
    {
        private string _test = string.Empty;
        private string _hud = string.Empty;
        private void FixedUpdate()
        {
            if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && Main.Camera != null)
            {
                try
                {
                    DoThermalVision();
                    DoNightVison();
                    NoRecoil();
                    NoSway();
                    MaxSkills();
                    NoVisor();
                    InfiniteStamina();
                    SpeedHack();
                    UnlockDoors();
                    FlyHack();
                    AlwaysAutomatic();
                    PrepareHud();
                    DontMoveWeaponCloser();
                }
                catch { }
            }
        }

        private void DontMoveWeaponCloser()
        {
            if (MiscOptions.DontMoveWeaponCloser)
            {
               Main.LocalPlayer.ProceduralWeaponAnimation.Mask = EFT.Animations.EProceduralAnimationMask.ForceReaction;
            }
        }

        private void PrepareHud()
        {
            if (MiscOptions.DrawHud)
            {
                string tempHealth = "💖";
                string tempMag = string.Empty;

                var weapon = Main.LocalPlayer.Weapon;
                var mag = weapon?.GetCurrentMagazine();
                if (mag != null)
                {
                    tempMag = $"{mag.Count}+{weapon.ChamberAmmoCount}/{mag.MaxCount} [{weapon.SelectedFireMode}]";
                }

                tempHealth = $"{Main.LocalPlayer.HealthController.GetBodyPartHealth(EBodyPart.Common, true).Current} / {Main.LocalPlayer.HealthController.GetBodyPartHealth(EBodyPart.Common, true).Maximum}";

                _hud = $"HP: {tempHealth} Ammo: {tempMag}";
            }

        }

        private void AlwaysAutomatic()
        {
            if (MiscOptions.AlwaysAutomatic && Main.LocalPlayer.HandsController.Item is Weapon)
            {
                Main.LocalPlayer.Weapon.GetItemComponent<FireModeComponent>().FireMode = Weapon.EFireMode.fullauto;
                Main.LocalPlayer.GetComponent<Player.FirearmController>().Item.Template.BoltAction = false;
                Main.LocalPlayer.GetComponent<Player.FirearmController>().Item.Template.bFirerate = 1000;
                _test = Main.LocalPlayer.GetComponent<Player.FirearmController>().Item.Template.bFirerate.ToString();
            }
        }

        private void FlyHack()
        {
            if (Input.GetKey(MiscOptions.FlyHackKey) && MiscOptions.FlyHack)
            {
                Main.LocalPlayer.MovementContext.FreefallTime = -0.2f;
            }
        }

        private void UnlockDoors()
        {
            if (MiscOptions.DoorUnlocker)
            {
                if (Input.GetKeyDown(MiscOptions.DoorUnlockerKey))
                {
                    foreach (var door in LocationScene.GetAllObjects<WorldInteractiveObject>())
                    {
                        if (door.DoorState == EDoorState.Open || Vector3.Distance(door.transform.position, Main.LocalPlayer.Position) > 10f)
                            continue;

                        door.DoorState = EDoorState.Shut;
                    }
                }
            }

        }

        private void SpeedHack()
        {
            if (Input.GetKey(KeyCode.W))
                Main.LocalPlayer.Transform.position += Main.LocalPlayer.Transform.forward / 5f * (float) MiscOptions.SpeedHackValue;
            if (Input.GetKey(KeyCode.S))
                Main.LocalPlayer.Transform.position -= Main.LocalPlayer.Transform.forward / 5f * (float) MiscOptions.SpeedHackValue;
            if (Input.GetKey(KeyCode.A))
                Main.LocalPlayer.Transform.position -= Main.LocalPlayer.Transform.right / 5f * (float) MiscOptions.SpeedHackValue;
            if (Input.GetKey(KeyCode.D))
                Main.LocalPlayer.Transform.position += Main.LocalPlayer.Transform.right / 5f * (float) MiscOptions.SpeedHackValue;
        }

        private void InfiniteStamina()
        {
            if (MiscOptions.InfiniteStamina)
            {
                Main.LocalPlayer.Physical.StaminaParameters.AimDrainRate = 0f;
                Main.LocalPlayer.Physical.StaminaParameters.SprintDrainRate = 0f;
                Main.LocalPlayer.Physical.StaminaParameters.JumpConsumption = 0f;
                Main.LocalPlayer.Physical.StaminaParameters.ExhaustedMeleeSpeed = 10000f;
            }
        }

        private void NoVisor()
        {
            Main.Camera.GetComponent<VisorEffect>().Intensity = MiscOptions.NoVisor ? 0f : 1f;
        }

        private void MaxSkills()
        {
            if (MiscOptions.MaxSkills)
            {
                foreach (GClass1060 skill in Main.LocalPlayer.Skills.Skills)
                {
                    if (!skill.IsEliteLevel)
                        skill.SetLevel(51);
                }
            }
        }

        private void NoSway()
        {
            if (MiscOptions.NoSway)
            {
                Main.LocalPlayer.ProceduralWeaponAnimation.Breath.Intensity = 0f;
                Main.LocalPlayer.ProceduralWeaponAnimation.WalkEffectorEnabled = false;
                Main.LocalPlayer.ProceduralWeaponAnimation.Walk.Intensity = 0f;
                Main.LocalPlayer.ProceduralWeaponAnimation.MotionReact.Intensity = 0f;
                Main.LocalPlayer.ProceduralWeaponAnimation.ForceReact.Intensity = 0f;
            }
            else
            {
                Main.LocalPlayer.ProceduralWeaponAnimation.Breath.Intensity = 1f;
                Main.LocalPlayer.ProceduralWeaponAnimation.WalkEffectorEnabled = true;
                Main.LocalPlayer.ProceduralWeaponAnimation.Walk.Intensity = 1f;
                Main.LocalPlayer.ProceduralWeaponAnimation.MotionReact.Intensity = 1f;
                Main.LocalPlayer.ProceduralWeaponAnimation.ForceReact.Intensity = 1f;
            }
        }

        private void NoRecoil()
        {
            if (MiscOptions.NoRecoil)
            {
                Main.LocalPlayer.ProceduralWeaponAnimation.Shootingg.RecoilStrengthXy = Vector2.zero;
                Main.LocalPlayer.ProceduralWeaponAnimation.Shootingg.RecoilStrengthZ = Vector2.zero;
                Main.LocalPlayer.ProceduralWeaponAnimation.HandsContainer.HandsRotation.Current.x = 0f;
                Main.LocalPlayer.ProceduralWeaponAnimation.HandsContainer.HandsRotation.Current.y = 0f;
                Main.LocalPlayer.ProceduralWeaponAnimation.HandsContainer.HandsRotation.Current.z = 0f;
                Main.LocalPlayer.ProceduralWeaponAnimation.HandsContainer.HandsPosition.Current.x = 0f;
                Main.LocalPlayer.ProceduralWeaponAnimation.HandsContainer.HandsPosition.Current.y = 0f;
                Main.LocalPlayer.ProceduralWeaponAnimation.HandsContainer.HandsPosition.Current.z = 0f;
                Main.LocalPlayer.ProceduralWeaponAnimation.Breath.HipPenalty = 0f;
                Main.LocalPlayer.ProceduralWeaponAnimation.MotionReact.Velocity.x = 0f;
                Main.LocalPlayer.ProceduralWeaponAnimation.MotionReact.Velocity.y = 0f;
                Main.LocalPlayer.ProceduralWeaponAnimation.MotionReact.Velocity.z = 0f;
            }
        }

        private void DoNightVison()
        {
            if (Input.GetKeyDown(MiscOptions.NightVisonKey))
                MiscOptions.NightVison = !MiscOptions.NightVison;

            Main.Camera.GetComponent<NightVision>().SetPrivateField("_on", MiscOptions.NightVison);
        }

        private void DoThermalVision()
        {
            if (Input.GetKeyDown(MiscOptions.ThermalVisonKey))
                MiscOptions.ThermalVison = !MiscOptions.ThermalVison;

            Main.Camera.GetComponent<ThermalVision>().On = MiscOptions.ThermalVison;
        }

        private void OnGUI()
        {
            if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive)
            {
                try
                {
                    Render.DrawTextOutline(new Vector2(20, 0), _test, Color.black, Color.white);
                    DrawCrossHair();
                    DrawFov();
                    DrawHud();
                }
                catch { }

            }
        }

        private void DrawHud()
        {
            if (MiscOptions.DrawHud)
            {
                Render.DrawString1(new Vector2(512, Screen.height - 56), _hud, Color.white, false, 20);
            }
        }


        private void DrawFov()
        {
            if (AimbotOptions.DrawAimbotFov)
            {
                Render.DrawCircle(new Vector2(Screen.width / 2, Screen.height / 2), AimbotOptions.AimbotFov, Color.white, 0.5f, true, 40);
            }
            if (AimbotOptions.DrawSilentFov)
            {
                Render.DrawCircle(new Vector2(Screen.width / 2, Screen.height / 2), AimbotOptions.SilentAimFov, Color.white, 0.5f, true, 40);
            }
        }

        private static void DrawCrossHair()
        {
            if (MiscVisualsOptions.DrawCrossHair)
            {
                Render.DrawLine(new Vector2(Screen.width / 2, Screen.height / 2 - 9), new Vector2(Screen.width / 2, Screen.height / 2 + 9), MiscVisualsOptions.CrossHairColor, 0.5f,
                    true);
                Render.DrawLine(new Vector2(Screen.width / 2 - 9, Screen.height / 2), new Vector2(Screen.width / 2 + 9, Screen.height / 2), MiscVisualsOptions.CrossHairColor, 0.5f,
                    true);
            }
            //else
            //{
            //    Vector3 crosshair = RayCast.BarrelRayCast(Main.LocalPlayer);
            //    Vector3 screenPos = Main.Camera.WorldToScreenPoint(crosshair);
            //    Render.DrawTextOutline(screenPos, "X", Color.black, Color.red );
            //}
        }
    }
}
