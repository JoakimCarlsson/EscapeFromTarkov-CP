using System;
using System.Collections;
using System.Linq;
using Comfort.Common;
using EFT;
using EFT.UI;
using SivaEftCheat.Features.ESP;
using UnityEngine;

namespace SivaEftCheat
{
    class Main : MonoBehaviour
    {
        public static Player LocalPlayer { get; set; }
        public static Camera Camera { get; set; }
        public static GameWorld GameWorld { get; set; }

        private IEnumerator _coroutineUpdateMain;
        public static GameObject HookObject;


        private void Start()
        {
            _coroutineUpdateMain = UpdateMain(10f);
            StartCoroutine(_coroutineUpdateMain);

            HookObject = new GameObject();
            HookObject.AddComponent<ExtractEsp>();
            HookObject.AddComponent<ItemEsp>();

            DontDestroyOnLoad(HookObject);
        }

        public IEnumerator UpdateMain(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);

                try
                {
                    //if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive)
                    //{
                        //if (GameWorld == null)
                            GameWorld = Singleton<GameWorld>.Instance;
                        //if (Camera == null)
                            Camera = Camera.main;
                       // if (LocalPlayer == null)
                            LocalPlayer = GameWorld.RegisteredPlayers.Find(p => p.IsYourPlayer());
                    //}
                }
                catch { }
            }
        }
    }
}
