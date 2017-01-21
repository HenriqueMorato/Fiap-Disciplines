using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;

namespace DisciplinesFiap
{
	public partial class CursoDetalheView : ContentPage
	{
		private CursoService _service = CursoService.getCursoService();
		private ObservableCollection<Modulo> _modulos { get; set; }

		private ObservableCollection<GroupedDisciplines> _disciplinas { get; set; }

		public CursoDetalheView(int cursoId)
		{
			InitializeComponent();

			var cursos = _service.GetCurso(cursoId);
			_modulos = new ObservableCollection<Modulo>(cursos.Modulo);
			//BindingContext = cursos;
			_disciplinas = GroupedDisciplines.CriarGrupo(_modulos);
			listView.ItemsSource = _disciplinas;
		}

		async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (listView.SelectedItem == null)
				return;

			var disciplinaSelecionada = e.SelectedItem as Disciplina;
			listView.SelectedItem = null;

			var page = new EditarDisciplinaView(disciplinaSelecionada, _modulos.ToList());

			page.DisciplinaEditado += (source, disciplina) =>
			{
				disciplinaSelecionada.Id = disciplina.Id;
				disciplinaSelecionada.Conteudo = disciplina.Conteudo;
				disciplinaSelecionada.Descricao = disciplina.Descricao;

				//todo trocar o modulo pai na api
				//if (((EditarDisciplinaView)source).ModuloNovo != null)
				//{
					
				//}
				//todo put na api
			};

			await Navigation.PushAsync(page);
		}

		void Deletar_Clicked(object sender, System.EventArgs e)
		{
			var disciplinaSelecionado = (sender as MenuItem).CommandParameter as Disciplina;

			//todo pensar numa query melhor
			//todo delete api
			foreach (GroupedDisciplines gd in _disciplinas)
			{
				foreach (Disciplina d in gd)
				{
					if (d.Id == disciplinaSelecionado.Id)
					{
						gd.Remove(d);
						return;
					}
				}
			}
		}
	}
}
