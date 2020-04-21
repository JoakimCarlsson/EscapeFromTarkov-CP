using System;
using System.Linq;
using EFT.UI;
using SivaEftCheat.Data;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features.ESP
{
    class LootableContainerEsp : MonoBehaviour
    {
        private void FixedUpdate()
        {
            try
            {
                foreach (GameLootContainer gameLootContainer in Main.LootableContainers)
                    gameLootContainer.RecalculateDynamics();
            }
            catch { }
        }

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
                        if (!GameUtils.IsLootableContainerValid(lootableContainer.LootableContainer) || !lootableContainer.IsOnScreen || lootableContainer.Distance > MiscVisualsOptions.DrawLootableContainersRange)
                            continue;

                        var item = lootableContainer.LootableContainer.ItemOwner.RootItem;

                        if (item.GetAllItems().Count() == 1)
                            continue;


                        string lootItemName = item.Name.Localized();

                        foreach (var allItem in item.GetAllItems())
                        {
                            if (item.GetAllItems().First() == allItem)
                            {
                                lootItemName = $"{allItem.Name.Localized()} {lootableContainer.FormattedDistance}";
                                MiscVisualsOptions.LootableContainerColor = new Color(1f, 0.2f, 0.09f);
                            }
                            else
                            {
                                if (!GameUtils.IsSpecialLootItem(allItem.TemplateId))
                                    continue;

                                lootItemName = allItem.Name.Localized();
                                MiscVisualsOptions.LootableContainerColor = Color.white;
                            }

                            Render.DrawString(new Vector2(lootableContainer.ScreenPosition.x, lootableContainer.ScreenPosition.y - x), lootItemName, MiscVisualsOptions.LootableContainerColor);
                            x -= 20;
                        }
                    }
                }
            }
            catch { }
        }
    }
}
