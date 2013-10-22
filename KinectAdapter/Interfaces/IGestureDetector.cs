using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KinectAdapter.Models;

namespace KinectAdapter.Interfaces
{
    /// <summary>
    /// Defines Gesture Detection support
    /// </summary>
    public interface IGestureDetector
    {
        /// <summary>
        /// Occurs when a gesture is recognized
        /// </summary>
        event EventHandler<GestureArgs> GestureDetected;

        /// <summary>
        /// Return true only if thegiven gesture is supported.
        /// </summary>
        /// <param name="gesture"></param>
        /// <returns></returns>
        bool IsGestureSupported(KinectGesture gesture);

        /// <summary>
        /// Get the Type of Gestures detected by this detector
        /// </summary>
        GestureType GestureType { get; } 
    }

    /// <summary>
    /// Thin wrapper of EventArgs to include GestureId
    /// </summary>
    public class GestureArgs : EventArgs
    {
        /// <summary>
        /// Unique identifier of the gesture
        /// </summary>
        public string GestureId { get; set; }

        /// <summary>
        /// The user ID that initiated the gesture. Use this property to distinguish between users.
        /// </summary>
        public int UserId { get; set; }

        public GestureArgs(string gestureName, int userId )
            : base()
        {
            GestureId = gestureName;
            UserId = userId;
        }
    }
}
