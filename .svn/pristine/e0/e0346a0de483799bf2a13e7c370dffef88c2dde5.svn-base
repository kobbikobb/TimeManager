using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Commands;
using System.Windows.Controls;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using TimeManagerLib.ViewModel.Extension;

namespace TimeManagerLib.View.Behaviors
{
    public class ButtonBehavior : CommandBehaviorBase<Button>
    {
        #region Eigindi

        public ICommand HiddenCommand { get; set; }
        public object HiddenCommandParamter { get; set; }
        public string HiddenCommandVerificationText { get; set; }
        public bool HiddenCommandClosesWindow { get; set; }

        #endregion

        #region Smiður

        public ButtonBehavior(Button button)
            : base(button)
        {
            button.Click += button_Click;
        }

        #endregion

        #region Aðgerðir

        private void button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                TargetObject.IsEnabled = false;

                if (!string.IsNullOrEmpty(HiddenCommandVerificationText))
                {
                    var message = new DialogMessage(HiddenCommandVerificationText, null)
                    {
                        Button = MessageBoxButton.YesNo,
                        Icon = MessageBoxImage.Question,
                        Caption = "Please note"
                    };

                    Messenger.Default.Send(message);

                    if (message.DefaultResult == MessageBoxResult.No)
                    {
                        TargetObject.IsEnabled = true;
                        return;
                    }
                }

                if (HiddenCommand != null)
                    HiddenCommand.Execute(HiddenCommandParamter);

                if (HiddenCommandClosesWindow)
                {
                    var window = TargetObject.GetVisualParent<Window>();
                    window.DialogResult = true;
                    window.Close();
                }

                TargetObject.IsEnabled = true;
            }
            catch (Exception ex)
            {
                TargetObject.IsEnabled = true;
                ex.Show();
            }
        }

        #endregion
    }
}
