using Core.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Core.Helper
{
    public class WaitDialog : IDisposable
    {
        public static T Run<T>(string message, Func<T> callback)
        {
            using (var dlg = new WaitDialog(message))
            {
                return dlg.Run(callback);
            }
        }
        
        private FormWaitDialog form = null;

        private WaitDialog(string message)
        {
            form = new FormWaitDialog() { Message = message };
        }

        private object Result { get; set; }

        public T Run<T>(Func<T> runInThread)
        {
            new Thread((state) =>
            {
                Result = runInThread();

                if (form != null)
                    if (form.InvokeRequired)
                        form.BeginInvoke((Action)(() => form.Close()));
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
