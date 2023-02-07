using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ToolTip = System.Windows.Forms.ToolTip;
using PersonManagement.Models;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using static System.Windows.Forms.AxHost;
using System.Threading;
using System.Net.NetworkInformation;

namespace PersonManagement
{
    public enum eMode
    {
        None = 0,
        Search = 1,
        AfterSearch = 2,
        New = 3,
        Update = 4,
        Delete = 5,
    }

    public partial class Form1 : Form
    {
        #region Properties & Members

        private string connectionString = null;
        private string m_DBServer = "LOCALHOST";

        private List<Person> m_people = null;
        
        private eMode m_eMode = eMode.None;
        private int m_lastSelectedPersonId = 0;

        private int m_actCount = 0;

        #endregion

        public Form1()
        {
            InitializeComponent();

            string error = null;
            InitializeData(out error);

            if (!string.IsNullOrEmpty(error))
            {
                MessageBox.Show(this, error, $"{Application.ProductName}-Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DefaultControlBehavior();
        }

        #region Functions

        /// <summary>
        /// Initialize the Database Data, if database do not exists yet, all database
        /// related will be created:
        /// - DB
        /// - Tables
        /// - Stored Procedures
        /// - Test-Values
        /// </summary>
        /// <param name="error">out -> Error/ Exception</param>
        private void InitializeData(out string error)
        {
            error = null;
            string er = null;

            try
            {
                #region Create Database if not exists

                if (!DBProvisioning.CreateDatabaseIfNotExists(out er, m_DBServer))
                {
                    error += er;
                    error= $"Error druing creation of database:\r\n\r\n{error}";
                    return;
                }
                error += er;

                #endregion

                #region DB-Provisioning

                // Create connection
                connectionString = SQLHelper.CreateConnectionStringFromObject(out er, m_DBServer, "dbPerson", null, null);
                error += er;

                if (null == connectionString)
                {
                    error = $"Error during creation of DB-ConnectionString:\r\n\r\n{error}";
                    return;
                }

                error += er;

                if (!string.IsNullOrEmpty(error))
                {
                    error = $"Error druing creation of database connection:\r\n\r\n{error}";
                    return;
                }

                // Create tables
                if (!DBProvisioning.CreateTables(out er, connectionString))
                {
                    error += er;
                    error = $"Error during creation of tablestructure:\r\n\r\n{error}";
                    return;
                }
                error += er;

                // Create Stored Procedures
                if (!DBProvisioning.CreateStoredProcedures(out er, connectionString))
                {
                    error += er;
                    error = $"Error creation of Stored Procedures:\r\n\r\n{error}";
                    return;
                }
                error += er;

                // Insert Test-Data
                if (!DBProvisioning.InsertDataIfNotExists(out er, connectionString))
                {
                    error += er;
                    error = $"Error creation of testdata:\r\n\r\n{error}";
                    return;
                }
                error += er;

                if (!string.IsNullOrEmpty(error))
                {
                    error = $"Error during provisioning:\r\n\r\n{error}";
                    return;
                }

                #endregion
            }
            catch (Exception ex)
            {
                error = $"Error during initializing of Form:\r\n\r\n{ex.Message}";
            }
        }

        /// <summary>
        /// Default Control Behaviour
        /// </summary>
        private void DefaultControlBehavior()
        {
            pOverlay.Visible = false;
            btnSearch.Enabled = true;
            btnNew.Enabled = true;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;
            btnExecute.Enabled = false;
            btnCancel.Enabled = false;

            gbPerson.Enabled = false;

            if (null != lvPeople.Items && lvPeople.Items.Count > 0)
            {
                gbResult.Enabled = true;
            }

            gbResult.Enabled = true;

            m_eMode = eMode.None;
        }

        #region Events

        #region Buttons

        /// <summary>
        /// Handels control availability when the Search button gets clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = true;
            btnNew.Enabled = false;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;

            btnExecute.Enabled = true;
            btnCancel.Enabled = true;

            gbPerson.Enabled = true;
            gbResult.Enabled = false;

            tbxFirstName.Text = null;
            tbxLastName.Text = null;
            tbxAge.Text = null;

            m_eMode = eMode.Search;
            tbxFirstName.Focus();
        }

        /// <summary>
        /// Handels control availability when the New button gets clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            btnNew.Enabled = true;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;

            btnExecute.Enabled = true;
            btnCancel.Enabled = true;

            gbPerson.Enabled = true;
            gbResult.Enabled = false;

            tbxFirstName.Text = null;
            tbxLastName.Text = null;
            tbxAge.Text = null;

            m_eMode = eMode.New;
            tbxFirstName.Focus();
        }

