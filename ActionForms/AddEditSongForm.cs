
namespace SongLibrary
{
    public partial class AddEditSongForm : Form
    {
        // Preserve comment style from AddSongForm
        public AddEditSongForm(string formTitle, string? title = null, string? artist = null, DateTime? releaseDate = null, decimal price = 0)
        {
            InitializeComponent();    
            this.Text = formTitle;
            titleTextBox.Text = title;
            artistTextBox.Text = artist;
            releaseDatePicker.Value = releaseDate ?? DateTime.Now;
            priceNumeric.Value = price;
        }

        // Expose values to the caller
        public string SongTitle => titleTextBox.Text.Trim();
        public string Artist => artistTextBox.Text.Trim();
        public DateTime ReleaseDate => releaseDatePicker.Value.Date;
        public decimal Price => priceNumeric.Value;

        private void okButton_Click(object sender, EventArgs e)
        {
            // Validate user inputs
            if (string.IsNullOrWhiteSpace(SongTitle))
            {
                MessageBox.Show("Please enter a Title.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                titleTextBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(Artist))
            {
                MessageBox.Show("Please enter an Artist.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                artistTextBox.Focus();
                return;
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
