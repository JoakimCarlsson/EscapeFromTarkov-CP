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
                int x = -20;

                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && PlayerOptions.DrawCorpses)
                {
                    var enumerator = Main.GameWorld.LootList.FindAll(item => item is Corpse).GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        Corpse corpse = enumerator.Current as Corpse;
                        if (corpse != null && corpse.gameObject != null && corpse.isActiveAndEnabled)
                        {
                            float num = (int)Vector3.Distance(Main.LocalPlayer.Transform.position, corpse.transform.position);
                            if (num <= PlayerOptions.DrawPlayerRange)
                            {
                                Vector3 screenPosition = GameUtils.WorldPointToScreenPoint(corpse.transform.position);
                                if (GameUtils.IsScreenPointVisible(screenPosition))
                                {
                                    Item item = corpse.ItemOwner.RootItem;

                                    if (item.GetAllItems().Count() == 1)
                                        continue;

                                    string lootItemName = item.Name.Localized();

                                    foreach (var allItem in item.GetAllItems())
                                    {

                                        if (item.GetAllItems().First() == allItem)
                                        {
                                            lootItemName = $"* Dead *  [{num}]";
                                            MiscVisualsOptions.LootableContainerColor = new Color(1f, 0.2f, 0.09f);
                                        }
                                        else
                                        {
                                            if (!GameUtils.IsSpecialLootItem(allItem.TemplateId))
                                                continue;

                                            lootItemName = allItem.Name.Localized();
                                            MiscVisualsOptions.LootableContainerColor = Color.white;
                                        }
                                        Render.DrawTextOutline(new Vector2(screenPosition.x, screenPosition.y - x), lootItemName, Color.black, MiscVisualsOptions.LootableContainerColor);
                                        x -= 20;
                                    }
                                }

                            }
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
