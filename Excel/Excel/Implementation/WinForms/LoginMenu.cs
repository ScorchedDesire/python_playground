using Excel.Shared.DTO;

namespace Excel.Core
{
    public partial class LoginMenu : Form
    {
        public async void LoginProcess()
        {
            // Our login and password
            string username = loginBox.Text;
            string password = passwordBox.Text;
            // Failsafe - empty fields
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Please enter username and password.");
                return;
            }
            // DTO
            var req = new LoginRequest
            {
                Username = username,
                Password = password
            };
            // Attempt login
            bool ok = await Program.AccountManager.Login(req);
            if (!ok)
            {
                MessageBox.Show("Invalid username or password.");
                return;
            }
            // On success
            this.Hide();
        }
        public LoginMenu()
        {
            InitializeComponent();
        }

        private async void loginButton_Click(object sender, EventArgs e)
        {
            LoginProcess();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
