using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fizbin.Kinect.Gestures;
using KinectAdapter.Fizbin.Gestures.Segments;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit.Interaction;

namespace KinectAdapter.Fizbin.Gestures.Segments
{
    /// <summary>
    /// Grip Release first time
    /// </summary>
    public class CircleRightHandSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            //Right Hand Above right Elbow
            if (skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y)
            {
                //Right Hand right and below the head
                if (skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.Head].Position.X
                    && skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
                {
                    return GesturePartResult.Succeed;
                }
                return GesturePartResult.Fail;    
            }
            return GesturePartResult.Fail;      
        }
    }

    /// <summary>
    /// Right and above head
    /// </summary>
    public class CircleRightHandSegment2 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            //Right Hand Above right Elbow
            if (skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y)
            {
                //Right Hand right and above the head
                if (skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.Head].Position.X
                    && skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.Head].Position.Y)
                {
                    return GesturePartResult.Succeed;
                }
                return GesturePartResult.Pausing;
            }
            return GesturePartResult.Fail;      
        }
    }

    /// <summary>
    /// left and above head
    /// </summary>
    public class CircleRightHandSegment3 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            //Right Hand Above right Elbow
            if (skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y)
            {
                //Right Hand left and above the head
                if (skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.Head].Position.X
                    && skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.Head].Position.Y)
                {
                    return GesturePartResult.Succeed;
                }
                return GesturePartResult.Pausing;
            }
            return GesturePartResult.Fail;
        }
    }

    /// <summary>
    /// left and below head
    /// </summary>
    public class CircleRightHandSegment4 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            //Right Hand Above right Elbow
            if (skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y)
            {
                //Right Hand left and below the head
                if (skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.Head].Position.X
                    && skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
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
    class CircleRightHand : ICompositeGesture
    {

        public string GetGestureName()
        {
            return "RightHandCircleHead";
        }

        public IRelativeGestureSegment[] GetGestureSegments()
        {
            IRelativeGestureSegment[] rightHandCircleHead = new IRelativeGestureSegment[4];
            rightHandCircleHead[0] = new CircleRightHandSegment1();
            rightHandCircleHead[1] = new CircleRightHandSegment2();
            rightHandCircleHead[2] = new CircleRightHandSegment3();
            rightHandCircleHead[3] = new CircleRightHandSegment4();
            return rightHandCircleHead;
        }
    }
}
