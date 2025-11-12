using SongLibrary.Utils;
using System.Data;
using System.Globalization;

namespace UnitTests
{
    public class FilterBuilderTests
    {
        private static DataTable CreateTestTable()
        {
            var dt = new DataTable();
            dt.Columns.Add(new DataColumn("id", typeof(int)));
            dt.Columns.Add(new DataColumn("title", typeof(string)));
            dt.Columns.Add(new DataColumn("artist", typeof(string)));
            dt.Columns.Add(new DataColumn("Release Date", typeof(DateTime)));
            dt.Columns.Add(new DataColumn("Price", typeof(decimal)));
            return dt;
        }

        [Fact]
        public void BuildRowFilter_StringSearch_IncludesLikeForStringColumns()
        {
            var dt = CreateTestTable();
            DataRow row = dt.NewRow();
            row["id"] = 1;
            row["title"] = "Beatles";
            row["artist"] = 1;
            row["Release Date"] = DateTime.Now;
            row["Price"] = 2.00m;
            var filter = FilterBuilder.BuildRowFilter(dt, "Beatles", false);

            Assert.Contains("[title] LIKE '%Beatles%'", filter, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("[artist] LIKE '%Beatles%'", filter, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void BuildRowFilter_IntegerSearch_IncludesExactAndConvertForId()
        {
            var dt = CreateTestTable();
            DataRow row = dt.NewRow();
            row["id"] = 1;
            row["title"] = "Beatles";
            row["artist"] = 1;
            row["Release Date"] = DateTime.Now;
            row["Price"] = 2.00m;
            var filter = FilterBuilder.BuildRowFilter(dt, "123", false);

            Assert.Contains("[id] = 123", filter);
            Assert.Contains("Convert([id], 'System.String') LIKE '%123%'", filter);
        }

        [Fact]
        public void BuildRowFilter_DecimalSearch_IncludesExactForPrice()
        {
            var dt = CreateTestTable();
            var raw = 2.00m.ToString(CultureInfo.InvariantCulture);
            DataRow row = dt.NewRow();
            row["id"] = 1;
            row["title"] = "Beatles";
            row["artist"] = 1;
            row["Release Date"] = DateTime.Now;
            row["Price"] = 2.00m;
            var filter = FilterBuilder.BuildRowFilter(dt, raw, false);

            Assert.Contains($"[Price] = {raw}", filter);
            Assert.Contains("Convert([Price], 'System.String') LIKE", filter);
        }

        [Fact]
        public void BuildRowFilter_DateSearch_IncludesDateLiteral()
        {
            var dt = CreateTestTable();
            var input = "2020-01-01";
            var expectedDateLiteral = "#01/01/2020#"; // MM/dd/yyyy used by BuildRowFilter
            DataRow row = dt.NewRow();
            row["id"] = 1;
            row["title"] = "Beatles";
            row["artist"] = 1;
            row["Release Date"] = "#01/01/2020#";
            row["Price"] = 2.00m;

            var filter = FilterBuilder.BuildRowFilter(dt, input, false);

            Assert.Contains($"[Release Date] = {expectedDateLiteral}", filter);
            Assert.Contains("Convert([Release Date], 'System.String') LIKE", filter);
        }

        [Fact]
        public void BuildRowFilter_Placeholder_ReturnsEmpty()
        {
            var dt = CreateTestTable();
            var filter = FilterBuilder.BuildRowFilter(dt, "anything", true);
            Assert.Equal(string.Empty, filter);
        }
    }
}