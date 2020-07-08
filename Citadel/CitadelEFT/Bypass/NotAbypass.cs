using System;
using System.Diagnostics;
using System.IO;
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
                
                NativeMemory memory = new LocalProcessMemory(process[0]);

                string xx = "";


                IntPtr pImage1 = MonoImageLoaded("System.Memory");
                xx += $"1 Image size: {memory.Read<uint>(pImage1 + 0x18)}" + "\n";
                memory.Write<uint>(pImage1 + 0x18, 0);
                xx += $"1 New image Size: {memory.Read<uint>(pImage1 + 0x18)}" + "\n";
                xx += "----------" + "\n";


                IntPtr pImage2 = MonoImageLoaded("EFT-Logging");
                xx += $"2 Image size: {memory.Read<uint>(pImage2 + 0x18)}" + "\n";
                memory.Write<uint>(pImage2 + 0x18, 0x5298);
                xx += $"2 New image Size: {memory.Read<uint>(pImage2 + 0x18)}" + "\n";
                xx += "----------" + "\n";


                foreach (ProcessModule processModule in process[0].Modules)
                {
                    xx += $"Module Name: {processModule.ModuleName}, Module Memory Size: {processModule.ModuleMemorySize}" + "\n";
                }
                //File.WriteAllText(Path.Combine(Path.GetTempPath(), "Modules.txt"), xx);
                if (File.Exists(Path.Combine(Path.GetTempPath(), "Modules.txt")))
                {
                    File.Delete(Path.Combine(Path.GetTempPath(), "Modules.txt"));
                }
            }
            catch
            {
                process[0].Kill();
            }
        }
    }
}