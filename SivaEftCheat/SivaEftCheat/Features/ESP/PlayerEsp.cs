using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFT;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features.ESP
{
    class PlayerEsp : MonoBehaviour
    {
        private void OnGUI()
        {
            foreach (Player player in Main.GameWorld.RegisteredPlayers)
            {
                float distance = Vector3.Distance(Main.LocalPlayer.Transform.position, player.Transform.position);
                Vector3 screenPosition = GameUtils.WorldPointToScreenPoint(player.Transform.position);


            }
        }
    }
}
