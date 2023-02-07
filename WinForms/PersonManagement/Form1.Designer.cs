namespace PersonManagement
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.gbPerson = new System.Windows.Forms.GroupBox();
            this.tlpPersonInput = new System.Windows.Forms.TableLayoutPanel();
            this.tbxAge = new System.Windows.Forms.TextBox();
            this.tbxLastName = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.lblLastName = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.tbxFirstName = new System.Windows.Forms.TextBox();
            this.pView = new System.Windows.Forms.Panel();
            this.gbResult = new System.Windows.Forms.GroupBox();
            this.lvPeople = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tlpActionButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnExecute = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.btnModify = new System.Windows.Forms.Button();
            this.ttBtnSearch = new System.Windows.Forms.ToolTip(this.components);
            this.ttBtnNew = new System.Windows.Forms.ToolTip(this.components);
            this.ttBtnModify = new System.Windows.Forms.ToolTip(this.components);
            this.ttBtnDelete = new System.Windows.Forms.ToolTip(this.components);
            this.ttBtnExecute = new System.Windows.Forms.ToolTip(this.components);
            this.ttBtnCancel = new System.Windows.Forms.ToolTip(this.components);
            this.pOverlay = new System.Windows.Forms.Panel();
            this.lblProgressState = new System.Windows.Forms.Label();
            this.pbState = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel1.SuspendLayout();
            this.gbPerson.SuspendLayout();
            this.tlpPersonInput.SuspendLayout();
            this.pView.SuspendLayout();
            this.gbResult.SuspendLayout();
            this.tlpActionButtons.SuspendLayout();
            this.pOverlay.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.gbPerson, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 48);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(792, 139);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // gbPerson
            // 
            this.gbPerson.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.gbPerson.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPerson.AutoSize = true;
            this.gbPerson.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gbPerson.Controls.Add(this.tlpPersonInput);
            this.gbPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbPerson.ForeColor = System.Drawing.Color.Black;
            this.gbPerson.Location = new System.Drawing.Point(13, 13);
            this.gbPerson.Name = "gbPerson";
            this.gbPerson.Size = new System.Drawing.Size(344, 113);
            this.gbPerson.TabIndex = 0;
            this.gbPerson.TabStop = false;
            this.gbPerson.Text = "Person";
            // 
            // tlpPersonInput
            // 
            this.tlpPersonInput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpPersonInput.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpPersonInput.BackColor = System.Drawing.Color.Transparent;
            this.tlpPersonInput.ColumnCount = 2;
            this.tlpPersonInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpPersonInput.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPersonInput.Controls.Add(this.tbxAge, 1, 2);
            this.tlpPersonInput.Controls.Add(this.tbxLastName, 1, 1);
            this.tlpPersonInput.Controls.Add(this.lblFirstName, 0, 0);
            this.tlpPersonInput.Controls.Add(this.lblLastName, 0, 1);
            this.tlpPersonInput.Controls.Add(this.lblAge, 0, 2);
            this.tlpPersonInput.Controls.Add(this.tbxFirstName, 1, 0);
            this.tlpPersonInput.Location = new System.Drawing.Point(4, 20);
            this.tlpPersonInput.Margin = new System.Windows.Forms.Padding(10, 20, 10, 10);
            this.tlpPersonInput.Name = "tlpPersonInput";
            this.tlpPersonInput.RowCount = 4;
            this.tlpPersonInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPersonInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPersonInput.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpPersonInput.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpPersonInput.Size = new System.Drawing.Size(332, 83);
            this.tlpPersonInput.TabIndex = 0;
            // 
            // tbxAge
            // 
            this.tbxAge.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxAge.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbxAge.Location = new System.Drawing.Point(73, 55);
            this.tbxAge.MaxLength = 3;
            this.tbxAge.Name = "tbxAge";
            this.tbxAge.Size = new System.Drawing.Size(256, 20);
            this.tbxAge.TabIndex = 2;
            this.tbxAge.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxAge_KeyDown);
            this.tbxAge.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbxAge_KeyPress);
            // 
            // tbxLastName
            // 
            this.tbxLastName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxLastName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbxLastName.Location = new System.Drawing.Point(73, 29);
            this.tbxLastName.MaxLength = 150;
            this.tbxLastName.Name = "tbxLastName";
            this.tbxLastName.Size = new System.Drawing.Size(256, 20);
            this.tbxLastName.TabIndex = 1;
            this.tbxLastName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxLastName_KeyDown);
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(5, 5);
            this.lblFirstName.Margin = new System.Windows.Forms.Padding(5);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(55, 13);
            this.lblFirstName.TabIndex = 0;
            this.lblFirstName.Text = "Firstname:";
            // 
            // lblLastName
            // 
            this.lblLastName.AutoSize = true;
            this.lblLastName.Location = new System.Drawing.Point(5, 31);
            this.lblLastName.Margin = new System.Windows.Forms.Padding(5);
            this.lblLastName.Name = "lblLastName";
            this.lblLastName.Size = new System.Drawing.Size(56, 13);
            this.lblLastName.TabIndex = 1;
            this.lblLastName.Text = "Lastname:";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(5, 57);
            this.lblAge.Margin = new System.Windows.Forms.Padding(5);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(29, 13);
            this.lblAge.TabIndex = 2;
            this.lblAge.Text = "Age:";
            // 
            // tbxFirstName
            // 
            this.tbxFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxFirstName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tbxFirstName.Location = new System.Drawing.Point(73, 3);
            this.tbxFirstName.MaxLength = 150;
            this.tbxFirstName.Name = "tbxFirstName";
            this.tbxFirstName.Size = new System.Drawing.Size(256, 20);
            this.tbxFirstName.TabIndex = 0;
            this.tbxFirstName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxFirstName_KeyDown);
            // 
            // pView
            // 
            this.pView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pView.Controls.Add(this.gbResult);
            this.pView.Location = new System.Drawing.Point(12, 180);
            this.pView.Name = "pView";
            this.pView.Padding = new System.Windows.Forms.Padding(10);
            this.pView.Size = new System.Drawing.Size(792, 431);
            this.pView.TabIndex = 1;
            // 
            // gbResult
            // 
            this.gbResult.Controls.Add(this.lvPeople);
            this.gbResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbResult.Location = new System.Drawing.Point(10, 10);
            this.gbResult.Name = "gbResult";
            this.gbResult.Padding = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.gbResult.Size = new System.Drawing.Size(772, 411);
            this.gbResult.TabIndex = 0;
            this.gbResult.TabStop = false;
            this.gbResult.Text = "Result";
            // 
            // lvPeople
            // 
            this.lvPeople.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvPeople.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPeople.FullRowSelect = true;
            this.lvPeople.HideSelection = false;
            this.lvPeople.Location = new System.Drawing.Point(5, 23);
            this.lvPeople.MultiSelect = false;
            this.lvPeople.Name = "lvPeople";
            this.lvPeople.Size = new System.Drawing.Size(762, 383);
            this.lvPeople.TabIndex = 0;
            this.lvPeople.UseCompatibleStateImageBehavior = false;
            this.lvPeople.View = System.Windows.Forms.View.Details;
            this.lvPeople.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvPeople_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Id";
            this.columnHeader1.Width = 25;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Firstname";
            this.columnHeader2.Width = 150;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Lastname";
            this.columnHeader3.Width = 150;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Age";
            // 
            // tlpActionButtons
            // 
            this.tlpActionButtons.BackColor = System.Drawing.SystemColors.ControlLight;
            this.tlpActionButtons.ColumnCount = 9;
            this.tlpActionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 5F));
            this.tlpActionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpActionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpActionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpActionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpActionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tlpActionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpActionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tlpActionButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpActionButtons.Controls.Add(this.btnNew, 2, 0);
            this.tlpActionButtons.Controls.Add(this.btnDelete, 4, 0);
            this.tlpActionButtons.Controls.Add(this.btnSearch, 1, 0);
            this.tlpActionButtons.Controls.Add(this.btnCancel, 7, 0);
            this.tlpActionButtons.Controls.Add(this.btnExecute, 6, 0);
            this.tlpActionButtons.Controls.Add(this.splitter1, 5, 0);
            this.tlpActionButtons.Controls.Add(this.btnModify, 3, 0);
            this.tlpActionButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.tlpActionButtons.Location = new System.Drawing.Point(0, 0);
            this.tlpActionButtons.Margin = new System.Windows.Forms.Padding(5, 10, 5, 5);
            this.tlpActionButtons.Name = "tlpActionButtons";
            this.tlpActionButtons.RowCount = 1;
            this.tlpActionButtons.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpActionButtons.Size = new System.Drawing.Size(816, 38);
            this.tlpActionButtons.TabIndex = 2;
            // 
            // btnNew
            // 
            this.btnNew.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnNew.BackgroundImage")));
            this.btnNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnNew.Location = new System.Drawing.Point(40, 3);
            this.btnNew.Margin = new System.Windows.Forms.Padding(1, 3, 1, 1);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(32, 32);
            this.btnNew.TabIndex = 0;
            this.ttBtnNew.SetToolTip(this.btnNew, "New");
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnDelete.BackgroundImage")));
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Location = new System.Drawing.Point(108, 3);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(1, 3, 1, 1);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(32, 32);
            this.btnDelete.TabIndex = 3;
            this.ttBtnDelete.SetToolTip(this.btnDelete, "Delete");
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearch.BackgroundImage")));
            this.btnSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnSearch.Location = new System.Drawing.Point(6, 3);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(1, 3, 1, 1);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(32, 32);
            this.btnSearch.TabIndex = 0;
            this.ttBtnSearch.SetToolTip(this.btnSearch, "Search");
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnCancel.BackgroundImage")));
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancel.Location = new System.Drawing.Point(184, 3);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(1, 3, 1, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(32, 32);
            this.btnCancel.TabIndex = 5;
            this.ttBtnCancel.SetToolTip(this.btnCancel, "(F11) Cancel Action");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnExecute
            // 
            this.btnExecute.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExecute.BackgroundImage")));
            this.btnExecute.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnExecute.Location = new System.Drawing.Point(150, 3);
            this.btnExecute.Margin = new System.Windows.Forms.Padding(1, 3, 1, 1);
            this.btnExecute.Name = "btnExecute";
            this.btnExecute.Size = new System.Drawing.Size(32, 32);
            this.btnExecute.TabIndex = 4;
            this.ttBtnExecute.SetToolTip(this.btnExecute, "(F10) Execute Action");
            this.btnExecute.UseVisualStyleBackColor = true;
            this.btnExecute.Click += new System.EventHandler(this.btnExecute_Click);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Gainsboro;
            this.splitter1.Enabled = false;
            this.splitter1.Location = new System.Drawing.Point(144, 3);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(2, 32);
            this.splitter1.TabIndex = 6;
            this.splitter1.TabStop = false;
            // 
            // btnModify
            // 
            this.btnModify.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnModify.BackgroundImage")));
            this.btnModify.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnModify.Location = new System.Drawing.Point(74, 3);
            this.btnModify.Margin = new System.Windows.Forms.Padding(1, 3, 1, 1);
            this.btnModify.Name = "btnModify";
            this.btnModify.Size = new System.Drawing.Size(32, 32);
            this.btnModify.TabIndex = 7;
            this.ttBtnModify.SetToolTip(this.btnModify, "Modify");
            this.btnModify.UseVisualStyleBackColor = true;
            this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
            // 
            // ttBtnSearch
            // 
            this.ttBtnSearch.AutoPopDelay = 3000;
            this.ttBtnSearch.InitialDelay = 2000;
            this.ttBtnSearch.ReshowDelay = 100;
            // 
            // ttBtnNew
            // 
            this.ttBtnNew.AutoPopDelay = 3000;
            this.ttBtnNew.InitialDelay = 2000;
            this.ttBtnNew.ReshowDelay = 100;
            // 
            // ttBtnModify
            // 
            this.ttBtnModify.AutoPopDelay = 3000;
            this.ttBtnModify.InitialDelay = 2000;
            this.ttBtnModify.ReshowDelay = 100;
            // 
            // ttBtnDelete
            // 
            this.ttBtnDelete.AutoPopDelay = 3000;
            this.ttBtnDelete.InitialDelay = 2000;
            this.ttBtnDelete.ReshowDelay = 100;
            // 
            // ttBtnExecute
            // 
            this.ttBtnExecute.AutoPopDelay = 3000;
            this.ttBtnExecute.InitialDelay = 2000;
            this.ttBtnExecute.ReshowDelay = 100;
            // 
            // ttBtnCancel
            // 
            this.ttBtnCancel.AutoPopDelay = 3000;
            this.ttBtnCancel.InitialDelay = 2000;
            this.ttBtnCancel.ReshowDelay = 100;
            // 
            // pOverlay
            // 
            this.pOverlay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pOverlay.BackColor = System.Drawing.Color.Transparent;
            this.pOverlay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pOverlay.Controls.Add(this.lblProgressState);
            this.pOverlay.Controls.Add(this.pbState);
            this.pOverlay.Location = new System.Drawing.Point(70, 200);
            this.pOverlay.MaximumSize = new System.Drawing.Size(685, 81);
            this.pOverlay.MinimumSize = new System.Drawing.Size(685, 81);
            this.pOverlay.Name = "pOverlay";
            this.pOverlay.Padding = new System.Windows.Forms.Padding(10);
            this.pOverlay.Size = new System.Drawing.Size(685, 81);
            this.pOverlay.TabIndex = 1;
            this.pOverlay.Visible = false;
            // 
            // lblProgressState
            // 
            this.lblProgressState.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblProgressState.AutoSize = true;
            this.lblProgressState.BackColor = System.Drawing.Color.Transparent;
            this.lblProgressState.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgressState.Location = new System.Drawing.Point(42, 9);
            this.lblProgressState.MaximumSize = new System.Drawing.Size(600, 25);
            this.lblProgressState.MinimumSize = new System.Drawing.Size(600, 22);
            this.lblProgressState.Name = "lblProgressState";
            this.lblProgressState.Size = new System.Drawing.Size(600, 22);
            this.lblProgressState.TabIndex = 2;
            this.lblProgressState.Text = "Wird geladen...";
            this.lblProgressState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pbState
            // 
            this.pbState.AccessibleDescription = "";
            this.pbState.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pbState.Cursor = System.Windows.Forms.Cursors.AppStarting;
            this.pbState.ForeColor = System.Drawing.Color.DarkGreen;
            this.pbState.Location = new System.Drawing.Point(41, 32);
            this.pbState.Margin = new System.Windows.Forms.Padding(20);
            this.pbState.MaximumSize = new System.Drawing.Size(600, 30);
            this.pbState.MinimumSize = new System.Drawing.Size(600, 30);
            this.pbState.Name = "pbState";
            this.pbState.Size = new System.Drawing.Size(600, 30);
            this.pbState.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbState.TabIndex = 0;
            this.pbState.Value = 20;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 611);
            this.Controls.Add(this.pOverlay);
            this.Controls.Add(this.tlpActionButtons);
            this.Controls.Add(this.pView);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(720, 650);
            this.Name = "Form1";
            this.Text = "Person Management";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.gbPerson.ResumeLayout(false);
            this.tlpPersonInput.ResumeLayout(false);
            this.tlpPersonInput.PerformLayout();
            this.pView.ResumeLayout(false);
            this.gbResult.ResumeLayout(false);
            this.tlpActionButtons.ResumeLayout(false);
            this.pOverlay.ResumeLayout(false);
            this.pOverlay.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox gbPerson;
        private System.Windows.Forms.TableLayoutPanel tlpPersonInput;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Label lblLastName;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.TextBox tbxFirstName;
        private System.Windows.Forms.TextBox tbxAge;
        private System.Windows.Forms.TextBox tbxLastName;
        private System.Windows.Forms.Panel pView;
        private System.Windows.Forms.TableLayoutPanel tlpActionButtons;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnExecute;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip ttBtnSearch;
        private System.Windows.Forms.ToolTip ttBtnNew;
        private System.Windows.Forms.ToolTip ttBtnModify;
        private System.Windows.Forms.ToolTip ttBtnDelete;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ToolTip ttBtnCancel;
        private System.Windows.Forms.ToolTip ttBtnExecute;
        private System.Windows.Forms.Button btnModify;
        private System.Windows.Forms.GroupBox gbResult;
        private System.Windows.Forms.ListView lvPeople;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.Panel pOverlay;
        private System.Windows.Forms.ProgressBar pbState;
        private System.Windows.Forms.Label lblProgressState;
    }
}

