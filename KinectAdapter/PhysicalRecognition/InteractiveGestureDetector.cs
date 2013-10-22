using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KinectAdapter.Interfaces;
using KinectAdapter.Fizbin;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit.Interaction;
using System.Diagnostics;
using KinectAdapter.Models;

namespace KinectAdapter.PhysicalRecognition
{
    public class InteractiveGestureDetector : IGestureDetector
    {

        private Skeleton[] skeletons = new Skeleton[0];
        // skeleton gesture recognizer
        private AutoRegisteringGestureController gestureController;


        private KinectSensor _sensor;  //The Kinect Sensor the application will use
        private InteractionStream _interactionStream;

        private Skeleton[] _skeletons; //the skeletons 
        private UserInfo[] _userInfos; //the information about the interactive users


        public InteractiveGestureDetector(KinectSensor sensor)
            : base()
        {
            _sensor = sensor;
            _skeletons = new Skeleton[_sensor.SkeletonStream.FrameSkeletonArrayLength];
            _userInfos = new UserInfo[InteractionFrame.UserInfoArrayLength];

            _sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);

            _sensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;

            _sensor.DepthFrameReady += SensorOnDepthFrameReady;
            _sensor.SkeletonFrameReady += SensorOnSkeletonFrameReady;

            //Interaction stream
            _interactionStream = new InteractionStream(_sensor, new DummyInteractionClient());
            _interactionStream.InteractionFrameReady += InteractionStreamOnInteractionFrameReady;

            _sensor.SkeletonStream.Enable();
            // initialize the gesture recognizer
            gestureController = new AutoRegisteringGestureController();

            gestureController.GestureRecognized += new EventHandler<GestureEventArgs>(gestureController_GestureRecognized);
        }

        void gestureController_GestureRecognized(object sender, GestureEventArgs e)
        {
            if (GestureDetected != null)
                GestureDetected(this, new GestureArgs(e.GestureName, e.TrackingId));
        }

        #region Kinect Events
        private void SensorOnSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs skeletonFrameReadyEventArgs)
        {
            using (SkeletonFrame skeletonFrame = skeletonFrameReadyEventArgs.OpenSkeletonFrame())
            {
                if (skeletonFrame == null)
                    return;

                try
                {
                    skeletonFrame.CopySkeletonDataTo(_skeletons);
                    var accelerometerReading = _sensor.AccelerometerGetCurrentReading();
                    _interactionStream.ProcessSkeleton(_skeletons, accelerometerReading, skeletonFrame.Timestamp);
                }
                catch (InvalidOperationException)
                {
                    // SkeletonFrame functions may throw when the sensor gets
                    // into a bad state.  Ignore the frame in that case.
                }

            }
        }

        private void SensorOnDepthFrameReady(object sender, DepthImageFrameReadyEventArgs depthImageFrameReadyEventArgs)
        {
            using (DepthImageFrame depthFrame = depthImageFrameReadyEventArgs.OpenDepthImageFrame())
            {
                if (depthFrame == null)
                    return;

                try
                {
                    _interactionStream.ProcessDepth(depthFrame.GetRawPixelData(), depthFrame.Timestamp);
                }
                catch (InvalidOperationException)
                {
                    // DepthFrame functions may throw when the sensor gets
                    // into a bad state.  Ignore the frame in that case.
                }
            }
        }

        private void InteractionStreamOnInteractionFrameReady(object sender, InteractionFrameReadyEventArgs args)
        {
            using (var iaf = args.OpenInteractionFrame()) //dispose as soon as possible
            {
                if (iaf == null)
                    return;

                iaf.CopyInteractionDataTo(_userInfos);
            }
            var hasUser = false;
            foreach (var userInfo in _userInfos)
            {
                var userID = userInfo.SkeletonTrackingId;
                if (userID == 0)
                    continue;

                hasUser = true;
                //Update with all skeletons and current user info
                //TODO: Maybe just use the last one?
                //foreach(var skeleton in _skeletons)
                gestureController.UpdateAllGestures(_skeletons.Last(), userInfo);
                
            }
            if (!hasUser)
                Debug.WriteLine("No user detected, skipping frame!");
            
        }
        #endregion

        #region IGestureDetector Implementation
        public event EventHandler<KinectAdapter.Interfaces.GestureArgs> GestureDetected;

        public bool IsGestureSupported(KinectGesture gesture)
        {
            return gesture.GestureType == GestureType.Physical && gestureController.Gestures.Contains(gesture.GestureName);
        }

        public GestureType GestureType { get { return Models.GestureType.Physical; } }
        #endregion
    }
}
