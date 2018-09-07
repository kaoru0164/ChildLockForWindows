using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ChildLock
{
    class FormMain : Form
    {
        /// <summary>
        /// キーボードフックの制御.
        /// </summary>
        private KeyboardHook keybordHook = new KeyboardHook();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            keybordHook.OnKeyPress += KeybordHook_OnKeyPress;
            keybordHook.Start();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            keybordHook.Stop();
        }

        /// <summary>
        /// キーが押された時のイベント.
        /// </summary>
        /// <param name="key">押されたキー</param>
        private void KeybordHook_OnKeyPress(Keys key)
        {
            Text = key.ToString();
        }
    }
}
