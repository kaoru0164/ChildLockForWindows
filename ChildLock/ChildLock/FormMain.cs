using ChildLock.Animation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace ChildLock
{
    class FormMain : Form
    {
        /// <summary>
        /// アニメーション用タイマー.
        /// </summary>
        private Timer animationTimer;

        /// <summary>
        /// キーボードフックの制御.
        /// </summary>
        private KeyboardHook keybordHook;


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
        /// 画面のサイズ.
        /// </summary>
        private Rectangle screenRectangle;
        /// <summary>
        /// 背景画像.
        /// </summary>
        private Bitmap backgroundBitmap;

        /// <summary>
        /// キー画像.
        /// </summary>
        private Bitmap keyBitmap;
        /// <summary>
        /// キー画像の表示位置.
        /// </summary>
        private Point keyPoint;

        /// <summary>
        /// アニメーションオブジェクトの生成.
        /// </summary>
        private AnimationObjectFactory animationObjectFactory;
        /// <summary>
        /// アニメーション用オブジェクトのリスト.
        /// </summary>
        private List<AnimationObject> animationObjectList;

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

            animationTimer = new Timer();
            animationTimer.Interval = 30;
            animationTimer.Tick += AnimationTimer_Tick;

            keybordHook = new KeyboardHook();

            lockState = LockState.Lock;

            animationObjectList = new List<AnimationObject>();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            keybordHook.OnKeyPress += KeybordHook_OnKeyPress;
            keybordHook.Start();

            screenRectangle = Screen.PrimaryScreen.Bounds;

            backgroundBitmap = new Bitmap(screenRectangle.Width, screenRectangle.Height);
            Graphics graphics = Graphics.FromImage(backgroundBitmap);
            graphics.CopyFromScreen(new Point(0, 0), new Point(0, 0), backgroundBitmap.Size);
            graphics.Dispose();

            keyBitmap = Properties.Resources.LockKeyImage;
            keyPoint = new Point(screenRectangle.Width - keyBitmap.Width - 10, 10);

            animationObjectFactory = new AnimationObjectFactory(screenRectangle);
            animationObjectList.Add(animationObjectFactory.CreateBackgroundObject());

            animationTimer.Start();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            animationTimer.Stop();
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
                    SetLockState(LockState.Unlocking);
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

            e.Graphics.DrawImage(backgroundBitmap, 0, 0);

            foreach (var animationObject in animationObjectList)
            {
                animationObject.Draw(e.Graphics);
            }

            e.Graphics.DrawImage(keyBitmap, keyPoint);
        }

        /// <summary>
        /// アニメーション用タイマー.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            List<AnimationObject> finishedObjectList = new List<AnimationObject>();

            foreach (var animationObject in animationObjectList)
            {
                animationObject.Update();
                if (animationObject.IsFinished)
                {
                    finishedObjectList.Add(animationObject);
                }
            }

            foreach (var finishedObject in finishedObjectList)
            {
                animationObjectList.Remove(finishedObject);
            }

            Invalidate();

        }

        /// <summary>
        /// キーが押された時のイベント.
        /// </summary>
        /// <param name="key">押されたキー</param>
        private void KeybordHook_OnKeyPress(Keys key)
        {
            //animationObjectList.Add(AnimationObjectFactory.CreateAnimationObject(screenRectangle, key.ToString()));

            if (lockState == LockState.Unlocking)
            {
                if (key.ToString().Equals(unlockKeyWord[unlockCount].ToString()))
                {
                    unlockCount++;
                    if (unlockCount >= unlockKeyWord.Length)
                    {
                        SetLockState(LockState.Unlock);
                    }
                }
                else
                {
                    SetLockState(LockState.Lock);
                }
            }

        }

        /// <summary>
        /// ロック状態を設定する.
        /// </summary>
        /// <param name="newLockState">新しい状態</param>
        private void SetLockState(LockState newLockState)
        {
            switch (newLockState)
            {
                case LockState.Lock:
                    keyBitmap = Properties.Resources.LockKeyImage;
                    break;
                case LockState.Unlocking:
                    keyBitmap = Properties.Resources.UnlockingKeyImage;
                    unlockCount = 0;
                    break;
                case LockState.Unlock:
                    keyBitmap = Properties.Resources.UnlockKeyImage;
                    break;
            }

            lockState = newLockState;
            Invalidate();
        }

        /// <summary>
        /// ロック状態.
        /// </summary>
        private enum LockState
        {
            Lock,
            Unlocking,
            Unlock,
        }
    }
}
