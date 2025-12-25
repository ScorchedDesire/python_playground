using Excel.Core.Abstraction;

namespace Excel.Core.Implementation.Classes
{
    public class SettingsManager : ISettingsManager
    {
        // Settings file name
        private const string FileName = "settings.ini";
        public int ColumnWidth { get; set; }
        public int RowHeight { get; set; }
        public string Alignment { get; set; }

        public SettingsManager()
        {
            LoadSettings();
        }

        // Load settings from the file
        public void LoadSettings()
        {
            if (!File.Exists(FileName))
            {
                CreateDefaultSettings();
                return;
            }

            var lines = File.ReadAllLines(FileName);
            var dict = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (!line.Contains("=")) continue;

                var parts = line.Split('=', 2);
                dict[parts[0].Trim().ToLower()] = parts[1].Trim();
            }

            // Read settings with fallbacks (In case of missing entries or corrupted file)
            ColumnWidth = ReadInt(dict, "width", 90);
            RowHeight = ReadInt(dict, "height", 30);
            Alignment = ReadString(dict, "align", "left");
        }
        // Save settings to the file
        public void SaveSettings()
        {
            using var writer = new StreamWriter(FileName, false);

            writer.WriteLine($"width={ColumnWidth}");
            writer.WriteLine($"height={RowHeight}");
            writer.WriteLine($"align={Alignment}");
        }
        // Apply changes
        public void ApplyToGrid(DataGridView grid)
        {
            foreach (DataGridViewColumn col in grid.Columns)
                col.Width = ColumnWidth;

            foreach (DataGridViewRow row in grid.Rows)
                row.Height = RowHeight;

            DataGridViewContentAlignment align;

            switch (Alignment.ToLower())
            {
                case "center":
                    align = DataGridViewContentAlignment.MiddleCenter;
                    break;
                case "right":
                    align = DataGridViewContentAlignment.MiddleRight;
                    break;
                default:
                    align = DataGridViewContentAlignment.MiddleLeft;
                    break;
            }

            foreach (DataGridViewColumn col in grid.Columns)
                col.DefaultCellStyle.Alignment = align;
        }
        // Default settings
        private void CreateDefaultSettings()
        {
            ColumnWidth = 90;
            RowHeight = 30;
            Alignment = "left";

            SaveSettings();
        }
        // Helpers to read values
        private int ReadInt(Dictionary<string, string> dict, string key, int fallback)
        {
            if (dict.TryGetValue(key, out string value) &&
                int.TryParse(value, out int result))
                return result;

            return fallback;
        }
        private string ReadString(Dictionary<string, string> dict, string key, string fallback)
        {
            if (dict.TryGetValue(key, out string value))
                return value;

            return fallback;
        }
    }
}
