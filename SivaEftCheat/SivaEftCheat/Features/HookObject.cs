using System;
using System.Collections.Generic;
using System.Linq;
using EFT;
using EFT.InventoryLogic;
using SivaEftCheat.Data;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features
{
    public class HookObject
    {
        public object SilentAimHook(object ammo, Vector3 origin, Vector3 direction, int fireIndex, Player player, Item weapon, float speedFactor = 1f, int fragmentIndex = 0)
        {
            try
            {
                if (AimbotOptions.SilentAim)
                {
                    GamePlayer target = Aimbot.Target;
                    if (target != null)
                    {
                        Vector3 test = target.Player.PlayerBones.Neck.position + new Vector3(0f, 0.07246377f, 0f);
                        if (Main.LocalPlayer.Weapon != null)
                        {
                            direction = (test - origin).normalized;
                        }
                    }
                }

                Aimbot.CreateShotHook.Unhook();
                object[] parameters =
                {
                    ammo,
                    origin,
                    direction,
                    fireIndex,
                    player,
                    weapon,
                    speedFactor,
                    fragmentIndex
                };
                object result = Aimbot.CreateShotHook.OriginalMethod.Invoke(this, parameters);
                Aimbot.CreateShotHook.Hook();
                return result;
            }
            catch
            {
                return null;
            }

        }

        public static float BulletPenetration(object ammo, int randomInt, object randoms)
        {
            return 500000f;
        }
    }
}
