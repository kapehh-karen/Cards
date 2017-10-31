using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Notification
{
    public static class NotificationMessage
    {
        public const string DefaultTitle = "CARDS";

        public delegate void MessageEvent(string message, string title, object[] param, NotificationLevel level);
        public static event MessageEvent ReceiveMessage;

        public static void Info(string message, params object[] param)
        {
            Message(message, DefaultTitle, param, NotificationLevel.INFO);
        }

        public static void Info(string message, string title = DefaultTitle, params object[] param)
        {
            Message(message, title, param, NotificationLevel.INFO);
        }

        public static void Error(string message, params object[] param)
        {
            Message(message, DefaultTitle, param, NotificationLevel.ERROR);
        }

        public static void Error(string message, string title = DefaultTitle, params object[] param)
        {
            Message(message, title, param, NotificationLevel.ERROR);
        }

        public static void SystemInfo(string message, string title = DefaultTitle, params object[] param)
        {
            Message(message, title, param, NotificationLevel.SYSTEM_INFO);
        }

        public static void SystemInfo(string message, params object[] param)
        {
            Message(message, DefaultTitle, param, NotificationLevel.SYSTEM_INFO);
        }

        public static void SystemError(string message, string title = DefaultTitle, params object[] param)
        {
            Message(message, title, param, NotificationLevel.SYSTEM_ERROR);
        }

        public static void SystemError(string message, params object[] param)
        {
            Message(message, DefaultTitle, param, NotificationLevel.SYSTEM_ERROR);
        }

        public static void Message(string message, string title, object[] param, NotificationLevel level)
        {
            ReceiveMessage?.Invoke(message, title, param, level);
        }
    }
}
