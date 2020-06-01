using System;
using Citadel.Options;
using UnityEngine;

namespace Citadel
{
    public class Menu : MonoBehaviour
    {
        private Rect _mainWindow;
        private Rect _playerVisualWindow;
        private Rect _itemVisuals;
        private Rect _aimbotVisualWindow;
        private Rect _miscFeatureslVisualWindow;
        private Rect _hotKeysVisualWindow;

        public static bool Visible = true;

        private bool _playerEspVisualVisible;
        private bool _miscVisualVisible;
        private bool _aimbotVisualVisible;
        private bool _miscFeatureslVisible;
        private bool _hotKeysVisible;

        private readonly string _watermark = "Pussy Destroyer";
        private string _aimButton = "Bone: Head";

        private void Start()
        {
            _mainWindow = new Rect(20f, 60f, 250f, 50f);
            _playerVisualWindow = new Rect(275f, 60f, 350f, 150f);
            _itemVisuals = new Rect(630f, 60f, 350f, 150f);
            _aimbotVisualWindow = new Rect(985f, 60f, 350f, 150f);
            _miscFeatureslVisualWindow = new Rect(1340f, 60f, 350f, 150f);
            _hotKeysVisualWindow = new Rect(1690, 60f, 350f, 150f);
        }

        private void Update()
        {
            if (Input.GetKeyDown(MiscOptions.ToggleMenu))
                Visible = !Visible;
        }

        private void OnGUI()
        {
            if (!Visible)
                return;

            _mainWindow = GUILayout.Window(0, _mainWindow, RenderUi, _watermark);

            if (_playerEspVisualVisible)
                _playerVisualWindow = GUILayout.Window(1, _playerVisualWindow, RenderUi, "Player Visual");
            if (_miscVisualVisible)
                _itemVisuals = GUILayout.Window(2, _itemVisuals, RenderUi, "Miscellaneous Visual");
            if (_aimbotVisualVisible)
                _aimbotVisualWindow = GUILayout.Window(3, _aimbotVisualWindow, RenderUi, "Aimbot");
            if (_miscFeatureslVisible)
                _miscFeatureslVisualWindow = GUILayout.Window(4, _miscFeatureslVisualWindow, RenderUi, "Miscellaneous");
            if (_hotKeysVisible)
                _hotKeysVisualWindow = GUILayout.Window(5, _hotKeysVisualWindow, RenderUi, "Hot Keys");
        }

