using System;
using System.Drawing;

namespace ChildLock.Animation
{
    class Submarine : AnimationObject
    {
        /// <summary>
        /// 反転表示の場合はtrue.
        /// </summary>
        private bool isReversed;
        /// <summary>
        /// 潜水艦の画像.
        /// </summary>
        private Bitmap submarineBitmap;
        /// <summary>
        /// 移動スピード.
        /// </summary>
        private int moveSpeed;
        /// <summary>
        /// スクリーンのサイズ.
        /// </summary>
        private Rectangle screenRectangle;
        /// <summary>
        /// 画像の座標情報.
        /// </summary>
        private Rectangle sourceRectangle;
        /// <summary>
        /// 表示位置の座標情報.
        /// </summary>
        private Rectangle destRectangle;

        /// <summary>
        /// コンストラクタ.
        /// </summary>
        /// <param name="screenRectangle">スクリーンサイズ</param>
        /// <param name="grandPosition">地面の位置</param>
        public Submarine(Rectangle screenRectangle, int grandPosition)
        {
            Random random = new Random();
            isReversed = random.Next(2) == 0;

            this.screenRectangle = screenRectangle;

            submarineBitmap = Properties.Resources.Submarine;

            int startAttitude = random.Next(grandPosition, screenRectangle.Height - submarineBitmap.Height);
            int speed = random.Next(2, 5);
            if (isReversed)
            {
                sourceRectangle = new Rectangle(0, 0, submarineBitmap.Width, submarineBitmap.Height);
                destRectangle = new Rectangle(screenRectangle.Width, startAttitude, submarineBitmap.Width, submarineBitmap.Height);
                moveSpeed = -speed;
            }
            else
            {
                sourceRectangle = new Rectangle(submarineBitmap.Width, 0, -submarineBitmap.Width, submarineBitmap.Height);
                destRectangle = new Rectangle(0, startAttitude, submarineBitmap.Width, submarineBitmap.Height);
                moveSpeed = speed;
            }
        }

        /// <summary>
        /// 更新.
        /// </summary>
        public override void Update()
        {
            destRectangle.Offset(moveSpeed, 0);

            if (isReversed)
            {
                if (destRectangle.Right < 0)
                {
                    IsFinished = true;
                }

            }
            else
            {
                if (destRectangle.Left > screenRectangle.Width)
                {
                    IsFinished = true;
                }

            }
        }

        /// <summary>
        /// 描画.
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            graphics.DrawImage(submarineBitmap, destRectangle, sourceRectangle, GraphicsUnit.Pixel);
        }
    }
}