        /// <summary>
        /// Handels control availability when the Modify button gets clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnModify_Click(object sender, EventArgs e)
        {
            if (null != lvPeople.Items && lvPeople.Items.Count > 0)
            {
                if (null != lvPeople.SelectedItems && lvPeople.SelectedItems.Count > 0)
                {
                    if (m_lastSelectedPersonId <= 0)
                    {
                        if (int.TryParse(lvPeople.SelectedItems[0].Text, out int actPersonId))
                        {
                            m_lastSelectedPersonId = actPersonId;
                        }
                    }

                    btnSearch.Enabled = false;
                    btnNew.Enabled = false;
                    btnModify.Enabled = true;
                    btnDelete.Enabled = false;

                    btnExecute.Enabled = true;
                    btnCancel.Enabled = true;

                    gbPerson.Enabled = true;
                    gbResult.Enabled = false;

                    m_eMode = eMode.Update;
                    tbxFirstName.Focus();
                }
            }
            else
            {
                MessageBox.Show(this, $"To modify Person, you have to select one Person first!", $"{Application.ProductName}-Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show(this, "Do you really want to delete the current selected dataset?", $"{Application.ProductName}-Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                DeletePersonInBackground();
            }
        }

        /// <summary>
        /// Execute current Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExecute_Click(object sender, EventArgs e)
        {
            string error = null;

            switch (m_eMode)
            {
                case eMode.Search:
                    // Execute Search using the UI-Parameters
                    SearchInBackground();
                    break;
                case eMode.New:

                    // Create new Person
                    //CreateNewPerson(out error);
                    CreateNewPersonInBackground();

                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show(this, error, $"{Application.ProductName}-Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case eMode.Update:

                    // Modify existing Person
                    //ModifyPerson(out error);
                    ModifyPersonInBackground();

                    if (!string.IsNullOrEmpty(error))
                    {
                        MessageBox.Show(this, error, $"{Application.ProductName}-Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    break;
            }
        }

        /// <summary>
        /// Cancel current Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (m_eMode != eMode.None)
            {
                if (DialogResult.Yes == MessageBox.Show(this, "Do you really want to cancel the action?", $"{Application.ProductName}-Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    tbxFirstName.Text = null;
                    tbxLastName.Text = null;
                    tbxAge.Text = null;

                    DefaultControlBehavior();
                }
            }
        }

        #endregion

        #region TextBoxes

        /// <summary>
        /// Allows only Digits (Integer)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxAge_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Execute using the F10 key, or cancel using the F11 key
        /// </summary>
        /// <param name="e"></param>
        private void ExecuteOrCancel(KeyEventArgs e)
        {
            if (btnExecute.Enabled && btnCancel.Enabled)
            {
                switch (e.KeyCode)
                {
                    case Keys.F10:
                        // F10 -> Execute
                        btnExecute_Click(btnExecute, null);
                        break;
                    case Keys.F11:
                        // F11 -> Cancel
                        btnCancel_Click(btnCancel, null);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Execute using the F10 key, or cancel using the F11 key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxFirstName_KeyDown(object sender, KeyEventArgs e)
        {
            ExecuteOrCancel(e);
        }

        /// <summary>
        /// Execute using the F10 key, or cancel using the F11 key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxLastName_KeyDown(object sender, KeyEventArgs e)
        {
            ExecuteOrCancel(e);
        }

        /// <summary>
        /// Execute using the F10 key, or cancel using the F11 key
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbxAge_KeyDown(object sender, KeyEventArgs e)
        {
            ExecuteOrCancel(e);
        }

        #endregion

        #region ListView

        /// <summary>
        /// Handels the enabled state of the controls, when ListViewItem gets selected, or deselected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvPeople_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (sender is System.Windows.Forms.ListView lv)
            {
                if (lv.SelectedItems.Count > 0)
                {
                    // ListViewItem selected

                    var selectedItem = lv.SelectedItems[0];

                    if (null != selectedItem.SubItems && selectedItem.SubItems.Count > 0)
                    {
                        btnSearch.Enabled = true;
                        btnNew.Enabled = true;
                        btnModify.Enabled = true;
                        btnDelete.Enabled = true;
                        btnExecute.Enabled = false;
                        btnCancel.Enabled = false;

                        gbPerson.Enabled = false;
                        gbResult.Enabled = true;

                        if (int.TryParse(selectedItem.Text, out int actPersonId))
                        {
                            m_lastSelectedPersonId = actPersonId;
                        }
                        else
                        {
                            m_lastSelectedPersonId = 0;
                        }

                        tbxFirstName.Text = selectedItem.SubItems[1].Text;
                        tbxLastName.Text = selectedItem.SubItems[2].Text;
                        tbxAge.Text = selectedItem.SubItems[3].Text;

                        selectedItem.EnsureVisible();
                    }
                    else
                    {
                        // ListViewItem not selected
                        DefaultControlBehavior();
                        m_lastSelectedPersonId = 0;
                    }
                }
                else
                {
                    // ListViewItem not selected
                    DefaultControlBehavior();
                }
            }
        }

        #endregion

        #endregion

        #region Search

        /// <summary>
        /// Executes the Search in a background task and waits for finish the execution.
        /// </summary>
        private void SearchInBackground()
        {
            string error = null;

            m_people = null;
            lvPeople.Items.Clear();
            m_actCount = 0;

            string firstName = !string.IsNullOrEmpty(tbxFirstName.Text) ? tbxFirstName.Text.Trim() : null;
            string lastName = !string.IsNullOrEmpty(tbxLastName.Text) ? tbxLastName.Text.Trim() : null;
            string ageAsString = !string.IsNullOrEmpty(tbxAge.Text) ? tbxAge.Text.Trim() : null;
            int age = 0;

            if (!string.IsNullOrEmpty(ageAsString))
            {
                if (int.TryParse(ageAsString, out int tmpAge))
                {
                    age = tmpAge;
                }
            }

            m_eMode = eMode.Search;

            // Create Background-Task for ProgressBar
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.WorkerReportsProgress = true;
                worker.DoWork += Worker_ExecuteSearch;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerAsync();
            }

            var taskFactory = System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                if (ExecuteSearch(out error, firstName, lastName, age, true))
                {
                    m_eMode = eMode.AfterSearch;
                    InitializeListViewItems();

                    this.BeginInvoke(new Action(() =>
                    {
                        gbPerson.Enabled = false;
                        lvPeople.Items[0].Selected = true;
                    }
                    ));
                }
            });

            /// ##################################################################################
            /// Wait for finishing the task
            /// ##################################################################################

            System.Threading.Tasks.Task.Run(() =>
            {
                taskFactory.Wait();

                m_eMode = eMode.None;

                if (!string.IsNullOrEmpty(error))
                {
                    if (!string.IsNullOrEmpty(error))
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            MessageBox.Show(this, error, $"{Application.ProductName}-Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        ));
                        return;
                    }
                }
            });
        }

        /// <summary>
        /// Search all Persons which are defined by User, if nothing is defined in the UI, all Persons will be loaded
        /// and displayed to the ListView.
        /// </summary>
        /// <param name="error">out -> Error/Exception</param>
        /// <param name="firstName">Firstname to search (also Wildcard -> '%')</param>
        /// <param name="lastName">Lastname to search (also Wildcard -> '%')</param>
        /// <param name="age">Age to search</param>
        /// <param name="uiInteraction">Will be executed in UI</param>
        private bool ExecuteSearch(out string error, string firstName, string lastName, int age, bool uiInteraction)
        {
            error = null;
            string er = null;

            SqlConnection sqlConnection = null;

            string query = "SELECT Id, \r\n"                                                                                                                                                   +
                           "       FirstName, \r\n"                                                                                                                                            +
                           "       LastName, \r\n"                                                                                                                                             +
                           "       Age \r\n"                                                                                                                                                   +
                           "FROM tblPerson WITH(NOLOCK) \r\n"                                                                                                                                  +
                           "WHERE 1 = 1\r\n"                                                                                                                                                   +
                           $"{(!string.IsNullOrEmpty(firstName) ? (firstName.Contains("%") ? $"    AND FirstName LIKE '{firstName}'\r\n" : $"    AND FirstName = '{firstName}'\r\n") : null)}" +
                           $"{(!string.IsNullOrEmpty(lastName)  ? (lastName.Contains("%")  ? $"    AND LastName LIKE '{lastName}'\r\n"   : $"    AND LastName = '{lastName}'\r\n")   : null)}" +
                           $"{(age > 0                          ? $"    AND Age = {age.ToString()}\r\n" : null)}"                                                                              +
                           ";";
            try
            {
                sqlConnection = SQLHelper.CreateConnection(out er, connectionString);
                error += er;

                if (!string.IsNullOrEmpty(error) || null == sqlConnection)
                {
                    error += $"Error during creation of database connection:\r\n\r\n{error}\r\n\r\n";
                    return false;
                }

                if (sqlConnection.State == ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                SqlCommand command = new SqlCommand(query, sqlConnection);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    m_people = new List<Person>();

                    // Read Data from the Database
                    while (reader.Read())
                    {
                        Person tmpPerson = new Person();
                        tmpPerson.Id        = (null != reader["Id"] ? (int)reader["Id"] : 0);
                        tmpPerson.FirstName = (!string.IsNullOrEmpty(reader["FirstName"].ToString()) ? reader["FirstName"].ToString().Trim() : null);
                        tmpPerson.LastName  = (!string.IsNullOrEmpty(reader["LastName"].ToString()) ? reader["LastName"].ToString().Trim() : null);
                        tmpPerson.Age       = (null != reader["Age"] ? (int)reader["Age"] : 0);

                        m_people.Add(tmpPerson);
                    }
                }

                //if (uiInteraction)
                //{
                //    m_eMode = eMode.AfterSearch;
                //    InitializeListViewItems();
                //}
                return true;
            }
            catch (Exception ex)
            {
                error += $"Error during loading:\r\n\r\n{ex.Message}\r\n\r\n";
            }
            finally
            {
                if (null != sqlConnection)
                {
                    if (sqlConnection.State != ConnectionState.Closed)
                    {
                        sqlConnection.Close();
                    }
                    sqlConnection.Dispose();
                    sqlConnection = null;
                }
            }

            return false;
        }

        /// <summary>
        /// Initializeses the TreeviewItems to have the same Values like the member
        /// Variable m_people
        /// </summary>
        private void InitializeListViewItems()
        {
            //System.Diagnostics.Debug.WriteLine($"\n--------------------");
            //DateTime start = DateTime.Now;

            if (null != m_people && m_people.Count > 0)
            {
                //    // To reduce flakering
                //    this.Invoke(new Action(() =>
                //    {
                //        lvPeople.BeginUpdate();
                //    }
                //    ));

                //    foreach (var person in m_people)
                //    {
                //        this.Invoke(new Action(() =>
                //        {
                //            ListViewItem listViewItem = new ListViewItem(person.Id.ToString());
                //            listViewItem.SubItems.Add(person.FirstName);
                //            listViewItem.SubItems.Add(person.LastName);
                //            listViewItem.SubItems.Add(person.Age.ToString());

                //            m_actCount++;

                //            // Add Item to list
                //            lvPeople.Items.Add(listViewItem);
                //        }
                //        ));
                //    }

                //    // End-Update -> To show the ListViewItems again
                //    this.Invoke(new Action(() =>
                //    {
                //        lvPeople.EndUpdate();
                //    }
                //    ));


                // To reduce flakering
                this.Invoke(new Action(() =>
                {
                    lvPeople.BeginUpdate();
                }
                ));

                List<ListViewItem> liOfListViewItems = new List<ListViewItem>();

                foreach (var person in m_people)
                {
                    this.Invoke(new Action(() =>
                    {
                        ListViewItem listViewItem = new ListViewItem(person.Id.ToString());
                        listViewItem.SubItems.Add(person.FirstName);
                        listViewItem.SubItems.Add(person.LastName);
                        listViewItem.SubItems.Add(person.Age.ToString());

                        m_actCount++;

                        // Add Item to list
                        liOfListViewItems.Add(listViewItem);

                        if (liOfListViewItems.Count == 2000)
                        {
                            // Add more items to ListView
                            lvPeople.Items.AddRange(liOfListViewItems.ToArray());
                            liOfListViewItems = new List<ListViewItem>();
                        }
                    }
                    ));
                }

                // End-Update -> To show the ListViewItems again
                this.Invoke(new Action(() =>
                {
                    if (null != liOfListViewItems && liOfListViewItems.Count > 0 && liOfListViewItems.Count < 2000)
                    {
                        lvPeople.Items.AddRange(liOfListViewItems.ToArray());
                        liOfListViewItems = null;
                    }

                    lvPeople.EndUpdate();
                }
                ));

                /*
                Test-Results:
                
                    Add each row (commented out section):
                    --------------------
                    Start: "2/7/2023 7:53:06 AM"
                    End: "2/7/2023 7:53:54 AM"
                    Duration: "00:00:48.0453325"
                    --------------------

                    Add 1000 rows:
                    --------------------
                    Start: "2/7/2023 7:58:15 AM"
                    End: "2/7/2023 7:58:44 AM"
                    Duration: "00:00:29.1169732"
                    --------------------

                    Add 2000 rows:
                    --------------------
                    Start: "2/7/2023 7:55:07 AM"
                    End: "2/7/2023 7:55:37 AM"
                    Duration: "00:00:30.1544307"
                    --------------------

                    Add 5000 rows:
                    --------------------
                    Start: "2/7/2023 7:56:39 AM"
                    End: "2/7/2023 7:57:13 AM"
                    Duration: "00:00:34.5881726"
                    --------------------
                */

                //DateTime end = DateTime.Now;
                //System.Diagnostics.Debug.WriteLine($"Start: \"{start}\"");
                //System.Diagnostics.Debug.WriteLine($"End: \"{end}\"");
                //System.Diagnostics.Debug.WriteLine($"Duration: \"{end - start}\"");
                //System.Diagnostics.Debug.WriteLine($"--------------------\n");
                //System.Diagnostics.Debugger.Break();
            }
        }
         
        #endregion

        #region Create or Update Person

        /// <summary>
        /// Validates the Userinput which will be used for creation and updating of Persons.
        /// </summary>
        /// <returns>Validation OK => TRUE/Validation not OK => FALSE</returns>
        private bool ValidateInput(out string error)
        {
            error = null;
            if (string.IsNullOrEmpty(tbxFirstName.Text))
            {
                error += $"Firstname cannot be empty!";
                tbxFirstName.Focus();
                return false;
            }
            tbxFirstName.Text = tbxFirstName.Text.Trim();

            if (string.IsNullOrEmpty(tbxLastName.Text))
            {
                error += $"LastName cannot be empty!";
                tbxLastName.Focus();
                return false;
            }
            tbxLastName.Text = tbxLastName.Text.Trim();

            if (string.IsNullOrEmpty(tbxAge.Text))
            {
                error += $"Age cannot be empty!";
                tbxAge.Focus();
                return false;
            }
            tbxAge.Text = tbxAge.Text.Trim();
            if (!int.TryParse(tbxAge.Text, out int age))
            {
                error += $"Age must be a number!";
                tbxAge.Focus();
                return false;
            }

            if (age <= 0)
            {
                error += $"Age must be greater than 0!";
                tbxAge.Focus();
                return false;
            }

            return true;
        }

        #region Create Person

        /// <summary>
        /// Executes Creation in a background task and waits for finish the execution.
        /// </summary>
        private void CreateNewPersonInBackground()
        {
            string error = null;

            m_eMode = eMode.New;

            // Create Background-Task for ProgressBar
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.WorkerReportsProgress = true;
                worker.DoWork += Worker_CreateNewPerson;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerAsync();
            }

            var taskFactory = System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                Person tmpPerson = CreateNewPerson(out error);
                if (null != tmpPerson)
                {
                    AfterCreation(tmpPerson);
                }
            });

            /// ##################################################################################
            /// Wait for finishing the task
            /// ##################################################################################

            System.Threading.Tasks.Task.Run(() =>
            {
                taskFactory.Wait();

                m_eMode = eMode.None;

                if (!string.IsNullOrEmpty(error))
                {
                    if (!string.IsNullOrEmpty(error))
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            MessageBox.Show(this, error, $"{Application.ProductName}-Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        ));
                        return;
                    }
                }
            });
        }

