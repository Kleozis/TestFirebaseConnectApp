using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace TestFirebaseConnectApp.GUI
{
    public class PasswordBoxExtension
    {
        #region Attached Property BindPassword
        public static readonly DependencyProperty BindPasswordProperty =
            DependencyProperty.RegisterAttached(
                "BindPassword",
                typeof(bool),
                typeof(PasswordBoxExtension),
                new PropertyMetadata(false, OnBindPasswordPropertyChanged)
                );

        private static void OnBindPasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(d is PasswordBox pBox)) return;

            if ((bool)e.OldValue) // was bind
            {
                pBox.PasswordChanged -= OnPasswordChanged;
            }
            
            if ((bool)e.NewValue) // need to bind
            {
                pBox.PasswordChanged += OnPasswordChanged;
            }
        }

        private static void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            var pBox = sender as PasswordBox;
            SetExtractedPassword(pBox, pBox.Password);
            SetIsPasswordEmpty(pBox, pBox.Password.Length == 0);
        }

        public static void SetBindPassword(PasswordBox pb, bool value)
        {
            pb.SetValue(BindPasswordProperty, value);
        }

        public static bool GetBindPassword(PasswordBox pb)
        {
            return (bool)pb.GetValue(BindPasswordProperty);
        }
        #endregion

        #region Attached Property ExtractedPassword
        public static readonly DependencyProperty ExtractedPasswordProperty =
            DependencyProperty.RegisterAttached(
                "ExtractedPassword",
                typeof(string),
                typeof(PasswordBoxExtension),
                new PropertyMetadata("")
                );

        public static void SetExtractedPassword(PasswordBox pb, string value)
        {
            pb.SetValue(ExtractedPasswordProperty, value);
        }

        public static string GetExtractedPassword(PasswordBox pb)
        {
            return (string)pb.GetValue(ExtractedPasswordProperty);
        }
        #endregion

        #region Attached Property IsPasswordEmpty
        public static readonly DependencyProperty IsPasswordEmptyProperty =
            DependencyProperty.RegisterAttached(
                "IsPasswordEmpty",
                typeof(bool),
                typeof(PasswordBoxExtension),
                new PropertyMetadata(true)
                );

        public static void SetIsPasswordEmpty(PasswordBox pb, bool value)
        {
            pb.SetValue(IsPasswordEmptyProperty, value);
        }

        public static bool GetIsPasswordEmpty(PasswordBox pb)
        {
            return (bool)pb.GetValue(IsPasswordEmptyProperty);
        }
        #endregion

    }
}
