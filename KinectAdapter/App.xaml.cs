using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using XBMC;
using System.Threading;

namespace KinectAdapter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (e.Args.Contains("/debug"))
            {
                StartupUri = new Uri("DebugWindow.xaml",
                        UriKind.Relative);
            }
            else
            {
                StartupUri = new Uri("UserWindow.xaml",
                        UriKind.Relative);
            }

            
        }
    }
}