        /// <summary>
        /// Creates a new Person
        /// </summary>
        private Person CreateNewPerson(out string error)
        {
            error = null;
            string er = null;

            Person newPerson = null;

            if (!ValidateInput(out er))
            {
                error += er;
                return newPerson;
            }
            error += er;

            if (int.TryParse(tbxAge.Text, out int age))
            {
                newPerson = new Person();
                if (newPerson.CreateNewPerson(out er, connectionString, tbxFirstName.Text, tbxLastName.Text, age))
                {
                    error += er;
                    return newPerson;
                }
                error += er;
                error = $"Error during modify of Person:\r\n{error}\r\n\r\n";
            }
            else
            {
                error += $"Cannot convert Age \"{tbxAge.Text}\" to integer!\r\n\r\n";
            }

            return newPerson;
        }

        /// <summary>
        /// UI-Handling after successfully Modification of Person
        /// </summary>
        /// <param name="newPerson"></param>
        private void AfterCreation(Person newPerson)
        {
            this.BeginInvoke(new Action(() =>
            {
                // Load the crated dataset
                if (newPerson != null && lvPeople != null)
                {
                    lvPeople.BeginUpdate();

                    ListViewItem listViewItem = new ListViewItem(newPerson.Id.ToString());
                    listViewItem.SubItems.Add(newPerson.FirstName);
                    listViewItem.SubItems.Add(newPerson.LastName);
                    listViewItem.SubItems.Add(newPerson.Age.ToString());

                    lvPeople.Items.Add(listViewItem);
                    lvPeople.Items[lvPeople.Items.Count - 1].Selected = true;
                    lvPeople.EndUpdate();
                }
            }
            ));

            this.BeginInvoke(new Action(() =>
            {
                // Set Control availability
                btnSearch.Enabled = true;
                btnNew.Enabled = true;
                btnModify.Enabled = true;
                btnDelete.Enabled = true;
                btnExecute.Enabled = false;
                btnCancel.Enabled = false;

                gbPerson.Enabled = false;
                gbResult.Enabled = true;
            }
            ));
        }

