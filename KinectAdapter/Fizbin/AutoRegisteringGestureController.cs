using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fizbin.Kinect.Gestures;
using System.Reflection;
using KinectAdapter.Interfaces;
using KinectAdapter.Fizbin.Gestures;

namespace KinectAdapter.Fizbin
{
    public class AutoRegisteringGestureController : GestureController
    {
        public AutoRegisteringGestureController()
            : base() { AutoRegisterCompositeGestures(); }

        

        /// <summary>
        /// AutoRegister all types that implement the ICompositeGesture Interface
        /// </summary>
        protected void AutoRegisterCompositeGestures()
        {
            System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.Namespace == "KinectAdapter.Fizbin.Gestures" && t.GetInterfaces().Contains(typeof(ICompositeGesture)))
            .ToList()
            .ForEach(t =>
            {
                var obj = Activator.CreateInstance(t) as ICompositeGesture;
                this.AddGesture(obj.GetGestureName(), obj.GetGestureSegments());
            });
        }



       

        
    }
}
