using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace DisciplinesFiap
{
	public partial class CursosView : ContentPage
	{
		private CursoService _cursoService = new CursoService();
		private ObservableCollection<Curso> _cursos { get; set; }


		public CursosView()
		{
			InitializeComponent();

			NavigationPage.SetHasBackButton(this, false);

			_cursos = new ObservableCollection<Curso>(_cursoService.GetAllCurso());
			//BindingContext = _cursoService.GetAllCurso();
			listView.ItemsSource = _cursos;
		}

		void Busca_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			listView.ItemsSource = _cursoService.BuscaCursoPorNome(e.NewTextValue);
		}

		async void Curso_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return;

			var selecaoCurso = e.SelectedItem as Curso;

			listView.SelectedItem = null;

			await Navigation.PushAsync(new CursoDetalheView(int.Parse(selecaoCurso.Id)));
		}

		async void AdicionarCurso_Clicked(object sender, System.EventArgs e)
		{
			var page = new EditarCursoView(new Curso());

			page.CursoAdicionado += (source, curso) => 
			{
				_cursos.Add(curso);
				_cursoService.AdicionaCurso(curso);
			};

			//todo pushModal
			await Navigation.PushAsync(page);
		}

		async void Editar_Clicked(object sender, System.EventArgs e)
		{
			var cursoSelecionado = (sender as MenuItem).CommandParameter as Curso;

			var page = new EditarCursoView(cursoSelecionado);

			page.CursoEditado += (source, curso) => 
			{
				cursoSelecionado.Id = curso.Id;
				cursoSelecionado.Titulo = curso.Titulo;
				cursoSelecionado.Local = curso.Local;
				cursoSelecionado.Inicio = curso.Inicio;
				cursoSelecionado.Duracao = curso.Duracao;
				cursoSelecionado.Dias = curso.Dias;
				cursoSelecionado.Horario = curso.Horario;
				cursoSelecionado.Investimento = curso.Investimento;
				_cursoService.EditarCurso(cursoSelecionado.Id, cursoSelecionado);
			};

			//todo pushModal
			await Navigation.PushAsync(page);
		}

		async void Deletar_Clicked(object sender, System.EventArgs e)
		{
			var cursoSelecionado = (sender as MenuItem).CommandParameter as Curso;

			if(await DisplayAlert("Alerta", $"Tem certeza que quer deletar o curso {cursoSelecionado.Titulo} ?", "Sim", "Não"))
			{
				_cursos.Remove(cursoSelecionado);
				_cursoService.RemoveCurso(cursoSelecionado);
			}
		}

		//Desabilitar back button dispositivos android
		protected override bool OnBackButtonPressed()
		{
			return true;
		}
	}
}
