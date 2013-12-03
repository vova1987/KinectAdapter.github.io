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
    public class SwipeUpSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {

            //left hand above head and left to head
            if (skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.Head].Position.Y
                && skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.Head].Position.X)
            {
                // left hand is gripped
                if (userInfo.HandPointers[0].HandEventType == InteractionHandEventType.Grip)
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
    public class SwipeUpSegment2 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            //left hand left to head
            if (skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.Head].Position.X)
            {
                //left hand below left elbow
                if (skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.ElbowLeft].Position.Y)
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
    public class SwipeUpGesture : ICompositeGesture
    {

        string ICompositeGesture.GetGestureName()
        {
            return "SwipeUp";
        }

        IRelativeGestureSegment[] ICompositeGesture.GetGestureSegments()
        {
            IRelativeGestureSegment[] SwipeUpSegments = new IRelativeGestureSegment[2];
            SwipeUpSegments[0] = new SwipeUpSegment1();
            SwipeUpSegments[1] = new SwipeUpSegment2();
            return SwipeUpSegments;
        }
    }
}