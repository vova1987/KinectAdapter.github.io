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
    public class PushLeftForwardSegment1 : IRelativeGestureSegment
    {
         /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {

            // left hand between shoulders, right is not
            if (skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X
                && skeleton.Joints[JointType.HandLeft].Position.X > skeleton.Joints[JointType.ShoulderLeft].Position.X
                && skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ShoulderRight].Position.X)
            {
                // left hand below head height
                if (skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
                {
                    // left hand close to center of shoulders in the z axis
                    if (skeleton.Joints[JointType.ShoulderCenter].Position.Z - skeleton.Joints[JointType.HandLeft].Position.Z < 0.3)
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

    public class PushLeftForwardSegment2 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {

            // left hand between shoulders. right is not
            if (skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X
                && skeleton.Joints[JointType.HandLeft].Position.X > skeleton.Joints[JointType.ShoulderLeft].Position.X
                && skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ShoulderRight].Position.X)
            {
                // left hand below head height 
                if (skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
                {
                    // left hand far from center of shoulders in the z axis
                    if (skeleton.Joints[JointType.ShoulderCenter].Position.Z - skeleton.Joints[JointType.HandLeft].Position.Z > 0.5)
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
    public class PushLeftForwardGesture : ICompositeGesture
    {

        string ICompositeGesture.GetGestureName()
        {
            return "PushLeftForward";
        }

        IRelativeGestureSegment[] ICompositeGesture.GetGestureSegments()
        {
            IRelativeGestureSegment[] pushLeftForwardSegments = new IRelativeGestureSegment[2];
            pushLeftForwardSegments[0] = new PushLeftForwardSegment1();
            pushLeftForwardSegments[1] = new PushLeftForwardSegment2();
            return pushLeftForwardSegments;
        }
    }
}