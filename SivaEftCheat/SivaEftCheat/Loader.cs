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
        public static Texture2D GreenTexture;
        public static Texture2D RedTexture;


        public void Start()
        {
            RedTexture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            RedTexture.SetPixel(0, 0, Color.red);
            RedTexture.SetPixel(1, 0, Color.red);
            RedTexture.SetPixel(0, 1, Color.red);
            RedTexture.SetPixel(1, 1, Color.red);
            RedTexture.Apply();
            GreenTexture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            GreenTexture.SetPixel(0, 0, Color.green);
            GreenTexture.SetPixel(1, 0, Color.green);
            GreenTexture.SetPixel(0, 1, Color.green);
            GreenTexture.SetPixel(1, 1, Color.green);
            GreenTexture.Apply();
        }

        public static void Load()
        {
            HookObject = new GameObject();
            HookObject.AddComponent<Main>();
            Object.DontDestroyOnLoad(HookObject);
        }
    }
}
