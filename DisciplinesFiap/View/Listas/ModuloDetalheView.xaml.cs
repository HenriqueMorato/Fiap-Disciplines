using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace DisciplinesFiap
{
	public partial class ModuloDetalheView : ContentPage
	{
		private CursoService _service = CursoService.getCursoService();
		private ObservableCollection<Modulo> _modulos { get; set; }

		public ModuloDetalheView(int cursoId)
		{
			InitializeComponent();

			var cursos = _service.GetCurso(cursoId);
			_modulos = new ObservableCollection<Modulo>(cursos.Modulo);
			_modulos.OrderBy(m => m.Ordem);
			listView.ItemsSource = _modulos;
		}

		async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (listView.SelectedItem == null)
				return;

			var moduloSelecionado = e.SelectedItem as Modulo;

			listView.SelectedItem = null;

			var page = new EditarModuloView(moduloSelecionado);

			//page.DisciplinaEditado += (source, disciplina) =>
			//{
			//	disciplinaSelecionada.Id = disciplina.Id;
			//	disciplinaSelecionada.Conteudo = disciplina.Conteudo;
			//	disciplinaSelecionada.Descricao = disciplina.Descricao;
			//};

			await Navigation.PushAsync(page);
		}
	}
}
