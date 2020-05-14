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
            IntPtr pImage = MonoImageLoaded("Assembly-CSharp");

            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName == "EscapeFromTarkov")
                {
                    try
                    {
                        NativeMemory memory = new LocalProcessMemory(process);
                        memory.Write<UInt32>(pImage + 0x18, 0x7CEC98);
                        break;
                    }
                    catch
                    {
                        process.Kill();
                    }
                }
            }
        }
    }
}

//AppDomain currentDomain = AppDomain.CurrentDomain;
//Assembly[] assemblies = currentDomain.GetAssemblies();

//Console.WriteLine("List of assemblies loaded in current appdomain:");
//foreach (Assembly assembly in assemblies)
//{

//}