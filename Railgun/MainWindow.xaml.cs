/*
* Copyright 2023 Plextora
* Licensed under the GPL-3.0 license (https://www.gnu.org/licenses/gpl-3.0.en.html#license-text)
*/


using System.Windows;
using System.Windows.Input;

namespace Railgun
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e) => DragMove();
    }
}
