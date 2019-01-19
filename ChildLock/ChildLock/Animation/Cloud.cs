using System;
using System.Drawing;

namespace ChildLock.Animation
{
    class Cloud : AnimationObject
    {
        /// <summary>
        /// 反転表示の場合はtrue.
        /// </summary>
        private bool isReversed;
        /// <summary>
        /// 雲の画像.
        /// </summary>
        private Bitmap cloudBitmap;
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
        public Cloud(Rectangle screenRectangle)
        {
            Random random = new Random();
            isReversed = random.Next(2) == 0;

            this.screenRectangle = screenRectangle;

            int resourceId = random.Next(11);

            if (resourceId == 0)
            {
                cloudBitmap = Properties.Resources.ThunderCloud;
            }
            else if ((resourceId % 2) == 0)
            {
                cloudBitmap = Properties.Resources.Cloud;
            }
            else
            {
                cloudBitmap = Properties.Resources.Cloud2;
            }

            int startAttitude = random.Next(screenRectangle.Height / 10);
            if (isReversed)
            {
                sourceRectangle = new Rectangle(cloudBitmap.Width, 0, -cloudBitmap.Width, cloudBitmap.Height);
                destRectangle = new Rectangle(0, startAttitude, cloudBitmap.Width, cloudBitmap.Height);
                moveSpeed = random.Next(1, 3);
            }
            else
            {
                sourceRectangle = new Rectangle(0, 0, cloudBitmap.Width, cloudBitmap.Height);
                destRectangle = new Rectangle(screenRectangle.Width, startAttitude, cloudBitmap.Width, cloudBitmap.Height);
                moveSpeed = -random.Next(1, 3);
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
            graphics.DrawImage(cloudBitmap, destRectangle, sourceRectangle, GraphicsUnit.Pixel);
        }
    }
}
