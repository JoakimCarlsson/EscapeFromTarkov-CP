using System;
using EFT.Interactive;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Data
{
    class GameLootContainer
    {
        public LootableContainer LootableContainer { get; }
        public Vector3 ScreenPosition => screenPosition;

        public bool IsOnScreen { get; private set; }

        public float Distance { get; private set; }

        public string FormattedDistance => $"{Math.Round(Distance)}m";

        private Vector3 screenPosition;

        public GameLootContainer(LootableContainer lootableContainer)
        {
            if (lootableContainer == null)
                throw new ArgumentNullException(nameof(lootableContainer));

            LootableContainer = lootableContainer;
            screenPosition = default;
            Distance = 0f;
        }

        public void RecalculateDynamics()
        {
            if (!GameUtils.IsLootableContainerValid(LootableContainer))
                return;

            screenPosition = GameUtils.WorldPointToScreenPoint(LootableContainer.transform.position);
            IsOnScreen = GameUtils.IsScreenPointVisible(screenPosition);
            Distance = Vector3.Distance(Main.Camera.transform.position, LootableContainer.transform.position);
        }
    }
}
