using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT;
using EFT.Interactive;
using SivaEftCheat.Options;
using UnityEngine;

namespace SivaEftCheat.Utils
{
    class GameUtils
    {
        public static Vector3 FinalVector(Diz.Skinning.Skeleton skeletor, int BoneId)
        {
            try
            {
                return skeletor.Bones.ElementAt(BoneId).Value.position;
            }
            catch { return Vector3.zero; }
        }

        public static bool IsSpecialLootItem(string templateId)
        {
            return MiscVisualsOptions.RareItems.Contains(templateId);
        }
        public static bool IsLootableContainerValid(LootableContainer lootableContainer)
        {
            return lootableContainer != null && lootableContainer.Template != null;
        }
        public static bool IsMedItem(string templateId)
        {
            return MiscVisualsOptions.MedsItems.Contains(templateId);
        }
        public static float GetColorAlpha(float distance, float distanceMax)
        {
            float colorAlpha = 1f;

            if (distance > 0)
            {
                float alphaCalc = (distance / distanceMax) * 0.5f;

                colorAlpha = colorAlpha - alphaCalc;
            }

            return colorAlpha;
        }

        public static bool IsPlayerAlive(Player player)
        {
            if (!IsPlayerValid(player))
                return false;

            if (player.HealthController == null)
                return false;

            return player.HealthController.IsAlive;
        }
        public static bool IsLootItemValid(LootItem lootItem)
        {
            return lootItem != null && lootItem.Item != null && lootItem.Item.Template != null;
        }
        public static bool IsPlayerValid(Player player)
        {
            return player != null && player.Transform != null && player.PlayerBones != null && player.PlayerBones.transform != null;
        }

        public static Vector3 WorldPointToScreenPoint(Vector3 worldPoint)
        {
            Vector3 screenPoint = Main.Camera.WorldToScreenPoint(worldPoint);

            screenPoint.y = Screen.height - screenPoint.y;

            return screenPoint;
        }
        public static float InPoint(Vector3 c1, Vector3 c2)
        {
            return (c1 - c2).sqrMagnitude;
        }
        public static bool IsScreenPointVisible(Vector3 screenPoint)
        {
            return screenPoint.z > 0.01f && screenPoint.x > -5f && screenPoint.y > -5f && screenPoint.x < Screen.width && screenPoint.y < Screen.height;
        }

        public static bool IsFriend(Player player)
        {
            return Main.LocalPlayer.Profile.Info.GroupId == player.Profile.Info.GroupId && player.Profile.Info.GroupId != "0" && player.Profile.Info.GroupId != "" && player.Profile.Info.GroupId != null;
        }

        public static void DrawHealth(Vector2 pos, float width, float height, float health, float maxHealth)
        {
            try
            {
                Render.BoxRect(new Rect(pos.x, pos.y, width, height), Color.black);
                pos.x += 1f;
                pos.y += 1f;
                Color color = Color.green;
                if (health <= 50f)
                {
                    color = Color.yellow;
                }
                if (health <= 25f)
                {
                    color = Color.red;
                }
                Render.BoxRect(new Rect(pos.x, pos.y, width * (health / maxHealth * 100f) / 100f - 2f, height - 2f), color);
            }
            catch
            {
            }
        }
    }
}
