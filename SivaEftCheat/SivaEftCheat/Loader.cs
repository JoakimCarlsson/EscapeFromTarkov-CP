using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SivaEftCheat
{
    public class Loader
    {
        public static GameObject HookObject;
        public static void Load()
        {
            HookObject = new GameObject();
            HookObject.AddComponent<Main>();
            Object.DontDestroyOnLoad(HookObject);
        }
    }
}
