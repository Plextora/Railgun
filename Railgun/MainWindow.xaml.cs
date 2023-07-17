/*
* Copyright 2023 Plextora
* Licensed under the GPL-3.0 license (https://www.gnu.org/licenses/gpl-3.0.en.html#license-text)
*/

#region

using System;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Railgun.Utils.Classes;

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
        private readonly Random _random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            _timer = new Timer();
            _timer.Elapsed += Click;
            _timer.AutoReset = true;
        }

        private void Window_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();

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

        private void Click(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if (_isClick)
            {
                _timer.Interval = 1000 / GetRandomCps();
                ClickEmulation.SimulateMouseClick();
            }
        }

        private void ToggleButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isClick)
            {
                _isClick = false;
                Indicator.Background =
                    App.ResourceDictionary["Indicator"]["Indicator.Off.Background"] as SolidColorBrush;
                _timer.Enabled = false;
            }
            else if (!_isClick)
            {
                _isClick = true;
                Indicator.Background =
                    App.ResourceDictionary["Indicator"]["Indicator.On.Background"] as SolidColorBrush;
                _timer.Enabled = true;
            }
        }
    }
}
