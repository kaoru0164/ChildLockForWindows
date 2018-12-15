using System.Drawing;

namespace ChildLock.Animation
{
    class AnimationObjectFactory
    {
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
