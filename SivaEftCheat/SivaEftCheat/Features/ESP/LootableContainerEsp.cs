using System.Linq;
using EFT.UI;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features.ESP
{
    class LootableContainerEsp : MonoBehaviour
    {
        private void OnGUI()
        {
            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && MiscVisualsOptions.DrawLootableContainers && Main.Camera != null)
                {
                    Render.DrawString(new Vector2(20, 60), $"Lootable Containres Amount: {Main.LootableContainers.Count}", Color.white, false);

                    int x = -20;
                    foreach (var lootableContainer in Main.LootableContainers)
                    {
                        if (!lootableContainer.IsOnScreen || lootableContainer.Distance > MiscVisualsOptions.DrawLootableContainersRange)
                            continue;

                        var rootItem = lootableContainer.LootableContainer.ItemOwner.RootItem;

                        if (rootItem.GetAllItems().Count() == 1)
                            continue;

                        foreach (var item in rootItem.GetAllItems())
                        {
                            string itemText;
                            if (rootItem.GetAllItems().First() == item)
                            {
                                itemText = $"{item.Name.Localized()} [{lootableContainer.FormattedDistance}]";
                                MiscVisualsOptions.LootableContainerColor = new Color(1f, 0.2f, 0.09f);
                            }
                            else
                            {
                                if (!GameUtils.IsSpecialLootItem(item.TemplateId))
                                    continue;

                                if (MiscVisualsOptions.DrawItemsPrice)
                                    itemText = $"{item.Name.Localized()} $ {item.Template.CreditsPrice / 1000} K";
                                else
                                    itemText = item.Name.Localized();

                                MiscVisualsOptions.LootableContainerColor = Color.white;
                            }

                            Render.DrawString(new Vector2(lootableContainer.ScreenPosition.x, lootableContainer.ScreenPosition.y - x), itemText, MiscVisualsOptions.LootableContainerColor);
                            x -= 20;
                        }
                    }
                }
            }
            catch { }
        }
    }
}
