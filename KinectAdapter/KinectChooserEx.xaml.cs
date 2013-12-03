using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect.Toolkit.Controls;

namespace KinectAdapter
{
    /// <summary>
    /// Interaction logic for KinectChooserEx.xaml
    /// </summary>
    public partial class KinectChooserEx : UserControl
    {
        private KinectSensorChooser sensorChooser;
        
        /// <summary>
        /// Called when sensor is available
        /// </summary>
        public event EventHandler<KinectSensorArgs> KinectChanged;

        public KinectChooserEx()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (DesignerProperties.GetIsInDesignMode(this))
                return; 

            this.sensorChooser = new KinectSensorChooser();
            this.sensorChooser.KinectChanged += SensorChooserOnKinectChanged;
            this.sensorChooserUI.KinectSensorChooser = this.sensorChooser;
            this.sensorChooser.Start();
        }

        private void SensorChooserOnKinectChanged(object sender, KinectChangedEventArgs args)
        {
            bool error = false;
            if (args.OldSensor != null)
            {
                try
                {
                    args.OldSensor.DepthStream.Disable();
                    args.OldSensor.SkeletonStream.Disable();
                }
                catch (InvalidOperationException)
                {
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                    error = true;
                }
            }

            if (args.NewSensor != null)
            {
                try
                {
                    args.NewSensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30);
                    args.NewSensor.SkeletonStream.Enable();

                    try
                    {
                        args.NewSensor.SkeletonStream.TrackingMode = SkeletonTrackingMode.Seated;
                    }
                    catch (InvalidOperationException)
                    {
                        // Non Kinect for Windows devices do not support Near mode, so reset back to default mode.
                        args.NewSensor.DepthStream.Range = DepthRange.Default;
                        args.NewSensor.SkeletonStream.EnableTrackingInNearRange = false;
                        error = true;
                    }
                }
                catch (InvalidOperationException)
                {
                    error = true;
                    // KinectSensor might enter an invalid state while enabling/disabling streams or stream features.
                    // E.g.: sensor might be abruptly unplugged.
                }
            }

            if (!error)
            {
                KinectSensor = args.NewSensor;
                SetNearModeDependentValues();
            }
            else
                KinectSensor = null;
        }


        /// <summary>
        /// Whether to configure the Kinect Sensor as read mode
        /// </summary>
        [Description("Whether to configure the Kinect Sensor as read mode")]
        public bool NearMode
        {
            get { return (bool)GetValue(NearModeProperty); }
            set { SetValue(NearModeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for NearMode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NearModeProperty =
            DependencyProperty.Register("NearMode", typeof(bool), typeof(KinectChooserEx), new PropertyMetadata(false, NearModePropertyChangedCallback));

        private static void NearModePropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((KinectChooserEx) dependencyObject).NearModeChanged();
        }

        private void NearModeChanged()
        {
            if (KinectSensor == null)
                return;

            SetNearModeDependentValues();
        }

        private void SetNearModeDependentValues()
        {
            if (KinectSensor == null)
                return;
            try
            {
                KinectSensor.DepthStream.Range = NearMode ? DepthRange.Near : DepthRange.Default;
                KinectSensor.SkeletonStream.EnableTrackingInNearRange = NearMode;
            }
            catch (Exception ex)
            {
                NearMode = false;
                //ON old sensors, near mode is not supported...
                KinectSensor.DepthStream.Range = DepthRange.Default;
                KinectSensor.SkeletonStream.EnableTrackingInNearRange = NearMode;
            }
            
        }

        /// <summary>
        /// The Kinect Sensor
        /// </summary>
        public KinectSensor KinectSensor
        {
            get { return (KinectSensor)GetValue(SensorProperty); }
            private set { SetValue(SensorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for KinectSensor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SensorProperty =
            DependencyProperty.Register("KinectSensor", typeof(KinectSensor), typeof(KinectChooserEx), new PropertyMetadata(null, KinectSensorChangedCallback));

        private static void KinectSensorChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            ((KinectChooserEx) dependencyObject).KinectSensorChanged();
        }

        private void KinectSensorChanged()
        {
            if (KinectChanged != null && KinectSensor!=null)
                this.KinectChanged(this, new KinectSensorArgs(KinectSensor));
        }
    }


    public class KinectSensorArgs : EventArgs
    {
        public KinectSensorArgs(KinectSensor sensor) : base()
        {
            Sensor = sensor;
        }

        public KinectSensor Sensor { get; set; }
    }
}
