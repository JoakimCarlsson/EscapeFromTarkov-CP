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
                        List<GInterface7>.Enumerator enumerator = GameWorld.LootList.FindAll(item => item is LootItem || item is LootableContainer || item is Corpse).GetEnumerator();
                        LootItems.Clear();
                        LootableContainers.Clear();
                        Corpses.Clear();
                        while (enumerator.MoveNext())
                        {
                            GInterface7 ginterface = enumerator.Current;

                            if (ginterface is LootItem lootItem)
                            {
                                if (lootItem.gameObject != null && ItemOptions.DrawItems)
                                {
                                    if (ItemOptions.DrawQuestItems)
                                        if (lootItem.Item.QuestItem)
                                            LootItems.Add(lootItem);

                                    if (ItemOptions.DrawMedtems)
                                        if (GameUtils.IsMedItem(lootItem.TemplateId))
                                            LootItems.Add(lootItem);

                                    if (ItemOptions.DrawSpecialItems)
                                        if (GameUtils.IsSpecialLootItem(lootItem.TemplateId))
                                            LootItems.Add(lootItem);

                                    if (ItemOptions.DrawCommonItems)
                                        if (lootItem.Item.Template.Rarity == ELootRarity.Common)
                                             LootItems.Add(lootItem);

                                    if (ItemOptions.DrawRareItems)
                                        if (lootItem.Item.Template.Rarity == ELootRarity.Rare)
                                            LootItems.Add(lootItem);

                                    if (ItemOptions.DrawSuperRareItems)
                                        if (lootItem.Item.Template.Rarity == ELootRarity.Superrare)
                                            LootItems.Add(lootItem);
                                }
                            }
                            if (ginterface is LootableContainer lootableContainer)
                            {
                                if (lootableContainer.gameObject != null)
                                {
                                    LootableContainers.Add(lootableContainer);
                                }
                            }

                            if (ginterface is Corpse corpse)
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
