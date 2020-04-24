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
    class GameCorpse
    {
        public Corpse Corpse { get; }
        public Vector3 ScreenPosition => _screenPosition;

        public bool IsOnScreen { get; private set; }

        public float Distance { get; private set; }

        public string FormattedDistance => $"{Math.Round(Distance)}m";

        private Vector3 _screenPosition;

        public GameCorpse(Corpse corpse)
        {
            Corpse = corpse ?? throw new ArgumentNullException(nameof(corpse));
            _screenPosition = default;
            Distance = 0f;
        }

        public void RecalculateDynamics()
        {
            if (!GameUtils.IsCorpseValid(Corpse))
                return;

            _screenPosition = GameUtils.WorldPointToScreenPoint(Corpse.transform.position);
            IsOnScreen = GameUtils.IsScreenPointVisible(_screenPosition);
            Distance = Vector3.Distance(Main.Camera.transform.position, Corpse.transform.position);
        }
    }
}
