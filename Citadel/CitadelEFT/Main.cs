using System.Collections.Generic;
using System.Linq;
using Citadel.Data;
using Citadel.Options;
using Citadel.Utils;
using Comfort.Common;
using EFT;
using EFT.Interactive;
using EFT.UI;
using JsonType;
using UnityEngine;

namespace Citadel
{
    class Main : MonoBehaviour
    {
        public static Player LocalPlayer { get; set; }
        public static Camera Camera { get; set; }
        public static GameWorld GameWorld { get; set; }
        public static int ClosePlayers { get; set; }

        internal static List<GameLootItem> LootItems = new List<GameLootItem>();
        internal static List<GameLootContainer> LootableContainers = new List<GameLootContainer>();
        internal static List<GameCorpse> Corpses = new List<GameCorpse>();
        internal static List<GamePlayer> Players = new List<GamePlayer>();

        private float _nextPlayerCacheTime;
        private static readonly float _cachePlayersInterval = 1f;

        private float _nextListCacheTime;
        private static readonly float _cacheListInterval = 1f;

        private float _nextMainCacheTime;
        private static readonly float _cacheMainInterval = 5f;

        private void LateUpdate()
        {
            try
            {
                if (Time.time >= _nextMainCacheTime)
                {
                    UpdateMain();
                    _nextMainCacheTime = Time.time + _cacheMainInterval;
                }

                if (Time.time >= _nextListCacheTime)
                {
                    GetLists();
                    _nextListCacheTime = Time.time + _cacheListInterval;
                }

                if (Time.time >= _nextPlayerCacheTime)
                {
                    GetPlayers();
                    _nextPlayerCacheTime = Time.time + _cachePlayersInterval;
                }

                foreach (GamePlayer gamePlayer in Players)
                    gamePlayer.RecalculateDynamics();

                foreach (GameLootItem gameLootItem in LootItems)
                    gameLootItem.RecalculateDynamics();

                foreach (GameLootContainer gameLootContainer in LootableContainers)
                    gameLootContainer.RecalculateDynamics();

                foreach (GameCorpse gameCorpse in Corpses)
                    gameCorpse.RecalculateDynamics();
            }
            catch { }

        }
        private void GetPlayers()
        {
            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && Camera != null)
                {
                    Players.Clear();
                    ClosePlayers = 0;
                    var enumerator = GameWorld.RegisteredPlayers.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        Player player = enumerator.Current;
                        if (player == null)
                            continue;

                        if (player.IsYourPlayer())
                        {
                            LocalPlayer = player;
                            continue;
                        }


                        //We don't have to do this that often. 
                        //This might also cause some lag.
                        if (PlayerOptions.CustomTexture)
                        {
                                Renderer[] renderers = player.GetComponentsInChildren<Renderer>();

                                Texture2D texture2D = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                                foreach (Renderer renderer in renderers)
                                {
                                    texture2D.SetPixel(1, 1,Color.white);
                                    texture2D.Apply();
                                    renderer.material.mainTexture = texture2D;
                                }
                        }


                        if (50f > Vector3.Distance(player.Transform.position, Main.LocalPlayer.Transform.position))
                            ClosePlayers++;

                        Players.Add(new GamePlayer(player));
                    }
                }
            }
            catch
            {
            }
        }
        private void GetLists()
        {
            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && Camera != null)
                {
                    var enumerator = GameWorld.LootList.FindAll(item => item is Corpse || item is LootableContainer || item is LootItem).GetEnumerator();
                    LootItems.Clear();
                    LootableContainers.Clear();
                    Corpses.Clear();

                    while (enumerator.MoveNext())
                    {
                        var current = enumerator.Current;
                        if (current is LootItem lootItem)
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
                        }
                        if (current is LootableContainer lootableContainer)
                        {
                            if (lootableContainer.gameObject != null)
                            {
                                if (lootableContainer.ItemOwner.RootItem.GetAllItems().Any(item => GameUtils.IsSpecialLootItem(item.TemplateId)))
                                {
                                    LootableContainers.Add(new GameLootContainer(lootableContainer));
                                }
                            }
                        }
                        if (current is Corpse corpse)
                        {
                            if (PlayerOptions.DrawCorpses)
                            {
                                if (corpse.gameObject != null)
                                {
                                    Corpses.Add(new GameCorpse(corpse));
                                }
                            }
                        }
                    }

                }
            }
            catch { }
        }
        private void UpdateMain()
        {

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
