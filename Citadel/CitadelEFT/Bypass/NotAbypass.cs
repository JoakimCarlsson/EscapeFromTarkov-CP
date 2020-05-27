using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using Citadel.Utils;
using Memory;

namespace Citadel.Bypass
{
    class NotABypass
    {
        [DllImport("mono-2.0-bdwgc.dll", EntryPoint = "mono_image_loaded")]
        public static extern IntPtr MonoImageLoaded(string image);

        public void DoStuff()
        {
            Process[] process = Process.GetProcessesByName("EscapeFromTarkov");

            try
            {
                IntPtr pImage = MonoImageLoaded("Assembly-CSharp");
                NativeMemory memory = new LocalProcessMemory(process[0]);
                memory.Write<uint>(pImage + 0x18, 8196248);
            }
            catch
            {
                process[0].Kill();
            }
        }
    }
}