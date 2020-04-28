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
            if (AimbotOptions.SilentAim)
            {
                GamePlayer target = Aimbot.Target;
                if (target != null)
                {
                    Vector3 headPosition = GameUtils.FinalVector(target.Player.PlayerBody.SkeletonRootJoint, AimbotOptions.AimbotBone);
                    if (Main.LocalPlayer.Weapon != null)
                    {
                        direction = (headPosition - origin).normalized;
                        speedFactor = 100f;
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

        public static float BulletPenetration(object ammo, int randomInt, object randoms)
        {
            return 500000f;
        }
    }
}
