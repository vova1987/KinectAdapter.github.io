using Microsoft.Kinect;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect.Toolkit.Interaction;
using KinectAdapter.Fizbin.Gestures.Segments;

namespace KinectAdapter.Fizbin.Gestures.Segments
{
    /// <summary>
    /// The first part of the swipe left gesture
    /// </summary>
    public class SwipeLeftSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {

            // right hand in front of right elbow. left hand left of shoulder center
            if (skeleton.Joints[JointType.HandRight].Position.Z < skeleton.Joints[JointType.ElbowRight].Position.Z
                && skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ShoulderCenter].Position.X)
            {
                // right hand below head
                if (skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
                {
                    // right hand right of right shoulder
                    if (skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ShoulderRight].Position.X)
                    {
                        // right hand is gripped.
                        if (userInfo.HandPointers[1].HandEventType == InteractionHandEventType.Grip)
                        {
                            return GesturePartResult.Succeed;
                        }
                        return GesturePartResult.Fail;
                    }
                    return GesturePartResult.Fail;
                }
                return GesturePartResult.Fail;
            }
            return GesturePartResult.Fail;
        }
    }

    /// <summary>
    /// The second part of the swipe left gesture
    /// </summary>
    public class SwipeLeftSegment2 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            // right hand in front of right elbow. left hand left of shoulder center
            if (skeleton.Joints[JointType.HandRight].Position.Z < skeleton.Joints[JointType.ElbowRight].Position.Z
                && skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ShoulderCenter].Position.X)
            {
                // right hand below head
                if (skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
                {
                    // right hand left of right shoulder & right of left shoulder
                    if (skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X
                        && skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ShoulderLeft].Position.X)
                    {
                        return GesturePartResult.Succeed;
                    }
                    return GesturePartResult.Pausing;
                }
                return GesturePartResult.Fail;
            }
            return GesturePartResult.Fail;
        }
    }

    /// <summary>
    /// The third part of the swipe left gesture
    /// </summary>
    public class SwipeLeftSegment3 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            // //Right hand in front of right elbow. left hand below shoulders.
            if (skeleton.Joints[JointType.HandRight].Position.Z < skeleton.Joints[JointType.ElbowRight].Position.Z
                && skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.ShoulderCenter].Position.Y)
            {
                // //right hand below head.
                if (skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
                {
                    // //right hand left of left shoulder
                    if (skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ShoulderLeft].Position.X)
                    {
                        return GesturePartResult.Succeed;
                    }

                    return GesturePartResult.Pausing;
                }

                return GesturePartResult.Fail;
            }

            return GesturePartResult.Fail;
        }
    }
}

namespace KinectAdapter.Fizbin.Gestures
{
    public class SwipeLeftGesture : ICompositeGesture
    {

        string ICompositeGesture.GetGestureName()
        {
            return "SwipeLeft";
        }

        IRelativeGestureSegment[] ICompositeGesture.GetGestureSegments()
        {
            IRelativeGestureSegment[] LeftSwipeSegments = new IRelativeGestureSegment[3];
            LeftSwipeSegments[0] = new SwipeLeftSegment1();
            LeftSwipeSegments[1] = new SwipeLeftSegment2();
            LeftSwipeSegments[2] = new SwipeLeftSegment3();
            return LeftSwipeSegments;
        }
    }
}
