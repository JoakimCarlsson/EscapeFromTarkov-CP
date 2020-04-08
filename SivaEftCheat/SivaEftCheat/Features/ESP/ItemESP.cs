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
        private readonly List<LootItem> _items = new List<LootItem>();

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
                GUI.Label(new Rect(20f, 40f, 500f, 500f), $"Items Debug text: {_items.Count}");

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