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
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && MiscVisualsOptions.DrawItems)
                {
                    Render.DrawTextOutline(new Vector2(20, 40), $"Loot Items Amount: {Main.LootItems.Count}", Color.black, Color.white);

                    foreach (var lootItem in Main.LootItems)
                    {
                        float distance = Vector3.Distance(Main.LocalPlayer.Transform.position, lootItem.transform.position);
                        
                        if (distance > MiscVisualsOptions.DrawItemRange)
                            continue;
                        Vector3 screenPosition = GameUtils.WorldPointToScreenPoint(lootItem.transform.position);

                        if (!GameUtils.IsScreenPointVisible(screenPosition))
                            continue;

                        Color itemColor = default;
                        switch (lootItem.Item.Template.Rarity)
                        {
                            case ELootRarity.Common:
                                itemColor = MiscVisualsOptions.CommonColor;
                                break;
                            case ELootRarity.Rare:
                                itemColor = MiscVisualsOptions.RareColor;
                                break;
                            case ELootRarity.Superrare:
                                itemColor = MiscVisualsOptions.SuperRareColor;
                                break;
                        }

                        if (lootItem.Item.QuestItem)
                            itemColor = MiscVisualsOptions.QuestItemsColor;

                        if (GameUtils.IsSpecialLootItem(lootItem.TemplateId))
                            itemColor = MiscVisualsOptions.SpecialColor;

                        if (GameUtils.IsMedItem(lootItem.TemplateId))
                            itemColor = MiscVisualsOptions.MedColor;

                        string text = $"{lootItem.Name.Localized()} [{(int)distance} M]";
                        Render.DrawTextOutline(screenPosition, text, Color.black, itemColor);
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