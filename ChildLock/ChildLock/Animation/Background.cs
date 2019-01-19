using System;
using System.Drawing;

namespace ChildLock.Animation
{
    class Background : AnimationObject
    {
        /// <summary>
        /// 道路の描画位置.
        /// </summary>
        private Rectangle roadRectangle;
        /// <summary>
        /// 道路の描画ブラシ.
        /// </summary>
        private Brush roadBrush;

        /// <summary>
        /// 地面の描画位置.
        /// </summary>
        private Rectangle groundRectangle;
        /// <summary>
        /// 地面の描画ブラシ.
        /// </summary>
        private Brush groundBrush;

        /// <summary>
        /// コンストラクタ.
        /// </summary>
        /// <param name="screenRectangle">画面のサイズ</param>
        /// <param name="roadPosition">道路の位置</param>
        /// <param name="groundPosition">地面の位置</param>
        public Background(Rectangle screenRectangle, int roadPosition, int groundPosition)
        {
            const int lineHeight = 1;

            roadRectangle = new Rectangle(0, roadPosition, screenRectangle.Width, lineHeight);
            roadBrush = new SolidBrush(Color.DarkGray);

            groundRectangle = new Rectangle(0, groundPosition, screenRectangle.Width, lineHeight);
            groundBrush = new SolidBrush(Color.Chocolate);
        }

        public override void Update()
        {
            // 何もしない.
        }

        public override void Draw(Graphics graphics)
        {
            graphics.FillRectangle(roadBrush, roadRectangle);
            graphics.FillRectangle(groundBrush, groundRectangle);
        }
    }
}
