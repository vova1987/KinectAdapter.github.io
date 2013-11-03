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
    public class PushBothForwardSegment1 : IRelativeGestureSegment
    {
         /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {

            // both hands between shoulders
            if (skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X
                && skeleton.Joints[JointType.HandLeft].Position.X > skeleton.Joints[JointType.ShoulderLeft].Position.X
                && skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X
                && skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ShoulderLeft].Position.X)
            {
                // both hands below head height
                if (skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.Head].Position.Y
                    && skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
                {
                    // both hands close to center of shoulders in the z axis
                    if (skeleton.Joints[JointType.ShoulderCenter].Position.Z - skeleton.Joints[JointType.HandLeft].Position.Z < 0.3
                        && skeleton.Joints[JointType.ShoulderCenter].Position.Z - skeleton.Joints[JointType.HandRight].Position.Z < 0.3)
                    {
                        return GesturePartResult.Succeed;
                    }
                    return GesturePartResult.Fail;
                }
                return GesturePartResult.Fail;
            }
            return GesturePartResult.Fail;
        }
    }

    public class PushBothForwardSegment2 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {

            // both hands between shoulders
            if (skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X
                && skeleton.Joints[JointType.HandLeft].Position.X > skeleton.Joints[JointType.ShoulderLeft].Position.X
                && skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X
                && skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ShoulderLeft].Position.X)
            {
                // both hands below head height 
                if (skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.Head].Position.Y
                    && skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
                {
                    // both hands far from center of shoulders in the z axis
                    if (skeleton.Joints[JointType.ShoulderCenter].Position.Z - skeleton.Joints[JointType.HandLeft].Position.Z > 0.5
                        && skeleton.Joints[JointType.ShoulderCenter].Position.Z - skeleton.Joints[JointType.HandRight].Position.Z > 0.5)
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
    public class PushBothForwardGesture : ICompositeGesture
    {

        string ICompositeGesture.GetGestureName()
        {
            return "PushBothForward";
        }

        IRelativeGestureSegment[] ICompositeGesture.GetGestureSegments()
        {
            IRelativeGestureSegment[] pushBothForwardSegments = new IRelativeGestureSegment[2];
            pushBothForwardSegments[0] = new PushBothForwardSegment1();
            pushBothForwardSegments[1] = new PushBothForwardSegment2();
            return pushBothForwardSegments;
        }
    }
}