using Excel.Core.Abstraction;
using Excel.Core.Implementation.Classes;

namespace Excel
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        public static ISessionManager SessionManager;
        public static IAccountManager AccountManager;

        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            SessionManager = new SessionManager();
            AccountManager = new AccountManager(SessionManager);

            Application.Run(new MainMenu());
        }
    }
}