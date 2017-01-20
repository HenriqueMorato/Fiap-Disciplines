using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DisciplinesFiap
{
	public partial class EditarDisciplinaView : ContentPage
	{
		public event EventHandler<Disciplina> DisciplinaEditado;
		
		public EditarDisciplinaView(Disciplina disciplina)
		{
			if (disciplina == null)
				throw new ArgumentNullException(nameof(disciplina));

			InitializeComponent();

			BindingContext = new Disciplina
			{
				Id = disciplina.Id,
				Conteudo = disciplina.Conteudo,
				Descricao = disciplina.Descricao
			};
		}

		async void Save_Clicked(object sender, System.EventArgs e)
		{
			var disciplina = BindingContext as Disciplina;

			if(string.IsNullOrWhiteSpace(disciplina.Conteudo))
			{
				await DisplayAlert("Erro", "Por favor, preencha o conteúdo.", "OK");
				return;
			}

			DisciplinaEditado?.Invoke(this, disciplina);

			await Navigation.PopAsync();
		}
	}
}
