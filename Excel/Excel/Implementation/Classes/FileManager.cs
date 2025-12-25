using Excel.Core.Abstraction;
using Excel.Core.Implementation.Entities;
using System.Text.Json;

namespace Excel.Core.Implementation.Classes
{
    public class FileManager
    {
        // Saves the table data to a JSON file at the specified path
        public void Save(string path, ITable table)
        {
            var data = new TableData
            {
                Rows = table.RowCount,
                Columns = table.ColumnCount,
                Cells = new string?[table.RowCount][]
            };

            for (int r = 0; r < table.RowCount; r++)
            {
                data.Cells[r] = new string?[table.ColumnCount];
                for (int c = 0; c < table.ColumnCount; c++)
                {
                    var raw = table.GetRawValue(r, c);
                    data.Cells[r][c] = raw?.ToString();
                }
            }

            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(path, json);
        }

        // Loads the table data from a JSON file at the specified path
        public ITable Load(string path)
        {
            var json = File.ReadAllText(path);

            var data = JsonSerializer.Deserialize<TableData>(json)
                       ?? throw new Exception("Invalid file format");

            ITable table = new Table(data.Rows, data.Columns);

            for (int r = 0; r < data.Rows; r++)
            {
                for (int c = 0; c < data.Columns; c++)
                {
                    string? raw = data.Cells[r][c];
                    table.SetRawValue(r, c, raw);
                }
            }

            return table;
        }
    }
}
