using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KinectAdapter.Interfaces;
using System.Xml.Serialization;
using KinectAdapter.Models;
using System.IO;

namespace KinectAdapter
{
    /// <summary>
    /// This is the main class.
    /// Given a list of gesture detectors, the adapter wires each detected gesture to the corresponding Action defined the in XML configuration file.
    /// The Action is then sent using the provided CommandSender
    /// </summary>
    public class GestureToCommandAdapter
    {
        #region members
        ICommandSender _commandSender;
        IGestureDetector[] _gestureDetectors;
        Dictionary<string, IList<XbmcCommand>> _gestureToCommandMap;
        #endregion

        //For debugging
        public static readonly bool ShowNotification = true;

        public GestureToCommandAdapter(string GestureFilePath,ICommandSender sender, params IGestureDetector[] detectors )
        {
            _commandSender = sender;
            _gestureDetectors = detectors;
            _gestureToCommandMap = new Dictionary<string, IList<XbmcCommand>>();
            foreach(var detector in _gestureDetectors)
                detector.GestureDetected += OnGestureRecognized;
            LoadAndRegisterGestures(GestureFilePath);
        }

        /// <summary>
        /// When a gesture is recognized, send the corresponding Actions using the CommandSender
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGestureRecognized(object sender, GestureArgs e)
        {
            //Send all commands associated with the recognized gesture
            if (_gestureToCommandMap.ContainsKey(e.GestureId))
            {
                foreach (var command in _gestureToCommandMap[e.GestureId])
                {
                    _commandSender.SendCommand(command);
                    if (ShowNotification)
                        _commandSender.SendNotification("Gesture Recognized", e.GestureId);
                }
            }
        }

        /// <summary>
        /// Parse Configuration file
        /// </summary>
        /// <param name="GestureFilePath">The path tot he Configuration file</param>
        private void LoadAndRegisterGestures(string GestureFilePath)
        {
            //Load gestures from file
            XmlSerializer xmlser = new XmlSerializer(typeof(List<GestureCommandPair>));
            List<GestureCommandPair> gestureToCommandList;
            using (StreamReader srdr = new StreamReader(GestureFilePath))
            {
                gestureToCommandList = (List<GestureCommandPair>)xmlser.Deserialize(srdr);
            }

            if (gestureToCommandList != null)
            {
                //Register gestures with Detectors
                foreach (var detector in _gestureDetectors)
                {
                    var gestures = from pair in gestureToCommandList
                                   where pair.Gesture.GestureType == detector.GestureType
                                   select pair.Gesture;
                    try
                    {
                        detector.RegisterGestures(gestures);
                    }
                    catch (GestureNotSupportedException ex)
                    {
                        //Throw exception if a gesture is not supported by the detector
                        throw new ApplicationException(ex.Message);
                    }
                    
                }
                //Populate Gesture to Command Dictionary
                foreach (var pair in gestureToCommandList)
                {
                    
                    if (!_gestureToCommandMap.ContainsKey(pair.Gesture.GestureId))
                        _gestureToCommandMap[pair.Gesture.GestureId] = new List<XbmcCommand>();
                    _gestureToCommandMap[pair.Gesture.GestureId].Add(pair.Command);
                }
            }
                
        }
    }
}
