using Excel.Core;
using Excel.Core.Abstraction;
using Excel.Core.Implementation.Classes;
using Excel.Core.Implementation.Entities;
using Excel.Core.Implementation.WinForms;
using Excel.Shared.DTO;
using System.Text;

namespace Excel
{
    public partial class MainMenu : Form
    {
        const int caseAdd = 0;
        const int caseDelete = 1;

        const int initialRows = 20;
        const int initialColumns = 10;

        private ITable _table;
        private IFormulaManager _evaluator;
        private IFunctionManager _functions;
        private ISettingsManager _settings;

        private void DisableApp()
        {
            // In case of user being logged out/expired or void license:
            excelGrid.Enabled = false;
            fileToolStripMenuItem.Enabled = false;
            settingsToolStripMenuItem.Enabled = false;
            logoutToolStripMenuItem.Enabled = false;
            accountStatusToolStripMenuItem.Enabled = false;
            applyChangesButton.Enabled = false;
            option1.Enabled = false;
            amountToApply.Enabled = false;
            loginToolStripMenuItem.Enabled = true;
            registerToolStripMenuItem.Enabled = true;
        }

        private void EnableApp()
        {
            // In case of user being logged in with valid license:
            excelGrid.Enabled = true;
            fileToolStripMenuItem.Enabled = true;
            settingsToolStripMenuItem.Enabled = true;
            logoutToolStripMenuItem.Enabled = true;
            accountStatusToolStripMenuItem.Enabled = true;
            applyChangesButton.Enabled = true;
            option1.Enabled = true;
            amountToApply.Enabled = true;
            loginToolStripMenuItem.Enabled = false;
            registerToolStripMenuItem.Enabled = false;
        }

        private async Task UpdateAppState()
        {
            var info = await Program.AccountManager.GetAccountInfo();

            if (info == null)
            {
                // Not logged in
                DisableApp();
                return;
            }

            // License VOID
            if (info.License == LicenseType.Void)
            {
                DisableApp();
                return;
            }

            // Lifetime
            if (info.License == LicenseType.Lifetime)
            {
                EnableApp();
                return;
            }

            // Trial / Standard
            if (info.ExpirationDate.HasValue)
            {
                if (info.ExpirationDate.Value < DateTime.UtcNow)
                {
                    // License expired
                    DisableApp();
                    return;
                }
            }

            // Valid license
            EnableApp();
        }
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
        public void RecalculateTable()
        {
            // Recalculates all cells in the table to update the formulas
            for (int r = 0; r < _table.RowCount; r++)
            {
                for (int c = 0; c < _table.ColumnCount; c++)
                {
                    _evaluator.EvaluateCell(r, c);
                    excelGrid[c, r].Value = _table.GetDisplayValue(r, c);
                }
            }
        }
        private void RefreshSheetUI()
        {
            for (int r = 0; r < _table.RowCount; r++)
                for (int c = 0; c < _table.ColumnCount; c++)
                    excelGrid[c, r].Value = _table.GetDisplayValue(r, c);
        }

        public void InitializeTable(ITable newTable)
        {
            // Table reference
            _table = newTable;
            // Initializing function registry
            _functions = new FunctionManager();
            _functions.AutoRegisterAll();
            // Initializing evaluator
            _evaluator = new FormulaManager(_table, _functions);
            // Initializing event handlers (and removing old ones to prevent stacking)
            _table.CellChanged -= Table_CellChanged;
            // Attach UI update event
            _table.CellChanged += Table_CellChanged;
            // Resizing grid
            excelGrid.RowCount = _table.RowCount;
            excelGrid.ColumnCount = _table.ColumnCount;
            for (int c = 0; c < excelGrid.ColumnCount; c++)
                excelGrid.Columns[c].HeaderText = ToColumnName(c);
            // Re-evaluating whole sheet (to ensure formulas work after loading the file)
            RecalculateTable();
        }

        public void LoadSettings()
        {
            _settings = new SettingsManager();
            _settings.ApplyToGrid(excelGrid);
        }

        public MainMenu()
        {
            // Initializing UI
            InitializeComponent();
            InitializeTable(new Table(initialRows, initialColumns));
            LoadSettings();

            // Attempt auto-login on startup
            Shown += async (_, __) => await TryAutoLogin();
        }

        public void EditColumns()
        {
            var tm = new TableManager();
            switch (option1.SelectedIndex)
            {
                case caseAdd:
                    tm.AddColumns(excelGrid, (int)amountToApply.Value);
                    break;
                case caseDelete:
                    tm.DeleteColumns(excelGrid, (int)amountToApply.Value);
                    break;
            }
        }

        private async Task TryAutoLogin()
        {
            bool loggedIn = await Program.AccountManager.AutoLogin();

            if (loggedIn)
            {
                var info = await Program.AccountManager.GetAccountInfo();
                if (info != null)
                    Text = $"Excel - Logged in as {info.Username}";
            }
            await UpdateAppState();
        }

        // Apply Column changes
        private void applyChangesButton_Click(object sender, EventArgs e)
        {
            EditColumns();
        }

        // After editing our cell...
        private void excelGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string raw = excelGrid[e.ColumnIndex, e.RowIndex].Value?.ToString() ?? "";

