namespace Excel.Core.Abstraction
{
    public interface ISettingsManager
    {
        int ColumnWidth { get; set; }
        int RowHeight { get; set; }
        string Alignment { get; set; }  // "left", "center", "right"

        void LoadSettings();
        void SaveSettings();

        void ApplyToGrid(DataGridView grid);
    }
}
