using System;
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
        /// 乱数.
        /// </summary>
        private Random random;

        /// <summary>
        /// コンストラクタ.
        /// </summary>
        /// <param name="screenRectangle">スクリーンサイズ</param>
        public AnimationObjectFactory(Rectangle screenRectangle)
        {
            this.screenRectangle = screenRectangle;
            roadPosition = (screenRectangle.Height / 3) * 2;
            groundPosition = roadPosition + 200;

            random = new Random();
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
        public AnimationObject CreateAnimationObject(string key)
        {
            AnimationObject createdObject;
            switch (key)
            {
                case "A":
                    createdObject = new Car(Car.CarType.Ambulance, screenRectangle, roadPosition);
                    break;
                case "B":
                    if (random.Next(2) == 0)
                    {
                        createdObject = new Car(Car.CarType.Bus, screenRectangle, roadPosition);
                    }
                    else
                    {
                        createdObject = new Car(Car.CarType.SchoolBus, screenRectangle, roadPosition);
                    }
                    break;
                case "C":
                    createdObject = new Cloud(screenRectangle);
                    break;
                case "F":
                    createdObject = new Car(Car.CarType.FireEngine, screenRectangle, roadPosition);
                    break;

                case "G":
                    createdObject = new Car(Car.CarType.Bulldozer, screenRectangle, roadPosition);
                    break;
                case "H":
                    createdObject = new Car(Car.CarType.Excavator, screenRectangle, roadPosition);
                    break;
                case "I":
                    createdObject = new Car(Car.CarType.Forklift, screenRectangle, roadPosition);
                    break;

                case "P":
                    createdObject = new Car(Car.CarType.PatrolCar, screenRectangle, roadPosition);
                    break;
                case "T":
                    if (random.Next(2) == 0)
                    {
                        createdObject = new Car(Car.CarType.Truck, screenRectangle, roadPosition);
                    }
                    else
                    {
                        createdObject = new Car(Car.CarType.MixerTruck, screenRectangle, roadPosition);
                    }
                    break;
                default:
                    createdObject = new Car(Car.CarType.Taxi, screenRectangle, roadPosition);
                    break;
            }
            return createdObject;
        }
    }
}
