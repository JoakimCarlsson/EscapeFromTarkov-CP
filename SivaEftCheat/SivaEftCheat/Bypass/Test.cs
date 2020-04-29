using System;
using System.Diagnostics;

namespace Citadel.Bypass
{
    class Test
    {
        public void Main()
        {
            Process[] processes = Process.GetProcesses();
            IntPtr assemblyAddress = new IntPtr();

            foreach (var process in processes)
            {
                if (process.ProcessName == "EscapeFromTarkov")
                {
                    foreach (ProcessModule module in process.Modules)
                    {
                        if (module.ModuleName == "Assembly-CSharp")
                        {
                            assemblyAddress = module.BaseAddress;
                        }
                    }
                    break;
                }
            }


        }
    }
}
