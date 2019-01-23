using System;
using System.Drawing;

namespace ChildLock.Animation
{
    class Ufo : AnimationObject
    {
        /// <summary>
        /// 飛行機の画像.
        /// </summary>
        private Bitmap ufoBitmap;
        /// <summary>
        /// 移動量のオフセット.
        /// </summary>
        private int moveOffset;
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
        /// 乱数生成.
        /// </summary>
        private Random random;
        /// <summary>
        /// 更新カウンター.
        /// </summary>
        private int updateCounter;

        /// <summary>
        /// コンストラクタ.
        /// </summary>
        /// <param name="screenRectangle">スクリーンサイズ</param>
        /// <param name="roadPosition">道路の位置</param>
        public Ufo(Rectangle screenRectangle, int roadPosition)
        {
            updateCounter = 0;
            random = new Random();

            this.screenRectangle = screenRectangle;

            ufoBitmap = Properties.Resources.UFO;

            int startAttitude = random.Next(roadPosition / 3);

            updateMoveOffset();


            sourceRectangle = new Rectangle(ufoBitmap.Width, 0, -ufoBitmap.Width, ufoBitmap.Height);
            if (random.Next(2) == 1)
            {
                moveOffset = -moveOffset;
                destRectangle = new Rectangle(screenRectangle.Width - ufoBitmap.Width, startAttitude, ufoBitmap.Width, ufoBitmap.Height);
            }
            else
            {
                destRectangle = new Rectangle(0, startAttitude, ufoBitmap.Width, ufoBitmap.Height);

            }

        }

        /// <summary>
        /// 更新.
        /// </summary>
        public override void Update()
        {
            updateCounter += 3;

            if (updateCounter > 3600)
            {
                updateCounter = 0;
                updateMoveOffset();
            }

            destRectangle.Offset(Convert.ToInt32(moveOffset + 20 * Math.Sin(updateCounter / (Math.PI * 2))), Convert.ToInt32(Math.Sin((updateCounter / 10) / (Math.PI * 2))));

            if (destRectangle.Right < 0)
            {
                IsFinished = true;
            }
            else if (destRectangle.Left > screenRectangle.Width)
            {
                IsFinished = true;
            }

        }

        /// <summary>
        /// 描画.
        /// </summary>
        /// <param name="graphics"></param>
        public override void Draw(Graphics graphics)
        {
            graphics.DrawImage(ufoBitmap, destRectangle, sourceRectangle, GraphicsUnit.Pixel);
        }

        private void updateMoveOffset()
        {
            moveOffset = random.Next(5, 15);
            if (random.Next(2) == 1)
            {
                moveOffset = -moveOffset;
            }
        }
    }
}
