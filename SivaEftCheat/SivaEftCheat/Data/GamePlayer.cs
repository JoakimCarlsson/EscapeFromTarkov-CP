using System;
using System.Collections.Generic;
using EFT;
using EFT.InventoryLogic;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Data
{
    public class GamePlayer
    {
        public Player Player { get; }

        public Vector3 ScreenPosition => _screenPosition;

        public Vector3 HeadScreenPosition => _headScreenPosition;

        public bool IsOnScreen { get; private set; }
        public float Distance { get; private set; }
        public float DistanceFromCenter { get; set; }
        public static bool IsVisible { get; set; }
        public bool IsAI { get; private set; }
        public int Value { get; set; }
        public bool TeamMate { get; set; }
        public Color PlayerColor => _playerColor;
        public bool  HasSpecialItem { get; set; }

        private static string Group = string.Empty;

        public string FormattedDistance => $"{(int)Math.Round(Distance)}m";
        public string FormattedValue => $"{Value}K";

        private Color _playerColor;
        private Vector3 _screenPosition;
        private Vector3 _headScreenPosition;
        private static Item _tempItem;
        private static IEnumerator<Item> _equipItemList;
        public GamePlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            Player = player;
            _screenPosition = default;
            _headScreenPosition = default;
            IsOnScreen = false;
            Distance = 0f;
            Value = 0;
            IsAI = true;
            IsVisible = false;
            TeamMate = false;
            DistanceFromCenter = 0f;
            HasSpecialItem = false;
            _playerColor = Color.white;
        }

        public void RecalculateDynamics()
        {
            if (!GameUtils.IsPlayerValid(Player))
                return;

            _screenPosition = GameUtils.WorldPointToScreenPoint(Player.Transform.position);

            if (Player.PlayerBones != null)
                _headScreenPosition = GameUtils.WorldPointToScreenPoint(Player.PlayerBones.Head.position);

            if ((Player.Profile != null) && (Player.Profile.Info != null))
                IsAI = (Player.Profile.Info.RegistrationDate <= 0);

            IsOnScreen = GameUtils.IsScreenPointVisible(_screenPosition);
            Distance = Vector3.Distance(Main.Camera.transform.position, Player.Transform.position);
            IsVisible = RayCast.IsVisible(Player);
            TeamMate = IsInYourGroup(Player);
            Value = CalculateValue(Player);
            DistanceFromCenter = Vector2.Distance(Main.Camera.WorldToScreenPoint(Player.PlayerBones.Head.position), GameUtils.ScreenCenter);
            _playerColor = GetPlayerColor(Player);
        }

        public static Color GetPlayerColor(Player player)
        {
            if (IsVisible)
            {
                return Color.green;
            }
            if (GameUtils.IsFriend(player))
            {
                return PlayerOptions.FriendColor;
            }
            if (player.Profile.Info.Settings.IsBoss())
            {
                return PlayerOptions.BossColor;
            }
            if (player.IsAI)
            {
                return PlayerOptions.ScavColor;
            }

            if (player.Profile.Info.Side == EPlayerSide.Savage)
            {
                return PlayerOptions.PlayerScavColor;
            }

            return PlayerOptions.PlayerColor;
        }

        public bool IsInYourGroup(Player player)
        {
            return Group == player.Profile.Info.GroupId && Group != "0" && Group != "" && Group != null;
        }


        public int CalculateValue(Player player)
        {
            int value = 0;
            _equipItemList = player.Profile.Inventory.Equipment.GetAllItems().GetEnumerator();
            while (_equipItemList.MoveNext())
            {
                _tempItem = _equipItemList.Current;
                value += _tempItem.Template.CreditsPrice;
                if (_tempItem.Template._parent == "5448bf274bdc2dfc2f8b456a")
                {
                    var x = _tempItem.GetAllItems().GetEnumerator();
                    while (x.MoveNext())
                    {
                        if (GameUtils.IsSpecialLootItem(x.Current.TemplateId))
                            HasSpecialItem = true;

                        value -= x.Current.Template.CreditsPrice;
                    }
                }
            }
            return (value / 1000);
        }

        //public bool HasSpecialItems(Player player)
        //{
        //    //_equipItemList = player.Profile.Inventory.Equipment.GetAllItems();
        //    //foreach (Item item in _equipItemList)
        //    //{
        //    //    if (GameUtils.IsSpecialLootItem(item.TemplateId))
        //    //    {
        //    //        return true;
        //    //    }
        //    //}

        //    //return false;

        //    //_equipItemList = player.Profile.Inventory.Equipment.GetAllItems().GetEnumerator();
        //    //while (_equipItemList.MoveNext())
        //    //{
        //    //    _tempItem = _equipItemList.Current;
        //    //    if (_tempItem.Template._parent == "5448bf274bdc2dfc2f8b456a")
        //    //    {
        //    //        var x = _tempItem.GetAllItems().GetEnumerator();
        //    //        while (x.MoveNext())
        //    //        {
        //    //            if (x.Current != null && GameUtils.IsSpecialLootItem(x.Current.TemplateId))
        //    //            {
        //    //                return true;
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //    //return false;
        //}
    }
}
