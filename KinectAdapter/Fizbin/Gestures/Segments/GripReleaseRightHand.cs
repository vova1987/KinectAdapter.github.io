using Microsoft.Kinect;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect.Toolkit.Interaction;
using KinectAdapter.Fizbin.Gestures.Segments;

namespace KinectAdapter.Fizbin.Gestures.Segments
{
    /// <summary>
    /// Grip Release first time
    /// </summary>
    public class GripReleaseRightHandSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            // right hand between shoulders
            if (skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X
                && skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ShoulderLeft].Position.X)
            {
                // right hand below head height
                if (skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
                {
                    // right hand far from center of shoulders in the z axis
                    if (skeleton.Joints[JointType.ShoulderCenter].Position.Z - skeleton.Joints[JointType.HandRight].Position.Z > 0.5)
                    {
                        //right hand grip release
                        if (userInfo.HandPointers[1].HandEventType == InteractionHandEventType.GripRelease)
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
    public class GripReleaseRightHand : ICompositeGesture
    {
        string ICompositeGesture.GetGestureName()
        {
            return "GripReleaseRightHand";
        }

        IRelativeGestureSegment[] ICompositeGesture.GetGestureSegments()
        {
            IRelativeGestureSegment[] GripReleaseRightHand = new IRelativeGestureSegment[1];
            GripReleaseRightHand[0] = new GripReleaseRightHandSegment1();
            return GripReleaseRightHand;
        }
    }
}