        #endregion

        #region Modify existing Person

        /// <summary>
        /// Executes Creation in a background task and waits for finish the execution.
        /// </summary>
        private void ModifyPersonInBackground()
        {
            string error = null;

            m_eMode = eMode.Update;

            // Create Background-Task for ProgressBar
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.WorkerReportsProgress = true;
                worker.DoWork += Worker_ModifyPerson;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerAsync();
            }

            var taskFactory = System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                if (ModifyPerson(out error))
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        AfterModification(out error);
                    }
                    ));
                    //m_eMode = eMode.AfterUpdate;
                }
            });

            /// ##################################################################################
            /// Wait for finishing the task
            /// ##################################################################################

            System.Threading.Tasks.Task.Run(() =>
            {
                taskFactory.Wait();

                m_eMode = eMode.None;

                if (!string.IsNullOrEmpty(error))
                {
                    if (!string.IsNullOrEmpty(error))
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            MessageBox.Show(this, error, $"{Application.ProductName}-Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        ));
                        return;
                    }
                }
            });
        }

        /// <summary>
        /// Modify Person
        /// </summary>
        /// <param name="error">out -> Error/Exception</param>
        /// <returns>Person successfully modified => TRUE/Person not successfully modified, or Error => FALSE</returns>
        private bool ModifyPerson(out string error)
        {
            error = null;
            string er = null;

            if (!ValidateInput(out er))
            {
                error += er;
                return false;
            }
            error += er;

            if (null != m_people && m_people.Count > 0)
            {
                Person personToModify = m_people.Where(x => x.Id == m_lastSelectedPersonId).FirstOrDefault();

                if (null != personToModify)
                {
                    if (int.TryParse(tbxAge.Text, out int age))
                    {
                        if (personToModify.ModifyPerson(out er, connectionString, tbxFirstName.Text, tbxLastName.Text, age))
                        {
                            error += er;
                            return true;
                        }
                        error += er;
                        error = $"Error during modification of Person:\r\n{error}\r\n\r\n";
                    }
                    else
                    {
                        error += $"Cannot convert Age \"{tbxAge.Text}\" to integer!\r\n\r\n";
                    }
                }
                else
                {
                    error += $"Person with Id \"{m_lastSelectedPersonId}\" not found in collection!\r\n\r\n";
                }
            }
            else
            {
                error += "No Person in list available!\r\n\r\n";
            }

            return false;
        }

        /// <summary>
        /// UI-Handling after successfully Modification of Person
        /// </summary>
        private void AfterModification(out string error)
        {
            error = null;

            if (null != m_people && m_people.Count > 0)
            {
                Person tmpPerson = m_people.Where(x => x.Id == m_lastSelectedPersonId).FirstOrDefault();

                if (null != tmpPerson)
                {
                    if (!string.IsNullOrEmpty(tbxAge.Text))
                    {
                        if (int.TryParse(tbxAge.Text, out int age))
                        {
                            tmpPerson.FirstName = tbxFirstName.Text;
                            tmpPerson.LastName = tbxLastName.Text;
                            tmpPerson.Age = age;

                            lvPeople.BeginUpdate();

                            if (null != lvPeople.SelectedItems[0] && !string.IsNullOrEmpty(lvPeople.SelectedItems[0].Text))
                            {
                                if (lvPeople.SelectedItems[0].Text.Trim() == m_lastSelectedPersonId.ToString())
                                {
                                    if (null != lvPeople.SelectedItems[0].SubItems && lvPeople.SelectedItems[0].SubItems.Count >= 4)
                                    {


                                        lvPeople.SelectedItems[0].SubItems[1].Text = tmpPerson.FirstName;
                                        lvPeople.SelectedItems[0].SubItems[2].Text = tmpPerson.LastName;
                                        lvPeople.SelectedItems[0].SubItems[3].Text = tmpPerson.Age.ToString();
                                    }
                                    else
                                    {
                                        // Maybe error message
                                    }
                                }
                                else
                                {
                                    error += $"AfterModification: Wrong Item selected!\r\n" +
                                             $"Selected-Item ID: \"{lvPeople.SelectedItems[0].Text.Trim()}\"\r\n" +
                                             $"Searched-Item ID: \"{m_lastSelectedPersonId}\"\r\n\r\n";
                                }
                            }
                            else
                            {
                                error += $"AfterModification: No Item selected!\r\n\r\n";
                            }

                            lvPeople.EndUpdate();
                        }
                        else
                        { 
                            error += $"AfterModification: Age is not a Number!\r\n\r\n";
                        }
                    }
                    else
                    {
                        error += $"AfterModification: Age in TextBox is NULL or EMPTY!\r\n\r\n";
                    }
                }
                else
                {
                    error += $"AfterModification: Cannot find Person in List with Id: \"{m_lastSelectedPersonId}\"!\r\n\r\n";
                }
            }
            else
            {
                error += $"AfterModification: List of people is empty!\r\n\r\n";
            }

            this.BeginInvoke(new Action(() =>
            {
                // Set Control availability
                btnSearch.Enabled = true;
                btnNew.Enabled = true;
                btnModify.Enabled = true;
                btnDelete.Enabled = true;
                btnExecute.Enabled = false;
                btnCancel.Enabled = false;

                gbPerson.Enabled = false;
                gbResult.Enabled = true;

                if (null != lvPeople.Items && lvPeople.Items.Count > 0)
                {
                    lvPeople.Items[m_people.FindIndex(x => x.Id == m_lastSelectedPersonId)].Selected = true;
                }
            }));
        }

        #endregion

        #endregion

        #region Delete

        /// <summary>
        /// Executes Deletion in a background task and waits for finish the execution.
        /// </summary>
        private void DeletePersonInBackground()
        {
            string error = null;

            m_eMode = eMode.Delete;

            // Create Background-Task for ProgressBar
            using (BackgroundWorker worker = new BackgroundWorker())
            {
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.WorkerReportsProgress = true;
                worker.DoWork += Worker_DeletePerson;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.RunWorkerAsync();
            }

            var taskFactory = System.Threading.Tasks.Task.Factory.StartNew(() =>
            {
                if (DeletePerson(out error))
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        AfterDeletion();
                    }
                    ));
                }
            });

            /// ##################################################################################
            /// Wait for finishing the task
            /// ##################################################################################

            System.Threading.Tasks.Task.Run(() =>
            {
                taskFactory.Wait();

                m_eMode = eMode.None;

                if (!string.IsNullOrEmpty(error))
                {
                    if (!string.IsNullOrEmpty(error))
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            MessageBox.Show(this, error, $"{Application.ProductName}-Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        ));
                        return;
                    }
                }
            });
        }

        /// <summary>
        /// Deletes Person from Database
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool DeletePerson(out string error)
        {
            error = null;
            string er = null;

            if (null != m_people && m_people.Count > 0)
            {
                Person personToDelete = m_people.Where(x => x.Id == m_lastSelectedPersonId).FirstOrDefault();

                if (null != personToDelete)
                {
                    if (personToDelete.DeletePerson(out er, connectionString))
                    {
                        error += er;
                        return true;
                    }
                    error += er;
                    error = $"Error during deletion of Person:\r\n{error}\r\n\r\n";
                }
                else
                {
                    error += $"Person with Id \"{m_lastSelectedPersonId}\" not found in collection!\r\n\r\n";
                }
            }
            else
            {
                error += "No Person in list available!\r\n\r\n";
            }

            return false;
        }

        /// <summary>
        /// UI-Behavior after deletion of Person
        /// </summary>
        private void AfterDeletion()
        {
            if (null != m_people && m_people.Count > 0)
            {
                m_people = m_people.Where(x => x.Id != m_lastSelectedPersonId).ToList();
                int index = lvPeople.Items.IndexOf(lvPeople.SelectedItems[0]);
                lvPeople.Items.RemoveAt(index);
            }
        }

        #endregion

        #region ProgressBar

        /// <summary>
        /// ProgressBar handling for Search
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_ExecuteSearch(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            int nThreadSleep = 300;

            this.BeginInvoke(new Action(() =>
            {
                // Controls
                tlpActionButtons.Enabled = false;
                gbPerson.Enabled = false;
                gbResult.Enabled = false;

                // ProgressBar
                pOverlay.Visible = true;
                pbState.Style = ProgressBarStyle.Marquee;
                pbState.MarqueeAnimationSpeed = 15;
                lblProgressState.Text = "Search...";
            }));

            while (m_eMode == eMode.Search)
            {
                Thread.Sleep(nThreadSleep);
            }

            if (null != m_people && m_people.Count > 0)
            {
                this.BeginInvoke(new Action(() =>
                {
                    // ProgressBar
                    pbState.Style = ProgressBarStyle.Continuous;
                    pbState.Value = 0;
                    //pbState.Maximum = 100;
                    pbState.Maximum = m_people.Count;
                }));


                while (m_eMode == eMode.AfterSearch)
                {
                    this.BeginInvoke(new Action(() =>
                    {
                        lblProgressState.Text = $"{m_actCount} of {m_people.Count} loaded";
                    }));

                    //worker.ReportProgress((m_actCount * 100) / m_people.Count);
                    worker.ReportProgress(m_actCount);
                    Thread.Sleep(nThreadSleep);
                }

                //m_eMode = eMode.AfterSearch;
            }
        }

        /// <summary>
        /// ProgressBar handling for Creation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_CreateNewPerson(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            int nThreadSleep = 300;

            this.BeginInvoke(new Action(() =>
            {
                // Controls
                tlpActionButtons.Enabled = false;
                gbPerson.Enabled = false;
                gbResult.Enabled = false;

                // ProgressBar
                pOverlay.Visible = true;
                pbState.Style = ProgressBarStyle.Marquee;
                pbState.MarqueeAnimationSpeed = 15;
                lblProgressState.Text = "Create...";
            }));

            while (m_eMode == eMode.New)
            {
                Thread.Sleep(nThreadSleep);
            }
        }

        /// <summary>
        /// ProgressBar handling for Modification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void Worker_ModifyPerson(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            int nThreadSleep = 300;

            this.BeginInvoke(new Action(() =>
            {
                // Controls
                tlpActionButtons.Enabled = false;
                gbPerson.Enabled = false;
                gbResult.Enabled = false;

                // ProgressBar
                pOverlay.Visible = true;
                pbState.Style = ProgressBarStyle.Marquee;
                pbState.MarqueeAnimationSpeed = 15;
                lblProgressState.Text = "Modify...";
            }));

            while (m_eMode == eMode.Update)
            {
                Thread.Sleep(nThreadSleep);
            }
        }

        private void Worker_DeletePerson(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            int nThreadSleep = 300;

            this.BeginInvoke(new Action(() =>
            {
                // Controls
                tlpActionButtons.Enabled = false;
                gbPerson.Enabled = false;
                gbResult.Enabled = false;

                // ProgressBar
                pOverlay.Visible = true;
                pbState.Style = ProgressBarStyle.Marquee;
                pbState.MarqueeAnimationSpeed = 15;
                lblProgressState.Text = "Delete...";
            }));

            while (m_eMode == eMode.Delete)
            {
                Thread.Sleep(nThreadSleep);
            }
        }

        /// <summary>
        /// ProgressBar Worker for progress changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbState.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// ProgressBar-Worker completed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            string error = null;

            this.BeginInvoke(new Action(() =>
            {
                // ProgressBar
                pOverlay.Visible = false;
                pbState.Style = ProgressBarStyle.Blocks;
                pbState.MarqueeAnimationSpeed = 15;
                lblProgressState.Text = null;

                // Controls
                tlpActionButtons.Enabled = true;
            }
            ));

            this.BeginInvoke(new Action(() =>
            {
                if (null != lvPeople.Items && lvPeople.Items.Count > 0)
                {
                    //switch (m_eMode)
                    //{
                    //    case eMode.None:
                    //        DefaultControlBehavior();
                    //        break;
                    //    case eMode.AfterSearch:
                    //        //gbPerson.Enabled = false;
                    //        //lvPeople.Items[0].Selected = true;
                    //        break;
                    //    case eMode.New:
                    //        // Will be handeled inside of the Function "AfterCreation();"
                    //        break;
                    //    case eMode.AfterUpdate:
                    //        //AfterModification(out error);
                    //        break;
                    //    case eMode.Delete:
                    //        AfterDeletion();
                    //        break;
                    //    default:
                    //        DefaultControlBehavior();
                    //        break;
                    //}

                    btnSearch.Enabled = true;
                    btnNew.Enabled = true;
                    btnModify.Enabled = true;
                    btnDelete.Enabled = true;
                    btnExecute.Enabled = false;
                    btnCancel.Enabled = false;

                    gbPerson.Enabled = false;
                    gbResult.Enabled = true;
                }
                else
                {
                    DefaultControlBehavior();
                }

                m_eMode = eMode.None;

                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show(this, error, $"{Application.ProductName}-Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            ));
        }


        #endregion

        #endregion
    }
}
