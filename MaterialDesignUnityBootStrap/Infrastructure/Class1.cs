using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MaterialDesignUnityBootStrap.Infrastructure
{
    public class PasswordBoxHintProxy : IHintProxy
    {
        private readonly PasswordBox _passwordBox;

        public string Content => _passwordBox.Password;

        public bool IsLoaded => _passwordBox.IsLoaded;

        public bool IsVisible => _passwordBox.IsVisible;

        public bool IsEmpty() => string.IsNullOrEmpty(Content);

        public bool IsFocused() => _passwordBox.IsKeyboardFocused;

        public event EventHandler ContentChanged;
        public event EventHandler IsVisibleChanged;
        public event EventHandler Loaded;
        public event EventHandler FocusedChanged;

        public PasswordBoxHintProxy(PasswordBox textBox)
        {
            _passwordBox = textBox ?? throw new ArgumentNullException(nameof(textBox));
            _passwordBox.PasswordChanged += PasswordChanged;
            _passwordBox.Loaded += TextBoxLoaded;
            _passwordBox.IsVisibleChanged += TextBoxIsVisibleChanged;
            _passwordBox.IsKeyboardFocusedChanged += TextBoxIsKeyboardFocusedChanged;
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            ContentChanged?.Invoke(sender, EventArgs.Empty);
        }

        private void TextBoxIsKeyboardFocusedChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            FocusedChanged?.Invoke(sender, EventArgs.Empty);
        }

        private void TextBoxIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            IsVisibleChanged?.Invoke(sender, EventArgs.Empty);
        }

        private void TextBoxLoaded(object sender, RoutedEventArgs e)
        {
            Loaded?.Invoke(sender, EventArgs.Empty);
        }

        public void Dispose()
        {

            _passwordBox.PasswordChanged -= PasswordChanged;
            _passwordBox.Loaded -= TextBoxLoaded;
            _passwordBox.IsVisibleChanged -= TextBoxIsVisibleChanged;
            _passwordBox.IsKeyboardFocusedChanged -= TextBoxIsKeyboardFocusedChanged;
        }
    }
}
