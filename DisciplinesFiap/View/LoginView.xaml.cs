using DisciplinesFiap.Model;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DisciplinesFiap
{
	public partial class LoginView : ContentPage
	{
        private CursoService _cursoService = CursoService.getCursoService();
        public LoginView()
		{
			InitializeComponent();

            BindingContext = new Usuario();
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

            await Navigation.PushAsync(await CursosView.Create());
		}
	}
}
