using Microsoft.Data.Sqlite;
using System;
using System.Data;
using System.Globalization;

namespace SongLibrary.Data
{
    /// <summary>
    /// Encapsulates all DB access for the song_library table.
    /// MainForm should call these methods and not perform SQL itself.
    /// </summary>
    public class SongRepository
    {
        private readonly string _dbPath;

        public SongRepository(string dbPath)
        {
            _dbPath = dbPath ?? throw new ArgumentNullException(nameof(dbPath));
        }

        // SELECT All FROM song_library
        public DataTable GetAll()
        {
            var dt = new DataTable();

            using var conn = new SqliteConnection($"Data Source={_dbPath}");
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM song_library";

            using var reader = cmd.ExecuteReader();
            dt.Load(reader);

            return dt;
        }

        // INSERT INTO song_library 
        public int Insert(string title, string artist, DateTime releaseDate, decimal price)
        {
            using var conn = new SqliteConnection($"Data Source={_dbPath}");
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO song_library (title, artist, release_date, price) VALUES (@title, @artist, @release_date, @price)";
            cmd.Parameters.AddWithValue("@title", title ?? string.Empty);
            cmd.Parameters.AddWithValue("@artist", artist ?? string.Empty);
            // store date as text like previous code
            cmd.Parameters.AddWithValue("@release_date", releaseDate.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("@price", price);

            return cmd.ExecuteNonQuery();
        }

        // UPDATE song_library
        public int Update(int id, string title, string artist, DateTime releaseDate, decimal price)
        {
            using var conn = new SqliteConnection($"Data Source={_dbPath}");
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE song_library SET title=@title, artist=@artist, release_date=@release_date, price=@price WHERE id=@id";
            cmd.Parameters.AddWithValue("@title", title ?? string.Empty);
            cmd.Parameters.AddWithValue("@artist", artist ?? string.Empty);
            cmd.Parameters.AddWithValue("@release_date", releaseDate.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@id", id);

            return cmd.ExecuteNonQuery();
        }

        // DELETE FROM song_library
        public int Delete(int id)
        {
            using var conn = new SqliteConnection($"Data Source={_dbPath}");
            conn.Open();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE FROM song_library WHERE id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            return cmd.ExecuteNonQuery();
        }
    }
}