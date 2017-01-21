using DisciplinesFiap.Model;
using System;
using System.Collections.Generic;

using Xamarin.Forms;
using System.Linq;

namespace DisciplinesFiap
{
	public partial class LoginView : ContentPage
	{
		private CursoService _cursoService = CursoService.getCursoService();
        public LoginView()
		{
			InitializeComponent();

			var app = Application.Current as App;

			BindingContext = new Usuario(app.LoginKey);

			if (!App.UsuarioAutenticado)
				return;

			var stackLayout = new StackLayout()
			{
				Spacing = 20,
				Padding = 50,
				VerticalOptions = LayoutOptions.Center,
			};
			var button = new Button { Text = "Sair" };
			button.Clicked += OnLogout_Clicked;
			stackLayout.Children.Add(button);

			Content = stackLayout;
		}

		async void OnLogout_Clicked (object sender, System.EventArgs e)
		{
			App.UsuarioAutenticado = false;
			((App)App.Current).Logout();
		}

		async void OnLogin_Clicked(object sender, System.EventArgs e)
		{
            var usuario = BindingContext as Usuario;

            if (string.IsNullOrWhiteSpace(usuario.Login) || string.IsNullOrWhiteSpace(usuario.Senha))
            {
                await DisplayAlert("Atenção", "Por Favor entre com o usuário e senha!", "OK");
                return;
            }

            var autenticado = await _cursoService.Autenticar(usuario);

            if (!autenticado)
            {
                await DisplayAlert("Atenção", "Usuário/senha inválido!", "OK");
                return;
            }

			var app = Application.Current as App;
			app.LoginKey = usuario.Login;
			App.UsuarioAutenticado = true;

            await Navigation.PushAsync(await CursosView.Create());
		}
	}
}
