using Excel.Core.Abstraction;

namespace Excel.Core.Implementation.Classes
{
    public class TableManager
    {
        // Converts zero index to Excel-style column name
        private string ToColumnName(int index)
        {
            string name = "";
            while (index >= 0)
            {
                name = (char)('A' + (index % 26)) + name;
                index = index / 26 - 1;
            }
            return name;
        }
        // Adds columns to a DataGridView on demand
        public void AddColumns(DataGridView grid, int count)
        {
            int startIndex = grid.Columns.Count;
            DataGridViewColumn[] cols = new DataGridViewColumn[count];

            for (int i = 0; i < count; i++)
            {
                int colIndex = startIndex + i;
                string colName = ToColumnName(colIndex);

                cols[i] = new DataGridViewTextBoxColumn
                {
                    Name = colName,
                    HeaderText = colName
                };
            }

            grid.Columns.AddRange(cols);
        }
        // Deletes columns from a DataGridView on demand
        public void DeleteColumns(DataGridView grid, int count)
        {
            // Failsafe - in case of count > available columns
            int available = grid.Columns.Count;
            if (count > available)
                count = available;

            // Column removal
            for (int i = 0; i < count; i++)
            {
                int index = grid.Columns.Count - 1;
                grid.Columns.RemoveAt(index);
            }
        }

    }
}