            _table.SetRawValue(e.RowIndex, e.ColumnIndex, raw);

            // 1. Force recalculation of all affected cells BEFORE UI updates
            _evaluator.OnCellChanged(e.RowIndex, e.ColumnIndex);

            // 2. NOW update UI – AFTER all dependent formula values are ready
            RefreshSheetUI();
        }

        // On cell change...
        private void Table_CellChanged(int row, int col)
        {
            var v = _table.GetDisplayValue(row, col);
            excelGrid[col, row].Value = v;
        }

        // Before editing our cell...
        private void excelGrid_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // ...display the formula
            excelGrid[e.ColumnIndex, e.RowIndex].Value = _table.GetRawValue(e.RowIndex, e.ColumnIndex)?.ToString() ?? "";
        }

        // Loading a file
        private void loadFile_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog
            {
                Filter = "Mini-Excel File (*.exl)|*.exl"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var fm = new FileManager();
                var loadedTable = fm.Load(dialog.FileName);

                InitializeTable(loadedTable);
            }
        }

        // Saving a file
        private void saveFile_Click(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "Mini-Excel File (*.exl)|*.exl";
                dialog.Title = "Save your spreadsheet...";
                dialog.DefaultExt = "exl";
                dialog.AddExtension = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var fm = new FileManager();
                    fm.Save(dialog.FileName, _table);

                    MessageBox.Show("File saved.");
                }
            }
        }

        private void importFile_Click(object sender, EventArgs e)
        {
            using var dialog = new OpenFileDialog();
            dialog.Filter = "All Supported|*.csv;*.json;*.xls|CSV|*.csv|JSON|*.json|Excel|*.xls";

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var importer = new ImportManager();
            ITable newTable;

            string ext = Path.GetExtension(dialog.FileName).ToLower();

            if (ext == ".csv") newTable = importer.ImportCsv(dialog.FileName);
            else if (ext == ".json") newTable = importer.ImportJson(dialog.FileName);
            else newTable = importer.ImportXls(dialog.FileName);

            InitializeTable(newTable);
        }

        private void exportFile_Click(object sender, EventArgs e)
        {
            using var dialog = new SaveFileDialog();
            dialog.Filter = "CSV|*.csv|JSON|*.json|Excel|*.xls";
            dialog.AddExtension = true;

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            var exporter = new ExportManager();
            string ext = Path.GetExtension(dialog.FileName).ToLower();

            if (ext == ".csv") exporter.ExportCsv(dialog.FileName, _table);
            else if (ext == ".json") exporter.ExportJson(dialog.FileName, _table);
            else exporter.ExportXls(dialog.FileName, _table);

            MessageBox.Show("Exported successfully!");
        }

        private void newFile_Click(object sender, EventArgs e)
        {
            using var newFileMenu = new NewFileMenu();

            if (newFileMenu.ShowDialog() == DialogResult.OK)
            {
                int rows = newFileMenu.Rows;
                int cols = newFileMenu.Columns;
                InitializeTable(new Table(rows, cols));
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var settingsMenu = new SettingsMenu(_settings);

            if (settingsMenu.ShowDialog() == DialogResult.OK)
            {
                _settings.ColumnWidth = settingsMenu.SelectedWidth;
                _settings.RowHeight = settingsMenu.SelectedHeight;
                _settings.Alignment = settingsMenu.SelectedAlignment;

                _settings.SaveSettings();
                _settings.ApplyToGrid(excelGrid);
            }
        }

        private async void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var login = new LoginMenu();
            login.ShowDialog();
            var info = await Program.AccountManager.GetAccountInfo();
            if (info != null)
                Text = $"Excel - Logged in as {info.Username}";
            await UpdateAppState();
        }

        private async void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using var register = new RegisterMenu();
            register.ShowDialog();
            var info = await Program.AccountManager.GetAccountInfo();
            if (info != null)
                Text = $"Excel - Logged in as {info.Username}";
            await UpdateAppState();
        }

        private async void accountStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = await Program.AccountManager.GetAccountInfo();

            if (info == null)
            {
                MessageBox.Show("You are not logged in.");
                return;
            }

            var sb = new StringBuilder();

            sb.AppendLine($"Username: {info.Username}");
            sb.AppendLine($"Email: {info.Email}");
            sb.AppendLine($"License: {info.License}");

            if (info.License == LicenseType.Lifetime)
            {
                // Nothing to show
            }
            else if (info.License == LicenseType.Void)
            {
                // Void license
                sb.AppendLine("License status: VOID");
            }
            else
            {
                // Check expiration if it's Trial or Standard
                if (info.ExpirationDate.HasValue)
                {
                    DateTime exp = info.ExpirationDate.Value;

                    if (exp < DateTime.UtcNow)
                        sb.AppendLine($"License expired on: {exp}");
                    else
                        sb.AppendLine($"Expires: {exp}");
                }
            }

            MessageBox.Show(sb.ToString(), "Account Status");
        }

        private async void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.AccountManager.Logout();
            MessageBox.Show("Logged out successfully.");
            Text = $"Excel";
            await UpdateAppState();
        }
    }
}
