using Excel.Core.Abstraction;

namespace Excel.Core.Implementation.WinForms
{
    public partial class SettingsMenu : Form
    {
        public int SelectedWidth { get; private set; }
        public int SelectedHeight { get; private set; }
        public string SelectedAlignment { get; private set; }

        public SettingsMenu(ISettingsManager settings)
        {
            InitializeComponent();

            cellWidthPicker.Value = settings.ColumnWidth;
            cellHeightPicker.Value = settings.RowHeight;

            valueAlignments.Items.AddRange(new[] { "Left", "Center", "Right" });

            string align = settings.Alignment.ToLower();
            switch (align)
            {
                case "center":
                    valueAlignments.SelectedIndex = 1;
                    break;
                case "right":
                    valueAlignments.SelectedIndex = 2;
                    break;
                default:
                    valueAlignments.SelectedIndex = 0;
                    break;
            }
        }
        private void submitButton_Click(object sender, EventArgs e)
        {
            SelectedWidth = (int)cellWidthPicker.Value;
            SelectedHeight = (int)cellHeightPicker.Value;
            SelectedAlignment =
                valueAlignments.SelectedItem?.ToString()?.ToLower() ?? "left";

            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
