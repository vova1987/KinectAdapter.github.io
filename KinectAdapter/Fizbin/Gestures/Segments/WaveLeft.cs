﻿using Microsoft.Kinect;
using System.Diagnostics;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect.Toolkit.Interaction;
using KinectAdapter.Fizbin.Gestures.Segments;

namespace KinectAdapter.Fizbin.Gestures.Segments
{

    public class WaveLeftSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo UserInfo = null)
        {
            //left elbow is aporx. left of left shoulder
            if (skeleton.Joints[JointType.ElbowLeft].Position.X - skeleton.Joints[JointType.ShoulderLeft].Position.X < -0.1)
            {
                // hand above elbow and left of head
                if (skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.ElbowLeft].Position.Y
                    && skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.Head].Position.X)
                {
                    // hand right of elbow
                    if (skeleton.Joints[JointType.HandLeft].Position.X - skeleton.Joints[JointType.ElbowLeft].Position.X > 0.065)
                    {
                        return GesturePartResult.Succeed;
                    }

                    // hand has not dropped but is not quite where we expect it to be, pausing till next frame
                    return GesturePartResult.Pausing;
                }

                // hand dropped - no gesture fails
                return GesturePartResult.Fail;
            }
            return GesturePartResult.Fail;
        }
    }

    public class WaveLeftSegment2 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo UserInfo = null)
        {
            //left elbow is aporx. left of left shoulder
            if (skeleton.Joints[JointType.ElbowLeft].Position.X - skeleton.Joints[JointType.ShoulderLeft].Position.X < -0.1)
            {
                // hand above elbow and left of head
                if (skeleton.Joints[JointType.HandLeft].Position.Y > skeleton.Joints[JointType.ElbowLeft].Position.Y
                    && skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.Head].Position.X)
                {
                    // hand left of elbow and left shoulder
                    if (skeleton.Joints[JointType.HandLeft].Position.X - skeleton.Joints[JointType.ElbowLeft].Position.X < -0.065)
                    {
                        return GesturePartResult.Succeed;
                    }

                    // hand has not dropped but is not quite where we expect it to be, pausing till next frame
                    return GesturePartResult.Pausing;
                }

                // hand dropped - no gesture fails
                return GesturePartResult.Fail;
            }
            return GesturePartResult.Fail;
        }
    }

}

namespace KinectAdapter.Fizbin.Gestures
{
    public class WaveLeftGesture : ICompositeGesture
    {

        string ICompositeGesture.GetGestureName()
        {
            return "WaveLeft";
        }

        IRelativeGestureSegment[] ICompositeGesture.GetGestureSegments()
        {
            IRelativeGestureSegment[] waveLeftSegments = new IRelativeGestureSegment[3];
            waveLeftSegments[0] = new WaveLeftSegment1();
            waveLeftSegments[1] = new WaveLeftSegment2();
            waveLeftSegments[2] = new WaveLeftSegment1();
            return waveLeftSegments;
        }
    }
}
