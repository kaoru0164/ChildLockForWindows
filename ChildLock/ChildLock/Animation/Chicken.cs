using System;
using System.Drawing;

namespace ChildLock.Animation
{
    class Chicken:AnimationObject
    {

        /// <summary>
        /// 反転表示の場合はtrue.
        /// </summary>
        private bool isReversed;
        /// <summary>
        /// ニワトリかヒヨコの画像.
        /// </summary>
        private Bitmap bitmap;
        /// <summary>
        /// 移動スピード.
        /// </summary>
        private int moveSpeed;
        /// <summary>
        /// 基準の高さ.
        /// </summary>
        private int baseAttitude;
        /// <summary>
        /// アニメーションがジャンプする高さ.
        /// </summary>
        private int jumpHeight;
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
        /// 更新カウンター.
        /// </summary>
        private int updateCounter;
        /// <summary>
        /// カウンターの更新値.
        /// </summary>
        private int counterUpdateValue;

        /// <summary>
        /// コンストラクタ.
        /// </summary>
        /// <param name="screenRectangle">スクリーンサイズ</param>
        /// <param name="groundPosition">地面の位置</param>
        public Chicken(Rectangle screenRectangle, int groundPosition)
        {
            Random random = new Random();
            updateCounter = 0;
            isReversed = random.Next(2) == 0;

            this.screenRectangle = screenRectangle;

            if (random.Next(2) == 0)
            {
                bitmap = Properties.Resources.Chicken;
                counterUpdateValue = 5;
                jumpHeight = 10;
            }
            else
            {
                bitmap = Properties.Resources.Chick;
                counterUpdateValue = 10;
                jumpHeight = 5;
            }

            baseAttitude = groundPosition - bitmap.Height;
            int speedOffset = random.Next(2, 5);
            if (isReversed)
            {
                sourceRectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
                destRectangle = new Rectangle(screenRectangle.Width, baseAttitude, bitmap.Width, bitmap.Height);
                moveSpeed = -speedOffset;
            }
            else
            {
                sourceRectangle = new Rectangle(bitmap.Width, 0, -bitmap.Width, bitmap.Height);
                destRectangle = new Rectangle(0, baseAttitude, bitmap.Width, bitmap.Height);
                moveSpeed = speedOffset;
            }
        }

        /// <summary>
        /// 更新.
        /// </summary>
        public override void Update()
        {
            updateCounter += counterUpdateValue;
            if (updateCounter >= 360)
            {
                updateCounter = 0;
            }

            int attitudeOffset = Math.Min(Convert.ToInt32(jumpHeight * Math.Sin(updateCounter / (Math.PI * 2))), 0);
            destRectangle = new Rectangle(destRectangle.Left + moveSpeed
                , baseAttitude + attitudeOffset
                , destRectangle.Width, destRectangle.Height);

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
            graphics.DrawImage(bitmap, destRectangle, sourceRectangle, GraphicsUnit.Pixel);
        }
    }
}
