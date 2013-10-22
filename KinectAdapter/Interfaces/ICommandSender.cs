using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KinectAdapter.Interfaces
{
    public interface ICommandSender
    {
        /// <summary>
        /// Send a command with the given action ID.
        /// </summary>
        /// <param name="ActionId">The ActionId of the command</param>
        void SendCommand(string ActionId);

        /// <summary>
        /// Send a notification
        /// </summary>
        /// <param name="ActionId">The ActionId of the command</param>
        void SendNotification(string caption, string actionId);
    }
}
