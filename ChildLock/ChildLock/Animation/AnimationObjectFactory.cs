using System.Drawing;

namespace ChildLock.Animation
{
    class AnimationObjectFactory
    {
        /// <summary>
        /// スクリーンサイズ.
        /// </summary>
        private Rectangle screenRectangle;
        /// <summary>
        /// 道路の位置.
        /// </summary>
        private int roadPosition;
        /// <summary>
        /// 地面の位置.
        /// </summary>
        private int groundPosition;

        /// <summary>
        /// コンストラクタ.
        /// </summary>
        /// <param name="screenRectangle">スクリーンサイズ</param>
        public AnimationObjectFactory(Rectangle screenRectangle)
        {
            this.screenRectangle = screenRectangle;
            roadPosition = (screenRectangle.Height / 3) * 2;
            groundPosition = roadPosition + 200;
        }

        /// <summary>
        /// 背景用アニメーションオブジェクトを生成する.
        /// </summary>
        /// <returns>背景用アニメーションオブジェクト</returns>
        public AnimationObject CreateBackgroundObject()
        {
            return new Background(screenRectangle, roadPosition, groundPosition);
        }

        /// <summary>
        /// アニメーションオブジェクトを生成する.
        /// </summary>
        /// <param name="key"></param>
        /// <returns>アニメーションオブジェクト</returns>
        public static AnimationObject CreateAnimationObject(Rectangle screenRectangle, string key)
        {
            AnimationObject createObject;
            switch (key)
            {
                // TODO Create animation classes.
                case "A":
                    createObject = null;
                    break;
                default:
                    createObject = null;
                    break;
            }
            return createObject;
        }
    }
}
