using SearchInExcelTemplates.Dialogs;
using SearchInExcelTemplates.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
using System.Windows.Threading;

namespace SearchInExcelTemplates
{
   public enum eProcessStatus
    {
        LoadFiles,
        SearchForContent,
        Finished
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static eProcessStatus ProcessStatus { get; set; }

        public static List<FileResult> FileResults { get; set; } = null;

        public static int m_ActFile { get; set; }

        public static int m_MaxFile { get; set; }

        public static int m_ReadSuccessfully { get; set; } = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region Functions

        #region ListView-RightClick/ContextMenu Handling


        /// <summary>
        /// ListView-Rightclick Hansling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContextMenuLvResult(object sender, MouseButtonEventArgs e)
        {
            if (null == lvResult.SelectedItems || lvResult.SelectedItems.Count <= 0)
            {
                miCopy.IsEnabled = false;
            }
            else
            {
                miCopy.IsEnabled = true;
            }
        }

        /// <summary>
        /// ListviewItem-Rightclick Handling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnListViewItemPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (null == lvResult.SelectedItems || lvResult.SelectedItems.Count <= 0)
            {
                miCopy.IsEnabled = false;
            }
            else
            {
                miCopy.IsEnabled = true;
            }

            e.Handled = true;
        }

        /// <summary>
        /// Copies the selected items of the ListView into the Clipboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickMiCopy(object sender, RoutedEventArgs e)
        {
            if (null != lvResult.Items && lvResult.Items.Count > 0)
            {
                if (null != lvResult.SelectedItems && lvResult.SelectedItems.Count > 0)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();

                    // Create Table for Clipboard
                    sb.Append($"<table>");
                    sb.AppendFormat($"<tr>"                     +
                                    $"<td>Dateipfad</td>"       +
                                    $"<td>Modul-Name</td>"      +
                                    $"<td>Zeile</td>"           +
                                    $"<td>Zeileninhalt</td>"    +
                                    $"<td>InfoMsg</td>"         +
                                    $"<td>Modul-Existiert</td>" +
                                    $"<td>Geschützt</td>"       +
                                    $"<td>Vorhanden</td>"       +
                                    $"</tr>");

                    foreach (var item in lvResult.SelectedItems)
                    {
                        if (item is FileResult objFileResult)
                        {
                            // TableRow
                            sb.AppendFormat($"<tr>"                                 +
                                            $"<td>{objFileResult.FilePath}</td>"    +
                                            $"<td>{objFileResult.ModuleName}</td>"  +
                                            $"<td>{objFileResult.RowNumber}</td>"   +
                                            $"<td>{objFileResult.RowContent}</td>"  +
                                            $"<td>{objFileResult.InfoMsg}</td>"     +
                                            $"<td>{objFileResult.ModuleExist}</td>" +
                                            $"<td>{objFileResult.Protected}</td>"   +
                                            $"<td>{objFileResult.SearchMatch}</td>" +
                                            $"</tr>");
                        }
                    }
                    sb.Append("<table>");

                    Clipboard.SetDataObject(sb.ToString());
                }
            }
        }

        #endregion

        #region Button-Click Events

        /// <summary>
        /// Selects all rows in the ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickBtnSelectAll(object sender, RoutedEventArgs e)
        {
            if (null != lvResult.Items && lvResult.Items.Count > 0)
            {
                lvResult.SelectAll();
                lvResult.Focus();
            }
        }

        /// <summary>
        /// Unselects all rows in the ListView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickBtnUnselectAll(object sender, RoutedEventArgs e)
        {
            if (null != lvResult.Items && lvResult.Items.Count > 0)
            {
                lvResult.UnselectAll();
            }
        }

        /// <summary>
        /// Reverts the configuration in the UI to the Defalut-Configuration
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickBtnRevert(object sender, RoutedEventArgs e)
        {
            tbxSearchParameter.Text = null;
            tbxPath.Text = null;
            tbxFilter.Text = "*.xlsm, *.xlsb, *.xlt, *.xltm";
            pbPassword.Password = null;
            cbMatch.IsChecked = false;
            lvResult.ItemsSource = null;
        }

        /// <summary>
        /// Validates Configuration (Required fields) and search for the user specified content in the Modules.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickBtnSearch(object sender, RoutedEventArgs e)
        {
            string strMsg = null;
            string error = null;
            bool bRequiredValidation = true;

            m_ReadSuccessfully = 0;

            // Validate input
            // Required fields:
            if (string.IsNullOrEmpty(tbxSearchParameter.Text))
            {
                strMsg += "- Kein Suchparameter angegeben!\r\n";
                bRequiredValidation = false;
            }
            if (string.IsNullOrEmpty(tbxPath.Text))
            {
                strMsg += "- Kein Pfad angegeben!\r\n";
                bRequiredValidation = false;
            }

            if (!bRequiredValidation)
            {
                MessageBox.Show(this, strMsg, "SearchInExcelTemplates-Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SearchConfiguration objSearchConfiguration = new SearchConfiguration()
            {
                SearchParameter     = tbxSearchParameter.Text,
                Path                = tbxPath.Text.Replace("\"", null),
                Filter              = tbxFilter.Text,
                PasswordAsPlainText = pbPassword.Password,
                MatchCase           = null != cbMatch.IsChecked ? (bool)cbMatch.IsChecked : false
            };

            lvResult.ItemsSource = null;
            btnSelectAll.Visibility = Visibility.Collapsed;
            btnUnselectAll.Visibility = Visibility.Collapsed;

            ProcessStatus = eProcessStatus.LoadFiles;

            // Erstelle Background-Task für den ProgressBar
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.WorkerReportsProgress = true;
                worker.DoWork += Worker_DoWork;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerAsync();
            }

            var taskFactory = System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                string er = null;
                // Check if folderpath exists
                if(!objSearchConfiguration.ValidatePath(out er))
                {
                    error += er;
                    return false;
                }
                error += er;

                if(objSearchConfiguration.SearchInFiles(out er))
                {
                    this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                    {
                        lvResult.ItemsSource = null;
                        lvResult.ItemsSource = FileResults;
                        ClickBtnSelectAll(btnSelectAll, null);
                    }));
                }
                error += er;

                return true;
            });

            System.Threading.Tasks.Task.Run(() =>
            {
                taskFactory.Wait();

                ProcessStatus = eProcessStatus.Finished;

                string strCurrMsg = "# # # RESULT # # #\r\n\r\n";
                

                if (null != FileResults && FileResults.Count > 0)
                {
                    this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                    {
                        strCurrMsg += $"{m_ReadSuccessfully} Dateien von {m_MaxFile} erfolgreich geladen.\r\n";
                        strCurrMsg += $"{FileResults.Count} Vorkommnisse für \"{tbxSearchParameter.Text}\" in {m_MaxFile} Dateien gefunden.\r\n";
                    }));
                }
                else
                {
                    this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                    {
                        strCurrMsg += $"{m_ReadSuccessfully} Dateien von {m_MaxFile} erfolgreich geladen.\r\n";
                        strCurrMsg += $"Keine Vorkommnisse für \"{tbxSearchParameter.Text}\" in {m_MaxFile} Dateien gefunden.\r\n";
                    }));
                }

                if (!string.IsNullOrEmpty(error))
                {
                    this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                    {
                        strCurrMsg += $"\r\n# # # ERROR # # #\r\n{error}";
                    }));
                }

                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                {
                    MessageDialog objMsgDialog = new MessageDialog(strCurrMsg, $"{App.AppName}-Suche");
                    objMsgDialog.ShowDialog();
                }));

                this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                {
                    lvResult.ItemsSource = null;
                    lvResult.ItemsSource = FileResults;

                    if (null != lvResult.Items && lvResult.Items.Count > 0)
                    {
                        btnSelectAll.Visibility = Visibility.Visible;
                        btnUnselectAll.Visibility = Visibility.Visible;
                    }
                }));
            });
        }

        #endregion

        #region ProgressBar

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbStatus.Value = e.ProgressPercentage;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            int nThreadSleep = 300;

            this.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
            {
                gbSearchParameter.IsEnabled = false;
                gbResult.IsEnabled = false;

                grdButtons.IsEnabled = false;

                pbProgress.Visibility = Visibility.Visible;
                pbStatus.IsIndeterminate = true;

                pbStatus.Value = 0;
                pbStatus.Maximum = m_MaxFile;
                tbStatus.Text = $"Suche in Dateien...";
            }
            ));

            while (ProcessStatus == eProcessStatus.LoadFiles)
            {
                System.Threading.Thread.Sleep(nThreadSleep);
            }

            System.Threading.Thread.Sleep(nThreadSleep);

            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
            {
                pbStatus.Value = 0;
                pbStatus.Maximum = m_MaxFile;
                pbStatus.IsIndeterminate = false;
            }));

            System.Threading.Thread.Sleep(nThreadSleep);

            while (ProcessStatus == eProcessStatus.SearchForContent)
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(() =>
                {
                    tbStatus.Text = $"Suche in {m_ActFile} von {m_MaxFile} Dateien...";
                    worker.ReportProgress(m_ActFile);
                }
                ));

                System.Threading.Thread.Sleep(nThreadSleep);
            }
                System.Threading.Thread.Sleep(nThreadSleep);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(() =>
            {
                pbProgress.Visibility = Visibility.Collapsed;
                pbStatus.IsIndeterminate = false;

                gbSearchParameter.IsEnabled = true;
                gbResult.IsEnabled = true;

                grdButtons.IsEnabled = true;
            }
          ));
        }

        #endregion

        #endregion

    }
}
