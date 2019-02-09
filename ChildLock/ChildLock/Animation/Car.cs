using System;
using System.Drawing;

namespace ChildLock.Animation
{
    public class Car : AnimationObject
    {
        public enum CarType
        {
            PatrolCar,
            Ambulance,
            FireEngine,
            Bus,
            MixerTruck,
            SchoolBus,
            Taxi,
            Truck,
            Bulldozer,
            Excavator,
            Forklift,
        }

        /// <summary>
        /// 反転表示の場合はtrue.
        /// </summary>
        private bool isReversed;
        /// <summary>
        /// 車の画像.
        /// </summary>
        private Bitmap carBitmap;
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
        /// <param name="type">車の種類</param>
        /// <param name="screenRectangle">スクリーンサイズ</param>
        /// <param name="roadPosition">道路の位置</param>
        public Car(CarType type, Rectangle screenRectangle, int roadPosition)
        {
            Random random = new Random();
            isReversed = random.Next(2) == 0;

            this.screenRectangle = screenRectangle;

            int maxMoveSpeed;
            switch (type)
            {
                case CarType.PatrolCar:
                    carBitmap = Properties.Resources.PatrolCar;
                    maxMoveSpeed = 120;
                    break;
                case CarType.Ambulance:
                    carBitmap = Properties.Resources.Ambulance;
                    maxMoveSpeed = 100;
                    break;
                case CarType.FireEngine:
                    carBitmap = Properties.Resources.FireEngine;
                    maxMoveSpeed = 100;
                    break;
                case CarType.Bus:
                    carBitmap = Properties.Resources.Bus;
                    maxMoveSpeed = 60;
                    break;
                case CarType.MixerTruck:
                    carBitmap = Properties.Resources.MixerTruck;
                    maxMoveSpeed = 60;
                    break;
                case CarType.SchoolBus:
                    carBitmap = Properties.Resources.SchoolBus;
                    maxMoveSpeed = 60;
                    break;
                case CarType.Taxi:
                    carBitmap = Properties.Resources.Taxi;
                    maxMoveSpeed = 60;
                    break;
                case CarType.Truck:
                    carBitmap = Properties.Resources.Truck;
                    maxMoveSpeed = 60;
                    break;
                case CarType.Bulldozer:
                    carBitmap = Properties.Resources.Bulldozer;
                    maxMoveSpeed = 40;
                    break;
                case CarType.Excavator:
                    carBitmap = Properties.Resources.Excavator;
                    maxMoveSpeed = 40;
                    break;
                case CarType.Forklift:
                    carBitmap = Properties.Resources.Forklift;
                    maxMoveSpeed = 30;
                    break;
                default:
                    carBitmap = Properties.Resources.Taxi;
                    maxMoveSpeed = 60;
                    break;
            }

            if (isReversed)
            {
                sourceRectangle = new Rectangle(carBitmap.Width, 0, -carBitmap.Width, carBitmap.Height);
                destRectangle = new Rectangle(0, roadPosition - carBitmap.Height, carBitmap.Width, carBitmap.Height);
                moveSpeed = maxMoveSpeed / random.Next(1, 10);
            }
            else
            {
                sourceRectangle = new Rectangle(0, 0, carBitmap.Width, carBitmap.Height);
                destRectangle = new Rectangle(screenRectangle.Width, roadPosition - carBitmap.Height, carBitmap.Width, carBitmap.Height);
                moveSpeed = -maxMoveSpeed / random.Next(1, 10);
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
            graphics.DrawImage(carBitmap, destRectangle, sourceRectangle, GraphicsUnit.Pixel);
        }
    }
}
