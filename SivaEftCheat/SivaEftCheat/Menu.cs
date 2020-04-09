using EFT.InventoryLogic;
using SivaEftCheat.Options;
using UnityEngine;

namespace SivaEftCheat
{
    public class Menu : MonoBehaviour
    {
        private Rect _mainWindow;
        private Rect _playerVisualWindow;
        private Rect _itemVisuals;
        private Rect _aimbotVisualWindow;
        private Rect _miscFeatureslVisualWindow;
        private bool _visible = true;

        private bool _playerEspVisualVisible;
        private bool _miscVisualVisible;
        private bool _aimbotVisualVisible;
        private bool _miscFeatureslVisible;

        private string watermark = "TESTING";

        private void Start()
        {
            _mainWindow = new Rect(20f, 60f, 250f, 150f);
            _playerVisualWindow = new Rect(275f, 60f, 350f, 150f);
            _itemVisuals = new Rect(530f, 60f, 350f, 150f);
            _aimbotVisualWindow = new Rect(785f, 60f, 350f, 150f);
            _miscFeatureslVisualWindow = new Rect(1040f, 60f, 350f, 150f);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Insert))
                _visible = !_visible;
        }

        private void OnGUI()
        {
            //GUI.Label(new Rect(20, 20, 1000, 500), watermark);
            GUI.Label(new Rect(20, 20, 1000, 500), $"Debug Text: Loot Items Count: {Main.LootItems.Count}");
            GUI.Label(new Rect(20, 40, 1000, 500), $"Debug Text: LootableContainers Count: {Main.LootableContainers.Count}");
            GUI.Label(new Rect(20, 60, 1000, 500), $"Debug Text: Corpse Count: {Main.Corpses.Count}");
            GUI.Label(new Rect(20, 100, 1000, 500), $"Debug Text: Players Count: {Main.GameWorld.RegisteredPlayers.Count}");

            if (!_visible)
                return;

            _mainWindow = GUILayout.Window(0, _mainWindow, RenderUi, watermark);

            if (_playerEspVisualVisible)
                _playerVisualWindow = GUILayout.Window(1, _playerVisualWindow, RenderUi, "Player Visual");
            if (_miscVisualVisible)
                _itemVisuals = GUILayout.Window(2, _itemVisuals, RenderUi, "Item Visual");
            if (_aimbotVisualVisible)
                _aimbotVisualWindow = GUILayout.Window(3, _aimbotVisualWindow, RenderUi, "Aimbot");
            if (_miscFeatureslVisible)
                _miscFeatureslVisualWindow = GUILayout.Window(4, _miscFeatureslVisualWindow, RenderUi, "Miscellaneous");
        }

        private void RenderUi(int id)
        {
            GUI.color = new Color(28, 36, 33);
            switch (id)
            {
                case 0:
                    if (GUILayout.Button("Player Visuals"))
                        _playerEspVisualVisible = !_playerEspVisualVisible;
                    if (GUILayout.Button("Item Visuals"))
                        _miscVisualVisible = !_miscVisualVisible;
                    if (GUILayout.Button("Aimbot"))
                        _aimbotVisualVisible = !_aimbotVisualVisible;
                    if (GUILayout.Button("Miscellaneous"))
                        _miscFeatureslVisible = !_miscFeatureslVisible;
                    break;

                case 1:

                    break;

                case 2:
                    ItemOptions.DrawItems = GUILayout.Toggle(ItemOptions.DrawItems, "Draw Items");
                    ItemOptions.DrawCommonItems = GUILayout.Toggle(ItemOptions.DrawCommonItems, "Draw Common Items");
                    ItemOptions.DrawRareItems = GUILayout.Toggle(ItemOptions.DrawRareItems, "Draw Rare Items");
                    ItemOptions.DrawSuperRareItems = GUILayout.Toggle(ItemOptions.DrawSuperRareItems, "Draw Super Rare Items");
                    ItemOptions.DrawMedtems = GUILayout.Toggle(ItemOptions.DrawMedtems, "Draw Meds Items");
                    ItemOptions.DrawSpecialItems = GUILayout.Toggle(ItemOptions.DrawSpecialItems, "Draw Special Items");
                    ItemOptions.DrawQuestItems = GUILayout.Toggle(ItemOptions.DrawQuestItems, "Draw Quest Items");

                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"Item Distance {(int)ItemOptions.DrawItemRange} m");
                    ItemOptions.DrawItemRange = GUILayout.HorizontalSlider(ItemOptions.DrawItemRange, 0f, 1000f);
                    GUILayout.EndHorizontal();

                    GUILayout.Label("Lootable Container");
                    GUILayout.Space(10);
                    ItemOptions.DrawLootableContainers = GUILayout.Toggle(ItemOptions.DrawLootableContainers, "Draw Lootable Containers");
                    GUILayout.BeginHorizontal();
                    GUILayout.Label($"Container Distance {(int)ItemOptions.DrawLootableContainersRange} m");
                    ItemOptions.DrawLootableContainersRange = GUILayout.HorizontalSlider(ItemOptions.DrawLootableContainersRange, 0f, 1000f);
                    GUILayout.EndHorizontal();
                    break;

                case 3:

                    break;

                case 4:

                    break;
            }
            GUI.DragWindow();
        }
    }
}
