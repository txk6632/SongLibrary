namespace SongLibrary
{
    public partial class DeleteSongForm : Form
    {
        public int SongId { get; }
        public string SongTitle => titleLabel.Text;
        public string Artist => artistLabel.Text;
        public DateTime ReleaseDate { get; }
        public decimal Price { get; }

        public DeleteSongForm(string title, string artist, DateTime releaseDate, decimal price)
        {
            InitializeComponent();

            titleLabel.Text = title;
            artistLabel.Text = artist;
            releaseDateLabel.Text = releaseDate.ToString("MM-dd-yyyy");
            priceLabel.Text = price.ToString("N2");

            ReleaseDate = releaseDate;
            Price = price;
        }

        private void deleteButton_Click(object? sender, EventArgs e)
        {
            // Confirm deletion by closing with OK
            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancelButton_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}



