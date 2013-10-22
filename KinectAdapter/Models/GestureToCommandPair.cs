using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectAdapter.Models
{
    /// <summary>
    /// Model Class to represent a pair: Gesture and Command
    /// </summary>
    public class GestureCommandPair
    {
        public KinectGesture Gesture { get; set; }

        public string Command { get; set; }
    }

    
}
