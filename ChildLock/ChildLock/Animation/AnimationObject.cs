using System.Drawing;

namespace ChildLock.Animation
{
    public abstract class AnimationObject
    {
        public bool IsFinished
        {
            get;
            protected set;
        }

        public AnimationObject()
        {
            IsFinished = false;
        }

        public abstract void Update();
        public abstract void Draw(Graphics graphics);
    }
}
