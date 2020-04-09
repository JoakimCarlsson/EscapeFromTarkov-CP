using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EFT;
using EFT.Interactive;
using EFT.UI;
using JsonType;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features.ESP
{
    class ItemEsp : MonoBehaviour
    {
        private void OnGUI()
        {
            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && ItemOptions.DrawItems)
                {
                    Color itemColor = Color.clear;

                    foreach (var lootItem in Main.LootItems)
                    {
                        float distance = Vector3.Distance(Main.LocalPlayer.Transform.position, lootItem.transform.position);
                        
                        if (distance > ItemOptions.DrawItemRange)
                            continue;
                        Vector3 screenPosition = GameUtils.WorldPointToScreenPoint(lootItem.transform.position);

                        if (!GameUtils.IsScreenPointVisible(screenPosition))
                            continue;

                        if (lootItem.Item.Template.Rarity == ELootRarity.Common)
                            itemColor = ItemOptions.CommonColor;

                        if (lootItem.Item.Template.Rarity == ELootRarity.Rare)
                            itemColor = ItemOptions.RareColor;

                        if (lootItem.Item.Template.Rarity == ELootRarity.Superrare)
                            itemColor = ItemOptions.SuperRareColor;

                        if (lootItem.Item.QuestItem)
                            itemColor = ItemOptions.QuestItemsColor;

                        if (GameUtils.IsSpecialLootItem(lootItem.TemplateId))
                            itemColor = ItemOptions.SpecialColor;

                        if (GameUtils.IsMedItem(lootItem.TemplateId))
                            itemColor = ItemOptions.MedColor;

                        string text = $"{lootItem.Name.Localized()} [{(int)distance} M]";
                        Render.DrawTextOutline(screenPosition, text, Color.black, itemColor, 12);
                    }
                }
            }
            catch { }
        }



        //int y = 20;
        //foreach (Player player in Main.GameWorld.RegisteredPlayers)
        //{
        //    foreach (var item in player.Profile.Inventory.AllPlayerItems.ToList())
        //    {
        //        Render.DrawTextOutline(new Vector2(500, y), item.Name.Localized(), Color.black, Color.white, 25);

        //        y += 20;
        //    }

        //}
    }
}