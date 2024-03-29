﻿using System.Diagnostics;
using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace MaterialDesignUnityBootStrap.Infrastructure
{
    public class DragMoveBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseLeftButtonDown += (sender, args) =>
            {
                if (args.LeftButton == System.Windows.Input.MouseButtonState.Pressed && sender == AssociatedObject)
                    Window.GetWindow(AssociatedObject)?.DragMove();
            };
        }
    }
    public class WindowMaximizerBehavior : Behavior<UIElement>
    {
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.MouseLeftButtonDown += (sender, args) =>
            {
                if (args.ClickCount == 2 && sender == AssociatedObject)
                {
                    var window = Window.GetWindow(AssociatedObject);
                    Debug.Assert(window != null, nameof(window) + " != null");
                    if (window != null)
                        window.WindowState = window.WindowState == WindowState.Normal
                            ? WindowState.Maximized
                            : WindowState.Normal;
                }
            };
        }
    }
}
