using System;
using System.Collections.Generic;
using System.Linq;
using EFT;
using EFT.InventoryLogic;
using EFT.UI;
using SivaEftCheat.Data;
using SivaEftCheat.Enums;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features.ESP
{
    class PlayerEsp : MonoBehaviour
    {
        private void FixedUpdate()
        {
            foreach (GamePlayer gamePlayer in Main.Players)
                gamePlayer.RecalculateDynamics();
        }

        private void OnGUI()
        {
            if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && Main.GameWorld != null)
            {
                Render.DrawTextOutline(new Vector2(20, 20), $"Registerd Players: {Main.Players.Count}", Color.black, Color.white);
                try
                {
                    foreach (var player in Main.Players)
                    {
                        if ((!player.IsAI || PlayerOptions.DrawScavs) && (player.IsAI || PlayerOptions.DrawPlayers))
                        {
                            bool inRange = player.IsAI && player.Distance <= PlayerOptions.DrawScavsRange || !player.IsAI && player.Distance <= PlayerOptions.DrawPlayerRange;

                            if (inRange)
                            {
                                if (player.IsOnScreen)
                                {
                                    Color playerColor = GetPlayerColor(player);

                                    string nameText = string.Empty;
                                    string healthNumberText = string.Empty;
                                    string distanceText = string.Empty;

                                    if ((player.IsAI && PlayerOptions.DrawScavName) || (!player.IsAI && PlayerOptions.DrawPlayerName))
                                        nameText = player.IsAI ? "[SCAV]" : $"[{player.Player.Profile.Info.Nickname}]";

                                    if ((player.IsAI && PlayerOptions.DrawScavHealth) || (!player.IsAI && PlayerOptions.DrawPlayerHealth))
                                        healthNumberText = $"[{player.Player.HealthController.GetBodyPartHealth(EBodyPart.Common, true).Current}  hp]";

                                    if ((player.IsAI && PlayerOptions.DrawScavDistance) || (!player.IsAI && PlayerOptions.DrawPlayerDistance))
                                        distanceText = $"[{player.FormattedDistance}]";

                                    string text = $"{nameText} {healthNumberText} {distanceText} ";

                                    if (PlayerOptions.DrawPlayerLevel && !player.IsAI)
                                        text += $" [{player.Player.Profile.Info.Level} lvl]";

                                    Render.DrawString1(new Vector2(player.HeadScreenPosition.x, player.HeadScreenPosition.y - 20f), text, playerColor);

                                    if ((player.IsAI && PlayerOptions.DrawScavAimPos) || (!player.IsAI && PlayerOptions.DrawPlayerAimPos))
                                        DrawPlayerAim(player, playerColor);

                                    if ((player.IsAI && PlayerOptions.DrawScavHealthBar) || (!player.IsAI && PlayerOptions.DrawPlayerHealthBar))
                                        DrawHealthBar(player);

                                    if ((player.IsAI && PlayerOptions.DrawScavWeapon) || (!player.IsAI && PlayerOptions.DrawPlayerWeapon))
                                        DrawWeaponText(player, playerColor);

                                    if ((player.IsAI && PlayerOptions.DrawScavValue) || (!player.IsAI && PlayerOptions.DrawPlayerValue))
                                        DrawValueText(player, playerColor);

                                    if ((player.IsAI && PlayerOptions.DrawScavCornerBox) || (!player.IsAI && PlayerOptions.DrawPlayerCornerBox))
                                        DrawBox(player, playerColor);

                                    if ((player.IsAI && PlayerOptions.DrawScavSkeleton) || (!player.IsAI && PlayerOptions.DrawPlayerSkeleton))
                                        DrawSkeleton(player.Player);
                                }
                            }
                        }
                    }
                }
                catch { }
            }
        }

        private static void DrawPlayerAim(GamePlayer player, Color playerColor)
        {
            Render.DrawLine(player.StartPosition, player.EndPosition, playerColor, 0.5f, true);
        }

        private static void DrawHealthBar(GamePlayer player)
        {
            float num2 = Mathf.Abs(player.ScreenPosition.y - player.HeadScreenPosition.y) / 1.8f;
            Render.DrawHealth(new Vector2(player.ScreenPosition.x - num2 / 2f, player.ScreenPosition.y), num2, 5f,
                player.Player.HealthController.GetBodyPartHealth(EBodyPart.Common, true).Current,
                player.Player.HealthController.GetBodyPartHealth(EBodyPart.Common, true).Maximum);
        }

        private static void DrawWeaponText(GamePlayer player, Color playerColor)
        {
            Player.AbstractHandsController handsController = player.Player.HandsController;
            if (((handsController != null) ? handsController.Item : null) is Weapon && player.Player.Weapon.ShortName != null)
            {
                string text3 = $"{player.Player.Weapon.ShortName.Localized()}";
                Render.DrawString1(new Vector2(player.ScreenPosition.x, player.ScreenPosition.y + 5f), text3, playerColor);
            }
        }

        private static void DrawValueText(GamePlayer player, Color playerColor)
        {
            string text3 = $"$ {player.FormattedValue}";
            Render.DrawString1(new Vector2(player.ScreenPosition.x, player.ScreenPosition.y + 15f), text3, playerColor);
        }

        private static void DrawBox(GamePlayer player, Color playerColor)
        {
            float num3 = Mathf.Abs(player.ScreenPosition.y - player.HeadScreenPosition.y);
            Render.DrawCornerBox(new Vector2(player.HeadScreenPosition.x, player.HeadScreenPosition.y), num3 / 1.8f, num3,
                playerColor, true);
        }

        public static Color GetPlayerColor(GamePlayer player)
        {
            if (player.IsVisible)
            {
                return Color.green;
            }
            if (GameUtils.IsFriend(player.Player))
            {
                return PlayerOptions.FriendColor;
            }
            if (player.Player.Profile.Info.Settings.IsBoss())
            {
                return PlayerOptions.BossColor;
            }
            if (player.IsAI)
            {
                return PlayerOptions.ScavColor;
            }

            if (player.Player.Profile.Info.Side == EPlayerSide.Savage)
            {
                return PlayerOptions.PlayerScavColor;
            }


            return PlayerOptions.PlayerColor;
        }

        public static Dictionary<HumanBones, Vector3> GetBones(Player player)
        {
            try
            {
                Dictionary<HumanBones, Vector3> dictionary = new Dictionary<HumanBones, Vector3>();
                if (player.PlayerBody == null || player.PlayerBody.SkeletonRootJoint == null)
                {
                    return dictionary;
                }
                List<Transform> list = player.PlayerBody.SkeletonRootJoint.Bones.Values.ToList();
                if (list.Count == 0)
                {
                    return dictionary;
                }
                for (int i = 0; i < list.Count; i++)
                {
                    Transform transform = list[i];
                    if (!(transform == null) && NeededBones.Contains((HumanBones)i))
                    {
                        Vector3 vector = Main.Camera.WorldToScreenPoint(transform.position);
                        if (GameUtils.IsScreenPointVisible(vector))
                        {
                            vector.y = Screen.height - vector.y;
                            dictionary[(HumanBones)i] = vector;
                        }
                    }
                }
                return dictionary;
            }
            catch
            {
            }
            return null;
        }

        public static void DrawSkeleton(Player player)
        {
            try
            {
                Dictionary<HumanBones, Vector3> bones = GetBones(player);
                if (bones.Count != 0)
                {
                    ConnectBones(bones, HumanBones.HumanPelvis, HumanBones.HumanLThigh1);
                    ConnectBones(bones, HumanBones.HumanLThigh1, HumanBones.HumanLThigh2);
                    ConnectBones(bones, HumanBones.HumanLThigh2, HumanBones.HumanLCalf);
                    ConnectBones(bones, HumanBones.HumanLCalf, HumanBones.HumanLFoot);
                    ConnectBones(bones, HumanBones.HumanLFoot, HumanBones.HumanLToe);
                    ConnectBones(bones, HumanBones.HumanPelvis, HumanBones.HumanRThigh1);
                    ConnectBones(bones, HumanBones.HumanRThigh1, HumanBones.HumanRThigh2);
                    ConnectBones(bones, HumanBones.HumanRThigh2, HumanBones.HumanRCalf);
                    ConnectBones(bones, HumanBones.HumanRCalf, HumanBones.HumanRFoot);
                    ConnectBones(bones, HumanBones.HumanRFoot, HumanBones.HumanRToe);
                    ConnectBones(bones, HumanBones.HumanPelvis, HumanBones.HumanSpine1);
                    ConnectBones(bones, HumanBones.HumanSpine1, HumanBones.HumanSpine2);
                    ConnectBones(bones, HumanBones.HumanSpine2, HumanBones.HumanSpine3);
                    ConnectBones(bones, HumanBones.HumanSpine3, HumanBones.HumanNeck);
                    ConnectBones(bones, HumanBones.HumanNeck, HumanBones.HumanHead);
                    ConnectBones(bones, HumanBones.HumanSpine3, HumanBones.HumanLCollarbone);
                    ConnectBones(bones, HumanBones.HumanLCollarbone, HumanBones.HumanLForearm1);
                    ConnectBones(bones, HumanBones.HumanLForearm1, HumanBones.HumanLForearm2);
                    ConnectBones(bones, HumanBones.HumanLForearm2, HumanBones.HumanLForearm3);
                    ConnectBones(bones, HumanBones.HumanLForearm3, HumanBones.HumanLPalm);
                    ConnectBones(bones, HumanBones.HumanLPalm, HumanBones.HumanLDigit11);
                    ConnectBones(bones, HumanBones.HumanLDigit11, HumanBones.HumanLDigit12);
                    ConnectBones(bones, HumanBones.HumanLDigit12, HumanBones.HumanLDigit13);
                    ConnectBones(bones, HumanBones.HumanSpine3, HumanBones.HumanRCollarbone);
                    ConnectBones(bones, HumanBones.HumanRCollarbone, HumanBones.HumanRForearm1);
                    ConnectBones(bones, HumanBones.HumanRForearm1, HumanBones.HumanRForearm2);
                    ConnectBones(bones, HumanBones.HumanRForearm2, HumanBones.HumanRForearm3);
                    ConnectBones(bones, HumanBones.HumanRForearm3, HumanBones.HumanRPalm);
                    ConnectBones(bones, HumanBones.HumanRPalm, HumanBones.HumanRDigit11);
                    ConnectBones(bones, HumanBones.HumanRDigit11, HumanBones.HumanRDigit12);
                    ConnectBones(bones, HumanBones.HumanRDigit12, HumanBones.HumanRDigit13);
                }
            }
            catch
            {
            }
        }

        public static void ConnectBones(Dictionary<HumanBones, Vector3> bones, HumanBones start, HumanBones stop)
        {
            try
            {
                if (bones.ContainsKey(start) && bones.ContainsKey(stop) && GameUtils.IsScreenPointVisible(bones[start]) && GameUtils.IsScreenPointVisible(bones[stop]))
                {
                    //GameUtils.IsVisible(bones[stop])?  Color.green : Color.red
                    Render.DrawLine(bones[start], bones[stop], 2.5f, Color.white);
                }
            }
            catch
            {
            }
        }

        public static readonly List<HumanBones> NeededBones = new List<HumanBones>
        {
            HumanBones.HumanPelvis,
            HumanBones.HumanLThigh1,
            HumanBones.HumanLThigh2,
            HumanBones.HumanLCalf,
            HumanBones.HumanLFoot,
            HumanBones.HumanLToe,
            HumanBones.HumanPelvis,
            HumanBones.HumanRThigh1,
            HumanBones.HumanRThigh2,
            HumanBones.HumanRCalf,
            HumanBones.HumanRFoot,
            HumanBones.HumanRToe,
            HumanBones.HumanSpine1,
            HumanBones.HumanSpine2,
            HumanBones.HumanSpine3,
            HumanBones.HumanNeck,
            HumanBones.HumanHead,
            HumanBones.HumanLCollarbone,
            HumanBones.HumanLForearm1,
            HumanBones.HumanLForearm2,
            HumanBones.HumanLForearm3,
            HumanBones.HumanLPalm,
            HumanBones.HumanLDigit11,
            HumanBones.HumanLDigit12,
            HumanBones.HumanLDigit13,
            HumanBones.HumanRCollarbone,
            HumanBones.HumanRForearm1,
            HumanBones.HumanRForearm2,
            HumanBones.HumanRForearm3,
            HumanBones.HumanRPalm,
            HumanBones.HumanRDigit11,
            HumanBones.HumanRDigit12,
            HumanBones.HumanRDigit13
        };
    }


}
