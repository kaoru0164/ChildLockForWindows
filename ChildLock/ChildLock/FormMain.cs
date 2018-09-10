using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace ChildLock
{
    class FormMain : Form
    {
        /// <summary>
        /// キーボードフックの制御.
        /// </summary>
        private KeyboardHook keybordHook = new KeyboardHook();


        /// <summary>
        /// ロック状態.
        /// </summary>
        private LockState lockState;
        /// <summary>
        /// アンロックするためのキーワード.
        /// </summary>
        private string unlockKeyWord = "UNLOCK";
        /// <summary>
        /// アンロックのカウント.
        /// </summary>
        private int unlockCount;

        /// <summary>
        /// 背景画像.
        /// </summary>
        private Bitmap backgroundBitmap;
        /// <summary>
        /// 背景画像の表示位置.
        /// </summary>
        private Point backgroundPoint;

        /// <summary>
        /// キー画像.
        /// </summary>
        private Bitmap keyBitmap;
        /// <summary>
        /// キー画像の表示位置.
        /// </summary>
        private Point keyPoint;

        /// <summary>
        /// コンストラクタ.
        /// </summary>
        public FormMain()
        {
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.Manual;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer
                , true);
            WindowState = FormWindowState.Maximized;
            TopMost = true;

            lockState = LockState.Lock;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            keybordHook.OnKeyPress += KeybordHook_OnKeyPress;
            keybordHook.Start();

            Rectangle screenRectangle = Screen.PrimaryScreen.Bounds;

            backgroundBitmap = new Bitmap(screenRectangle.Width, screenRectangle.Height);
            Graphics graphics = Graphics.FromImage(backgroundBitmap);
            graphics.CopyFromScreen(new Point(0, 0), new Point(0, 0), backgroundBitmap.Size);
            graphics.Dispose();
            backgroundPoint = new Point(0, 0);

            keyBitmap = Properties.Resources.LockKeyImage;
            keyPoint = new Point(screenRectangle.Width - keyBitmap.Width - 10, 10);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            keybordHook.Stop();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (keyPoint.X <= e.X
                && e.X <= keyPoint.X + keyBitmap.Width
                && keyPoint.Y <= e.Y
                && e.Y <= keyPoint.Y + keyBitmap.Height)
            {
                if (lockState == LockState.Lock)
                {
                    lockState = LockState.Unlocking;
                    unlockCount = 0;
                }
                else if (lockState == LockState.Unlock)
                {
                    Close();
                }
            }

            base.OnMouseClick(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.DrawImage(backgroundBitmap, backgroundPoint);

            e.Graphics.DrawImage(keyBitmap, keyPoint);
        }

        /// <summary>
        /// キーが押された時のイベント.
        /// </summary>
        /// <param name="key">押されたキー</param>
        private void KeybordHook_OnKeyPress(Keys key)
        {
            if (lockState == LockState.Unlocking)
            {
                if (key.ToString().Equals(unlockKeyWord[unlockCount].ToString()))
                {
                    unlockCount++;
                    if (unlockCount >= unlockKeyWord.Length)
                    {
                        lockState = LockState.Unlock;
                        keyBitmap = Properties.Resources.UnlockKeyImage;
                        Invalidate();
                    }
                }
                else
                {
                    lockState = LockState.Lock;
                }
            }

        }

        private enum LockState
        {
            Lock,
            Unlocking,
            Unlock,
        }
    }
}
