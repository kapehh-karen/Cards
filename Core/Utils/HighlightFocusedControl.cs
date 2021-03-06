﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Core.Utils
{
    public class HighlightFocusedControl
    {
        private enum ControlEvent
        {
            Reinstall = 0,
            Install = 1,
            Uninstall = 2
        }

        public HighlightFocusedControl(Form form)
        {
            this.Form = form;
        }

        public void Install(bool force = false)
        {
            if (Installed && !force)
                return;

            InstallEventHandlers(this.Form);
            Installed = true;
        }

        public Form Form { get; set; }

        public bool Installed { get; private set; } = false;
        
        private Pen pen = new Pen(Color.DeepSkyBlue, 3);

        // Удалил nestedControl.HasChildren
        private bool IsControlContainer(Control nestedControl)
            => nestedControl is TabControl || nestedControl is TabPage || nestedControl is GroupBox;

        /// <summary>
        /// Recursive method to install the paint event handler for all container controls on a form, 
        /// including the form itself, and also to install the "enter" event handler for all controls 
        /// on a form.
        /// </summary>
        private void InstallEventHandlers(Control containerControl, ControlEvent controlEvent = ControlEvent.Reinstall)
        {
            // Событие отрисовки
            if (controlEvent != ControlEvent.Install) containerControl.Paint -= ContainerControl_Paint;
            if (controlEvent != ControlEvent.Uninstall) containerControl.Paint += ContainerControl_Paint;

            // Добавление нового контрола
            if (controlEvent != ControlEvent.Install) containerControl.ControlAdded -= ContainerControl_ControlAdded;
            if (controlEvent != ControlEvent.Uninstall) containerControl.ControlAdded += ContainerControl_ControlAdded;

            // Удаление существующего контрола
            if (controlEvent != ControlEvent.Install) containerControl.ControlRemoved -= ContainerControl_ControlRemoved;
            if (controlEvent != ControlEvent.Uninstall) containerControl.ControlRemoved += ContainerControl_ControlRemoved;

            foreach (Control nestedControl in containerControl.Controls)
            {
                InstallControlEventHandlers(nestedControl, controlEvent);

                if (IsControlContainer(nestedControl))
                    InstallEventHandlers(nestedControl, controlEvent);
            }
        }

        private void InstallControlEventHandlers(Control nestedControl, ControlEvent controlEvent = ControlEvent.Reinstall)
        {
            if (controlEvent != ControlEvent.Install) nestedControl.Enter -= Control_Event;
            if (controlEvent != ControlEvent.Uninstall) nestedControl.Enter += Control_Event;
        }
        
        /// <summary>
        /// Event handler method that gets called when a control receives focus. This just indicates 
        /// that the whole form needs to be redrawn. (This is a bit inefficient, but will presumably 
        /// only be noticeable if there are many, many controls on the form.)
        /// </summary>
        private void Control_Event(object sender, EventArgs e)
        {
            this.Form.Refresh();
        }

        /// <summary>
        /// Event handler method to draw a dark blue rectangle around a control if it has focus, and 
        /// if it is in the container control that is invoking this method.
        /// </summary>
        private void ContainerControl_Paint(object sender, PaintEventArgs e)
        {
            Control activeControl = this.Form.ActiveControl;
            if (activeControl != null && activeControl.Parent == sender)
            {
                e.Graphics.DrawRectangle(pen,
                            new Rectangle(activeControl.Location.X - 2, activeControl.Location.Y - 2,
                                          activeControl.Size.Width + 3, activeControl.Size.Height + 3));
            }
        }

        private void ContainerControl_ControlRemoved(object sender, ControlEventArgs e)
        {
            var control = e.Control;

            InstallControlEventHandlers(control, ControlEvent.Uninstall);

            if (IsControlContainer(control))
                InstallEventHandlers(control, ControlEvent.Uninstall);
        }

        private void ContainerControl_ControlAdded(object sender, ControlEventArgs e)
        {
            var control = e.Control;

            InstallControlEventHandlers(control, ControlEvent.Install);

            if (IsControlContainer(control))
                InstallEventHandlers(control, ControlEvent.Install);
        }
    }
}
