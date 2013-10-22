using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectAdapter.Models
{
    public class KinectGesture
    {
        public string GestureName { get; set; }

        public GestureType GestureType { get; set; }
    }

    public enum GestureType
    {
        Physical,
        Voice
    }

}
