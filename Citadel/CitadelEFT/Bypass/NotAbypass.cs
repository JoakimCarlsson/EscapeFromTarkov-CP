using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
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

                //foreach (ProcessModule processModule in process[0].Modules)
                //{
                //    if (processModule.ModuleName == "Assembly-CSharp")
                //    {
                //        Console.WriteLine($"File Name: {processModule.ModuleName}, Size: {processModule.ModuleMemorySize}");
                //    }
                //}

                //Console.WriteLine($"Old Size: {memory.Read<uint>(pImage + 0x18)}");
                memory.Write<uint>(pImage + 0x18, 0x7E0698);
                //Console.WriteLine($"New Size: {memory.Read<uint>(pImage + 0x18)}");
            }
            catch
            {
                process[0].Kill();
            }
        }
    }
}