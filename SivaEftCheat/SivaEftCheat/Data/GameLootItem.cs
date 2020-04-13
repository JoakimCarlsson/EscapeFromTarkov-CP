using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT.Interactive;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Data
{
    public class GameLootItem
    {
        public LootItem LootItem { get; }

        public Vector3 ScreenPosition => screenPosition;

        public bool IsOnScreen { get; private set; }

        public float Distance { get; private set; }

        public string FormattedDistance => $"{Math.Round(Distance)}m";

        private Vector3 screenPosition;

        public GameLootItem(LootItem lootItem)
        {
            if (lootItem == null)
                throw new ArgumentNullException(nameof(lootItem));

            LootItem = lootItem;
            screenPosition = default;
            Distance = 0f;
        }

        public void RecalculateDynamics()
        {
            if (!GameUtils.IsLootItemValid(LootItem))
                return;

            screenPosition = GameUtils.WorldPointToScreenPoint(LootItem.transform.position);
            IsOnScreen = GameUtils.IsScreenPointVisible(screenPosition);
            Distance = Vector3.Distance(Main.Camera.transform.position, LootItem.transform.position);
        }
    }
}
