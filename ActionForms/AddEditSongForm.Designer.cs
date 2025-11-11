using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SongLibrary
{
    partial class AddEditSongForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        private TextBox titleTextBox;
        private TextBox artistTextBox;
        private DateTimePicker releaseDatePicker;
        private NumericUpDown priceNumeric;
        private Button okButton;
        private Button cancelButton;
        private Label labelTitle;
        private Label labelArtist;
        private Label labelReleaseDate;
        private Label labelPrice;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            titleTextBox = new TextBox();
            artistTextBox = new TextBox();
            releaseDatePicker = new DateTimePicker();
            priceNumeric = new NumericUpDown();
            okButton = new Button();
            cancelButton = new Button();
            labelTitle = new Label();
            labelArtist = new Label();
            labelReleaseDate = new Label();
            labelPrice = new Label();
            ((ISupportInitialize)priceNumeric).BeginInit();
            SuspendLayout();
            // 
            // titleTextBox
            // 
            titleTextBox.Location = new Point(100, 12);
            titleTextBox.Name = "titleTextBox";
            titleTextBox.Size = new Size(260, 23);
            titleTextBox.TabIndex = 1;
            // 
            // artistTextBox
            // 
            artistTextBox.Location = new Point(100, 49);
            artistTextBox.Name = "artistTextBox";
            artistTextBox.Size = new Size(260, 23);
            artistTextBox.TabIndex = 3;
            // 
            // releaseDatePicker
            // 
            releaseDatePicker.Format = DateTimePickerFormat.Short;
            releaseDatePicker.Location = new Point(100, 85);
            releaseDatePicker.Name = "releaseDatePicker";
            releaseDatePicker.Size = new Size(120, 23);
            releaseDatePicker.TabIndex = 5;
            // 
            // priceNumeric
            // 
            priceNumeric.DecimalPlaces = 2;
            priceNumeric.Location = new Point(100, 122);
            priceNumeric.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            priceNumeric.Name = "priceNumeric";
            priceNumeric.Size = new Size(120, 23);
            priceNumeric.TabIndex = 7;
            priceNumeric.ThousandsSeparator = true;
            // 
            // okButton
            // 
            okButton.Location = new Point(113, 165);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 27);
            okButton.TabIndex = 8;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(194, 165);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 27);
            cancelButton.TabIndex = 9;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Location = new Point(12, 15);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(33, 15);
            labelTitle.TabIndex = 0;
            labelTitle.Text = "Title:";
            // 
            // labelArtist
            // 
            labelArtist.AutoSize = true;
            labelArtist.Location = new Point(12, 52);
            labelArtist.Name = "labelArtist";
            labelArtist.Size = new Size(38, 15);
            labelArtist.TabIndex = 2;
            labelArtist.Text = "Artist:";
            // 
            // labelReleaseDate
            // 
            labelReleaseDate.AutoSize = true;
            labelReleaseDate.Location = new Point(12, 89);
            labelReleaseDate.Name = "labelReleaseDate";
            labelReleaseDate.Size = new Size(75, 15);
            labelReleaseDate.TabIndex = 4;
            labelReleaseDate.Text = "Release date:";
            // 
            // labelPrice
            // 
            labelPrice.AutoSize = true;
            labelPrice.Location = new Point(12, 126);
            labelPrice.Name = "labelPrice";
            labelPrice.Size = new Size(36, 15);
            labelPrice.TabIndex = 6;
            labelPrice.Text = "Price:";
            // 
            // EditSongForm
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            ClientSize = new Size(374, 204);
            Controls.Add(cancelButton);
            Controls.Add(okButton);
            Controls.Add(priceNumeric);
            Controls.Add(labelPrice);
            Controls.Add(releaseDatePicker);
            Controls.Add(labelReleaseDate);
            Controls.Add(artistTextBox);
            Controls.Add(labelArtist);
            Controls.Add(titleTextBox);
            Controls.Add(labelTitle);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "EditSongForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Edit Song";
            ((ISupportInitialize)priceNumeric).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}