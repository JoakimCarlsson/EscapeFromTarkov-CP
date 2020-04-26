using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting;
using EFT.Interactive;
using EFT.InventoryLogic;
using EFT.UI;
using SivaEftCheat.Data;
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

                        var items = corpse.Corpse.ItemOwner.RootItem;

                        if (items.GetAllItems().Count() == 1)
                            continue;

                        string itemName;
                        foreach (var item in corpse.Corpse.ItemOwner.RootItem.GetAllItems())
                        {
                            if (items.GetAllItems().First() == item)
                            {
                                itemName = $"Dead [{corpse.FormattedDistance}]";
                                MiscVisualsOptions.LootableContainerColor = new Color(1f, 0.2f, 0.09f);
                            }
                            else
                            {
                                if (MiscVisualsOptions.ContainerIds.Contains(item.Name.Localized().ToLower()))
                                    break;

                                if (!GameUtils.IsSpecialLootItem(item.TemplateId))
                                    continue;


                                if (MiscVisualsOptions.DrawItemsPrice)
                                    itemName = $"{item.Name.Localized()} $ {item.Template.CreditsPrice / 1000} K";
                                else
                                    itemName = item.Name.Localized();
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
