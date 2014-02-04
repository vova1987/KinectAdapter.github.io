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

    public class CircleLeftHandSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            //left Hand Above left Elbow
            if (skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.ElbowLeft].Position.Y)
            {
                //Left Hand left and below the head
                if (skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.Head].Position.X
                    && skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
                {
                    return GesturePartResult.Succeed;
                }
                return GesturePartResult.Fail;    
            }
            return GesturePartResult.Fail;      
        }
    }


    public class CircleLeftHandSegment2 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            //Left Hand Above left Elbow
            if (skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.ElbowLeft].Position.Y)
            {
                //Left Hand left and above the head
                if (skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.Head].Position.X
                    && skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.Head].Position.Y)
                {
                    return GesturePartResult.Succeed;
                }
                return GesturePartResult.Pausing;
            }
            return GesturePartResult.Fail;      
        }
    }


    public class CircleLeftHandSegment3 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            //Left Hand Above left Elbow
            if (skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.ElbowLeft].Position.Y)
            {
                //Left Hand right and above the head
                if (skeleton.Joints[JointType.HandLeft].Position.X > skeleton.Joints[JointType.Head].Position.X
                    && skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.Head].Position.Y)
                {
                    return GesturePartResult.Succeed;
                }
                return GesturePartResult.Pausing;
            }
            return GesturePartResult.Fail;
        }
    }

    public class CircleLeftHandSegment4 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo userInfo)
        {
            //Left Hand Above left Elbow
            if (skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.ElbowLeft].Position.Y)
            {
                //Left Hand right and below the head
                if (skeleton.Joints[JointType.HandLeft].Position.X > skeleton.Joints[JointType.Head].Position.X
                    && skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
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
    class CircleLeftHand : ICompositeGesture
    {

        public string GetGestureName()
        {
            return "LeftHandCircleHead";
        }

        public IRelativeGestureSegment[] GetGestureSegments()
        {
            IRelativeGestureSegment[] leftHandCircleHead = new IRelativeGestureSegment[4];
            leftHandCircleHead[0] = new CircleLeftHandSegment1();
            leftHandCircleHead[1] = new CircleLeftHandSegment2();
            leftHandCircleHead[2] = new CircleLeftHandSegment3();
            leftHandCircleHead[3] = new CircleLeftHandSegment4();
            return leftHandCircleHead;
        }
    }
}
