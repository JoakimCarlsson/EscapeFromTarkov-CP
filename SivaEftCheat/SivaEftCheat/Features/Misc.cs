﻿using System.Collections.Generic;
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
                }
                catch { }
            }
        }

        private void FlyHack()
        {
            
        }

        private void UnlockDoors()
        {
            if (MiscOptions.DoorUnlocker)
            {
                if (Input.GetKeyDown(MiscOptions.DoorUnlockerKey))
                {
                    foreach (var door in FindObjectsOfType<WorldInteractiveObject>())
                    {
                        if (door.DoorState == EDoorState.Open ||
                            Vector3.Distance(door.transform.position, Main.LocalPlayer.Position) > 10f)
                            continue;

                        door.DoorState = EDoorState.Shut;
                    }
                }
            }

        }

        private void SpeedHack()
        {
            if (Input.GetKey(KeyCode.W))
                Main.LocalPlayer.Transform.position += Main.LocalPlayer.Transform.forward / 5f * MiscOptions.SpeedHackValue;
            if (Input.GetKey(KeyCode.S))
                Main.LocalPlayer.Transform.position -= Main.LocalPlayer.Transform.forward / 5f * MiscOptions.SpeedHackValue;
            if (Input.GetKey(KeyCode.A))
                Main.LocalPlayer.Transform.position -= Main.LocalPlayer.Transform.right / 5f * MiscOptions.SpeedHackValue;
            if (Input.GetKey(KeyCode.D))
                Main.LocalPlayer.Transform.position += Main.LocalPlayer.Transform.right / 5f * MiscOptions.SpeedHackValue;
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
            Main.LocalPlayer.ProceduralWeaponAnimation.Shootingg.Intensity = MiscOptions.NoRecoil ? 0f : 1f;
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
                    DrawCrossHair();
                    DrawFov();
                }
                catch { }

            }
        }



        private void DrawFov()
        {
            if (AimbotOptions.DrawAimbotFov)
            {
                Render.DrawCircle(new Vector2(Screen.width / 2, Screen.height / 2), AimbotOptions.AimbotFov, Color.white, 0.5f, true, 40 );
            }
            if (AimbotOptions.DrawSilentFov)
            {
                Render.DrawCircle(new Vector2(Screen.width / 2, Screen.height / 2), AimbotOptions.SilentAimFov, Color.white, 0.5f, true, 40 );
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
        }
    }
}
