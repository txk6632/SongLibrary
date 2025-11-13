using Microsoft.Extensions.Logging;
using SongLibrary.Data;
using System.Data;
using System.Globalization;

namespace SongLibrary
{

    public partial class MainForm : Form
    {
        private readonly ILogger<MainForm> _logger;
        private readonly string _dbPath = Path.Combine(Application.StartupPath, @"Data Sources\SongLibrary.db");
        private DataTable? _songsTable;
        private BindingSource _bindingSource;
        private const string _placeholderText = "Search by ID,Title,Artist,Release Date, or Price...";
        private bool _isPlaceholderActive = true;
        private static readonly Random _rndErr = new Random();
        private bool _isDateFilterApplied = false;
        private string _currentDateFilter = string.Empty;
        private SongRepository _repo;

        public MainForm(ILogger<MainForm> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            InitializeComponent();
            _logger.LogInformation("MainForm initialized");
            _bindingSource = new BindingSource();
            _repo = new SongRepository(_dbPath);
            fromDatePicker.Value = DateTime.Today.AddDays(-30);
            LoadSongsFromDatabase();

            // Setup search box placeholder
            searchBox.Text = _placeholderText;
            searchBox.ForeColor = SystemColors.GrayText;

            // Wire search box events
            searchBox.GotFocus += searchBox_GotFocus;
            searchBox.LostFocus += searchBox_LostFocus;

            // Wire add button click
            btnAdd.Click -= btnAdd_Click;
            btnAdd.Click += btnAdd_Click;

            // Wire form resize event
            Resize -= MainForm_Resize;
            Resize += MainForm_Resize;

            // Wire cell click event
            songLibraryGrid.CellContentClick -= songLibraryGrid_CellContentClick;
            songLibraryGrid.CellContentClick += songLibraryGrid_CellContentClick;

            // Wire date filter buttons
            btnApplyDateFilter.Click -= btnApplyDateFilter_Click;
            btnApplyDateFilter.Click += btnApplyDateFilter_Click;
            btnClearDateFilter.Click -= btnClearDateFilter_Click;
            btnClearDateFilter.Click += btnClearDateFilter_Click;
        }

