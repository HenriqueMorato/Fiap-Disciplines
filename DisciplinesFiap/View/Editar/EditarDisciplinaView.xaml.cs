﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DisciplinesFiap
{
	public partial class EditarDisciplinaView : ContentPage
	{
		public event EventHandler<Disciplina> DisciplinaEditado;
		public event EventHandler<Disciplina> DisciplinaAdicionada;
		//todo pensar numa forma de não ter tantas variáveis
		private List<Modulo> _modulos;
		public Modulo ModuloDisciplina;

		//todo pensar numa forma de não ter tantos args
		public EditarDisciplinaView(Disciplina disciplina, List<Modulo> modulos)
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

			foreach (var m in modulos)
				picker.Items.Add(m.Descricao);

			//modulo pai da disciplina
			//todo pensar numa query melhor
			foreach (Modulo m in modulos)
			{
				foreach (Disciplina d in m.Disciplina)
				{
					if (d.Id == disciplina.Id)
					{
						picker.SelectedIndex = m.Disciplina.FindIndex(a => a.Id == d.Id);
						break;
					}
				}
			}

			if (!string.IsNullOrWhiteSpace(disciplina.Id))
				return;

			label.IsEnabled = true;
			picker.IsEnabled = true;
			_modulos = modulos;
		}

		async void Save_Clicked(object sender, System.EventArgs e)
		{
			var disciplina = BindingContext as Disciplina;

			if(string.IsNullOrWhiteSpace(disciplina.Conteudo))
			{
				await DisplayAlert("Erro", "Por favor, preencha o conteúdo.", "OK");
				return;
			}

			if(String.IsNullOrWhiteSpace(disciplina.Id))
			{
				//todo conferir o id

				DisciplinaAdicionada?.Invoke(this, disciplina);
			}
			else
				DisciplinaEditado?.Invoke(this, disciplina);

			await Navigation.PopAsync();
		}

		void Handle_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(ModuloDisciplina != null)
				ModuloDisciplina = _modulos[picker.SelectedIndex];
		}
	}
}