        private void RenderUi(int id)
        {
            switch (id)
            {
                case 0:
                    if (GUILayout.Button("Player Visuals"))
                        _playerEspVisualVisible = !_playerEspVisualVisible;
                    if (GUILayout.Button("Miscellaneous Visuals"))
                        _miscVisualVisible = !_miscVisualVisible;
                    if (GUILayout.Button("Aimbot"))
                        _aimbotVisualVisible = !_aimbotVisualVisible;
                    if (GUILayout.Button("Miscellaneous"))
                        _miscFeatureslVisible = !_miscFeatureslVisible;
                    if (GUILayout.Button("Hot Keys"))
                        _hotKeysVisible = !_hotKeysVisible;
                    break;

                case 1:
                    GUILayout.Label("Players");
                    PlayerOptions.DrawPlayers = GUILayout.Toggle(PlayerOptions.DrawPlayers, "Draw Players");
                    PlayerOptions.DrawPlayerHealthBar = GUILayout.Toggle(PlayerOptions.DrawPlayerHealthBar, "Draw Player Health Bar");
                    PlayerOptions.DrawPlayerHealth = GUILayout.Toggle(PlayerOptions.DrawPlayerHealth, "Draw Player Health");
                    PlayerOptions.DrawPlayerName = GUILayout.Toggle(PlayerOptions.DrawPlayerName, "Draw Player Name");
                    PlayerOptions.DrawPlayerLevel = GUILayout.Toggle(PlayerOptions.DrawPlayerLevel, "Draw Player Level");
                    PlayerOptions.DrawPlayerWeapon = GUILayout.Toggle(PlayerOptions.DrawPlayerWeapon, "Draw Player Weapon");
                    PlayerOptions.DrawPlayerCornerBox = GUILayout.Toggle(PlayerOptions.DrawPlayerCornerBox, "Draw Player DrawBox");
                    PlayerOptions.DrawPlayerSkeleton = GUILayout.Toggle(PlayerOptions.DrawPlayerSkeleton, "Draw Player Skeleton");
                    PlayerOptions.DrawPlayerAimPos = GUILayout.Toggle(PlayerOptions.DrawPlayerAimPos, "Draw Player Aim Pos");
                    PlayerOptions.DrawPlayerDistance = GUILayout.Toggle(PlayerOptions.DrawPlayerDistance, "Draw Player Distance");
                    PlayerOptions.DrawPlayerValue = GUILayout.Toggle(PlayerOptions.DrawPlayerValue, "Draw Player Value");
                    PlayerOptions.DrawCorpses = GUILayout.Toggle(PlayerOptions.DrawCorpses, "Draw Player Corpses");
                    PlayerOptions.CustomTexture = GUILayout.Toggle(PlayerOptions.CustomTexture, "Custom Textures");

                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"Player Distance {(int)PlayerOptions.DrawPlayerRange} m");
                    PlayerOptions.DrawPlayerRange = GUILayout.HorizontalSlider(PlayerOptions.DrawPlayerRange, 0f, 1000f);
                    GUILayout.EndHorizontal();

                    GUILayout.Space(20);
                    GUILayout.Label("Scavs");
                    PlayerOptions.DrawScavs = GUILayout.Toggle(PlayerOptions.DrawScavs, "Draw Scavs");
                    PlayerOptions.DrawScavHealthBar = GUILayout.Toggle(PlayerOptions.DrawScavHealthBar, "Draw Scav Health Bar");
                    PlayerOptions.DrawScavHealth = GUILayout.Toggle(PlayerOptions.DrawScavHealth, "Draw Scav Health");
                    PlayerOptions.DrawScavName = GUILayout.Toggle(PlayerOptions.DrawScavName, "Draw Scav Name");
                    PlayerOptions.DrawScavWeapon = GUILayout.Toggle(PlayerOptions.DrawScavWeapon, "Draw Scav Weapon");
                    PlayerOptions.DrawScavCornerBox = GUILayout.Toggle(PlayerOptions.DrawScavCornerBox, "Draw Scav DrawBox");
                    PlayerOptions.DrawScavSkeleton = GUILayout.Toggle(PlayerOptions.DrawScavSkeleton, "Draw Scav Skeleton");
                    PlayerOptions.DrawScavAimPos = GUILayout.Toggle(PlayerOptions.DrawScavAimPos, "Draw Scav Aim Pos");
                    PlayerOptions.DrawScavValue = GUILayout.Toggle(PlayerOptions.DrawScavValue, "Draw Scav Value");
                    PlayerOptions.DrawScavDistance = GUILayout.Toggle(PlayerOptions.DrawScavDistance, "Draw Scav Distance");

                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"ScavColor Distance {(int)PlayerOptions.DrawScavsRange} m");
                    PlayerOptions.DrawScavsRange = GUILayout.HorizontalSlider(PlayerOptions.DrawScavsRange, 0f, 1000f);
                    GUILayout.EndHorizontal();
                    break;

                case 2:
                    MiscVisualsOptions.DrawItems = GUILayout.Toggle(MiscVisualsOptions.DrawItems, "Draw Items");
                    MiscVisualsOptions.DrawItemsPrice = GUILayout.Toggle(MiscVisualsOptions.DrawItemsPrice, "Draw Items Price");
                    MiscVisualsOptions.DrawCommonItems = GUILayout.Toggle(MiscVisualsOptions.DrawCommonItems, "Draw Common Items");
                    MiscVisualsOptions.DrawRareItems = GUILayout.Toggle(MiscVisualsOptions.DrawRareItems, "Draw Rare Items");
                    MiscVisualsOptions.DrawSuperRareItems = GUILayout.Toggle(MiscVisualsOptions.DrawSuperRareItems, "Draw Super Rare Items");
                    MiscVisualsOptions.DrawMedtems = GUILayout.Toggle(MiscVisualsOptions.DrawMedtems, "Draw Meds Items");
                    MiscVisualsOptions.DrawSpecialItems = GUILayout.Toggle(MiscVisualsOptions.DrawSpecialItems, "Draw Special Items");
                    MiscVisualsOptions.DrawQuestItems = GUILayout.Toggle(MiscVisualsOptions.DrawQuestItems, "Draw Quest Items");

                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"Item Distance {(int)MiscVisualsOptions.DrawItemRange} m");
                    MiscVisualsOptions.DrawItemRange = GUILayout.HorizontalSlider(MiscVisualsOptions.DrawItemRange, 0f, 1000f);
                    GUILayout.EndHorizontal();

