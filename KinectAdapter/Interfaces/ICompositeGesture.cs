using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fizbin.Kinect.Gestures;

namespace KinectAdapter.Fizbin.Gestures
{
    /// <summary>
    /// An interface that represents a Composite gesture consisting of multiple Segments.
    /// </summary>
    public interface ICompositeGesture
    {

        string GetGestureName();

        IRelativeGestureSegment[] GetGestureSegments();
    }
}
