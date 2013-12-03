using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KinectAdapter.Models;

namespace KinectAdapter.Interfaces
{
    public interface ICommandSender
    {
        /// <summary>
        /// Send a command with the given action ID.
        /// </summary>
        /// <param name="ActionId">The command to send</param>
        void SendCommand(XbmcCommand  ActionId);

        /// <summary>
        /// Send a notification
        /// </summary>
        /// <param name="ActionId">The ActionId of the command</param>
        void SendNotification(string caption, string actionId);
    }

    public interface INotifybleCommandSender : ICommandSender
    {
        event EventHandler<CommandSenderEventArgs> CommandSenderStatusChanged;
    }

    public class CommandSenderEventArgs : EventArgs
    {
        public CommandSenderEventArgs(bool isConnected)
            : base()
        {
            Connected = isConnected;
        }

        public bool Connected { get; set; }
    }
}
