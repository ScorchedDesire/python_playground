namespace Excel.Core.Implementation.Entities
{
    // Used for transferring table data into files
    public class TableData
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public string?[][] Cells { get; set; } = default!;
    }
}
