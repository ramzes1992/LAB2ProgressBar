using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LAB2ProgressBar.Views
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
        bool _cancellationPending = false;

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            _cancellationPending = true;
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            v_Button_Cancel.IsEnabled = true;
            v_Button_Start.IsEnabled = false;

            while (!_cancellationPending)
            {
                await DoSomeWorkAsync();
                v_ProgressBar.Value++;

                if (v_ProgressBar.Value >= 100)
                    v_ProgressBar.Value = 0;
            }

            v_Button_Cancel.IsEnabled = false;
            v_Button_Start.IsEnabled = true;
            _cancellationPending = false;
        }

        private Task DoSomeWorkAsync()
        {
            return Task.Run(() => { Thread.Sleep(500); });
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            v_ProgressBar.Value = 0;
        }
    }
}