                    GUILayout.Space(20);
                    GUILayout.Label("Container ESP ");
                    MiscVisualsOptions.DrawLootableContainers = GUILayout.Toggle(MiscVisualsOptions.DrawLootableContainers, "Draw Lootable Containers");
                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"Container Distance {(int)MiscVisualsOptions.DrawLootableContainersRange} m");
                    MiscVisualsOptions.DrawLootableContainersRange = GUILayout.HorizontalSlider(MiscVisualsOptions.DrawLootableContainersRange, 0f, 1000f);
                    GUILayout.EndHorizontal();
                    GUILayout.Space(20);
                    GUILayout.Label("Other Visuals");
                    MiscOptions.DrawHud = GUILayout.Toggle(MiscOptions.DrawHud, "Draw Hud");
                    MiscVisualsOptions.DrawExtractEsp = GUILayout.Toggle(MiscVisualsOptions.DrawExtractEsp, "Draw Extracts");
                    MiscVisualsOptions.DrawExtractEspSwitches = GUILayout.Toggle(MiscVisualsOptions.DrawExtractEspSwitches, "Draw Extracts Switches");
                    MiscVisualsOptions.DrawGrenadeEsp = GUILayout.Toggle(MiscVisualsOptions.DrawGrenadeEsp, "Draw Grenades");
                    MiscVisualsOptions.DrawCrossHair = GUILayout.Toggle(MiscVisualsOptions.DrawCrossHair, "Draw Crosshair");

                    GUILayout.Space(20);
                    GUILayout.Label("Radar");
                    MiscVisualsOptions.DrawRadar = GUILayout.Toggle(MiscVisualsOptions.DrawRadar, "Draw Radar");
                    MiscVisualsOptions.DrawPlayers = GUILayout.Toggle(MiscVisualsOptions.DrawPlayers, "Draw Players");
                    MiscVisualsOptions.DrawScavs = GUILayout.Toggle(MiscVisualsOptions.DrawScavs, "Draw Scavs");
                    MiscVisualsOptions.DrawWealth = GUILayout.Toggle(MiscVisualsOptions.DrawWealth, "Draw Wealth");

                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"Radar Size {(int)MiscVisualsOptions.RadarSize}");
                    MiscVisualsOptions.RadarSize = GUILayout.HorizontalSlider(MiscVisualsOptions.RadarSize, 0f, 1000f);
                    GUILayout.EndHorizontal();

                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"Radar Range {(int)MiscVisualsOptions.RadarRange} m");
                    MiscVisualsOptions.RadarRange = GUILayout.HorizontalSlider(MiscVisualsOptions.RadarRange, 0f, 1000f);
                    GUILayout.EndHorizontal();
                    break;

                case 3:
                    AimbotOptions.Aimbot = GUILayout.Toggle(AimbotOptions.Aimbot, "Aimbot");
                    AimbotOptions.DrawAimbotFov = GUILayout.Toggle(AimbotOptions.DrawAimbotFov, "Draw Aimbot Fov");
                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"Aimbot Fov {(int)AimbotOptions.AimbotFov}");
                    AimbotOptions.AimbotFov = GUILayout.HorizontalSlider(AimbotOptions.AimbotFov, 0f, 1000f);
                    GUILayout.EndHorizontal();
                    if (GUILayout.Button("Aimbot Key: " + AimbotOptions.AimbotKey))
                        AimbotOptions.AimbotKey = KeyCode.None;

                    if (GUILayout.Button(_aimButton))
                    {
                        if (_aimButton.Contains("Head"))
                        {
                            _aimButton = "Bone: Neck";
                            AimbotOptions.AimbotBone = 132;
                        }
                        else if (_aimButton.Contains("Neck"))
                        {
                            _aimButton = "Bone: Body 1";
                            AimbotOptions.AimbotBone = 37;

                        }
                        else if (_aimButton.Contains("Body 1"))
                        {
                            _aimButton = "Bone: Body 2";
                            AimbotOptions.AimbotBone = 36;
                        }
                        else if (_aimButton.Contains("Body 2"))
                        {
                            _aimButton = "Bone: Head";
                            AimbotOptions.AimbotBone = 133;
                        }
                    }
                    GUI.color = Color.red;
                    AimbotOptions.SilentAim = GUILayout.Toggle(AimbotOptions.SilentAim, "Silent Aim");
                    GUI.color = Color.white;

                    AimbotOptions.AutoShoot = GUILayout.Toggle(AimbotOptions.AutoShoot, "Auto Shoot");
                    AimbotOptions.TargetSnapLine = GUILayout.Toggle(AimbotOptions.TargetSnapLine, "Target Snapline");

                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"Distance {(int)AimbotOptions.Distnace} m");
                    AimbotOptions.Distnace = GUILayout.HorizontalSlider(AimbotOptions.Distnace, 0f, 1000);
                    GUILayout.EndHorizontal();
                    break;

                case 4:
                    GUI.color = Color.red;
                    MiscOptions.MaxSkills = GUILayout.Toggle(MiscOptions.MaxSkills, "Max Skills");
                    GUI.color = Color.white;

                    MiscOptions.InfiniteStamina = GUILayout.Toggle(MiscOptions.InfiniteStamina, "Infinite Stamina");

                    GUI.color = Color.red;
                    MiscOptions.SpeedHack = GUILayout.Toggle(MiscOptions.SpeedHack, "Speed Hack");
                    GUI.color = Color.white;

                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"Speed Hack: {(float)Math.Round(MiscOptions.SpeedHackValue, 1, MidpointRounding.ToEven)}");
                    MiscOptions.SpeedHackValue = GUILayout.HorizontalSlider(MiscOptions.SpeedHackValue, 0f, 1f);
                    GUILayout.EndHorizontal();

                    MiscOptions.SuperJump = GUILayout.Toggle(MiscOptions.SuperJump, "Super Jump");

                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"Super Jump: {(float)Math.Round(MiscOptions.SuperJumpValue, 1, MidpointRounding.ToEven)}");
                    MiscOptions.SuperJumpValue = GUILayout.HorizontalSlider(MiscOptions.SuperJumpValue, 0f, 2f);
                    GUILayout.EndHorizontal();

                    GUI.color = Color.red;
                    MiscOptions.DoorUnlocker = GUILayout.Toggle(MiscOptions.DoorUnlocker, $"DoorUnlocker: {MiscOptions.DoorUnlockerKey}");
                    GUI.color = Color.white;

                    MiscOptions.ThermalVison = GUILayout.Toggle(MiscOptions.ThermalVison, $"Thermal Vison: {MiscOptions.ThermalVisonKey}");
                    MiscOptions.NightVison = GUILayout.Toggle(MiscOptions.NightVison, $"Night Vison: {MiscOptions.NightVisonKey}");
                    MiscOptions.NoVisor = GUILayout.Toggle(MiscOptions.NoVisor, "No Visor");
                    MiscOptions.ForceLight = GUILayout.Toggle(MiscOptions.ForceLight, "Force Light");
                    MiscOptions.AlwaysSprint = GUILayout.Toggle(MiscOptions.AlwaysSprint, "Always Max Speed");

                    GUI.color = Color.red;
                    MiscOptions.FlyHack = GUILayout.Toggle(MiscOptions.FlyHack, $"Fly Hack: {MiscOptions.FlyHackKey}");
                    GUI.color = Color.white;

                    GUILayout.Space(20);
                    GUILayout.Label("Weapon Options");
                    MiscOptions.NoRecoil = GUILayout.Toggle(MiscOptions.NoRecoil, "No Recoil");
                    MiscOptions.NoSway = GUILayout.Toggle(MiscOptions.NoSway, "No Sway");
                    MiscOptions.AlwaysAutomatic = GUILayout.Toggle(MiscOptions.AlwaysAutomatic, "Always Automatic");
                    MiscOptions.BulletPenetration = GUILayout.Toggle(MiscOptions.BulletPenetration, "Bullet Penetration");
                    MiscOptions.InstantHit = GUILayout.Toggle(MiscOptions.InstantHit, "Instant Hit");
                    MiscOptions.DontMoveWeaponCloser = GUILayout.Toggle(MiscOptions.DontMoveWeaponCloser, "Don't move weapon closer.");
                    break;

                case 5:
                    if (GUILayout.Button("Toogle Menu: " + MiscOptions.ToggleMenu))
                        MiscOptions.ToggleMenu = KeyCode.None;

                    if (GUILayout.Button("Player ESP: " + PlayerOptions.TogglePlayerEsp))
                        PlayerOptions.TogglePlayerEsp = KeyCode.None;

                    if (GUILayout.Button("Scav ESP: " + PlayerOptions.ToggleScavEsp))
                        PlayerOptions.ToggleScavEsp = KeyCode.None;

                    if (GUILayout.Button("Item ESP: " + MiscVisualsOptions.ToggleItemEsp))
                        MiscVisualsOptions.ToggleItemEsp = KeyCode.None;

                    if (GUILayout.Button("Lootable Container: " + MiscVisualsOptions.ToggleContainerEsp))
                        MiscVisualsOptions.ToggleContainerEsp = KeyCode.None;

                    if (GUILayout.Button("Unlock Doors: " + MiscOptions.DoorUnlockerKey))
                        MiscOptions.DoorUnlockerKey = KeyCode.None;

                    if (GUILayout.Button("Thermal Vison: " + MiscOptions.ThermalVisonKey))
                        MiscOptions.ThermalVisonKey = KeyCode.None;

                    if (GUILayout.Button("Night Vison: " + MiscOptions.NightVisonKey))
                        MiscOptions.NightVisonKey = KeyCode.None;

                    if (GUILayout.Button("Fly Hack: " + MiscOptions.FlyHackKey))
                        MiscOptions.FlyHackKey = KeyCode.None;
                    break;
            }
            GUI.DragWindow();

            if (PlayerOptions.TogglePlayerEsp == KeyCode.None)
            {
                Event e = Event.current;
                PlayerOptions.TogglePlayerEsp = e.keyCode;
            }
            if (PlayerOptions.ToggleScavEsp == KeyCode.None)
            {
                Event e = Event.current;
                PlayerOptions.ToggleScavEsp = e.keyCode;
            }
            if (MiscOptions.ToggleMenu == KeyCode.None)
            {
                Event e = Event.current;
                MiscOptions.ToggleMenu = e.keyCode;
            }
            if (MiscVisualsOptions.ToggleItemEsp == KeyCode.None)
            {
                Event e = Event.current;
                MiscVisualsOptions.ToggleItemEsp = e.keyCode;
            }
            if (MiscVisualsOptions.ToggleContainerEsp == KeyCode.None)
            {
                Event e = Event.current;
                MiscVisualsOptions.ToggleContainerEsp = e.keyCode;
            }
            if (MiscOptions.DoorUnlockerKey == KeyCode.None)
            {
                Event e = Event.current;
                MiscOptions.DoorUnlockerKey = e.keyCode;
            }
            if (MiscOptions.ThermalVisonKey == KeyCode.None)
            {
                Event e = Event.current;
                MiscOptions.ThermalVisonKey = e.keyCode;
            }
            if (MiscOptions.NightVisonKey == KeyCode.None)
            {
                Event e = Event.current;
                MiscOptions.NightVisonKey = e.keyCode;
            }
            if (MiscOptions.FlyHackKey == KeyCode.None)
            {
                Event e = Event.current;
                MiscOptions.FlyHackKey = e.keyCode;
            }

            if (AimbotOptions.AimbotKey == KeyCode.None)
            {
                Event e = Event.current;

                if (Input.GetMouseButtonDown(0))
                {
                    AimbotOptions.AimbotKey = KeyCode.Mouse0;
                    return;
                }

                if (Input.GetMouseButtonDown(1))
                {
                    AimbotOptions.AimbotKey = KeyCode.Mouse1;
                    return;
                }

                if (Input.GetMouseButtonDown(2))
                {
                    AimbotOptions.AimbotKey = KeyCode.Mouse2;
                    return;
                }

                AimbotOptions.AimbotKey = e.keyCode;
            }
        }
    }
}
