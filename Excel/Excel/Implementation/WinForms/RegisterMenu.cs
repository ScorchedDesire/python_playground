using Excel.Core.Abstraction;
using Excel.Shared.DTO;

namespace Excel.Core.Implementation.WinForms
{
    public partial class RegisterMenu : Form
    {
        public async void RegisterProcess()
        {
            // Our registration info
            string username = loginBox.Text;
            string email = emailBox.Text;
            string password = passwordBox.Text;
            // Failsafe - empty fields
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please fill in all fields.");
                return;
            }
            // DTO
            var req = new RegisterRequest
            {
                Username = username,
                Email = email,
                Password = password
            };
            // Attempt registration
            bool ok = await Program.AccountManager.Register(req);
            if (!ok)
            {
                MessageBox.Show("Username or email already in use.");
                return;
            }
            // On success
            MessageBox.Show("Registration successful! You can now log in.");
            this.Close();
        }
        public RegisterMenu()
        {
            InitializeComponent();
        }

        private async void registerButton_Click(object sender, EventArgs e)
        {
            RegisterProcess();
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
