using Microsoft.Data.Sqlite;
using System.Data;
using System.Globalization;

namespace SongLibrary
{
    public partial class MainForm : Form
    {
        private readonly string _dbPath = Path.Combine(System.Windows.Forms.Application.StartupPath, @"Data Sources\SongLibrary.db");
        private DataTable? _songsTable;
        private BindingSource _bindingSource;
        private const string _placeholderText = "Search by ID,Title,Artist,Release Date, or Price...";
        private bool _isPlaceholderActive = true;
        private static readonly Random _rndErr = new Random();

        public MainForm()
        {
            InitializeComponent();

            _bindingSource = new BindingSource();
            LoadSongsFromDatabase();

            // Setup search box placeholder
            searchBox.Text = _placeholderText;
            searchBox.ForeColor = SystemColors.GrayText;

            // Wire search box events
            searchBox.GotFocus += searchBox_GotFocus;
            searchBox.LostFocus += searchBox_LostFocus;

            // Wire add button click
            add_btn.Click -= add_btn_Click;
            add_btn.Click += add_btn_Click;

            // Wire form resize event
            Resize -= MainForm_Resize;
            Resize += MainForm_Resize;

            // Wire cell click event
            songLibraryGrid.CellContentClick -= songLibraryGrid_CellContentClick;
            songLibraryGrid.CellContentClick += songLibraryGrid_CellContentClick;
        }

        // Handle cell content clicks for Edit and Delete actions
        private void songLibraryGrid_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            // header or invalid
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return; 

            var col = songLibraryGrid.Columns[e.ColumnIndex];
            if (col == null) return;

            if (col.Name == "Edit")
            {
                EditSongRecord(e);
            }
            else if (col.Name == "Delete")
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
                LoadSongsFromDatabase();
            }
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
            var dt = new DataTable()
            {
                Columns =
                {
                    new DataColumn("id", typeof(int)),
                    new DataColumn("title", typeof(string)),
                    new DataColumn("artist", typeof(string)),
                    new DataColumn("release_date", typeof(string)),//SQLite stores dates as text
                    new DataColumn("price", typeof(decimal))
                }
            };

            try
            {
                using var connection = new SqliteConnection(connString);
                connection.Open();

                using var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM song_library";

                using var reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load song records: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ModifyDataTable(dt);

            _songsTable = dt;
            _bindingSource.DataSource = _songsTable.DefaultView;

            ModifyDataviewGrid(dt);
        }
        // Modify DataGridView properties and set tooltips
        private void ModifyDataviewGrid(DataTable dt)
        {
            // Set up DataGridView properties and set tooltip text for edit /delete columns
            songLibraryGrid.ShowCellToolTips = true;
            songLibraryGrid.AutoGenerateColumns = true;
            songLibraryGrid.DataSource = dt;
            songLibraryGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            songLibraryGrid.AllowUserToAddRows = false;
            int lastIndex = songLibraryGrid.Columns.Count - 1;
            songLibraryGrid.Columns["Delete"].DisplayIndex = lastIndex;
            songLibraryGrid.Columns["Edit"].DisplayIndex = lastIndex - 1;

            songLibraryGrid.Columns["Delete"].ToolTipText = "Delete Record";
            songLibraryGrid.Columns["Edit"].ToolTipText = "Edit Record";

            songLibraryGrid.Columns["Price"].DefaultCellStyle.Format = "N2";

            //Wire DataBindingComplete event to set tooltips
            if (songLibraryGrid != null)
            {
                songLibraryGrid.DataBindingComplete -= songLibraryGrid_DataBindingComplete;
                songLibraryGrid.DataBindingComplete += songLibraryGrid_DataBindingComplete;
            }
            else
            {
                throw new InvalidOperationException("Datasource is null.");          
            }

                AdjustActionColumnWidths();
        }
        // Called when the form is resized
        private void MainForm_Resize(object? sender, EventArgs e) => AdjustActionColumnWidths();

        // Adjust Edit/Delete column widths based on grid width so UI remains consistent,
        // since grid is responsive, if it's set to columns fill,the buttons columns become too wide on large screens.
        private void AdjustActionColumnWidths()
        {
            if (songLibraryGrid.Columns == null || songLibraryGrid.Columns.Count == 0)
                return;

            // Calculate a sensible width for action columns:
            // small screens -> 40px, larger -> scaled fraction of grid width, capped.
            int gridWidth = Math.Max(0, songLibraryGrid.ClientSize.Width);
            int scaled = gridWidth / 30; // ~3.3% of grid width
            int actionWidth = Math.Min(Math.Max(40, scaled), 120); // clamp between 40 and 120

            if (songLibraryGrid.Columns.Contains("Edit"))
            {
                var col = songLibraryGrid.Columns["Edit"];
                col.Width = actionWidth;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }

            if (songLibraryGrid.Columns.Contains("Delete"))
            {
                var col = songLibraryGrid.Columns["Delete"];
                col.Width = actionWidth;
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            }
        }

