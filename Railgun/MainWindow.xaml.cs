/*
* Copyright 2023 Plextora
* Licensed under the GPL-3.0 license (https://www.gnu.org/licenses/gpl-3.0.en.html#license-text)
*/

#region

using System;
using System.ComponentModel;
using System.Timers;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Gma.System.MouseKeyHook;
using Railgun.Utils.Classes;
using MouseEventArgs = System.Windows.Forms.MouseEventArgs;
using Timer = System.Timers.Timer;

#endregion

namespace Railgun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static Timer _timer;
        private static double _cps;
        private static bool _isClick;
        private static bool _isMouseLmb;
        private static char _keyChar;
        private static char _displayKeyChar;
        private static bool _isKey;
        private IKeyboardMouseEvents _globalHook;
        private readonly Random _random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            Subscribe();
            _timer = new Timer();
            _timer.Elapsed += Click;
            _timer.AutoReset = true;
        }

        public void Subscribe()
        {
            _globalHook = Hook.GlobalEvents();

            _globalHook.MouseDown += OnMouseDown;
            _globalHook.MouseUp += OnMouseUp;
            _globalHook.KeyPress += ToggleKey;
        }

        private void Window_OnClosing(object sender, CancelEventArgs e)
        {
            _globalHook.MouseDown -= OnMouseDown;
            _globalHook.MouseUp -= OnMouseUp;
            _globalHook.KeyPress -= ToggleKey;

            _globalHook.Dispose();
            _globalHook = null;
        }

        private void Window_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void CloseButton_OnClick(object sender, RoutedEventArgs e) => Close();

        private void MinimizeButton_OnClick(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

        private void CpsSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (CpsValue != null)
                CpsValue.Content = $"CPS: {e.NewValue}";
            _cps = e.NewValue;
        }

        public double GetRandomCps()
        {
            double maximum = _cps + 5;
            double minimum = _cps - 5;

            return Math.Round(_random.NextDouble() * (maximum - minimum) + minimum);
        }

        private async void Click(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if (_isClick && _isMouseLmb)
            {
                _timer.Interval = 1000 / GetRandomCps();
                await ClickEmulation.SimulateMouseClick();
            }
        }

        private void ToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            ToggleButton.Content = "...";
            _isKey = true;
        }

        private void ToggleKey(object sender, KeyPressEventArgs e)
        {
            if (_isKey)
            {
                _keyChar = e.KeyChar;
                _displayKeyChar = char.ToUpper(e.KeyChar);
                ToggleButton.Content = _displayKeyChar;
                _isKey = false;
            }

            if (e.KeyChar == _keyChar)
            {
                if (_isClick == false)
                {
                    _isClick = true;
                    Indicator.Background =
                        App.ResourceDictionary["Indicator"]["Indicator.On.Background"] as SolidColorBrush;
                }
                else if (_isClick)
                {
                    _isClick = false;
                    Indicator.Background =
                        App.ResourceDictionary["Indicator"]["Indicator.Off.Background"] as SolidColorBrush;
                }

                if (_timer.Enabled)
                    _timer.Enabled = false;
                else if (_timer.Enabled == false)
                    _timer.Enabled = true;
            }
        }

        private static void OnMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _isMouseLmb = true;
        }

        private static void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                _isMouseLmb = false;
        }
    }
}
