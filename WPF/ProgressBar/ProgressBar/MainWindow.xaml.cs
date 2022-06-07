using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace ProgressBar
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

        /// <summary>
        /// Function to simulate a long process, which can block the GUI
        /// </summary>
        private bool SimulateLongWork()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread.Sleep(new TimeSpan(0,0,15));
                Trace.WriteLine("Process some data...");
            }
            return true;
        }

        bool workDone = false;

        private void ClickbtnTest(object sender, RoutedEventArgs e)
        {
            workDone = false;
            pbMyBar.Visibility = Visibility.Visible;
            //pbMyBar.IsIndeterminate = true;
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.WorkerReportsProgress = true;
                worker.DoWork += Worker_DoWork;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerAsync();
            }

            // It will not work, if we call the Function directly:
            //SimulateLongWork();
            //workDone = true;

            // So we have to execute it in a different Thread, than the GUI Thread
            System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                return SimulateLongWork();
            }).ContinueWith(t =>
            {
                if (t.Result)
                {
                    workDone = true;
                }
            }, System.Threading.Tasks.TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// What to do when work is completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("All Done");
            pbMyBar.Value = 0;
            pbMyBar.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// What worker have to do.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
           
            //worker.ReportProgress(0);
            //for (int i = 0; i < 10; i++)
            //{
            //    Thread.Sleep(new TimeSpan(0,0,1));
            //    worker.ReportProgress((i + 1) * 10);
            //}
            while (!workDone)
            {
                worker.ReportProgress(0);
                for (int i = 0; i <= 10; i++)
                {
                    Thread.Sleep(new TimeSpan(0, 0, 0, 0, 50));
                    worker.ReportProgress((i + 1) * 10);
                }
                worker.ReportProgress(0);
            }
        }

        /// <summary>
        /// What happened, when Progress changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbMyBar.Value = e.ProgressPercentage;
        }
    }
}