        // Set tooltips for action columns after data binding completes
        private void songLibraryGrid_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (songLibraryGrid.Columns.Contains("Edit") || songLibraryGrid.Columns.Contains("Delete"))
            {
                foreach (DataGridViewRow row in songLibraryGrid.Rows)
                {
                    if (row.IsNewRow) continue;

                    if (songLibraryGrid.Columns.Contains("Edit"))
                    {
                        var editCell = row.Cells["Edit"];
                        editCell.ToolTipText = "Edit Record";
                    }

                    if (songLibraryGrid.Columns.Contains("Delete"))
                    {
                        var deleteCell = row.Cells["Delete"];
                        deleteCell.ToolTipText = "Delete Record";
                    }
                }
            }
        }
        // Modify DataTable to adjust column names and types
        private void ModifyDataTable(DataTable dt)
        {
            //SQLite stores dates as text, convert to DateTime so user can sort on grid
            dt.Columns.Add("Release Date", typeof(DateTime));
            foreach (DataRow row in dt.Rows)
            {
                DateTime releaseDate = Convert.ToDateTime(row["release_date"]);
                row["Release Date"] = releaseDate;
            }
            dt.Columns.Remove("release_date");
            dt.Columns["Release Date"]!.SetOrdinal(3);
            dt.Columns["title"]!.ColumnName = "Title";   
            dt.Columns["artist"]!.ColumnName = "Artist";
            dt.Columns["price"]!.ColumnName = "Price";
        }
        // Apply filter based on search box text
        private void ApplyFilter()
        {
            if (_songsTable == null)
                return;

            var raw = searchBox.Text?.Trim() ?? string.Empty;
            var text = raw.Replace("'", "''");

            if (string.IsNullOrEmpty(text) || _isPlaceholderActive)
            {
                if (_bindingSource.List is DataView dvClear)
                    dvClear.RowFilter = string.Empty;
                else
                    _bindingSource.Filter = null;
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
                // String types, just use LIKE search
                if (col.DataType == typeof(string))
                {
                    parts.Add($"[{col.ColumnName}] LIKE '%{text}%'");
                    continue;
                }

                // Integer types
                if (col.DataType == typeof(int) || col.DataType == typeof(long) || col.DataType == typeof(short))
                {
                    //If parsing returned true, then use the intVal for exact match
                    if (isInt) parts.Add($"[{col.ColumnName}] = {intVal}");
                    // //Always use the raw value as a string for partial match
                    parts.Add($"Convert([{col.ColumnName}], 'System.String') LIKE '%{text}%'");
                    continue;
                }

                // Floating / decimal types
                if (col.DataType == typeof(decimal) || col.DataType == typeof(double) || col.DataType == typeof(float))
                {
                    //If parsing returned true, then use the decVal for exact match
                    if (isDecimal) parts.Add($"[{col.ColumnName}] = {decVal.ToString(CultureInfo.InvariantCulture)}");
                    //Always use the raw value as a string for partial match
                    parts.Add($"Convert([{col.ColumnName}], 'System.String') LIKE '%{text}%'");
                    continue;
                }

                // Date/time types
                if (col.DataType == typeof(DateTime))
                {
                    //If parsing returned true, then use the dateVal for exact match
                    if (isDate) parts.Add($"[{col.ColumnName}] = #{dateVal.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}#");                  
                    //Always use the raw value as a string for partial match
                    parts.Add($"Convert([{col.ColumnName}], 'System.String') LIKE '%{text}%'");
                    continue;
                }

                // Fallback for other types: convert to string and use LIKE, this part isn't necessary for 
                // for this app since all columns are string/int/decimal/date, but included for completeness.
                parts.Add($"Convert([{col.ColumnName}], 'System.String') LIKE '%{text}%'");
            }

            //Creates filter string by combining all parts with OR
            var filter = string.Join(" OR ", parts);

            //If the binding source list is a dataview then apply the filter to the dataview's RowFilter property
            if (_bindingSource.List is DataView dv) 
                dv.RowFilter = filter;
            //If for some reason it's not a dataview, fallback to setting the BindingSource's Filter property
            else
                _bindingSource.Filter = filter;
        }
        // Edit song record, runs on cell content click for Edit column
        private async void EditSongRecord(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            // Get current values
            var title = songLibraryGrid.Rows[e.RowIndex].Cells["title"].Value?.ToString() ?? string.Empty;
            var artist = songLibraryGrid.Rows[e.RowIndex].Cells["artist"].Value?.ToString() ?? string.Empty;
            DateTime releaseDate = Convert.ToDateTime(songLibraryGrid.Rows[e.RowIndex].Cells["release date"].Value);
            decimal price = (decimal)songLibraryGrid.Rows[e.RowIndex].Cells["price"].Value;
            int id = Convert.ToInt32(songLibraryGrid.Rows[e.RowIndex].Cells["id"].Value?.ToString());

            // Show AddEdit dialog passing constructor parameters
            using var dlg = new AddEditSongForm("Edit Song",title, artist, releaseDate, price);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                if (_rndErr.Next(5) == 0)
                {
                    throw new InvalidOperationException("Invalid operation. The connection is closed.(Simulated 1/5 failure)");
                }
                // Update record in database
                string strQuery = "UPDATE song_library SET title=@title, artist=@artist, release_date=@release_date, price=@price WHERE id=@id";
                var rows = await ExecuteNonQuery(strQuery, dlg, id);
                if (rows > 0)
                {
                    MessageBox.Show("Record updated successfully.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Refresh grid
                    LoadSongsFromDatabase();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to update record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        // Add button click handler
        private async void add_btn_Click(object? sender, EventArgs e)
        {      
            // Show AddEdit dialog passing only the form title
            using var dlg = new AddEditSongForm("Add Song");
            if (dlg.ShowDialog(this) != DialogResult.OK) return;
            try
            {
                if (_rndErr.Next(5) == 0)
                {
                    throw new InvalidOperationException("Invalid operation. The connection is closed.(Simulated 1/5 failure)");
                }
                string strQuery = "INSERT INTO song_library (title, artist, release_date, price) VALUES (@title, @artist, @release_date, @price)";
                var rows = await ExecuteNonQuery(strQuery, dlg, null);
                if (rows > 0)
                {
                    MessageBox.Show("Record inserted successfully.", "Insert", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Refresh grid
                    LoadSongsFromDatabase();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to insert record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }        
        }
        private async Task<int> ExecuteNonQuery(string strQuery, AddEditSongForm dlg, int? id)
        {
            using var connection = new SqliteConnection($"Data Source={_dbPath}");
            await connection.OpenAsync();

            using var cmd = connection.CreateCommand();
            cmd.CommandText = strQuery;
            cmd.Parameters.AddWithValue("@title", dlg.SongTitle);
            cmd.Parameters.AddWithValue("@artist", dlg.Artist);
            cmd.Parameters.AddWithValue("@release_date", dlg.ReleaseDate.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("@price", dlg.Price);
            if(id.HasValue)
            {
                cmd.Parameters.AddWithValue("@id", id);
            }
            return await cmd.ExecuteNonQueryAsync();
        }
        // Confirm and delete song record, runs on cell content click for Delete column
        private async void ConfirmAndDelete(DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = songLibraryGrid.Rows[e.RowIndex];
            if (row == null) return;

            string title = row.Cells["Title"].Value.ToString() ?? string.Empty;
            string artist = row.Cells["Artist"].Value.ToString() ?? string.Empty;
            int id = Convert.ToInt32(row.Cells["id"].Value);


            DateTime releaseDate = Convert.ToDateTime(row.Cells["Release Date"].Value);
            decimal price = Convert.ToDecimal(row.Cells["Price"].Value);

            using var dlg = new DeleteSongForm(title, artist, releaseDate, price);
            if (dlg.ShowDialog(this) != DialogResult.OK) return;

            try
            {
                using var connection = new SqliteConnection($"Data Source={_dbPath}");
                await connection.OpenAsync();
                using var cmd = connection.CreateCommand();

                cmd.CommandText = "DELETE FROM song_library WHERE id = @id";
                cmd.Parameters.AddWithValue("@id", id);

                var rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                {
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
                MessageBox.Show($"Failed to delete record: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Search box text changed event handler, applies filter
        private void searchBox_TextChanged(object sender, EventArgs e) => ApplyFilter();

     
    }
}
