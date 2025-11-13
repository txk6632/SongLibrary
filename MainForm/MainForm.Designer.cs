using Microsoft.Data.Sqlite;
using System.Data;

namespace SongLibrary
{
    partial class MainForm
    {
       
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        /// 

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            songLibraryGrid = new DataGridView();
            Edit = new DataGridViewImageColumn();
            Delete = new DataGridViewImageColumn();
            btnAdd = new Button();
            searchBox = new TextBox();
            panel1 = new Panel();
            groupBox1 = new GroupBox();
            btnClearDateFilter = new Button();
            fromDatePicker = new DateTimePicker();
            label2 = new Label();
            btnApplyDateFilter = new Button();
            label3 = new Label();
            toDatePicker = new DateTimePicker();
            flowLayoutPanel1 = new FlowLayoutPanel();
            pictureBox2 = new PictureBox();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            sqliteCommand1 = new SqliteCommand();
            ((System.ComponentModel.ISupportInitialize)songLibraryGrid).BeginInit();
            panel1.SuspendLayout();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // songLibraryGrid
            // 
            songLibraryGrid.AllowUserToAddRows = false;
            songLibraryGrid.AllowUserToDeleteRows = false;
            songLibraryGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            songLibraryGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            songLibraryGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            songLibraryGrid.BackgroundColor = Color.AliceBlue;
            songLibraryGrid.BorderStyle = BorderStyle.Fixed3D;
            songLibraryGrid.CellBorderStyle = DataGridViewCellBorderStyle.Raised;
            songLibraryGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            songLibraryGrid.Columns.AddRange(new DataGridViewColumn[] { Edit, Delete });
            songLibraryGrid.Location = new Point(1, 162);
            songLibraryGrid.Margin = new Padding(1);
            songLibraryGrid.Name = "songLibraryGrid";
            songLibraryGrid.ReadOnly = true;
            songLibraryGrid.RowHeadersWidth = 102;
            songLibraryGrid.Size = new Size(1272, 601);
            songLibraryGrid.TabIndex = 0;
            // 
            // Edit
            // 
            Edit.HeaderText = "";
            Edit.Image = Resource1.edit_button;
            Edit.MinimumWidth = 50;
            Edit.Name = "Edit";
            Edit.ReadOnly = true;
            Edit.Resizable = DataGridViewTriState.False;
            // 
            // Delete
            // 
            Delete.HeaderText = "";
            Delete.Image = Resource1.trash_can_10416;
            Delete.MinimumWidth = 50;
            Delete.Name = "Delete";
            Delete.ReadOnly = true;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnAdd.BackColor = SystemColors.ButtonFace;
            btnAdd.Cursor = Cursors.Hand;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAdd.ForeColor = SystemColors.ControlText;
            btnAdd.Image = Resource1.add_button_12017;
            btnAdd.ImageAlign = ContentAlignment.MiddleLeft;
            btnAdd.Location = new Point(1101, 38);
            btnAdd.Margin = new Padding(2, 3, 2, 3);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(160, 49);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add Song";
            btnAdd.TextAlign = ContentAlignment.MiddleRight;
            btnAdd.UseVisualStyleBackColor = false;
            // 
            // searchBox
            // 
            searchBox.Anchor = AnchorStyles.Top;
            searchBox.Location = new Point(349, 39);
            searchBox.Margin = new Padding(2, 3, 2, 3);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(591, 23);
            searchBox.TabIndex = 2;
            searchBox.TextChanged += searchBox_TextChanged;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.LightSkyBlue;
            panel1.Controls.Add(groupBox1);
            panel1.Controls.Add(flowLayoutPanel1);
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(searchBox);
            panel1.Controls.Add(btnAdd);
            panel1.Location = new Point(2, 0);
            panel1.Margin = new Padding(2, 3, 2, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1271, 161);
            panel1.TabIndex = 5;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top;
            groupBox1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            groupBox1.Controls.Add(btnClearDateFilter);
            groupBox1.Controls.Add(fromDatePicker);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(btnApplyDateFilter);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(toDatePicker);
            groupBox1.Location = new Point(387, 68);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(508, 80);
            groupBox1.TabIndex = 14;
            groupBox1.TabStop = false;
            groupBox1.Text = "Date Range Filter";
            // 
            // btnClearDateFilter
            // 
            btnClearDateFilter.Anchor = AnchorStyles.None;
            btnClearDateFilter.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnClearDateFilter.Location = new Point(368, 42);
            btnClearDateFilter.Name = "btnClearDateFilter";
            btnClearDateFilter.Size = new Size(110, 23);
            btnClearDateFilter.TabIndex = 12;
            btnClearDateFilter.Text = "Clear Date Filter";
            btnClearDateFilter.UseVisualStyleBackColor = true;
            btnClearDateFilter.Click += btnClearDateFilter_Click;
            // 
            // fromDatePicker
            // 
            fromDatePicker.Anchor = AnchorStyles.None;
            fromDatePicker.Format = DateTimePickerFormat.Short;
            fromDatePicker.Location = new Point(30, 42);
            fromDatePicker.Name = "fromDatePicker";
            fromDatePicker.Size = new Size(101, 23);
            fromDatePicker.TabIndex = 7;
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top;
            label2.AutoSize = true;
            label2.Location = new Point(30, 22);
            label2.Name = "label2";
            label2.Size = new Size(62, 15);
            label2.TabIndex = 9;
            label2.Text = "From Date";
            // 
            // btnApplyDateFilter
            // 
            btnApplyDateFilter.Anchor = AnchorStyles.None;
            btnApplyDateFilter.Location = new Point(244, 42);
            btnApplyDateFilter.Name = "btnApplyDateFilter";
            btnApplyDateFilter.Size = new Size(119, 23);
            btnApplyDateFilter.TabIndex = 11;
            btnApplyDateFilter.Text = "Apply Date Filter";
            btnApplyDateFilter.UseVisualStyleBackColor = true;
            btnApplyDateFilter.Click += btnApplyDateFilter_Click;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top;
            label3.AutoSize = true;
            label3.Location = new Point(137, 22);
            label3.Name = "label3";
            label3.Size = new Size(47, 15);
            label3.TabIndex = 10;
            label3.Text = "To Date";
            // 
            // toDatePicker
            // 
            toDatePicker.Anchor = AnchorStyles.None;
            toDatePicker.Format = DateTimePickerFormat.Short;
            toDatePicker.Location = new Point(137, 42);
            toDatePicker.Name = "toDatePicker";
            toDatePicker.Size = new Size(101, 23);
            toDatePicker.TabIndex = 8;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top;
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            flowLayoutPanel1.Location = new Point(298, 105);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(0, 0);
            flowLayoutPanel1.TabIndex = 13;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Resource1.music_library_svgrepo_com;
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.Margin = new Padding(2, 3, 2, 3);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(250, 148);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 4;
            pictureBox2.TabStop = false;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(1060, 130);
            label1.Name = "label1";
            label1.Size = new Size(200, 18);
            label1.TabIndex = 6;
            label1.Text = "*Click Column Headers to Sort";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top;
            pictureBox1.Image = Resource1.icons8_search_30;
            pictureBox1.Location = new Point(313, 35);
            pictureBox1.Margin = new Padding(2, 3, 2, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(31, 32);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // sqliteCommand1
            // 
            sqliteCommand1.CommandTimeout = 30;
            sqliteCommand1.Connection = null;
            sqliteCommand1.Transaction = null;
            sqliteCommand1.UpdatedRowSource = UpdateRowSource.None;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(1274, 779);
            Controls.Add(panel1);
            Controls.Add(songLibraryGrid);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2, 3, 2, 3);
            MinimumSize = new Size(1290, 818);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Music Library";
            ((System.ComponentModel.ISupportInitialize)songLibraryGrid).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SqliteCommand sqliteCommand1;
        private DataGridView songLibraryGrid;
        private Button btnAdd;
        private Panel panel1;
        private TextBox searchBox;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private DataGridViewImageColumn Edit;
        private DataGridViewImageColumn Delete;
        private Label label1;
        private Label label3;
        private Label label2;
        private DateTimePicker toDatePicker;
        private DateTimePicker fromDatePicker;
        private Button btnClearDateFilter;
        private Button btnApplyDateFilter;
        private FlowLayoutPanel flowLayoutPanel1;
        private GroupBox groupBox1;
    }
}

