using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SivaEftCheat
{
    public class TestHook
    {
        public MethodInfo OriginalMethod { get; private set; }

        public MethodInfo HookMethod { get; private set; }

        public TestHook()
        {
            original = null;
            OriginalMethod = (HookMethod = null);
        }

        public TestHook(MethodInfo orig, MethodInfo hook)
        {
            original = null;
            Init(orig, hook);
        }

        public MethodInfo GetMethodByName(Type typeOrig, string nameOrig)
        {
            return typeOrig.GetMethod(nameOrig);
        }

        public TestHook(Type typeOrig, string nameOrig, Type typeHook, string nameHook)
        {
            original = null;
            Init(GetMethodByName(typeOrig, nameOrig), GetMethodByName(typeHook, nameHook));
        }

        public void Init(MethodInfo orig, MethodInfo hook)
        {
            if (orig == null || hook == null)
            {
                throw new ArgumentException("Both original and hook need to be valid methods");
            }
            RuntimeHelpers.PrepareMethod(orig.MethodHandle);
            RuntimeHelpers.PrepareMethod(hook.MethodHandle);
            OriginalMethod = orig;
            HookMethod = hook;
        }

        public unsafe void Hook()
        {
            if (null == OriginalMethod || null == HookMethod)
            {
                throw new ArgumentException("Hook has to be properly Init'd before use");
            }
            if (original == null)
            {
                IntPtr functionPointer = OriginalMethod.MethodHandle.GetFunctionPointer();
                IntPtr functionPointer2 = HookMethod.MethodHandle.GetFunctionPointer();
                if (IntPtr.Size == 8)
                {
                    original = new byte[12];
                    uint newProtect;
                    Import.VirtualProtect(functionPointer, 12U, 64U, out newProtect);
                    byte* ptr = (byte*)((void*)functionPointer);
                    int num = 0;
                    while (num < 12L)
                    {
                        original[num] = ptr[num];
                        num++;
                    }
                    *ptr = 72;
                    ptr[1] = 184;
                    *(IntPtr*)(ptr + 2) = functionPointer2;
                    ptr[10] = byte.MaxValue;
                    ptr[11] = 224;
                    Import.VirtualProtect(functionPointer, 12U, newProtect, out newProtect);
                }
                else
                {
                    original = new byte[7];
                    uint newProtect;
                    Import.VirtualProtect(functionPointer, 7U, 64U, out newProtect);
                    byte* ptr2 = (byte*)((void*)functionPointer);
                    int num2 = 0;
                    while (num2 < 7L)
                    {
                        original[num2] = ptr2[num2];
                        num2++;
                    }
                    *ptr2 = 184;
                    *(IntPtr*)(ptr2 + 1) = functionPointer2;
                    ptr2[5] = byte.MaxValue;
                    ptr2[6] = 224;
                    Import.VirtualProtect(functionPointer, 7U, newProtect, out newProtect);
                }
            }
        }

        public unsafe void Unhook()
        {
            if (original != null)
            {
                uint num = (uint)original.Length;
                IntPtr functionPointer = OriginalMethod.MethodHandle.GetFunctionPointer();
                uint num2;
                Import.VirtualProtect(functionPointer, num, 64U, out num2);
                byte* ptr = (byte*)((void*)functionPointer);
                int num3 = 0;
                while (num3 < num)
                {
                    ptr[num3] = original[num3];
                    num3++;
                }
                Import.VirtualProtect(functionPointer, num, 64U, out num2);
                original = null;
            }
        }

        private const uint HOOK_SIZE_X64 = 12U;

        private const uint HOOK_SIZE_X86 = 7U;

        private byte[] original;

        internal class Import
        {
            [DllImport("kernel32.dll", SetLastError = true)]
            internal static extern bool VirtualProtect(IntPtr address, uint size, uint newProtect, out uint oldProtect);
        }
    }
}