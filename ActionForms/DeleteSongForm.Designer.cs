using System.ComponentModel;

namespace SongLibrary
{
    partial class DeleteSongForm
    {
        private IContainer components = null;

        private Label promptLabel;
        private Label labelTitleText;
        private Label labelArtistText;
        private Label labelReleaseDateText;
        private Label labelPriceText;

        internal Label titleLabel;
        internal Label artistLabel;
        internal Label releaseDateLabel;
        internal Label priceLabel;

        private Button deleteButton;
        private Button cancelButton;

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
            promptLabel = new Label();
            labelTitleText = new Label();
            labelArtistText = new Label();
            labelReleaseDateText = new Label();
            labelPriceText = new Label();
            titleLabel = new Label();
            artistLabel = new Label();
            releaseDateLabel = new Label();
            priceLabel = new Label();
            deleteButton = new Button();
            cancelButton = new Button();
            SuspendLayout();
            // 
            // promptLabel
            // 
            promptLabel.AutoSize = true;
            promptLabel.Location = new Point(12, 9);
            promptLabel.Name = "promptLabel";
            promptLabel.Size = new Size(289, 15);
            promptLabel.TabIndex = 0;
            promptLabel.Text = "Are you sure you want to delete the following record?";
            // 
            // labelTitleText
            // 
            labelTitleText.AutoSize = true;
            labelTitleText.Location = new Point(12, 40);
            labelTitleText.Name = "labelTitleText";
            labelTitleText.Size = new Size(33, 15);
            labelTitleText.TabIndex = 1;
            labelTitleText.Text = "Title:";
            // 
            // labelArtistText
            // 
            labelArtistText.AutoSize = true;
            labelArtistText.Location = new Point(12, 70);
            labelArtistText.Name = "labelArtistText";
            labelArtistText.Size = new Size(38, 15);
            labelArtistText.TabIndex = 3;
            labelArtistText.Text = "Artist:";
            // 
            // labelReleaseDateText
            // 
            labelReleaseDateText.AutoSize = true;
            labelReleaseDateText.Location = new Point(12, 100);
            labelReleaseDateText.Name = "labelReleaseDateText";
            labelReleaseDateText.Size = new Size(75, 15);
            labelReleaseDateText.TabIndex = 5;
            labelReleaseDateText.Text = "Release date:";
            // 
            // labelPriceText
            // 
            labelPriceText.AutoSize = true;
            labelPriceText.Location = new Point(12, 130);
            labelPriceText.Name = "labelPriceText";
            labelPriceText.Size = new Size(36, 15);
            labelPriceText.TabIndex = 7;
            labelPriceText.Text = "Price:";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.ForeColor = SystemColors.ControlText;
            titleLabel.Location = new Point(110, 40);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(0, 15);
            titleLabel.TabIndex = 2;
            // 
            // artistLabel
            // 
            artistLabel.AutoSize = true;
            artistLabel.Location = new Point(110, 70);
            artistLabel.Name = "artistLabel";
            artistLabel.Size = new Size(0, 15);
            artistLabel.TabIndex = 4;
            // 
            // releaseDateLabel
            // 
            releaseDateLabel.AutoSize = true;
            releaseDateLabel.Location = new Point(110, 100);
            releaseDateLabel.Name = "releaseDateLabel";
            releaseDateLabel.Size = new Size(0, 15);
            releaseDateLabel.TabIndex = 6;
            // 
            // priceLabel
            // 
            priceLabel.AutoSize = true;
            priceLabel.Location = new Point(110, 130);
            priceLabel.Name = "priceLabel";
            priceLabel.Size = new Size(0, 15);
            priceLabel.TabIndex = 8;
            // 
            // deleteButton
            // 
            deleteButton.BackColor = Color.Firebrick;
            deleteButton.ForeColor = SystemColors.ControlText;
            deleteButton.Location = new Point(139, 168);
            deleteButton.MinimumSize = new Size(90, 28);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(90, 28);
            deleteButton.TabIndex = 9;
            deleteButton.Text = "Delete";
            deleteButton.UseVisualStyleBackColor = false;
            deleteButton.Click += deleteButton_Click;
            // 
            // cancelButton
            // 
            cancelButton.Location = new Point(235, 168);
            cancelButton.MinimumSize = new Size(90, 28);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(90, 28);
            cancelButton.TabIndex = 10;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            cancelButton.Click += cancelButton_Click;
            // 
            // DeleteSongForm
            // 
            AcceptButton = deleteButton;
            AutoScaleDimensions = new SizeF(96F, 96F);
            AutoScaleMode = AutoScaleMode.Dpi;
            CancelButton = cancelButton;
            ClientSize = new Size(452, 219);
            Controls.Add(promptLabel);
            Controls.Add(labelTitleText);
            Controls.Add(titleLabel);
            Controls.Add(labelArtistText);
            Controls.Add(artistLabel);
            Controls.Add(labelReleaseDateText);
            Controls.Add(releaseDateLabel);
            Controls.Add(labelPriceText);
            Controls.Add(priceLabel);
            Controls.Add(deleteButton);
            Controls.Add(cancelButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(468, 258);
            Name = "DeleteSongForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Delete Song";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}