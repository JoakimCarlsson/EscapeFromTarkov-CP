using System;
using Citadel.Utils;
using EFT.Interactive;
using UnityEngine;

namespace Citadel.Data
{
    class GameLootContainer
    {
        public LootableContainer LootableContainer { get; }
        public Vector3 ScreenPosition => _screenPosition;

        public bool IsOnScreen { get; private set; }

        public float Distance { get; private set; }

        public string FormattedDistance => $"{Math.Round(Distance)}m";

        private Vector3 _screenPosition;

        public GameLootContainer(LootableContainer lootableContainer)
        {
            if (lootableContainer == null)
                throw new ArgumentNullException(nameof(lootableContainer));

            LootableContainer = lootableContainer;
            _screenPosition = default;
            Distance = 0f;
        }

        public void RecalculateDynamics()
        {
            if (!GameUtils.IsLootableContainerValid(LootableContainer))
                return;

            _screenPosition = GameUtils.WorldPointToScreenPoint(LootableContainer.transform.position);
            IsOnScreen = GameUtils.IsScreenPointVisible(_screenPosition);
            Distance = Vector3.Distance(Main.Camera.transform.position, LootableContainer.transform.position);
        }
    }
}
