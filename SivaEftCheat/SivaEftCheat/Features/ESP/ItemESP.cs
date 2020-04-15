using EFT.UI;
using JsonType;
using SivaEftCheat.Data;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features.ESP
{
    class ItemEsp : MonoBehaviour
    {
        private void FixedUpdate()
        {
            try
            {
                foreach (GameLootItem gameLootItem in Main.LootItems)
                    gameLootItem.RecalculateDynamics();
            }
            catch { }
        }

        private void OnGUI()
        {
            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && MiscVisualsOptions.DrawItems && Main.Camera != null)
                {
                    Render.DrawTextOutline(new Vector2(20, 40), $"Loot Items Amount: {Main.LootItems.Count}", Color.black, Color.white);

                    foreach (var lootItem in Main.LootItems)
                    {
                        if (lootItem.Distance > MiscVisualsOptions.DrawItemRange || !lootItem.IsOnScreen)
                            continue;

                        Color itemColor = default;
                        switch (lootItem.LootItem.Item.Template.Rarity)
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

                        if (lootItem.LootItem.Item.QuestItem)
                            itemColor = MiscVisualsOptions.QuestItemsColor;

                        if (GameUtils.IsSpecialLootItem(lootItem.LootItem.TemplateId))
                            itemColor = MiscVisualsOptions.SpecialColor;

                        if (GameUtils.IsMedItem(lootItem.LootItem.TemplateId))
                            itemColor = MiscVisualsOptions.MedColor;

                        string text = $"{lootItem.LootItem.Item.Name.Localized()} {lootItem.FormattedDistance}";
                        //Render.DrawTextOutline(lootItem.ScreenPosition, text, Color.black, itemColor);
                        Render.DrawString1(lootItem.ScreenPosition, text, itemColor);
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