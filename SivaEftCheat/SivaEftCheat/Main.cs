using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using EFT.InventoryLogic;
using EFT.UI;
using JsonType;
using SivaEftCheat.Features;
using SivaEftCheat.Features.ESP;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat
{
    class Main : MonoBehaviour
    {
        public static Player LocalPlayer { get; set; }
        public static Camera Camera { get; set; }
        public static GameWorld GameWorld { get; set; }

        internal static List<LootItem> LootItems = new List<LootItem>();
        internal static List<LootableContainer> LootableContainers = new List<LootableContainer>();
        internal static List<Corpse> Corpses = new List<Corpse>();

        private IEnumerator _coroutineUpdateMain;
        private IEnumerator _coroutineGetLists;
        private static GameObject HookObject;

        //    if (Input.GetKey(KeyCode.UpArrow))
        //{
        //    Main.LocalPlayer.MovementContext.FreefallTime = 0;
        //    Main.LocalPlayer.Transform.position += Main.Camera.transform.forward* 0.2f;
        //}

    private void Start()
        {
            _coroutineUpdateMain = UpdateMain(10f);
            StartCoroutine(_coroutineUpdateMain);
            _coroutineGetLists = GetLists(1f);
            StartCoroutine(_coroutineGetLists);

            HookObject = new GameObject();
            HookObject.AddComponent<Menu>();
            HookObject.AddComponent<ExtractEsp>();
            HookObject.AddComponent<ItemEsp>();
            HookObject.AddComponent<LootableContainerEsp>();
            HookObject.AddComponent<PlayerEsp>();
            HookObject.AddComponent<Misc>();

            DontDestroyOnLoad(HookObject);
        }

        private IEnumerator GetLists(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                try
                {
                    if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive)
                    {
                        List<GInterface7>.Enumerator enumerator = GameWorld.LootList.FindAll(item => item is LootItem || item is LootableContainer).GetEnumerator();
                        LootItems.Clear();
                        LootableContainers.Clear();
                        Corpses.Clear();

                        while (enumerator.MoveNext())
                        {
                            GInterface7 current = enumerator.Current;

                            switch (current)
                            {
                                case LootItem lootItem:
                                {
                                    if (lootItem.gameObject != null && MiscVisualsOptions.DrawItems)
                                    {
                                        if (MiscVisualsOptions.DrawQuestItems)
                                            if (lootItem.Item.QuestItem)
                                                LootItems.Add(lootItem);

                                        if (MiscVisualsOptions.DrawMedtems)
                                            if (GameUtils.IsMedItem(lootItem.TemplateId))
                                                LootItems.Add(lootItem);

                                        if (MiscVisualsOptions.DrawSpecialItems)
                                            if (GameUtils.IsSpecialLootItem(lootItem.TemplateId))
                                                LootItems.Add(lootItem);

                                        if (MiscVisualsOptions.DrawCommonItems)
                                            if (lootItem.Item.Template.Rarity == ELootRarity.Common)
                                                LootItems.Add(lootItem);

                                        if (MiscVisualsOptions.DrawRareItems)
                                            if (lootItem.Item.Template.Rarity == ELootRarity.Rare)
                                                LootItems.Add(lootItem);

                                        if (MiscVisualsOptions.DrawSuperRareItems)
                                            if (lootItem.Item.Template.Rarity == ELootRarity.Superrare)
                                                LootItems.Add(lootItem);
                                    }

                                    break;
                                }
                                case LootableContainer lootableContainer:
                                {
                                    if (lootableContainer.gameObject != null)
                                    {
                                        if (lootableContainer.ItemOwner.RootItem.GetAllItems().Any(item => GameUtils.IsSpecialLootItem(item.TemplateId)))
                                        {
                                            LootableContainers.Add(lootableContainer);
                                        }
                                    }

                                    break;
                                }
                            }

                            if (current is Corpse corpse)
                            {
                                if (corpse.gameObject != null)
                                {
                                    Corpses.Add(corpse);
                                }
                            }
                        }
                    }
                }
                catch { }
            }
        }

        public IEnumerator UpdateMain(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                    
                try
                {
                    if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive)
                    {
                        GameWorld = Singleton<GameWorld>.Instance;
                        Camera = Camera.main;
                        LocalPlayer = GameWorld.RegisteredPlayers.Find(p => p.IsYourPlayer());
                    }
                }
                catch
                {
                }
            }
        }
    }
}
