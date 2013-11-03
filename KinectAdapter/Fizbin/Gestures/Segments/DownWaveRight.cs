using Microsoft.Kinect;
using System.Diagnostics;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect.Toolkit.Interaction;
using KinectAdapter.Fizbin.Gestures.Segments;

namespace KinectAdapter.Fizbin.Gestures.Segments
{
    public class DownWaveRightSegment0 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo UserInfo = null)
        {
            // hand below elbow and right of head and below head
            if (skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.ElbowRight].Position.Y
                && skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.Head].Position.X
                && skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
            {
                    // left hand is gripped.
                    if (UserInfo.HandPointers[0].HandEventType == InteractionHandEventType.Grip)
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

    public class DownWaveRightSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo UserInfo = null)
        {
            // hand below elbow and right of head and below head
            if (skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.ElbowRight].Position.Y
                && skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.Head].Position.X
                && skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
            {
                // hand left of elbow
                if (skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ElbowRight].Position.X)
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

    public class DownWaveRightSegment2 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo UserInfo = null)
        {
            // hand below elbow and right of head and below head
            if (skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.ElbowRight].Position.Y
                && skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.Head].Position.X
                && skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
            {
                // hand right of elbow
                if (skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ElbowRight].Position.X)
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
    public class DownWaveRightGesture : ICompositeGesture
    {

        string ICompositeGesture.GetGestureName()
        {
            return "DownWaveRight";
        }

        IRelativeGestureSegment[] ICompositeGesture.GetGestureSegments()
        {
            IRelativeGestureSegment[] downWaveRightSegments = new IRelativeGestureSegment[3];
            downWaveRightSegments[0] = new DownWaveRightSegment0();
            downWaveRightSegments[1] = new DownWaveRightSegment1();
            downWaveRightSegments[2] = new DownWaveRightSegment2();
            return downWaveRightSegments;
        }
    }
}