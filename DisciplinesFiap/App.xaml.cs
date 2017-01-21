using Xamarin.Forms;

namespace DisciplinesFiap
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new MainPageView();
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		private const string _loginKey = "Login";
		public string LoginKey
		{
			get
			{
				if (Properties.ContainsKey(_loginKey))
					return Properties[_loginKey].ToString();

				return "";
			}
			set
			{
				Properties[_loginKey] = value;
			}
		}

		private const string _passwordKey = "Password";
		public string PasswordKey
		{
			get
			{
				if (Properties.ContainsKey(_passwordKey))
					return Properties[_passwordKey].ToString();

				return "";
			}
			set
			{
				Properties[_passwordKey] = value;
			}
		}

		public static bool UsuarioAutenticado { get; set; }

		public void Logout()
		{
			MainPage = new MainPageView();
		}
	}
}
