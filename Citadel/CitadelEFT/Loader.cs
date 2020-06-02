using Citadel.Bypass;
using Citadel.Features;
using Citadel.Features.ESP;
using UnityEngine;

namespace Citadel
{
    public class Loader
    {
        public static GameObject HookObject;
        public static void Load()
        {
            //AllocConsoleHandler.Open();
            NotABypass bypass = new NotABypass();
            bypass.DoStuff();
            HookObject = new GameObject();
            HookObject.AddComponent<Main>();
            HookObject.AddComponent<Menu>();
            HookObject.AddComponent<ExtractEsp>();
            HookObject.AddComponent<ItemEsp>();
            HookObject.AddComponent<LootableContainerEsp>();
            HookObject.AddComponent<PlayerEsp>();
            HookObject.AddComponent<Misc>();
            HookObject.AddComponent<Radar>();
            HookObject.AddComponent<CorpseEsp>();
            HookObject.AddComponent<Aimbot>();
            HookObject.AddComponent<GrenadeEsp>();

            Object.DontDestroyOnLoad(HookObject);
            //HookObject.hideFlags = HideFlags.HideInHierarchy;
            //HookObject.hideFlags = HideFlags.HideAndDontSave;
        }
    }
}