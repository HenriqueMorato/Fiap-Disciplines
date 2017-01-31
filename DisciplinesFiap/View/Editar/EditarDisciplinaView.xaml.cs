using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace DisciplinesFiap
{
	public partial class EditarDisciplinaView : ContentPage
	{
		public event EventHandler<Disciplina> DisciplinaEditado;
		public event EventHandler<Disciplina> DisciplinaAdicionada;
		private List<Modulo> _modulos;
		public Modulo ModuloDisciplina;

		public EditarDisciplinaView(Disciplina disciplina, List<Modulo> modulos)
		{
            _modulos = modulos;

            if (disciplina == null)
				throw new ArgumentNullException(nameof(disciplina));

			InitializeComponent();

			BindingContext = new Disciplina
			{
				Id = disciplina.Id,
				Conteudo = disciplina.Conteudo,
				Descricao = disciplina.Descricao
			};


			foreach (var m in modulos)
				picker.Items.Add(m.Id + "|" + m.Descricao);

            if (disciplina.Id != 0)
            {
                var moduloaux = modulos.First(m => m.Id == disciplina.Modulo_Id);
                var moduloauxindice = modulos.IndexOf(moduloaux);

                picker.SelectedIndex = moduloauxindice;
                ModuloDisciplina = moduloaux;
            }
            else
            {
                picker.SelectedIndex = 0;
                ModuloDisciplina = modulos[0];
            }

            Title = disciplina.Descricao;
		}

		async void Save_Clicked(object sender, System.EventArgs e)
		{
			var disciplina = BindingContext as Disciplina;

			if(string.IsNullOrWhiteSpace(disciplina.Conteudo))
			{
				await DisplayAlert("Erro", "Por favor, preencha o conteúdo.", "OK");
				return;
			}

            if (ModuloDisciplina == null)
            {
                await DisplayAlert("Erro", "Por favor, selecione o módulo.", "OK");
                return;
            }

            disciplina.Modulo_Id = ModuloDisciplina.Id;

            if (disciplina.Id == 0)
			{
                disciplina.Modulo_Id = ModuloDisciplina.Id;
                DisciplinaAdicionada?.Invoke(this, disciplina);
			}
			else
				DisciplinaEditado?.Invoke(this, disciplina);

			await Navigation.PopAsync();
		}

		void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
            if (picker.SelectedIndex != -1)
            {
                string moduloString = picker.Items[picker.SelectedIndex];
                string[] conteudo = moduloString.Split('|');

                var modulo = _modulos.First(m => m.Id == int.Parse(conteudo[0]));

                ModuloDisciplina = modulo;
            }
            else
            {
                ModuloDisciplina = null;
            }

		}
	}
}
