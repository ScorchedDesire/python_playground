namespace Excel.Core.Implementation.WinForms
{
    public partial class NewFileMenu : Form
    {
        public int Rows { get; private set; }
        public int Columns { get; private set; }
        public NewFileMenu()
        {
            InitializeComponent();
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            Rows = (int)rowCount.Value;
            Columns = (int)columnCount.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
