﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Notification
{
    public static class NotificationMessage
    {
        public delegate void MessageEvent(string message, string title, object[] param, NotificationLevel level);
        public static event MessageEvent ReceiveMessage;

        public static void Info(string message, params object[] param)
        {
            Message(message, Consts.ProgramTitle, param, NotificationLevel.INFO);
        }

        public static void InfoEx(string message, string title = Consts.ProgramTitle, params object[] param)
        {
            Message(message, title, param, NotificationLevel.INFO);
        }

        public static void Warning(string message, params object[] param)
        {
            Message(message, Consts.ProgramTitle, param, NotificationLevel.WARNING);
        }

        public static void WarningEx(string message, string title = Consts.ProgramTitle, params object[] param)
        {
            Message(message, title, param, NotificationLevel.WARNING);
        }

        public static void Error(string message, params object[] param)
        {
            Message(message, Consts.ProgramTitle, param, NotificationLevel.ERROR);
        }

        public static void ErrorEx(string message, string title = Consts.ProgramTitle, params object[] param)
        {
            Message(message, title, param, NotificationLevel.ERROR);
        }

        public static void SystemInfoEx(string message, string title = Consts.ProgramTitle, params object[] param)
        {
            Message(message, title, param, NotificationLevel.SYSTEM_INFO);
        }

        public static void SystemInfo(string message, params object[] param)
        {
            Message(message, Consts.ProgramTitle, param, NotificationLevel.SYSTEM_INFO);
        }

        public static void SystemWarning(string message, params object[] param)
        {
            Message(message, Consts.ProgramTitle, param, NotificationLevel.SYSTEM_WARNING);
        }

        public static void SystemWarningEx(string message, string title = Consts.ProgramTitle, params object[] param)
        {
            Message(message, title, param, NotificationLevel.SYSTEM_WARNING);
        }

        public static void SystemErrorEx(string message, string title = Consts.ProgramTitle, params object[] param)
        {
            Message(message, title, param, NotificationLevel.SYSTEM_ERROR);
        }

        public static void SystemError(string message, params object[] param)
        {
            Message(message, Consts.ProgramTitle, param, NotificationLevel.SYSTEM_ERROR);
        }

        public static void Message(string message, string title, object[] param, NotificationLevel level)
        {
            ReceiveMessage?.Invoke(message, title, param, level);
        }
    }
}
