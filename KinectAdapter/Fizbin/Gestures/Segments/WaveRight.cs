using Microsoft.Kinect;
using System.Diagnostics;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect.Toolkit.Interaction;
using KinectAdapter.Fizbin.Gestures.Segments;

namespace KinectAdapter.Fizbin.Gestures.Segments
{

    public class WaveRightSegment1 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo UserInfo = null)
        {
            //right elbow is aporx. right of right shoulder
            if (skeleton.Joints[JointType.ElbowRight].Position.X - skeleton.Joints[JointType.ShoulderRight].Position.X > 0.1)
            {
                // hand above elbow and right of head
                if (skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y
                    && skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.Head].Position.X)
                {
                    // hand left of elbow
                    if (skeleton.Joints[JointType.HandRight].Position.X - skeleton.Joints[JointType.ElbowRight].Position.X < -0.065)
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

    public class WaveRightSegment2 : IRelativeGestureSegment
    {
        /// <summary>
        /// Checks the gesture.
        /// </summary>
        /// <param name="skeleton">The skeleton.</param>
        /// <returns>GesturePartResult based on if the gesture part has been completed</returns>
        public GesturePartResult CheckGesture(Skeleton skeleton, UserInfo UserInfo = null)
        {
            //right elbow is aporx. right of right shoulder
            if (skeleton.Joints[JointType.ElbowRight].Position.X - skeleton.Joints[JointType.ShoulderRight].Position.X > 0.1)
            {
                // hand above elbow and right of head
                if (skeleton.Joints[JointType.HandRight].Position.Y > skeleton.Joints[JointType.ElbowRight].Position.Y
                    && skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.Head].Position.X)
                {
                    // hand right of elbow
                    if (skeleton.Joints[JointType.HandRight].Position.X - skeleton.Joints[JointType.ElbowRight].Position.X > 0.065)
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
    public class WaveRightGesture : ICompositeGesture
    {

        string ICompositeGesture.GetGestureName()
        {
            return "WaveRight";
        }

        IRelativeGestureSegment[] ICompositeGesture.GetGestureSegments()
        {
            IRelativeGestureSegment[] waveRightSegments = new IRelativeGestureSegment[3];
            waveRightSegments[0] = new WaveRightSegment1();
            waveRightSegments[1] = new WaveRightSegment2();
            waveRightSegments[2] = new WaveRightSegment1();
            return waveRightSegments;
        }
    }
}