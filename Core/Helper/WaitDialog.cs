using Core.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Core.Helper
{
    public class WaitDialog : IDisposable
    {
        public static T Run<T>(string message, Func<WaitDialog, T> callback)
        {
            using (var dlg = new WaitDialog(message))
            {
                return dlg.RunInBackground(callback);
            }
        }

        public static T Run<T>(string message, Func<T> callback)
        {
            using (var dlg = new WaitDialog(message))
            {
                return dlg.RunInBackground((s) => callback());
            }
        }

        public static void Run(string message, Action callback)
        {
            using (var dlg = new WaitDialog(message))
            {
                dlg.RunInBackground((s) => { callback(); return true; });
            }
        }

        public static void Run(string message, Action<WaitDialog> callback)
        {
            using (var dlg = new WaitDialog(message))
            {
                dlg.RunInBackground((s) => { callback(s); return true; });
            }
        }

        private FormWaitDialog form = null;

        private WaitDialog(string message)
        {
            form = new FormWaitDialog() { Message = message };
        }
        
        public string Message
        {
            get => form?.Message;
            set
            {
                if (form != null)
                    if (form.InvokeRequired)
                        form.Invoke((MethodInvoker) delegate { form.Message = value; });
                    else
                        form.Message = value;
            }
        }

        private object Result { get; set; }

        private T RunInBackground<T>(Func<WaitDialog, T> runInThread)
        {
            new Thread((state) =>
            {
                Result = runInThread(this);

                if (form != null)
                    if (form.InvokeRequired)
                        form.Invoke((MethodInvoker) delegate { form.Close(); });
                    else
                        form.Close();
            }).Start(this);

            form.ShowDialog();

            return (T)Result;
        }

        public void Dispose()
        {
            if (form != null)
            {
                form.Dispose();
                form = null;
            }
        }
    }
}
