using System;
using System.Drawing;

namespace ChildLock.Animation
{
    class Airplane : AnimationObject
    {
        /// <summary>
        /// 反転表示の場合はtrue.
        /// </summary>
        private bool isReversed;
        /// <summary>
        /// 飛行機の画像.
        /// </summary>
        private Bitmap airplaneBitmap;
        /// <summary>
        /// 移動スピード.
        /// </summary>
        private int moveSpeed;
        /// <summary>
        /// 高度の変化.
        /// </summary>
        private int attitudeChange;
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
        /// <param name="roadPosition">道路の位置</param>
        public Airplane(Rectangle screenRectangle, int roadPosition)
        {
            Random random = new Random();
            isReversed = random.Next(2) == 0;

            this.screenRectangle = screenRectangle;

            airplaneBitmap = Properties.Resources.Airplane;

            int startAttitude = random.Next(roadPosition / 3);
            int speedOffset = random.Next(2, 20);
            if (isReversed)
            {
                sourceRectangle = new Rectangle(0, 0, airplaneBitmap.Width, airplaneBitmap.Height);
                destRectangle = new Rectangle(screenRectangle.Width, startAttitude, airplaneBitmap.Width, airplaneBitmap.Height);
                moveSpeed = -airplaneBitmap.Width / speedOffset;
            }
            else
            {
                sourceRectangle = new Rectangle(airplaneBitmap.Width, 0, -airplaneBitmap.Width, airplaneBitmap.Height);
                destRectangle = new Rectangle(0, startAttitude, airplaneBitmap.Width, airplaneBitmap.Height);
                moveSpeed = airplaneBitmap.Width / speedOffset;
            }
            attitudeChange = random.Next(-3, 3);
        }

        /// <summary>
        /// 更新.
        /// </summary>
        public override void Update()
        {
            destRectangle.Offset(moveSpeed, attitudeChange);

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
            graphics.DrawImage(airplaneBitmap, destRectangle, sourceRectangle, GraphicsUnit.Pixel);
        }

    }
}
