using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KinectAdapter.Interfaces;
using XBMC;
using System.Diagnostics;
using KinectAdapter.Models;

namespace KinectAdapter.XBMC
{
    public class XbmcCommandSender : INotifybleCommandSender
    {
        #region XBMC Defaults

        public const int XbmcDefaultUdpPort = 9777;
        
        public const string XbmcDefaultHost = "localhost";

        #endregion

        #region members
        protected EventClient _eventClient;
        private string _host;
        private int _port;
        #endregion



        public XbmcCommandSender(string host = XbmcDefaultHost, int UdpPort = XbmcDefaultUdpPort)
        {
            _host = host;
            _port = UdpPort;
            _eventClient = new EventClient();
            Initialize(_host, _port);
        }

        /// <summary>
        /// Try to initialize connection with XMBC Server. 
        /// If the connection fails, throw an exception.
        /// </summary>
        /// <param name="host">host to connect to</param>
        /// <param name="UdpPort">Udp Port to conenct to</param>
        private bool Initialize(string host, int UdpPort) 
        {
            
            bool success = false ;
            try
            {

                success = _eventClient.Connect(host, UdpPort);
                success = _eventClient.SendPing() && success;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception reoccurred when trying to connect with XBMC Client: " + ex.Message);
                Debug.WriteLine(ex.StackTrace);
                Debug.WriteLine("Failed to connect to XBMC. A retry will be made on each command send attempt");
            }
            if (CommandSenderStatusChanged != null)
                CommandSenderStatusChanged(this,new CommandSenderEventArgs(success));
            return success;
        }

        #region ICommandSender Implementation

        /// <summary>
        /// Use the inherited SendAction command of the XBMC Client
        /// </summary>
        /// <param name="ActionId"></param>
        void ICommandSender.SendCommand(XbmcCommand ActionId)
        {
            if (!_eventClient.Connected && !Initialize(_host,_port))
            {
                Debug.WriteLine("XBMC Disconnected. Command will not be sent!");
                return;
            }
                
            switch (ActionId.CommandType)
            {
                case CommandType.ActionCommand: _eventClient.SendAction(ActionId.CommandId);
                    break;
                case CommandType.KbCommand: _eventClient.SendButton(ActionId.CommandId, "KB", ButtonFlagsType.BTN_DOWN | ButtonFlagsType.BTN_NO_REPEAT);
                    break;
                case CommandType.MouseCommand:
                    string[] args = ActionId.CommandId.Split(',');
                    _eventClient.SendMouse(UInt16.Parse(args[0]), UInt16.Parse(args[1]));
                    break;
            }
        }

        /// <summary>
        /// Use the XBMC Client to send a notification MessageBox 
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="actionId"></param>
        void ICommandSender.SendNotification(string caption, string actionId)
        {
            if (!_eventClient.Connected && !Initialize(_host, _port))
            {
                Debug.WriteLine("XBMC Disconnected. Command will not be sent!");
                return;
            }
            _eventClient.SendNotification(caption, actionId);
        }

        public event EventHandler<CommandSenderEventArgs> CommandSenderStatusChanged;


        #endregion

        
    }
}
