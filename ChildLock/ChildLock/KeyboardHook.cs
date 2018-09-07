using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ChildLock
{
    public class KeyboardHook
    {
        /// <summary>
        /// キーが押された時のメッセージ.
        /// </summary>
        private const uint MESSAGE_KEY_DOWN = 0x100;
        /// <summary>
        /// キーが放された時のメッセージ.
        /// </summary>
        private const uint MESSAGE_KEY_UP = 0x101;

        /// <summary>
        /// キーが押された時のイベント用デリゲート.
        /// </summary>
        /// <param name="key"></param>
        public delegate void OnKeyPressEvent(Keys key);

        /// <summary>
        /// キーが押された時のイベント.
        /// </summary>
        public event OnKeyPressEvent OnKeyPress;

        /// <summary>
        /// キーフックの実行状態.
        /// </summary>
        public bool IsHooking
        {
            get; private set;
        }

        /// <summary>
        /// ネイティブメソッドから受け取るコールバック.
        /// </summary>
        private event NativeMethods.KeybordHookCallback HookCallback;

        /// <summary>
        /// フックプロシージャのハンドル.
        /// </summary>
        private IntPtr hookProcedureHandle;

        /// <summary>
        /// コンストラクタ.
        /// </summary>
        public KeyboardHook()
        {
            OnKeyPress = delegate { };
            HookCallback = KeyboardHook_KeybordHookCallback;
            IsHooking = false;
        }

        /// <summary>
        /// キーフックを開始する.
        /// </summary>
        public void Start()
        {
            if (IsHooking)
            {
                return;
            }

            IsHooking = true;

            IntPtr hInstance = Marshal.GetHINSTANCE(typeof(KeyboardHook).Assembly.GetModules()[0]);

            hookProcedureHandle = NativeMethods.SetWindowsHookEx(13, HookCallback, hInstance, 0);

            if (hookProcedureHandle == IntPtr.Zero)
            {
                IsHooking = false;
                throw new System.ComponentModel.Win32Exception();
            }
        }

        /// <summary>
        /// キーフックを終了する.
        /// </summary>
        public void Stop()
        {
            if (!IsHooking)
            {
                return;
            }

            if (hookProcedureHandle != IntPtr.Zero)
            {
                IsHooking = false;

                NativeMethods.UnhookWindowsHookEx(hookProcedureHandle);
                hookProcedureHandle = IntPtr.Zero;
            }
        }

        /// <summary>
        /// キーフックのコールバック.
        /// </summary>
        /// <param name="nCode"></param>
        /// <param name="msg"></param>
        /// <param name="kbDllHookStruct"></param>
        /// <returns></returns>
        private IntPtr KeyboardHook_KeybordHookCallback(int nCode, uint msg, ref KBDLLHOOKSTRUCT kbDllHookStruct)
        {
            if (nCode >= 0 && msg == MESSAGE_KEY_UP)
            {
                Keys key = (Keys)kbDllHookStruct.vkCode;
                OnKeyPress(key);
            }

            return (IntPtr)1;
            // キー処理をブロックしないときは下の処理に切り替える.
            // return NativeMethods.CallNextHookEx(hookProcedureHandle, nCode, msg, ref kbDllHookStruct);
        }

        /// <summary>
        /// ネイティブメソッドの定義クラス.
        /// </summary>
        private static class NativeMethods
        {
            /// <summary>
            /// フックプロシージャのデリゲート.
            /// </summary>
            /// <param name="nCode">フックプロシージャに渡すフックコード</param>
            /// <param name="msg">フックプロシージャに渡す値</param>
            /// <param name="kbDllHookStruct">フックプロシージャに渡す値</param>
            /// <returns>フックプロシージャの戻り値</returns>
            public delegate IntPtr KeybordHookCallback(int nCode, uint msg, ref KBDLLHOOKSTRUCT kbDllHookStruct);

            [DllImport("user32.dll")]
            public static extern IntPtr SetWindowsHookEx(int idHook, KeybordHookCallback lpfn, IntPtr hMode, uint dwThreadId);

            [DllImport("user32.dll")]
            public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, uint msg, ref KBDLLHOOKSTRUCT kbDllHookStruct);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool UnhookWindowsHookEx(IntPtr hhk);
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public uint vkCode;
            public uint scanCode;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }
    }
}
