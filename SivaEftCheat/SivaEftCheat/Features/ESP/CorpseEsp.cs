using System.Linq;
using EFT.Interactive;
using EFT.InventoryLogic;
using EFT.UI;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features.ESP
{
    class CorpseEsp : MonoBehaviour
    {
        public void OnGUI()
        {
            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && PlayerOptions.DrawCorpses && Main.Camera != null)
                {
                    Render.DrawString(new Vector2(20, 80), $"Corpses: {Main.Corpses.Count}", Color.white, false);

                    int x = -20;
                    foreach (var corpse in Main.Corpses)
                    {
                        if (!corpse.IsOnScreen || corpse.Distance > MiscVisualsOptions.DrawLootableContainersRange)
                            continue;

                        var item = corpse.Corpse.ItemOwner.RootItem;

                        if (item.GetAllItems().Count() == 1)
                            continue;

                        foreach (var allItem in item.GetAllItems())
                        {
                            string itemName;
                            if (item.GetAllItems().First() == allItem)
                            {
                                itemName = $"Dead [{corpse.FormattedDistance}]";
                                MiscVisualsOptions.LootableContainerColor = new Color(1f, 0.2f, 0.09f);
                            }
                            else
                            {
                                if (!GameUtils.IsSpecialLootItem(allItem.TemplateId))
                                    continue;

                                itemName = allItem.Name.Localized();
                                MiscVisualsOptions.LootableContainerColor = Color.white;
                            }

                            Render.DrawString(new Vector2(corpse.ScreenPosition.x, corpse.ScreenPosition.y - x), itemName, MiscVisualsOptions.LootableContainerColor);
                            x -= 20;
                        }
                    }
                }


            }
            catch
            {
            }
        }
    }
}
