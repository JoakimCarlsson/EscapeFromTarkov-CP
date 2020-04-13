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
                Draw();
            }
        }

        public static Color GetPlayerColor(bool isVisible, bool isFriend, string side)
        {
            if (isVisible)
            {
                return Color.green;
            }
            if (isFriend)
            {
                return PlayerOptions.FriendColor;
            }
            if (side != null && side == "SCAV")
            {
                return PlayerOptions.Scav;
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

        public static void DrawSkeleton(Dictionary<HumanBones, Vector3> bones)
        {
            try
            {
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

        public static void Draw()
        {
            try
            {
                foreach (var player in Main.Players)
                {
                    bool isScav = player.IsAI;

                    if ((!isScav || PlayerOptions.DrawScavs) && (isScav || PlayerOptions.DrawPlayers))
                    {
                        bool inRange = isScav && player.Distance <= PlayerOptions.DrawScavsRange;

                        if (!isScav && player.Distance <= PlayerOptions.DrawPlayerRange)
                        {
                            inRange = true;
                        }

                        if (inRange)
                        {
                            if (player.IsOnScreen)
                            {
                                string nameText = isScav ? "SCAV" : player.Player.Profile.Info.Nickname;

                                string healthNumberText = player.Player.HealthController.GetBodyPartHealth(EBodyPart.Common, true).Current.ToString();
                                bool visible = GameUtils.IsVisible(player.Player.PlayerBones.Head.position);

                                Color playerColor = GetPlayerColor(visible, GameUtils.IsFriend(player.Player), nameText);
                                string text2 = $"[{nameText}] [{healthNumberText} hp] [{player.FormattedDistance}] ";

                                if (PlayerOptions.DrawPlayerLevel && !isScav)
                                {
                                    text2 += $" [{player.Player.Profile.Info.Level} lvl]";
                                }
                                Vector2 vector3 = GUI.skin.GetStyle(text2).CalcSize(new GUIContent(text2));

                                Render.DrawTextOutline(new Vector2(player.HeadScreenPosition.x - vector3.x / 2f, player.HeadScreenPosition.y - 20f), text2, Color.black, playerColor);

                                if ((isScav && PlayerOptions.DrawScavHealthBar) || (!isScav && PlayerOptions.DrawPlayerHealthBar))
                                {
                                    float num2 = Mathf.Abs(player.ScreenPosition.y - player.HeadScreenPosition.y) / 1.8f;
                                    Render.DrawHealth(new Vector2(player.ScreenPosition.x - num2 / 2f, player.ScreenPosition.y), num2, 5f, player.Player.HealthController.GetBodyPartHealth(EBodyPart.Common, true).Current, player.Player.HealthController.GetBodyPartHealth(EBodyPart.Common, true).Maximum);
                                }
                                if ((isScav && PlayerOptions.DrawScavWeapon) || (!isScav && PlayerOptions.DrawPlayerWeapon))
                                {
                                    Player.AbstractHandsController handsController = player.Player.HandsController;
                                    if (((handsController != null) ? handsController.Item : null) is Weapon && player.Player.Weapon.ShortName != null)
                                    {
                                        string text3 = $"{player.Player.Weapon.ShortName.Localized()}";
                                        Vector2 vector4 = GUI.skin.GetStyle(text3).CalcSize(new GUIContent(text3));
                                        Render.DrawTextOutline(new Vector2(player.ScreenPosition.x - vector4.x / 2f, player.ScreenPosition.y + 5f), text3, Color.black, playerColor);
                                    }
                                }
                                if ((isScav && PlayerOptions.DrawScavCornerBox) || (!isScav && PlayerOptions.DrawPlayerCornerBox))
                                {
                                    float num3 = Mathf.Abs(player.ScreenPosition.y - player.HeadScreenPosition.y);
                                    Render.DrawCornerBox(new Vector2(player.HeadScreenPosition.x, player.HeadScreenPosition.y), num3 / 1.8f, num3, playerColor, true);
                                }

                                if ((isScav && PlayerOptions.DrawScavSkeleton) || (!isScav && PlayerOptions.DrawPlayerSkeleton))
                                {
                                    Dictionary<HumanBones, Vector3> bones = GetBones(player.Player);
                                    if (bones.Count != 0)
                                    {
                                        DrawSkeleton(bones);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
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
