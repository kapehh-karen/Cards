using Core.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Core
{
    public static class UnhandledException
    {
        public static void Init()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string exceptionMessage = e.ExceptionObject.ToString();

            if (e.IsTerminating)
            {
                NotificationMessage.Error(exceptionMessage);
            }
            else
            {
                NotificationMessage.Warning(exceptionMessage);
            }
        }
    }
}
