using Microsoft.Extensions.Logging;

namespace SongLibrary
{
    public partial class DeleteSongForm : Form
    {
        public int SongId { get; }
        public string SongTitle => titleLabel.Text;
        public string Artist => artistLabel.Text;
        public DateTime ReleaseDate { get; }
        public decimal Price { get; }
        private readonly ILogger<DeleteSongForm>? _logger;

        public DeleteSongForm(string title, string artist, DateTime releaseDate, decimal price, ILogger<DeleteSongForm>? logger = null)
        {
            _logger = logger;
            InitializeComponent();

            titleLabel.Text = title;
            artistLabel.Text = artist;
            releaseDateLabel.Text = releaseDate.ToString("MM-dd-yyyy");
            priceLabel.Text = price.ToString("N2");
            ReleaseDate = releaseDate;
            Price = price;
            _logger?.LogDebug("DeleteSongForm opened for {title} by {artist}", title, artist);
        }

        private void deleteButton_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            _logger?.LogDebug("DeleteSongForm Closed OK");
            Close();
        }

        private void cancelButton_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            _logger?.LogDebug("DeleteSongForm Closed Cancel");
            Close();
        }

    }
}



