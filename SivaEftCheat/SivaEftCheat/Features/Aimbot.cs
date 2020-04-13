using System.Collections.Generic;
using System.Linq;
using EFT;
using EFT.Ballistics;
using EFT.InventoryLogic;
using EFT.UI;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features
{
    class Aimbot : MonoBehaviour
    {
        public static string _hud = string.Empty;
        public static bool NotHooked = true;
        public static TestHook createShotHook;

        private void Update()
        {
            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive)
                {
                    if (AimbotOptions.SilentAim && NotHooked)
                    {
                        createShotHook = new TestHook();
                        createShotHook.Init(typeof(BallisticsCalculator).GetMethod("CreateShot"), typeof(SilentAim).GetMethod("SilentAimHook"));
                        createShotHook.Hook();
                        NotHooked = false;
                    }
                }
            }
            catch { }
        }

        private void OnGUI()
        {
            try
            {
                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive)
                {
                    TargetSnapLine();
                    DoAimbot();
                }
            }
            catch { }
        }

        private void DoAimbot()
        {
            if (AimbotOptions.Aimbot && Input.GetKey(AimbotOptions.AimbotKey))
            {
                Dictionary<Player, int> dictionary = new Dictionary<Player, int>();
                Vector2 centerOfScreen = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Vector3 aimPos = Vector3.zero;
                foreach (var player in Main.GameWorld.RegisteredPlayers)
                {
                    int distanceFromCenter = (int)Vector2.Distance(Main.Camera.WorldToScreenPoint(player.PlayerBones.Head.position), centerOfScreen);
                    int distanceFromPlayer = (int)Vector3.Distance(Main.LocalPlayer.Transform.position, player.Transform.position);
                    Vector3 vector3 = player.Transform.position - Main.Camera.transform.position;
                    if (distanceFromPlayer <= AimbotOptions.AimbotDistnace && distanceFromCenter <= AimbotOptions.AimbotFov && Vector3.Dot(Main.Camera.transform.TransformDirection(Vector3.forward), vector3) > 0f)
                    {
                        dictionary.Add(player, distanceFromCenter);
                    }
                }
                if (dictionary.Count > 0.01)
                {
                    dictionary = (from pair in dictionary orderby pair.Value select pair).ToDictionary(pair => pair.Key, pair => pair.Value);
                    Player player = dictionary.Keys.First();
                    //Vector3 bottomPosition = Main.Camera.WorldToScreenPoint(player.Transform.position);
                    float distance = Vector3.Distance(Main.Camera.transform.position, player.Transform.position);

                    Vector3 headPosition = player.PlayerBones.Head.position;

                    Weapon weapon = Main.LocalPlayer.Weapon;
                    if (/*bottomPosition.z > 0.01 && */weapon != null)
                    {
                        float travelTime = distance / Main.LocalPlayer.Weapon.CurrentAmmoTemplate.InitialSpeed;
                        headPosition.x += player.Velocity.x * travelTime;
                        headPosition.y += player.Velocity.y * travelTime;
                        aimPos = headPosition;
                        //Render.DrawString1(new Vector2(bottomPosition.x, (float)Screen.height - bottomPosition.y + 20f), "This dude is about to die", Color.red, true, 12, FontStyle.Bold, 3);
                    }
                }
                if (aimPos != Vector3.zero)
                {
                    AimAtPos(aimPos);
                }
            }
        }

        private void TargetSnapLine()
        {
            if (AimbotOptions.TargetSnapLine)
            {
                Dictionary<Player, int> dictionary = new Dictionary<Player, int>();
                Vector2 vector = new Vector2(Screen.width / 2f, Screen.height / 2f);
                Vector3 zero = Vector3.zero;
                foreach (var player in Main.GameWorld.RegisteredPlayers)
                {
                    int num = (int)Vector2.Distance(Main.Camera.WorldToScreenPoint(player.PlayerBones.Head.position), vector);
                    int num2 = (int)Vector3.Distance(Main.LocalPlayer.Transform.position, player.Transform.position);
                    Vector3 vector2 = player.Transform.position - Main.Camera.transform.position;
                    if (num2 <= AimbotOptions.AimbotDistnace && num <= AimbotOptions.AimbotFov && Vector3.Dot(Main.Camera.transform.TransformDirection(Vector3.forward), vector2) > 0f)
                    {
                        dictionary.Add(player, num);
                    }
                }
                if (dictionary.Count > 0.01)
                {
                    dictionary = (from pair in dictionary orderby pair.Value select pair).ToDictionary(pair => pair.Key, pair => pair.Value);
                    Player player = dictionary.Keys.First();
                    Camera.main.WorldToScreenPoint(player.Transform.position);
                    player.PlayerBones.Head.position += new Vector3(0f, 0.07246377f, 0f);
                    Vector3 vector3 = Main.Camera.WorldToScreenPoint(player.PlayerBones.Head.position);
                    Weapon weapon = Main.LocalPlayer.Weapon;
                    if (vector3.z > 0.01 && weapon != null)
                    {
                        Render.DrawLine(new Vector2(Screen.width / 2f, Screen.height / 2f), new Vector2(vector3.x, Screen.height - vector3.y), MiscVisualsOptions.CrossHairColor, 0.5f, true);
                    }
                }
            }
        }

        public static void AimAtPos(Vector3 position)
        {
            if (Main.LocalPlayer.Weapon != null)
            {
                Vector3 eulerAngles = Quaternion.LookRotation((position - Main.LocalPlayer.Fireport.position).normalized).eulerAngles;
                if (eulerAngles.x > 180f)
                {
                    eulerAngles.x -= 360f;
                }
                Main.LocalPlayer.MovementContext.Rotation = new Vector2(eulerAngles.y, eulerAngles.x);
            }
        }
    }
}
