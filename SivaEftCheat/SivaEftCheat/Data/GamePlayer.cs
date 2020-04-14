using System;
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

        private static string Group = string.Empty;

        public string FormattedDistance => $"{(int)Math.Round(Distance)}m";

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
            IsAI = true;
            IsVisible = false;
        }

        public void RecalculateDynamics()
        {
            if (!GameUtils.IsPlayerValid(Player))
                return;

            screenPosition = GameUtils.WorldPointToScreenPoint(Player.Transform.position);

            if (Player.PlayerBones != null)
                headScreenPosition = GameUtils.WorldPointToScreenPoint(Player.PlayerBones.Head.position);

            IsOnScreen = GameUtils.IsScreenPointVisible(screenPosition);
            Distance = Vector3.Distance(Main.Camera.transform.position, Player.Transform.position);
            IsVisible = RayCast.IsVisible(Player);
            if ((Player.Profile != null) && (Player.Profile.Info != null))
                IsAI = (Player.Profile.Info.RegistrationDate <= 0);

        }

        public static bool IsInYourGroup(Player player)
        {
            return Group == player.Profile.Info.GroupId && Group != "0" && Group != "" && Group != null;
        }
    }
}
