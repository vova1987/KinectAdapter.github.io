using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Kinect.Toolkit;
using Microsoft.Kinect;
using KinectAdapter.Models;
using KinectAdapter.Interfaces;
using KinectAdapter.PhysicalRecognition;
using KinectAdapter.XBMC;
using System.Xml.Serialization;
using System.IO;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace KinectAdapter
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window, INotifyPropertyChanged
    {
        private KinectSensor _kinectSensor;

        public  ObservableCollection<GestureCommandPair> GestureList {get; set;}

        // skeleton gesture recognizer
        private GestureToCommandAdapter _adapter;
        private string _lastDetectedGesture;
        public string GestureFilePath { get; set; }

        public string LastDetectedGesture
        {
            get { return _lastDetectedGesture; }
            set
            {
                if (_lastDetectedGesture == value)
                    return;

                _lastDetectedGesture = value;

                if (this.PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("LastDetectedGesture"));
            }
        }

        public UserWindow()
        {
            GestureFilePath = @"GestureToCommand.XML";
            GestureList = new ObservableCollection<GestureCommandPair>();
            DataContext = this;
            InitializeComponent();
            
            this.kinectChooserEx.KinectChanged += kinectChooserEx_KinectChanged;
        }

        void kinectChooserEx_KinectChanged(object sender, KinectSensorArgs e)
        {
            _kinectSensor = e.Sensor;
            
            kinectStatusText.Text = e.Sensor.Status.ToString();
            //Initialize once a sensor is available!
            Initialize();
        }

        /// <summary>
        /// Initialize Adapter and Kinect Device.
        /// </summary>
        private void Initialize()
        {
            
            
            _kinectSensor.Start();
            kinectStatusText.Text = _kinectSensor.Status.ToString(); //update UI
            IGestureDetector gestureDetector = new InteractiveGestureDetector(_kinectSensor);
            IGestureDetector voiceDetector = new VoiceRecognition.KinectVoiceGestureDetector(_kinectSensor);
            INotifybleCommandSender commandSender = new XbmcCommandSender();
            commandSender.CommandSenderStatusChanged += commandSender_CommandSenderStatusChanged;
            commandSender.SendNotification("Kinect Adapter Connected","");
            _adapter = new GestureToCommandAdapter(GestureFilePath, commandSender, gestureDetector, voiceDetector);
            //Gestures
            gestureDetector.GestureDetected += gestureDetector_GestureDetected;
            //display available gestures
            //Load gestures from file
            XmlSerializer xmlser = new XmlSerializer(typeof(List<GestureCommandPair>));
            using (StreamReader srdr = new StreamReader(GestureFilePath))
            {
                var list  = (IList<GestureCommandPair>)xmlser.Deserialize(srdr);
                foreach (var g in list)
                    GestureList.Add(g);
            }
        }

        void commandSender_CommandSenderStatusChanged(object sender, CommandSenderEventArgs e)
        {
            XbmcStatusText.Text = e.Connected ? "Connected" : "Disconnected";
        }

        /// <summary>
        /// Update last gesture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void gestureDetector_GestureDetected(object sender, Interfaces.GestureArgs e)
        {
            LastDetectedGesture = e.GestureId;
        }

        #region Events

        /// <summary>
        /// Event implementing INotifyPropertyChanged interface.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        

    }
}
