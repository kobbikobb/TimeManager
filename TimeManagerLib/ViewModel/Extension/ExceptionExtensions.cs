using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using TimeManagerLib.Helpers;

namespace TimeManagerLib.ViewModel.Extension
{
    public static class ExceptionExtensions
    {
        public static void Show(this Exception exception)
        {
            Logger.Instance.WriteException(exception);

            var message = new DialogMessage(exception.ToString(), null)
            {
                Button = MessageBoxButton.OK,
                Icon = MessageBoxImage.Error,
                Caption = "Error"
            };

            Messenger.Default.Send(message);
        }
    }
}
