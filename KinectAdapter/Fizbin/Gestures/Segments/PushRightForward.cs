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

    public class PushRightForwardSegment0 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {

            // right hand is pressed and to the right of right shoulder
            if (userInfo.HandPointers[1].IsPressed)
                //&& skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ShoulderRight].Position.X)
            {
                return GesturePartResult.Succeed;
            }
            return GesturePartResult.Fail;
        }
    }

    public class PushRightForwardSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {

            // right hand is not pressed and to the right of right shoulder
            if (!userInfo.HandPointers[1].IsPressed)
                //&& skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ShoulderRight].Position.X)
            {
                return GesturePartResult.Succeed;
            }
            return GesturePartResult.Pausing;
        }
    }
}

namespace KinectAdapter.Fizbin.Gestures
{
    public class PushRightForwardGesture : ICompositeGesture
    {

        string ICompositeGesture.GetGestureName()
        {
            return "PushRightForward";
        }

        IRelativeGestureSegment[] ICompositeGesture.GetGestureSegments()
        {
            IRelativeGestureSegment[] pushRightForwardSegments = new IRelativeGestureSegment[2];
            pushRightForwardSegments[0] = new PushRightForwardSegment0();
            pushRightForwardSegments[1] = new PushRightForwardSegment1();
            return pushRightForwardSegments;
        }
    }
}