using Citadel.Data;
using Citadel.Options;
using Citadel.Utils;
using EFT;
using EFT.InventoryLogic;
using UnityEngine;

namespace Citadel.Features
{
    public class HookObject
    {
        public object SilentAimHook(object ammo, Vector3 origin, Vector3 direction, int fireIndex, Player player, Item weapon, float speedFactor = 1f, int fragmentIndex = 0)
        {
            if (Main.LocalPlayer.HandsController.Item is Weapon)
            {
                if (AimbotOptions.SilentAim)
                {
                    GamePlayer target = Aimbot.Target;
                    if (target != null)
                    {
                        Vector3 aimPosition = GameUtils.FinalVector(target.Player, AimbotOptions.AimbotBone) + new Vector3(0f, 0.07246377f, 0f);
                        if (Main.LocalPlayer.Weapon != null)
                        {
                            direction = (aimPosition - origin).normalized;
                            speedFactor = 100f;
                        }
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
