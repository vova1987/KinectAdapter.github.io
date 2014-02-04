using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Kinect;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect.Toolkit.Interaction;
using KinectAdapter.Fizbin.Gestures.Segments;

namespace KinectAdapter.Fizbin.Gestures.Segments
{

    public class PushLeftForwardSegment0 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {

            // left hand is pressed and to the left of left shoulder
            if (userInfo.HandPointers[0].IsPressed
                && skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ShoulderLeft].Position.X)
            {
                return GesturePartResult.Succeed;
            }
            return GesturePartResult.Fail;
        }
    }

    public class PushLeftForwardSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {

            // left hand is not pressed and to the left of left shoulder
            if (!userInfo.HandPointers[0].IsPressed
                && skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ShoulderLeft].Position.X)
            {
                return GesturePartResult.Succeed;
            }
            return GesturePartResult.Pausing;
        }
    }
}

namespace KinectAdapter.Fizbin.Gestures
{
    public class PushLeftForwardGesture : ICompositeGesture
    {

        string ICompositeGesture.GetGestureName()
        {
            return "PushLeftForward";
        }

        IRelativeGestureSegment[] ICompositeGesture.GetGestureSegments()
        {
            IRelativeGestureSegment[] pushLeftForwardSegments = new IRelativeGestureSegment[2];
            pushLeftForwardSegments[0] = new PushLeftForwardSegment0();
            pushLeftForwardSegments[1] = new PushLeftForwardSegment1();
            return pushLeftForwardSegments;
        }
    }
}