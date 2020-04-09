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
            if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && ItemOptions.DrawLootableContainers)
            {
                int x = -20;

                foreach (var lootableContainer in Main.LootableContainers)
                {
                    float distance = Vector3.Distance(Main.LocalPlayer.Transform.position, lootableContainer.transform.position);
                    Vector3 screenPosition = GameUtils.WorldPointToScreenPoint(lootableContainer.transform.position);

                    if (!GameUtils.IsScreenPointVisible(screenPosition))
                        continue;

                    if (distance > ItemOptions.DrawLootableContainersRange)
                        continue;

                    var item = lootableContainer.ItemOwner.RootItem;

                    if (item.GetAllItems().Count() == 1)
                        continue;


                    string lootItemName = item.Name.Localized();

                    foreach (var allItem in item.GetAllItems())
                    {
                        if (item.GetAllItems().First() == allItem)
                        {
                            lootItemName = $"{allItem.Name.Localized()} [{(int)distance} M]";
                            ItemOptions.LootableContainerColor = new Color(1f, 0.2f, 0.09f);
                        }
                        else
                        {
                            if (!GameUtils.IsSpecialLootItem(allItem.TemplateId))
                                continue;

                            lootItemName = allItem.Name.Localized();
                            ItemOptions.LootableContainerColor = Color.white;
                        }
                        Render.DrawTextOutline(new Vector2(screenPosition.x, screenPosition.y - x), lootItemName, Color.black, ItemOptions.LootableContainerColor, 12);
                        x -= 20;
                    }
                }
            }
        }
    }
}
