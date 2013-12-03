using Microsoft.Kinect;
using System.Diagnostics;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect.Toolkit.Interaction;
using KinectAdapter.Fizbin.Gestures.Segments;
using System;

namespace KinectAdapter.Fizbin.Gestures.Segments
{
    /// <summary>
    /// The first part of the swipe down gesture with the right hand
    /// </summary>
    public class SwipeDownSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {

            //right hand above head and right to head
            if (skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.Head].Position.Y
                && skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.Head].Position.X)
            {
                // right hand is gripped
                if (userInfo.HandPointers[1].HandEventType == InteractionHandEventType.Grip)
                {
                    return GesturePartResult.Succeed;
                }
                return GesturePartResult.Fail;
            }
            return GesturePartResult.Fail;
        }
    }

    /// <summary>
    /// The second part of the swipe down gesture for the right hand
    /// </summary>
    public class SwipeDownSegment2 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            //right hand right to head
            if (skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.Head].Position.X)
            {
                //right hand below right elbow
                if (skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.ElbowRight].Position.Y)
                {
                    return GesturePartResult.Succeed;
                }
                return GesturePartResult.Pausing;
            }
            return GesturePartResult.Fail;
        }
    }

}

namespace KinectAdapter.Fizbin.Gestures
{
    public class SwipeDownGesture : ICompositeGesture
    {

        string ICompositeGesture.GetGestureName()
        {
            return "SwipeDown";
        }

        IRelativeGestureSegment[] ICompositeGesture.GetGestureSegments()
        {
            IRelativeGestureSegment[] SwipeDownSegments = new IRelativeGestureSegment[2];
            SwipeDownSegments[0] = new SwipeDownSegment1();
            SwipeDownSegments[1] = new SwipeDownSegment2();
            return SwipeDownSegments;
        }
    }
}