using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Notification
{
    public static class NotificationMessage
    {
        public delegate void MessageEvent(string message, object[] param, NotificationLevel level);
        public static event MessageEvent ReceiveMessage;

        public static void Info(string message, params object[] param)
        {
            Message(message, param, NotificationLevel.INFO);
        }

        public static void Error(string message, params object[] param)
        {
            Message(message, param, NotificationLevel.SYSTEM_ERROR);
        }

        public static void SystemInfo(string message, params object[] param)
        {
            Message(message, param, NotificationLevel.SYSTEM_INFO);
        }

        public static void Message(string message, object[] param, NotificationLevel level)
        {
            ReceiveMessage?.Invoke(message, param, level);
        }
    }
}
