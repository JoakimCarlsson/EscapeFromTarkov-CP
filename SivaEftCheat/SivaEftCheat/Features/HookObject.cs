using System.Collections.Generic;
using System.Linq;
using EFT;
using EFT.InventoryLogic;
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
                Dictionary<Player, int> dictionary = new Dictionary<Player, int>();
                Vector2 vector = new Vector2(Screen.width / 2f, Screen.height / 2f);
                foreach (var target in Main.GameWorld.RegisteredPlayers)
                {
                    int num = (int)Vector2.Distance(Main.Camera.WorldToScreenPoint(target.PlayerBones.Head.position), vector);
                    int num2 = (int)Vector3.Distance(Main.LocalPlayer.Transform.position, target.Transform.position);
                    Vector3 vector2 = target.Transform.position - Main.Camera.transform.position;
                    if (num2 <= AimbotOptions.AimbotDistnace && num <= AimbotOptions.SilentAimFov && Vector3.Dot(Main.Camera.transform.TransformDirection(Vector3.forward), vector2) > 0f)
                    {
                        dictionary.Add(target, num);
                    }
                }
                if (dictionary.Count > 0.01)
                {
                    dictionary = (from pair in dictionary orderby pair.Value select pair).ToDictionary(pair => pair.Key, pair => pair.Value);
                    Player player2 = dictionary.Keys.First();
                    Main.Camera.WorldToScreenPoint(player2.Transform.position);
                    Vector3 vector3 = Main.Camera.WorldToScreenPoint(player2.Transform.position);
                    Weapon weapon2 = Main.LocalPlayer.Weapon;
                    Vector3 test = player2.PlayerBones.Neck.position + new Vector3(0f, 0.07246377f, 0f);
                    if (vector3.z > 0.01 && weapon2 != null)
                    {
                        direction = (test - origin).normalized;
                    }
                }
            }
            Aimbot.createShotHook.Unhook();
            object[] parameters = {
                ammo,
                origin,
                direction,
                fireIndex,
                player,
                weapon,
                speedFactor,
                fragmentIndex
            };
            object result = Aimbot.createShotHook.OriginalMethod.Invoke(this, parameters);
            Aimbot.createShotHook.Hook();
            return result;
        }
    }
}
