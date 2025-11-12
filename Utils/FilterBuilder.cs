using System.Data;
using System.Globalization;

namespace SongLibrary.Utils
{
    // Testable helper that builds a DataView RowFilter string from user input.
    public static class FilterBuilder
    {
        public static string BuildRowFilter(DataTable table, string? rawInput, bool isPlaceholderActive)
        {
            if (table == null) throw new ArgumentNullException(nameof(table));
            var raw = (rawInput ?? string.Empty).Trim();
            var text = raw.Replace("'", "''");

            if (string.IsNullOrEmpty(text) || isPlaceholderActive)
                return string.Empty;

            var isInt = int.TryParse(raw, NumberStyles.Integer, CultureInfo.InvariantCulture, out var intVal);
            var isDecimal = decimal.TryParse(raw, NumberStyles.Number, CultureInfo.InvariantCulture, out var decVal);
            var isDate = DateTime.TryParse(raw, CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateVal)
                         || DateTime.TryParse(raw, out dateVal);

            var parts = new List<string>();

            foreach (DataColumn col in table.Columns)
            {
                var colName = col.ColumnName;

                if (col.DataType == typeof(string))
                {
                    parts.Add($"[{colName}] LIKE '%{text}%'");
                    continue;
                }

                if (col.DataType == typeof(int) || col.DataType == typeof(long) || col.DataType == typeof(short))
                {
                    if (isInt) parts.Add($"[{colName}] = {intVal}");
                    parts.Add($"Convert([{colName}], 'System.String') LIKE '%{text}%'");
                    continue;
                }

                if (col.DataType == typeof(decimal) || col.DataType == typeof(double) || col.DataType == typeof(float))
                {
                    if (isDecimal) parts.Add($"[{colName}] = {decVal.ToString(CultureInfo.InvariantCulture)}");
                    parts.Add($"Convert([{colName}], 'System.String') LIKE '%{text}%'");
                    continue;
                }

                if (col.DataType == typeof(DateTime))
                {
                    if (isDate)
                    {
                        parts.Add($"[{colName}] = #{dateVal.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}#");
                    }
                    parts.Add($"Convert([{colName}], 'System.String') LIKE '%{text}%'");
                    continue;
                }

                // fallback
                parts.Add($"Convert([{colName}], 'System.String') LIKE '%{text}%'");
            }

            return string.Join(" OR ", parts);
        }
    }
}