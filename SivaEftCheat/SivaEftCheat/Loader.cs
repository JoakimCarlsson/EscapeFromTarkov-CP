using SivaEftCheat.Features;
using SivaEftCheat.Features.ESP;
using UnityEngine;
using SivaEftCheat.Utils;

namespace SivaEftCheat
{
    public class Loader
    {
        public static GameObject MainObject;
        public static GameObject MenuObject;
        public static GameObject ExtractObject;
        public static GameObject ItemObject;
        public static GameObject LootObject;
        public static GameObject PlayerObject;
        public static GameObject MiscObject;
        public static GameObject CorpseObject;
        public static GameObject AimbotObject;
        public static GameObject GrenadeObject;

        public static void Load()
        {
            MainObject = new GameObject();
            MenuObject = new GameObject();
            ExtractObject = new GameObject();
            ItemObject = new GameObject();
            LootObject = new GameObject();
            PlayerObject = new GameObject();
            MiscObject = new GameObject();
            CorpseObject = new GameObject();
            AimbotObject = new GameObject();
            GrenadeObject = new GameObject();

            MainObject.AddComponent<Main>();
            MenuObject.AddComponent<Menu>();
            ExtractObject.AddComponent<ExtractEsp>();
            ItemObject.AddComponent<ItemEsp>();
            LootObject.AddComponent<LootableContainerEsp>();
            PlayerObject.AddComponent<PlayerEsp>();
            MiscObject.AddComponent<Misc>();
            MiscObject.AddComponent<Radar>();
            CorpseObject.AddComponent<CorpseEsp>();
            AimbotObject.AddComponent<Aimbot>();
            GrenadeObject.AddComponent<GrenadeEsp>();

            Object.DontDestroyOnLoad(MainObject);
            Object.DontDestroyOnLoad(MenuObject);
            Object.DontDestroyOnLoad(ExtractObject);
            Object.DontDestroyOnLoad(ItemObject);
            Object.DontDestroyOnLoad(LootObject);
            Object.DontDestroyOnLoad(PlayerObject);
            Object.DontDestroyOnLoad(MiscObject);
            Object.DontDestroyOnLoad(CorpseObject);
            Object.DontDestroyOnLoad(AimbotObject);
            Object.DontDestroyOnLoad(GrenadeObject);
        }
    }
}