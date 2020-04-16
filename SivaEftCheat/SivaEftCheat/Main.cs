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
using SivaEftCheat.Data;
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

        internal static List<GameLootItem> LootItems = new List<GameLootItem>();
        internal static List<GameLootContainer> LootableContainers = new List<GameLootContainer>();
        internal static List<GamePlayer> Players = new List<GamePlayer>();

        private IEnumerator _coroutineUpdateMain;
        private IEnumerator _coroutineGetLists;
        private IEnumerator _coroutineGetPlayers;

        private void Start()
        {
            _coroutineUpdateMain = UpdateMain(10f);
            StartCoroutine(_coroutineUpdateMain);

            _coroutineGetLists = GetLists(1f);
            StartCoroutine(_coroutineGetLists);

            _coroutineGetPlayers = GetPlayers(1f);
            StartCoroutine(_coroutineGetPlayers);
        }

        private IEnumerator GetPlayers(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                try
                {
                    Players.Clear();
                    var enumerator = GameWorld.RegisteredPlayers.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        Player player = enumerator.Current;
                        if (player ==null)
                            continue;
                        
                        if (player.IsYourPlayer())
                            LocalPlayer = player;

                        Players.Add(new GamePlayer(player));
                    }
                }
                catch
                {
                }
            }
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
                                                    LootItems.Add(new GameLootItem(lootItem));

                                            if (MiscVisualsOptions.DrawMedtems)
                                                if (GameUtils.IsMedItem(lootItem.TemplateId))
                                                    LootItems.Add(new GameLootItem(lootItem));

                                            if (MiscVisualsOptions.DrawSpecialItems)
                                                if (GameUtils.IsSpecialLootItem(lootItem.TemplateId))
                                                    LootItems.Add(new GameLootItem(lootItem));

                                            if (MiscVisualsOptions.DrawCommonItems)
                                                if (lootItem.Item.Template.Rarity == ELootRarity.Common)
                                                    LootItems.Add(new GameLootItem(lootItem));

                                            if (MiscVisualsOptions.DrawRareItems)
                                                if (lootItem.Item.Template.Rarity == ELootRarity.Rare)
                                                    LootItems.Add(new GameLootItem(lootItem));

                                            if (MiscVisualsOptions.DrawSuperRareItems)
                                                if (lootItem.Item.Template.Rarity == ELootRarity.Superrare)
                                                    LootItems.Add(new GameLootItem(lootItem));
                                        }

                                        break;
                                    }
                                case LootableContainer lootableContainer:
                                    {
                                        if (lootableContainer.gameObject != null)
                                        {
                                            if (lootableContainer.ItemOwner.RootItem.GetAllItems().Any(item => GameUtils.IsSpecialLootItem(item.TemplateId)))
                                            {
                                                LootableContainers.Add(new GameLootContainer(lootableContainer));
                                            }
                                        }

                                        break;
                                    }

                            }
                        }

                    }
                }
                catch { }
            }
        }

        private IEnumerator UpdateMain(float waitTime)
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
                    }
                }
                catch
                {
                }
            }
        }
    }
}
