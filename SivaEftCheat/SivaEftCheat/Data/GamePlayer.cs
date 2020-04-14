﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Data
{
    public class GamePlayer
    {
        public Player Player { get; }

        public Vector3 ScreenPosition => screenPosition;

        public Vector3 HeadScreenPosition => headScreenPosition;

        public bool IsOnScreen { get; private set; }

        public float Distance { get; private set; }
        public bool IsVisible { get; set; }
        public bool IsAI { get; private set; }
        public static int Value { get; set; }
        public bool TeamMate { get; set; }

        private static string Group = string.Empty;

        public string FormattedDistance => $"{(int)Math.Round(Distance)}m";
        public string FormattedValue = $"{Value}K";

        private Vector3 screenPosition;
        private Vector3 headScreenPosition;

        public GamePlayer(Player player)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            Player = player;
            screenPosition = default;
            headScreenPosition = default;
            IsOnScreen = false;
            Distance = 0f;
            Value = 0;
            IsAI = true;
            IsVisible = false;
            TeamMate = false;
        }

        public void RecalculateDynamics()
        {
            if (!GameUtils.IsPlayerValid(Player))
                return;

            screenPosition = GameUtils.WorldPointToScreenPoint(Player.Transform.position);

            if (Player.PlayerBones != null)
                headScreenPosition = GameUtils.WorldPointToScreenPoint(Player.PlayerBones.Head.position);

            if ((Player.Profile != null) && (Player.Profile.Info != null))
                IsAI = (Player.Profile.Info.RegistrationDate <= 0);


            IsOnScreen = GameUtils.IsScreenPointVisible(screenPosition);
            Distance = Vector3.Distance(Main.Camera.transform.position, Player.Transform.position);
            IsVisible = RayCast.IsVisible(Player);
            TeamMate = IsInYourGroup(Player);
            Value = CalculateValue(Player);
        }

        public bool IsInYourGroup(Player player)
        {
            return Group == player.Profile.Info.GroupId && Group != "0" && Group != "" && Group != null;
        }
        private static List<EFT.InventoryLogic.Item> _listToExclude;
        private static EFT.InventoryLogic.Item _tempItem;
        public static IEnumerator<EFT.InventoryLogic.Item> EquipItemList;

        public int CalculateValue(Player player)
        {
            int value = 0;
            _listToExclude = new List<EFT.InventoryLogic.Item>();
            EquipItemList = player.Profile.Inventory.Equipment.GetAllItems().GetEnumerator();
            while (EquipItemList.MoveNext())
            {
                _tempItem = EquipItemList.Current;
                value += _tempItem.Template.CreditsPrice;
                if (_tempItem.Template._parent == "5448bf274bdc2dfc2f8b456a")
                {
                    var x = _tempItem.GetAllItems().GetEnumerator();
                    while (x.MoveNext())
                    {
                        value -= x.Current.Template.CreditsPrice;
                    }
                }
            }
            return (value / 1000);
        }
    }
}
