﻿using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;

namespace MaterialDesignUnityBootStrap
{
    public class DialogWindow : IDialogWindow
    {
        public IDialogResult Result { get ; set; }
        public object Content { get ; set; }
        public Window Owner { get ; set ; }
        public object DataContext { get; set; }
        public Style Style { get ; set ; }

        public event RoutedEventHandler Loaded;
        public event EventHandler Closed;
        public event CancelEventHandler Closing;

        public void Close()
        {
            DialogHost.CloseDialogCommand.Execute(Result, null);
        }

        public void Show()
        {
            Owner.ShowDialog(Content, OpendEventHandler, ClosingEventHandler);

        }

        private void ClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            var cancel = new CancelEventArgs();
            Closing(this, cancel);
            if (!cancel.Cancel)
                Closed(this, EventArgs.Empty);

        }

        private void OpendEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            Loaded(this, new RoutedEventArgs());
        }

        public  bool? ShowDialog()
        {
           DialogHost.Show(Content, OpendEventHandler, ClosingEventHandler);
            return null; 
        }

    }
}
