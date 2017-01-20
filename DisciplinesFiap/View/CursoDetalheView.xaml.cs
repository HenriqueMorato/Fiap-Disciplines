using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;

namespace DisciplinesFiap
{
	public partial class CursoDetalheView : ContentPage
	{
		private CursoService _service = new CursoService();
		private ObservableCollection<Modulo> _modulos { get; set; }

		//private ObservableCollection<Disciplina> _disciplinas { get; set; }

		public CursoDetalheView(int cursoId)
		{
			InitializeComponent();

			var cursos = _service.GetCurso(cursoId);
			_modulos = new ObservableCollection<Modulo>(cursos.Modulos);
			//BindingContext = cursos;
			listView.ItemsSource = GroupedDisciplines.CriarGrupo(_modulos);
		}

		void Editar_Clicked(object sender, System.EventArgs e)
		{
			//var cursoSelecionado = (sender as MenuItem).CommandParameter as Curso;

			//var page = new EditarCursoView(cursoSelecionado);

			//page.CursoEditado += (source, curso) =>
			//{
			//	cursoSelecionado.Id = curso.Id;
			//	cursoSelecionado.Titulo = curso.Titulo;
			//	cursoSelecionado.Local = curso.Local;
			//	cursoSelecionado.Inicio = curso.Inicio;
			//	cursoSelecionado.Duracao = curso.Duracao;
			//	cursoSelecionado.Dias = curso.Dias;
			//	cursoSelecionado.Horario = curso.Horario;
			//	cursoSelecionado.Investimento = curso.Investimento;
			//	_cursoService.EditarCurso(cursoSelecionado.Id, cursoSelecionado);
			//};

			////todo pushModal
			//await Navigation.PushAsync(page);
		}

		async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (listView.SelectedItem == null)
				return;

			var disciplinaSelecionada = e.SelectedItem as Disciplina;

			listView.SelectedItem = null;

			var page = new EditarDisciplinaView(disciplinaSelecionada);

			page.DisciplinaEditado += (source, disciplina) =>
			{
				disciplinaSelecionada.Id = disciplina.Id;
				disciplinaSelecionada.Conteudo = disciplina.Conteudo;
				disciplinaSelecionada.Descricao = disciplina.Descricao;
			};

			await Navigation.PushAsync(page);
		}
	}
}
