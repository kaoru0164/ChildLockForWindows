using System;
using System.Drawing;
using System.Collections.Generic;

namespace ChildLock.Animation
{
    class AnimationObjectFactory
    {
        /// <summary>
        /// アニメーションオブジェクト生成のデリゲート.
        /// </summary>
        /// <returns>アニメーションオブジェクト</returns>
        private delegate AnimationObject CreateAnimationObjectDelegate();
        /// <summary>
        /// アニメーションオブジェクトの生成リスト.
        /// </summary>
        private List<CreateAnimationObjectDelegate> animationObjectCreatorList;

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

            animationObjectCreatorList = new List<CreateAnimationObjectDelegate>();
            animationObjectCreatorList.Add(CreateAirplane);
            animationObjectCreatorList.Add(CreateUfo);
            animationObjectCreatorList.Add(CreateCloud);
            animationObjectCreatorList.Add(CreateChicken);
            animationObjectCreatorList.Add(CreateSubmarine);
            animationObjectCreatorList.Add(CreateAmbulance);
            animationObjectCreatorList.Add(CreateFireEngine);
            animationObjectCreatorList.Add(CreatePatrolCar);
            animationObjectCreatorList.Add(CreateTaxi);
            animationObjectCreatorList.Add(CreateBus);
            animationObjectCreatorList.Add(CreateTruck);
            animationObjectCreatorList.Add(CreateSpecialVehicle);

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
            if (key.Equals("Z"))
            {
                return new Chicken(screenRectangle, groundPosition);
            }


            return animationObjectCreatorList[random.Next(animationObjectCreatorList.Count)]();
        }

        // ここから下はアニメーションオブジェクト生成用メソッド群(Delegateで使用)
        public AnimationObject CreateAirplane()
        {
            return new Airplane(screenRectangle, roadPosition);
        }

        public AnimationObject CreateUfo()
        {
            return new Ufo(screenRectangle, roadPosition);
        }

        public AnimationObject CreateCloud()
        {
            return new Cloud(screenRectangle);
        }

        public AnimationObject CreateChicken()
        {
            return new Chicken(screenRectangle, groundPosition);
        }

        public AnimationObject CreateSubmarine()
        {
            return new Submarine(screenRectangle, groundPosition);
        }

        public AnimationObject CreateAmbulance()
        {
            return new Car(Car.CarType.Ambulance, screenRectangle, roadPosition);
        }

        public AnimationObject CreateFireEngine()
        {
            return new Car(Car.CarType.FireEngine, screenRectangle, roadPosition);
        }

        public AnimationObject CreatePatrolCar()
        {
            return new Car(Car.CarType.PatrolCar, screenRectangle, roadPosition);
        }

        public AnimationObject CreateTaxi()
        {
            return new Car(Car.CarType.Taxi, screenRectangle, roadPosition);
        }

        public AnimationObject CreateBus()
        {
            AnimationObject createdObject;
            if (random.Next(2) == 0)
            {
                createdObject = new Car(Car.CarType.Bus, screenRectangle, roadPosition);
            }
            else
            {
                createdObject = new Car(Car.CarType.SchoolBus, screenRectangle, roadPosition);
            }
            return createdObject;
        }
        
        public AnimationObject CreateTruck()
        {
            AnimationObject createdObject;
            if (random.Next(2) == 0)
            {
                createdObject = new Car(Car.CarType.Truck, screenRectangle, roadPosition);
            }
            else
            {
                createdObject = new Car(Car.CarType.MixerTruck, screenRectangle, roadPosition);
            }
            return createdObject;
        }

        public AnimationObject CreateSpecialVehicle()
        {
            AnimationObject createdObject;
            switch (random.Next(3))
            {
                case 0:
                    createdObject = new Car(Car.CarType.Forklift, screenRectangle, roadPosition);
                    break;
                case 1:
                    createdObject = new Car(Car.CarType.Excavator, screenRectangle, roadPosition);
                    break;
                default:
                    createdObject = new Car(Car.CarType.Bulldozer, screenRectangle, roadPosition);
                    break;
            }
            return createdObject;
        }

    }
}
