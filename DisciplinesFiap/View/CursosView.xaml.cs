using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DisciplinesFiap
{
	public partial class CursosView : ContentPage
	{
		private CursoService _cursoService = new CursoService();

		public CursosView()
		{
			InitializeComponent();

			NavigationPage.SetHasBackButton(this, false);

			BindingContext = _cursoService.GetAllCurso();
			listView.ItemsSource = _cursoService.GetAllCurso();
		}

		void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			listView.ItemsSource = _cursoService.BuscaCursoPorNome(e.NewTextValue);
		}

		void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return;

			var selecaoCurso = e.SelectedItem as Curso;

			listView.SelectedItem = null;

			Navigation.PushAsync(new CursoDetalheView(int.Parse(selecaoCurso.Id)));
		}

		async void AdicionarCurso_Clicked(object sender, System.EventArgs e)
		{
			var page = new EditarCursoView(new Curso());

			await Navigation.PushAsync(page);
		}

		void Editar_Clicked(object sender, System.EventArgs e)
		{
			throw new NotImplementedException();
		}

		void Deletar_Clicked(object sender, System.EventArgs e)
		{
			throw new NotImplementedException();
		}

		//Desabilitar back button dispositivos android
		protected override bool OnBackButtonPressed()
		{
			return true;
		}
	}
}
