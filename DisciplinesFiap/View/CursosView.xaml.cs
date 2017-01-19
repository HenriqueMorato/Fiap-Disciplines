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

			listView.ItemsSource = _cursoService.GetAllCurso();
		}

		void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			listView.ItemsSource = _cursoService.BuscaCursoPorNome(e.NewTextValue);
		}

		void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			throw new NotImplementedException();
		}

		//Dispositivos android com back button
		protected override bool OnBackButtonPressed()
		{
			return true;
		}
	}
}
