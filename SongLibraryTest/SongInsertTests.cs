using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Xunit;

namespace SongLibraryTest
{
    public class SongInsertTests
    {
        private static async Task<int> InsertSongAsync(SqliteConnection conn, string title, string artist, DateTime releaseDate, decimal price)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO song_library (title, artist, release_date, price) VALUES (@title, @artist, @release_date, @price)";
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@artist", artist);
            // Use same formatting as your app
            cmd.Parameters.AddWithValue("@release_date", releaseDate.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("@price", price);
            return await cmd.ExecuteNonQueryAsync();
        }

        [Fact]
        public async Task InsertSong_Succeeds_AndValuesPersist()
        {
            await using var conn = new SqliteConnection("Data Source=:memory:");
            await conn.OpenAsync();

            // Create table schema similar to your app
            using (var create = conn.CreateCommand())
            {
                create.CommandText =
                    @"CREATE TABLE song_library (
                        id INTEGER PRIMARY KEY AUTOINCREMENT,
                        title TEXT,
                        artist TEXT,
                        release_date TEXT,
                        price NUMERIC
                      );";
                create.ExecuteNonQuery();
            }

            var title = "Test Song";
            var artist = "Tester";
            var releaseDate = new DateTime(2023, 11, 10);
            var price = 9.99m;

            var rows = await InsertSongAsync(conn, title, artist, releaseDate, price);
            Assert.Equal(1, rows);

            using var select = conn.CreateCommand();
            select.CommandText = "SELECT title, artist, release_date, price FROM song_library LIMIT 1";
            using var reader = await select.ExecuteReaderAsync();
            Assert.True(await reader.ReadAsync());
            Assert.Equal(title, reader.GetString(0));
            Assert.Equal(artist, reader.GetString(1));
            Assert.Equal(releaseDate.ToString("MM-dd-yyyy", CultureInfo.InvariantCulture), reader.GetString(2));
            // SQLite returns REAL as double; compare by converting
            var storedPrice = Convert.ToDecimal(reader.GetDouble(3));
            Assert.Equal(price, decimal.Round(storedPrice, 2));
        }

    }
}