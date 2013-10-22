using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KinectAdapter.Interfaces;
using XBMC;
using System.Diagnostics;

namespace KinectAdapter.XBMC
{
    public class XbmcCommandSender : ICommandSender
    {
        #region XBMC Defaults

        public const int XbmcDefaultUdpPort = 9777;
        
        public const string XbmcDefaultHost = "localhost";

        #endregion

        #region members
        EventClient _eventClient;
        #endregion

        public XbmcCommandSender(string host = XbmcDefaultHost, int UdpPort = XbmcDefaultUdpPort)
        {
            _eventClient = new EventClient();
            Initialize(host, UdpPort);
        }

        /// <summary>
        /// Try to initialize connection with XMBC Server. 
        /// If the connection fails, throw an exception.
        /// </summary>
        /// <param name="host">host to connect to</param>
        /// <param name="UdpPort">Udp Port to conenct to</param>
        private void Initialize(string host, int UdpPort) 
        {
            
            bool success = true ;
            try
            {

                success = _eventClient.Connect(host, UdpPort);
                success = _eventClient.SendPing() && success;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception occcured when trying to connect with XBMC Client: " + ex.Message);
                Debug.WriteLine(ex.StackTrace);
            }
            if(!success)
                throw new ApplicationException("Connection with XBMC Application failed");
        }

        #region ICommandSender Implementation

        /// <summary>
        /// Use the inherited SendAction command of the XBMC Client
        /// </summary>
        /// <param name="ActionId"></param>
        void ICommandSender.SendCommand(string ActionId)
        {
            _eventClient.SendAction(ActionId);
        }

        /// <summary>
        /// Use the XBMC Client to send a notification MessageBox 
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="actionId"></param>
        void ICommandSender.SendNotification(string caption, string actionId)
        {
            _eventClient.SendNotification(caption, actionId);
        }

        #endregion

        
    }
}
