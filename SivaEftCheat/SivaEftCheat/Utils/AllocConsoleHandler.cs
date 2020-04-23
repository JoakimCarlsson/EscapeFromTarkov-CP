using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SivaEftCheat.Utils
{
    public static class AllocConsoleHandler
    {
        [DllImport("Kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("msvcrt.dll")]
        public static extern int system(string cmd);

        public static void Open()
        {
            AllocConsole();
            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });
            Application.logMessageReceivedThreaded += (condition, stackTrace, type) => Console.WriteLine(condition + " " + stackTrace);
        }

        public static void ClearAllocConsole()
        {
            system("CLS");
        }
    }
}
