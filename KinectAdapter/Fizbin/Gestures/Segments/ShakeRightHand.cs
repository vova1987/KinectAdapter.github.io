using Microsoft.Kinect;
using System.Diagnostics;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect.Toolkit.Interaction;
using KinectAdapter.Fizbin.Gestures.Segments;
using System;

namespace KinectAdapter.Fizbin.Gestures.Segments
{
    /// <summary>
    /// The first part of the ShakeRightHand gesture with the right hand
    /// </summary>
    public class ShakeRightHandSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {

            //left elbow is aporx. left of left shoulder
            if (skeleton.Joints[JointType.ElbowRight].Position.X - skeleton.Joints[JointType.ShoulderRight].Position.X > 0.1)
            {
                // right hand is right to the shoulder center and head.
                if (skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.Head].Position.X
                    && skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ShoulderCenter].Position.X)
                {
                    // right hand below right elbow 
                    if (skeleton.Joints[JointType.HandRight].Position.Y - skeleton.Joints[JointType.ElbowRight].Position.Y < -0.05)
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
    /// The second part of the ShakeRightHand gesture for the right hand
    /// </summary>
    public class ShakeRightHandSegment2 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            //left elbow is aporx. left of left shoulder
            if (skeleton.Joints[JointType.ElbowRight].Position.X - skeleton.Joints[JointType.ShoulderRight].Position.X > 0.1)
            {
                // right hand right of head and shoulder center
                if (skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.Head].Position.X
                    && skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ShoulderCenter].Position.X)
                {
                    //right hand above right elbow
                    if (skeleton.Joints[JointType.HandRight].Position.Y - skeleton.Joints[JointType.ElbowRight].Position.Y > 0.05)
                    {
                        return GesturePartResult.Succeed;
                    }
                    else
                    {
                        return GesturePartResult.Pausing;
                    }
                }
                return GesturePartResult.Fail;
            }
            return GesturePartResult.Fail;
        }
    }

}

namespace KinectAdapter.Fizbin.Gestures
{
    public class ShakeRightHandGesture : ICompositeGesture
    {

        string ICompositeGesture.GetGestureName()
        {
            return "ShakeRightHand";
        }

        IRelativeGestureSegment[] ICompositeGesture.GetGestureSegments()
        {
            IRelativeGestureSegment[] ShakeRightHandSegments = new IRelativeGestureSegment[3];
            ShakeRightHandSegments[0] = new ShakeRightHandSegment2();
            ShakeRightHandSegments[1] = new ShakeRightHandSegment1();
            ShakeRightHandSegments[2] = new ShakeRightHandSegment2();
            return ShakeRightHandSegments;
        }
    }
}