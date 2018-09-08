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

            keyBitmap = Properties.Resources.KeyImage;
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
                Close();
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
            Text = key.ToString();
        }
    }
}
