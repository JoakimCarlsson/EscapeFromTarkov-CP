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
    class GameExtractPoint
    {
        public ExfiltrationPoint ExfiltrationPoint { get; }

        public Vector3 ScreenPosition => screenPosition;

        public bool IsOnScreen { get; private set; }

        public float Distance { get; private set; }
        public string Name { get; set; }

        public string FormattedDistance => $"{Math.Round(Distance)}m";

        private Vector3 screenPosition;

        public GameExtractPoint(ExfiltrationPoint exfiltrationPoint)
        {
            if (exfiltrationPoint == null)
                throw new ArgumentNullException(nameof(exfiltrationPoint));

            ExfiltrationPoint = exfiltrationPoint;
            screenPosition = default;
            Distance = 0f;
            Name = string.Empty;
        }

        public void RecalculateDynamics()
        {
            if (!GameUtils.IsExfiltrationPointValid(ExfiltrationPoint))
                return;

            screenPosition = GameUtils.WorldPointToScreenPoint(ExfiltrationPoint.transform.position);
            IsOnScreen = GameUtils.IsScreenPointVisible(screenPosition);
            Distance = Vector3.Distance(Main.Camera.transform.position, ExfiltrationPoint.transform.position);
            Name = ExfiltrationPoint.Settings.Name;
        }
    }
}
