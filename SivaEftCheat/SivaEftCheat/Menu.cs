using UnityEngine;

namespace SivaEftCheat
{
    public class Menu : MonoBehaviour
    {
        private Rect _mainWindow;
        private Rect _playerVisualWindow;
        private Rect _miscVisualWindow;
        private Rect _aimbotVisualWindow;
        private Rect _miscFeatureslVisualWindow;
        private Rect _weaponVisualWindow;
        private Rect _hotKeysVisualWindow;
        private bool _visible = false;

        private bool _playerEspVisualVisible;
        private bool _miscVisualVisible;
        private bool _aimbotVisualVisible;
        private bool _miscFeatureslVisible;
        private bool _weaponFeatureslVisible;
        private bool _hotKeysVisualVisible;

        private string watermark = "TESTING";

        private void Start()
        {
            _mainWindow = new Rect(20f, 60f, 250f, 150f);
            _playerVisualWindow = new Rect(275f, 60f, 250f, 150f);
            _miscVisualWindow = new Rect(530f, 60f, 250f, 150f);
            _aimbotVisualWindow = new Rect(785f, 60f, 250, 150f);
            _miscFeatureslVisualWindow = new Rect(1040f, 60f, 250f, 150f);
            _weaponVisualWindow = new Rect(1295f, 60f, 250f, 150f);
            _hotKeysVisualWindow = new Rect(1555f, 60f, 250f, 150f);
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
                _miscVisualWindow = GUILayout.Window(2, _miscVisualWindow, RenderUi, "Item Visual");
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
                    if (GUILayout.Button("Player Visual"))
                        _playerEspVisualVisible = !_playerEspVisualVisible;
                    if (GUILayout.Button("Misc Visual"))
                        _miscVisualVisible = !_miscVisualVisible;
                    if (GUILayout.Button("Aimbot"))
                        _aimbotVisualVisible = !_aimbotVisualVisible;
                    if (GUILayout.Button("Miscellaneous"))
                        _miscFeatureslVisible = !_miscFeatureslVisible;
                    break;

                case 1:

                    break;

                case 2:

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
