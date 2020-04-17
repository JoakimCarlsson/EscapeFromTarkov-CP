using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SivaEftCheat.Features;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Data
{
    class GameThrowable
    {
        public Throwable Grenade { get; }

        public Vector3 ScreenPosition => _screenPosition;
        public bool IsOnScreen { get; private set; }

        public float Distance { get; private set; }
        public string FormattedDistance => $"{Math.Round(Distance)}m";

        private Vector3 _screenPosition;

        public GameThrowable(Throwable grenade)
        {
            if (grenade == null)
            {
                throw new ArgumentNullException(nameof(grenade));
            }
            Grenade = grenade;
            _screenPosition = default;
            Distance = 0f;
        }

        public void RecalculateDynamics()
        {
            if (GameUtils.IsGrenadeValid(Grenade))
            {
                _screenPosition = GameUtils.WorldPointToScreenPoint(Grenade.transform.position);
                IsOnScreen = GameUtils.IsScreenPointVisible(_screenPosition);
                Distance = Vector3.Distance(Main.Camera.transform.position, Grenade.transform.position);
            }
        }

	}
}
