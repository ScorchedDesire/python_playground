using Excel.Core.Abstraction;
using System.Text;
using System.Text.Json;

namespace Excel.Core.Implementation.Classes
{
    // This is our export manager
    // It handles exporting tables to CSV, JSON, and XLS formats
    public class ExportManager
    {
        // Exporting to CSV format
        public void ExportCsv(string path, ITable table)
        {
            // Writes into file
            using (var writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                // Write header
                for (int r = 0; r < table.RowCount; r++)
                {
                    string[] rowData = new string[table.ColumnCount];

                    // Write each column in the row
                    for (int c = 0; c < table.ColumnCount; c++)
                    {
                        var val = table.GetDisplayValue(r, c)?.ToString() ?? "";

                        if (val.Contains(";"))
                            val = $"\"{val}\"";

                        rowData[c] = val;
                    }

                    writer.WriteLine(string.Join(";", rowData));
                }
            }
        }
        // Exporting to JSON format
        public void ExportJson(string path, ITable table)
        {
            // Create a list to hold the rows
            var rows = new List<List<object?>>();

            // Populate the list with table data
            for (int r = 0; r < table.RowCount; r++)
            {
                var row = new List<object?>();

                for (int c = 0; c < table.ColumnCount; c++)
                    row.Add(table.GetDisplayValue(r, c));

                rows.Add(row);
            }

            // Serialize the list to JSON
            var json = JsonSerializer.Serialize(rows, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            // Write JSON to file
            File.WriteAllText(path, json);
        }

        // Exporting to XLS format
        public void ExportXls(string path, ITable table)
        {
            // Build the XML content
            var sb = new StringBuilder();

            // XML Header
            sb.AppendLine("<?xml version=\"1.0\"?>");
            sb.AppendLine("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\">");
            sb.AppendLine("<Worksheet ss:Name=\"Sheet1\">");
            sb.AppendLine("<Table>");

            // Populate the table with data
            for (int r = 0; r < table.RowCount; r++)
            {
                // Start a new row
                sb.AppendLine("<Row>");
                for (int c = 0; c < table.ColumnCount; c++)
                {
                    // Get raw and display values
                    string raw = table.GetRawValue(r, c)?.ToString() ?? "";
                    string display = table.GetDisplayValue(r, c)?.ToString() ?? "";
                    // Check if the raw value is a formula
                    if (raw.StartsWith("="))
                    {
                        string formula = raw.Substring(1).Replace("'", "");
                        formula = XmlEscape(formula);

                        sb.AppendLine(
                            $"<Cell ss:Formula=\"={formula}\"><Data ss:Type=\"String\">{XmlEscape(display)}</Data></Cell>");
                    }
                    else
                    {
                        sb.AppendLine(
                            $"<Cell><Data ss:Type=\"String\">{XmlEscape(display)}</Data></Cell>");
                    }
                }

                sb.AppendLine("</Row>");
            }
            // Close XML tags
            sb.AppendLine("</Table>");
            sb.AppendLine("</Worksheet>");
            sb.AppendLine("</Workbook>");

            // Write XML content to file
            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
        }

        // Helper method to escape XML special characters
        private string XmlEscape(string str)
        {
            if (str == null) return "";
            return str
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;");
        }
    }
}
