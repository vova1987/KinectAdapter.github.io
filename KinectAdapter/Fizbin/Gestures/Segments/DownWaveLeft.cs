using Microsoft.Kinect;
using System.Diagnostics;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect.Toolkit.Interaction;
using KinectAdapter.Fizbin.Gestures.Segments;

namespace KinectAdapter.Fizbin.Gestures.Segments
{
    public class DownWaveLeftSegment0 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo UserInfo = null)
        {
            // hand below elbow and left of head and below head
            if (skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.ElbowLeft].Position.Y
                && skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.Head].Position.X
                && skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
            {
                    // right hand is gripped.
                    if (UserInfo.HandPointers[1].HandEventType == InteractionHandEventType.Grip)
                    {
                        return GesturePartResult.Succeed;
                    }

                // hand has not dropped but is not quite where we expect it to be, pausing till next frame
                return GesturePartResult.Pausing;
            }

            // hand dropped - no gesture fails
            return GesturePartResult.Fail;
        }
    }

    public class DownWaveLeftSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo UserInfo = null)
        {
            // hand below elbow and left of head and below head
            if (skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.ElbowLeft].Position.Y
                && skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.Head].Position.X
                && skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
            {
                // hand right of elbow
                if (skeleton.Joints[JointType.HandLeft].Position.X > skeleton.Joints[JointType.ElbowLeft].Position.X)
                {
                    return GesturePartResult.Succeed;
                }

                // hand has not dropped but is not quite where we expect it to be, pausing till next frame
                return GesturePartResult.Pausing;
            }

            // hand dropped - no gesture fails
            return GesturePartResult.Fail;
        }
    }

    public class DownWaveLeftSegment2 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo UserInfo = null)
        {
            // hand below elbow and left of head and below head
            if (skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.ElbowLeft].Position.Y
                && skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.Head].Position.X
                && skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
            {
                // hand left of elbow and left shoulder
                if (skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ElbowLeft].Position.X)
                {
                    return GesturePartResult.Succeed;
                }

                // hand has not dropped but is not quite where we expect it to be, pausing till next frame
                return GesturePartResult.Pausing;
            }

            // hand dropped - no gesture fails
            return GesturePartResult.Fail;
        }
    }

}

namespace KinectAdapter.Fizbin.Gestures
{
    public class DownWaveLeftGesture : ICompositeGesture
    {

        string ICompositeGesture.GetGestureName()
        {
            return "DownWaveLeft";
        }

        IRelativeGestureSegment[] ICompositeGesture.GetGestureSegments()
        {
            IRelativeGestureSegment[] downWaveLeftSegments = new IRelativeGestureSegment[3];
            downWaveLeftSegments[0] = new DownWaveLeftSegment0();
            downWaveLeftSegments[1] = new DownWaveLeftSegment1();
            downWaveLeftSegments[2] = new DownWaveLeftSegment2();
            return downWaveLeftSegments;
        }
    }
}
