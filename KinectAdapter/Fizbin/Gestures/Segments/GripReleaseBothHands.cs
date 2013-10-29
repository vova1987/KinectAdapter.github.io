using Microsoft.Kinect;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect.Toolkit.Interaction;
using KinectAdapter.Fizbin.Gestures.Segments;

//VOVACOOPER 29.10.2013 test
namespace KinectAdapter.Fizbin.Gestures.Segments
{
    /// <summary>
    /// Grip Release first time
    /// </summary>
    public class GripReleaseBothHandsSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            // right/Left hand between shoulders
            if (   skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X
                && skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ShoulderLeft ].Position.X
                && skeleton.Joints[JointType.HandLeft ].Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X
                && skeleton.Joints[JointType.HandLeft ].Position.X > skeleton.Joints[JointType.ShoulderLeft ].Position.X)
            {
                // right/Left hand below head height
                if (skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y
                    && skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
                {
                    // right hand far from center of shoulders in the z axis
                    if (skeleton.Joints[JointType.ShoulderCenter].Position.Z - skeleton.Joints[JointType.HandRight].Position.Z > 0.5
                        && skeleton.Joints[JointType.ShoulderCenter].Position.Z - skeleton.Joints[JointType.HandLeft].Position.Z > 0.5)
                    {                        
                        //Should be enough. 
                        // No need for other options like one grip and other not because microsoft allready checked this
                        if (userInfo.HandPointers[0].HandEventType == InteractionHandEventType.GripRelease
                            && userInfo.HandPointers[1].HandEventType == InteractionHandEventType.GripRelease)
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
    public class GripReleaseBothHands : ICompositeGesture
    {
        string ICompositeGesture.GetGestureName()
        {
            return "GripReleaseBothHands";
        }

        IRelativeGestureSegment[] ICompositeGesture.GetGestureSegments()
        {
            IRelativeGestureSegment[] GripReleaseBothHands = new IRelativeGestureSegment[2];
            GripReleaseBothHands[0] = new GripReleaseBothHandsSegment1();
            GripReleaseBothHands[1] = new GripReleaseBothHandsSegment1();
            return GripReleaseBothHands;
        }
    }
}
