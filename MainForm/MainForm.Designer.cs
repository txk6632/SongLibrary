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
            add_btn = new Button();
            searchBox = new TextBox();
            panel1 = new Panel();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            sqliteCommand1 = new SqliteCommand();
            ((System.ComponentModel.ISupportInitialize)songLibraryGrid).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // songLibraryGrid
            // 
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
            songLibraryGrid.RowHeadersWidth = 102;
            songLibraryGrid.Size = new Size(1538, 679);
            songLibraryGrid.TabIndex = 0;
            // 
            // Edit
            // 
            Edit.HeaderText = "";
            Edit.Image = Resource1.edit_button;
            Edit.MinimumWidth = 50;
            Edit.Name = "Edit";
            Edit.Resizable = DataGridViewTriState.False;
            // 
            // Delete
            // 
            Delete.HeaderText = "";
            Delete.Image = Resource1.trash_can_10416;
            Delete.MinimumWidth = 50;
            Delete.Name = "Delete";
            // 
            // add_btn
            // 
            add_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            add_btn.BackColor = SystemColors.ButtonFace;
            add_btn.Cursor = Cursors.Hand;
            add_btn.FlatStyle = FlatStyle.Flat;
            add_btn.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            add_btn.ForeColor = SystemColors.ControlText;
            add_btn.Image = Resource1.add_button_12017;
            add_btn.ImageAlign = ContentAlignment.MiddleLeft;
            add_btn.Location = new Point(1367, 40);
            add_btn.Margin = new Padding(2, 3, 2, 3);
            add_btn.Name = "add_btn";
            add_btn.Size = new Size(160, 49);
            add_btn.TabIndex = 1;
            add_btn.Text = "Add Song";
            add_btn.TextAlign = ContentAlignment.MiddleRight;
            add_btn.UseVisualStyleBackColor = false;
            // 
            // searchBox
            // 
            searchBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            searchBox.Location = new Point(421, 55);
            searchBox.Margin = new Padding(2, 3, 2, 3);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(788, 23);
            searchBox.TabIndex = 2;
            searchBox.TextChanged += searchBox_TextChanged;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.LightSkyBlue;
            panel1.Controls.Add(pictureBox2);
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(searchBox);
            panel1.Controls.Add(add_btn);
            panel1.Location = new Point(2, 0);
            panel1.Margin = new Padding(2, 3, 2, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1884, 161);
            panel1.TabIndex = 5;
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
            // pictureBox1
            // 
            pictureBox1.Image = Resource1.icons8_search_30;
            pictureBox1.Location = new Point(386, 51);
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
            AutoSize = true;
            BackColor = SystemColors.ButtonFace;
            ClientSize = new Size(1540, 857);
            Controls.Add(panel1);
            Controls.Add(songLibraryGrid);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2, 3, 2, 3);
            MinimumSize = new Size(641, 358);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Music Library";
            ((System.ComponentModel.ISupportInitialize)songLibraryGrid).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SqliteCommand sqliteCommand1;
        private DataGridView songLibraryGrid;
        private Button add_btn;
        private Panel panel1;
        private TextBox searchBox;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private DataGridViewImageColumn Edit;
        private DataGridViewImageColumn Delete;
    }
}

