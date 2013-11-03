using Microsoft.Kinect;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect.Toolkit.Interaction;
using KinectAdapter.Fizbin.Gestures.Segments;

namespace KinectAdapter.Fizbin.Gestures.Segments
{
    /// <summary>
    /// Grip Release first time
    /// </summary>
    public class GripReleaseLeftHandSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            // Left hand between shoulders
            if (skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X
                && skeleton.Joints[JointType.HandLeft].Position.X > skeleton.Joints[JointType.ShoulderLeft].Position.X)
            {
                // Left hand below head height
                if (skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
                {
                    // Left hand close to center of shoulders in the z axis
                    if (skeleton.Joints[JointType.ShoulderCenter].Position.Z - skeleton.Joints[JointType.HandLeft].Position.Z > 0.5)
                    {
                        // left hand grip release
                        if (userInfo.HandPointers[0].HandEventType == InteractionHandEventType.GripRelease)
                        {
                            return GesturePartResult.Succeed;
                        }
                        return GesturePartResult.Pausing;
                    }
                    return GesturePartResult.Fail;
                }
                return GesturePartResult.Fail;
            }
            return GesturePartResult.Fail;
        }
    }
}

namespace KinectAdapter.Fizbin.Gestures
{
    public class GripReleaseLeftHand : ICompositeGesture
    {
        string ICompositeGesture.GetGestureName()
        {
            return "GripReleaseLeftHand";
        }

        IRelativeGestureSegment[] ICompositeGesture.GetGestureSegments()
        {
            IRelativeGestureSegment[] GripReleaseLeftHand = new IRelativeGestureSegment[1];
            GripReleaseLeftHand[0] = new GripReleaseLeftHandSegment1();
            return GripReleaseLeftHand;
        }
    }
}
