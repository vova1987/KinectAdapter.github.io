using Microsoft.Kinect;
using System.Diagnostics;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect.Toolkit.Interaction;
using KinectAdapter.Fizbin.Gestures.Segments;

namespace KinectAdapter.Fizbin.Gestures.Segments
{

    /// <summary>
    /// The first part of the ShakeLeftHand gesture with the right hand
    /// </summary>
    public class ShakeLeftHandSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            //left elbow is aporx. left of left shoulder
            if (skeleton.Joints[JointType.ElbowLeft].Position.X - skeleton.Joints[JointType.ShoulderLeft].Position.X < -0.1)
            {
                // left hand is left to the shoulder center and head.
                if (skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.Head].Position.X
                    && skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ShoulderCenter].Position.X)
                {
                    // left hand below left elbow 
                    if (skeleton.Joints[JointType.HandLeft].Position.Y - skeleton.Joints[JointType.ElbowLeft].Position.Y < -0.05)
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
    /// The second part of the ShakeLeftHand gesture for the right hand
    /// </summary>
    public class ShakeLeftHandSegment2 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            //left elbow is aporx. left of left shoulder
            if (skeleton.Joints[JointType.ElbowLeft].Position.X - skeleton.Joints[JointType.ShoulderLeft].Position.X < -0.1)
            {
                // left hand left of head and shoulder center
                if (skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.Head].Position.X
                    && skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ShoulderCenter].Position.X)
                {
                    //left hand above left elbow
                    if (skeleton.Joints[JointType.HandLeft].Position.Y - skeleton.Joints[JointType.ElbowLeft].Position.Y > 0.05)
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
    public class ShakeLeftHandGesture : ICompositeGesture
    {

        string ICompositeGesture.GetGestureName()
        {
            return "ShakeLeftHand";
        }

        IRelativeGestureSegment[] ICompositeGesture.GetGestureSegments()
        {
            IRelativeGestureSegment[] ShakeLeftHandSegments = new IRelativeGestureSegment[3];
            ShakeLeftHandSegments[0] = new ShakeLeftHandSegment2();
            ShakeLeftHandSegments[1] = new ShakeLeftHandSegment1();
            ShakeLeftHandSegments[2] = new ShakeLeftHandSegment2();
            return ShakeLeftHandSegments;
        }
    }
}