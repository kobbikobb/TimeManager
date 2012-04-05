using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace TimeManagerLib.View.Behaviors
{
    /// <summary>
    /// Klasinn er með sér virkni fyrir takka.
    /// Tengist takka í gengum static föll.
    /// Takkinn geymir því eintak af klasanum
    /// NOTE: Það view sem vísar í þennan klasa verður að hafa reference á GalaSoft og CAL
    /// </summary>
    public class ButtonBehaviorController
    {
        #region Static breytur sem sjá um að geyma ButtonBehavior

        private static readonly DependencyProperty BehaviorProperty =
            DependencyProperty.RegisterAttached(
                "Behavior",
                typeof(ButtonBehavior),
                typeof(Button),
                null);

        private static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(ButtonBehaviorController),
                new PropertyMetadata(OnSetCommandCallback));

        private static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(object),
                typeof(ButtonBehaviorController),
                new PropertyMetadata(OnSetCommandParameterCallback));

        private static readonly DependencyProperty VerificationTextProperty =
            DependencyProperty.RegisterAttached(
                "VerificationText",
                typeof(string),
                typeof(ButtonBehaviorController),
                new PropertyMetadata(OnSetVerificationText));

        private static readonly DependencyProperty ClosesWindowProperty =
            DependencyProperty.RegisterAttached(
                "ClosesWindow",
                typeof(bool),
                typeof(ButtonBehaviorController),
                new PropertyMetadata(OnSetCloseWindowCallback));

        #endregion

        #region Smiðir fyrir static breytur

        private static ButtonBehavior GetOrCreateBehavior(DependencyObject dependencyObject)
        {
            var button = (Button)dependencyObject;

            var behavior = button.GetValue(BehaviorProperty) as ButtonBehavior;

            if (behavior == null)
            {
                behavior = new ButtonBehavior(button);
                button.SetValue(BehaviorProperty, behavior);
            }

            return behavior;
        }

        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var behavior = GetOrCreateBehavior(dependencyObject);

            behavior.HiddenCommand = (ICommand)e.NewValue;
        }

        private static void OnSetCommandParameterCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var behavior = GetOrCreateBehavior(dependencyObject);

            behavior.CommandParameter = e.NewValue;
        }

        private static void OnSetVerificationText(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var behavior = GetOrCreateBehavior(dependencyObject);

            behavior.HiddenCommandVerificationText = (string)e.NewValue;
        }

        private static void OnSetCloseWindowCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var behavior = GetOrCreateBehavior(dependencyObject);

            behavior.HiddenCommandClosesWindow = (bool)e.NewValue;
        }

        #endregion

        #region Tengingar við XAML

        public static void SetCommand(Button button, ICommand command)
        {
            button.SetValue(CommandProperty, command);
        }
        public static void SetCommandParameter(Button button, object command)
        {
            button.SetValue(CommandParameterProperty, command);
        }
        public static void SetVerificationText(Button button, string text)
        {
            button.SetValue(VerificationTextProperty, text);
        }
        public static void SetClosesWindow(Button button, bool canClose)
        {
            button.SetValue(ClosesWindowProperty, canClose);
        }

        #endregion
    }
}
