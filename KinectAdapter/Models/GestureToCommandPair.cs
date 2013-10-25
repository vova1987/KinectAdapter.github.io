using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace KinectAdapter.Models
{
    public enum GestureType
    {
        Physical,
        Voice
    }

    public enum CommandType
    {
        ActionCommand,
        KbCommand,
        MouseCommand
    }


    /// <summary>
    /// Model Class to represent a pair: Gesture and Command
    /// </summary>
    [XmlRoot]
    public class GestureCommandPair
    {
        public KinectGesture Gesture { get; set; }

        public XbmcCommand Command { get; set; }
    }
    [XmlRoot]
    public class KinectGesture
    {
        [XmlAttribute("Type")]
        public GestureType GestureType { get; set; } 
        [XmlText]
        public string GestureId { get; set; } 
    }


    
    [XmlRoot]
    public class XbmcCommand
    {
        [XmlAttribute("Type")]
        public CommandType CommandType { get; set; } 
        [XmlText]
        public string CommandId { get; set; }
    }



    
}
