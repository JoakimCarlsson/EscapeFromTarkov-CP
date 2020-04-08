using System;
using System.Collections;
using System.Collections.Generic;
using EFT.Interactive;
using EFT.UI;
using JsonType;
using SivaEftCheat.Options;
using SivaEftCheat.Utils;
using UnityEngine;

namespace SivaEftCheat.Features.ESP
{
    class ItemEsp : MonoBehaviour
    {
        private IEnumerator _coroutineUpdate;

        private void Start()
        {
            _coroutineUpdate = Updates(1f);
            StartCoroutine(_coroutineUpdate);
        }

        private IEnumerator Updates(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);

                try
                {

                }
                catch { }
            }
        }

        private void OnGUI()
        {
            try
            {

                if (!MonoBehaviourSingleton<PreloaderUI>.Instance.IsBackgroundBlackActive && ItemOptions.DrawItems)
                {

                }
            }
            catch { }
        }

        public static bool IsSpecialLootItem(string templateId)
        {
            return ItemOptions.RareItems.Contains(templateId);
        }
    }
}