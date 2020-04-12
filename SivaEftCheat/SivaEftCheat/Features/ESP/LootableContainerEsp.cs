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
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive &&
                    MiscVisualsOptions.DrawLootableContainers)
                {
                    Render.DrawTextOutline(new Vector2(20, 60),
                        $"Lootable Containres Amount: {Main.LootableContainers.Count}", Color.black, Color.white);

                    int x = -20;

                    foreach (var lootableContainer in Main.LootableContainers)
                    {
                        float distance = Vector3.Distance(Main.LocalPlayer.Transform.position,
                            lootableContainer.transform.position);
                        Vector3 screenPosition =
                            GameUtils.WorldPointToScreenPoint(lootableContainer.transform.position);

                        if (!GameUtils.IsScreenPointVisible(screenPosition))
                            continue;

                        if (distance > MiscVisualsOptions.DrawLootableContainersRange)
                            continue;

                        var item = lootableContainer.ItemOwner.RootItem;

                        if (item.GetAllItems().Count() == 1)
                            continue;


                        string lootItemName = item.Name.Localized();

                        foreach (var allItem in item.GetAllItems())
                        {
                            if (item.GetAllItems().First() == allItem)
                            {
                                lootItemName = $"{allItem.Name.Localized()} [{(int) distance} M]";
                                MiscVisualsOptions.LootableContainerColor = new Color(1f, 0.2f, 0.09f);
                            }
                            else
                            {
                                if (!GameUtils.IsSpecialLootItem(allItem.TemplateId))
                                    continue;

                                lootItemName = allItem.Name.Localized();
                                MiscVisualsOptions.LootableContainerColor = Color.white;
                            }

                            Render.DrawTextOutline(new Vector2(screenPosition.x, screenPosition.y - x), lootItemName,
                                Color.black, MiscVisualsOptions.LootableContainerColor);
                            x -= 20;
                        }
                    }
                }
            }
            catch { }
        }
    }
}
