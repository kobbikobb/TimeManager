using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Interactivity;
using TimeManager.Presentation.Views.Extension;

namespace TimeManager.Presentation.Views.Behaviors
{
    public class ButtonBehavior : CommandBehaviorBase<Button>
    {
        public ICommand HiddenCommand { get; set; }
        public object HiddenCommandParamter { get; set; }
        public string HiddenCommandVerificationText { get; set; }
        public bool HiddenCommandClosesWindow { get; set; }

        public ButtonBehavior(Button button) : base(button)
        {
            button.Click += ButtonClick;
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            try
            {
                TargetObject.IsEnabled = false;

                if (!string.IsNullOrEmpty(HiddenCommandVerificationText))
                {
                    if (MessageBox.Show(GetWindow(), HiddenCommandVerificationText, "Note",
                        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        TargetObject.IsEnabled = true;
                        return;
                    }
                }

                if (HiddenCommand != null)
                    HiddenCommand.Execute(HiddenCommandParamter);

                if (HiddenCommandClosesWindow)
                {
                    var window = GetWindow();
                    window.Close();
                }

                TargetObject.IsEnabled = true;
            }
            catch (Exception ex)
            {
                TargetObject.IsEnabled = true;
                MessageBox.Show(GetWindow(), ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private Window GetWindow()
        {
            return TargetObject.GetVisualParent<Window>();
        }
    }
}