        // Load songs from SQLite database into DataTable and bind to DataGridView
        private void LoadSongsFromDatabase()
        {
            if (!File.Exists(_dbPath))
            {
                MessageBox.Show($"Database file not found: {_dbPath}", "Missing DB", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var connString = $"Data Source={_dbPath}";
            var dt = new DataTable();           
            try
            {
                dt = _repo.GetAll();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load song records from DB");
                MessageBox.Show($"Failed to load song records: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ModifyDataTable(dt);
            _songsTable = dt;     
            _bindingSource.DataSource = _songsTable.DefaultView;
            ModifyDataviewGrid();
     
        }

        // Modify DataTable to adjust release date column type
        private void ModifyDataTable(DataTable dt)
        {
            //SQLite stores dates as text, needed to convert to DateTime so user can sort on grid
            dt.Columns.Add(ColumnNames.ReleaseDate, typeof(DateTime));
            foreach (DataRow row in dt.Rows)
            {
                DateTime releaseDate = Convert.ToDateTime(row["release_date"]);
                row[ColumnNames.ReleaseDate] = releaseDate;
            }
            dt.Columns.Remove("release_date");
            dt.Columns[ColumnNames.ReleaseDate]!.SetOrdinal(3);
        }

        // Modify DataGridView properties and set tooltips
        private void ModifyDataviewGrid()
        {
            // Set up DataGridView properties and set tooltip text for edit /delete columns
            songLibraryGrid.ShowCellToolTips = true;
            songLibraryGrid.AutoGenerateColumns = true;
            songLibraryGrid.DataSource = _bindingSource;
            songLibraryGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            songLibraryGrid.AllowUserToAddRows = false;
            int lastIndex = songLibraryGrid.Columns.Count - 1;
            songLibraryGrid.Columns[ColumnNames.Delete].DisplayIndex = lastIndex;
            songLibraryGrid.Columns[ColumnNames.Edit].DisplayIndex = lastIndex - 1;
            songLibraryGrid.Columns[ColumnNames.Delete].ToolTipText = "Delete Record";
            songLibraryGrid.Columns[ColumnNames.Edit].ToolTipText = "Edit Record";
            songLibraryGrid.Columns[ColumnNames.Price].DefaultCellStyle.Format = "N2";

            //Wire DataBindingComplete event to set tooltips 
            songLibraryGrid.DataBindingComplete -= songLibraryGrid_DataBindingComplete;
            songLibraryGrid.DataBindingComplete += songLibraryGrid_DataBindingComplete;
            AdjustActionColumnWidths();
        }

        // Adjust Edit/Delete column widths based on grid width so UI remains consistent,
        // since grid is responsive, if it's set to columns fill,the buttons columns become too wide on large screens. I designed 
        // on a widescreen gaming monitor so the buttons were huge when fill mode was used.
        private void AdjustActionColumnWidths()
        {
            if (songLibraryGrid.Columns == null || songLibraryGrid.Columns.Count == 0)
                return;

            // Calculate a sensible width for action columns:
            // small screens = 40px, larger use a fraction of grid width up to max of 120px
            int gridWidth = Math.Max(0, songLibraryGrid.ClientSize.Width);
            int scaled = gridWidth / 30; 
            int actionWidth = Math.Min(Math.Max(40, scaled), 120); 

            if (songLibraryGrid.Columns.Contains(ColumnNames.Edit))
            {
                var col = songLibraryGrid.Columns[ColumnNames.Edit];
                col.Width = actionWidth;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }

            if (songLibraryGrid.Columns.Contains(ColumnNames.Delete))
            {
                var col = songLibraryGrid.Columns[ColumnNames.Delete];
                col.Width = actionWidth;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
        }
        // Apply filter based on search box text
        private void ApplyFilter()
        {
            if (_songsTable == null)
                return;

            var raw = searchBox.Text?.Trim() ?? string.Empty;
            var text = raw.Replace("'", "''");

            // If there's no text search (or placeholder active) I preserve date filter
            if (string.IsNullOrEmpty(text) || _isPlaceholderActive)
            {
                if (_bindingSource.List is DataView dvClear)
                {
                    if (_isDateFilterApplied && !string.IsNullOrWhiteSpace(_currentDateFilter))
                        dvClear.RowFilter = _currentDateFilter;
                    else
                        dvClear.RowFilter = string.Empty;
                }
                else
                {
                    if (_isDateFilterApplied && !string.IsNullOrWhiteSpace(_currentDateFilter))
                        _bindingSource.Filter = _currentDateFilter;
                    else
                        _bindingSource.Filter = null;
                }
                return;
            }

            // Try parse the non-string types
            var isInt = int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var intVal);
            var isDecimal = decimal.TryParse(raw, NumberStyles.Number, CultureInfo.InvariantCulture, out var decVal);
            var isDate = DateTime.TryParse(raw, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateVal)
                         || DateTime.TryParse(raw, out dateVal);

            // Build filter parts for each column
            var parts = new List<string>();

            foreach (DataColumn col in _songsTable.Columns)
            {
                if (col.DataType == typeof(string))
                {
                    parts.Add($"[{col.ColumnName}] LIKE '%{text}%'");
                    continue;
                }

                if (col.DataType == typeof(int) || col.DataType == typeof(long) || col.DataType == typeof(short))
                {
                    if (isInt) parts.Add($"[{col.ColumnName}] = {intVal}");
                    parts.Add($"Convert([{col.ColumnName}], 'System.String') LIKE '%{text}%'");
                    continue;
                }

                if (col.DataType == typeof(decimal) || col.DataType == typeof(double) || col.DataType == typeof(float))
                {
                    if (isDecimal) parts.Add($"[{col.ColumnName}] = {decVal.ToString(CultureInfo.InvariantCulture)}");
                    parts.Add($"Convert([{col.ColumnName}], 'System.String') LIKE '%{text}%'");
                    continue;
                }

                if (col.DataType == typeof(DateTime))
                {
                    if (isDate) parts.Add($"[{col.ColumnName}] = #{dateVal.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}#");
                    parts.Add($"Convert([{col.ColumnName}], 'System.String') LIKE '%{text}%'");
                    continue;
                }

                parts.Add($"Convert([{col.ColumnName}], 'System.String') LIKE '%{text}%'");
            }

            var textFilter = string.Join(" OR ", parts);

            // If a date filter is active, combine them with AND, otherwise apply text-only filter
            if (_bindingSource.List is DataView dv)
            {
                if (_isDateFilterApplied && !string.IsNullOrWhiteSpace(_currentDateFilter))
                    dv.RowFilter = $"({_currentDateFilter}) AND ({textFilter})";
                else
                    dv.RowFilter = textFilter;
            }
            else// In case the list isn't of type DataView, set BindingSource.Filter, this is unlikely but just in case
            {
                if (_isDateFilterApplied && !string.IsNullOrWhiteSpace(_currentDateFilter))
                    _bindingSource.Filter = $"({_currentDateFilter}) AND ({textFilter})";
                else
                    _bindingSource.Filter = textFilter;
            }
        }
        // Confirm and delete song record, runs on cell content click for Delete column
        private void ConfirmAndDelete(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = songLibraryGrid.Rows[e.RowIndex];
            if (row == null) return;

            string title = row.Cells[ColumnNames.Title].Value.ToString() ?? string.Empty;
            string artist = row.Cells[ColumnNames.Artist].Value.ToString() ?? string.Empty;
            int id = Convert.ToInt32(row.Cells[ColumnNames.Id].Value);
            DateTime releaseDate = Convert.ToDateTime(row.Cells[ColumnNames.ReleaseDate].Value);
            decimal price = Convert.ToDecimal(row.Cells[ColumnNames.Price].Value);

            using var dlg = new DeleteSongForm(title, artist, releaseDate, price);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                var rows = _repo.Delete(id);
                if (rows > 0)
                {
                    _logger?.LogInformation($"Deleted Song {title}");
                    MessageBox.Show("Record deleted successfully.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSongsFromDatabase();
                }
                else
                {
                    MessageBox.Show("No rows were deleted.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete record id={Id}", id);
                MessageBox.Show($"Failed to delete record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Edit song record, runs on cell content click for Edit column
        private void EditSongRecord(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            // Get current values
            var title = songLibraryGrid.Rows[e.RowIndex].Cells[ColumnNames.Title].Value?.ToString() ?? string.Empty;
            var artist = songLibraryGrid.Rows[e.RowIndex].Cells[ColumnNames.Artist].Value?.ToString() ?? string.Empty;
            DateTime releaseDate = Convert.ToDateTime(songLibraryGrid.Rows[e.RowIndex].Cells[ColumnNames.ReleaseDate].Value);
            var priceStr = songLibraryGrid.Rows[e.RowIndex].Cells[ColumnNames.Price].Value?.ToString() ?? "0";
            decimal.TryParse(priceStr, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal price);
            int id = Convert.ToInt32(songLibraryGrid.Rows[e.RowIndex].Cells[ColumnNames.Id].Value?.ToString());

            // Show AddEdit dialog passing constructor parameters
            using var dlg = new AddEditSongForm("Edit Song", title, artist, releaseDate, price);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                if (_rndErr.Next(5) == 0)
                {
                    _logger?.LogError("Invalid operation. The connection is closed.(Simulated 1/5 failure)");
                    throw new InvalidOperationException("Invalid operation. The connection is closed.(Simulated 1/5 failure)");
                }
                var rows = _repo.Update(id, dlg.SongTitle, dlg.Artist, dlg.ReleaseDate, dlg.Price);
                if (rows > 0)
                {
                    _logger?.LogInformation($"Updated song {dlg.SongTitle}");
                    MessageBox.Show("Record updated successfully.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadSongsFromDatabase();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update record id={Id}", id);
                MessageBox.Show($"Failed to update record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Add button click handler
        private void btnAdd_Click(object? sender, EventArgs e)
        {
            // Show AddEdit dialog passing only the form title
            using var dlg = new AddEditSongForm("Add Song");
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            try
            {
                if (_rndErr.Next(5) == 0)
                {
                    _logger?.LogError("Invalid operation. The connection is closed.(Simulated 1/5 failure)");
                    throw new InvalidOperationException("Invalid operation. The connection is closed.(Simulated 1/5 failure)");
                }
                var rows = _repo.Insert(dlg.SongTitle, dlg.Artist, dlg.ReleaseDate, dlg.Price);
                if (rows > 0)
                {
                    _logger?.LogInformation($"Added song {dlg.SongTitle}");
                    MessageBox.Show("Record inserted successfully.", "Insert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Refresh grid
                    LoadSongsFromDatabase();
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError($"Failed to insert record: {ex.Message}");
                MessageBox.Show($"Failed to insert record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Set tooltips for action columns after data binding completes
        private void songLibraryGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (songLibraryGrid.Columns.Contains(ColumnNames.Edit) || songLibraryGrid.Columns.Contains(ColumnNames.Delete))
            {
                foreach (DataGridViewRow row in songLibraryGrid.Rows)
                {
                    if (row.IsNewRow) continue;

                    if (songLibraryGrid.Columns.Contains(ColumnNames.Edit))
                    {
                        var editCell = row.Cells[ColumnNames.Edit];
                        editCell.ToolTipText = "Edit Record";
                    }

                    if (songLibraryGrid.Columns.Contains(ColumnNames.Delete))
                    {
                        var deleteCell = row.Cells[ColumnNames.Delete];
                        deleteCell.ToolTipText = "Delete Record";
                    }
                }
            }
        }
     
        //Applies the Date Range Filter
        private void btnApplyDateFilter_Click(object? sender, EventArgs e)
        {
            // Ensure data is loaded
            if (_songsTable == null)
                return;

            // Check if dates are not Null
            if (fromDatePicker == null || toDatePicker == null)
                return;

            var from = fromDatePicker.Value.Date;
            var to = toDatePicker.Value.Date;

            // If user picked backwards, swap the dates
            if (from > to)
            {
                var tmp = from;
                from = to;
                to = tmp;
            }
            // If there is already a date filter applied, ie. user is applying a new date before clicking clear button, I check and clear original date filter first
            if (_isDateFilterApplied)
            {
                if (_bindingSource.List is DataView dvClear)
                {
                    dvClear.RowFilter = string.Empty;
                }
                else
                {                 
                    _bindingSource.Filter = null;           
                }
                _currentDateFilter = string.Empty;
                ApplyFilter();
            }

            
            // DataView RowFilter syntax for date range
            string dateFilter = $"[Release Date] >= #{from.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}# AND [Release Date] <= #{to.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}#";

            // Remember the date filter so ApplyFilter can preserve/merge it
            _currentDateFilter = dateFilter;
            _isDateFilterApplied = true;

            if (_bindingSource.List is DataView dv)
            {
                // Preserve any existing text filter already applied by ApplyFilter
                var existing = dv.RowFilter;
                if (string.IsNullOrWhiteSpace(existing))
                {
                    dv.RowFilter = dateFilter;
                }
                else
                {
                    // Join any existing filter with date filter
                    dv.RowFilter = $"({existing}) AND ({dateFilter})";
                }
            }
            else
            {
                // In case the list isn't of type DataView, set BindingSource.Filter
                var existing = _bindingSource.Filter;
                if (string.IsNullOrWhiteSpace(existing?.ToString()))
                    _bindingSource.Filter = dateFilter;
                else
                    _bindingSource.Filter = $"({existing}) AND ({dateFilter})";
            }
            _logger?.LogInformation("Date filter applied: {from} - {to}", from, to);
        }

        private void btnClearDateFilter_Click(object? sender, EventArgs e)
        {
            if (_songsTable == null)
                return;

            // clear stored date filter then re-apply the text search filter only
            _isDateFilterApplied = false;
            _currentDateFilter = string.Empty;
            ApplyFilter();
            // Reset date pickers to default values
            fromDatePicker.Value = DateTime.Today.AddDays(-30);
            toDatePicker.Value = DateTime.Today;
            _logger?.LogInformation("Date filter cleared");
        }

        // Handle cell content clicks for Edit and Delete actions
        private void songLibraryGrid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            // header or invalid
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var col = songLibraryGrid.Columns[e.ColumnIndex];
            if (col == null) return;

            if (col.Name == ColumnNames.Edit)
            {
                EditSongRecord(e);
            }
            else if (col.Name == ColumnNames.Delete)
            {
                ConfirmAndDelete(e);
            }
        }

        private void searchBox_GotFocus(object? sender, EventArgs e)
        {
            if (_isPlaceholderActive)
            {
                searchBox.Text = string.Empty;
                searchBox.ForeColor = SystemColors.ControlText;
                _isPlaceholderActive = false;
            }
        }
        private void searchBox_LostFocus(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchBox.Text))
            {
                searchBox.Text = _placeholderText;
                searchBox.ForeColor = SystemColors.GrayText;
                _isPlaceholderActive = true;
                ApplyFilter();
            }
        }
        // Search box text changed event handler, applies filter to grid
        private void searchBox_TextChanged(object sender, EventArgs e) => ApplyFilter();
        // Called when the form is resized
        private void MainForm_Resize(object? sender, EventArgs e) => AdjustActionColumnWidths();
    }
    //Add Column names as constants to avoid mistypings
    public static class ColumnNames
    {
        public const string Id = "id";
        public const string Title = "Title";
        public const string Artist = "Artist";
        public const string ReleaseDate = "Release Date";
        public const string Price = "Price";
        public const string Delete = "Delete";
        public const string Edit = "Edit";
    }
}
