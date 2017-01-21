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
			_modulos = new ObservableCollection<Modulo>(cursos.Modulo.OrderBy(m => m.Ordem));
			listView.ItemsSource = _modulos;
		}

		async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (listView.SelectedItem == null)
				return;

			var moduloSelecionado = e.SelectedItem as Modulo;

			listView.SelectedItem = null;

			var page = new EditarModuloView(moduloSelecionado);

			page.ModuloEditado += (source, modulo) =>
			{
				moduloSelecionado.Id = modulo.Id;
				moduloSelecionado.Descricao = modulo.Descricao;
				moduloSelecionado.Ordem = modulo.Ordem;
				moduloSelecionado.Carga = modulo.Carga;
				moduloSelecionado.Disciplina = modulo.Disciplina;

				//ordenar lista
				_modulos = new ObservableCollection<Modulo>(_modulos.OrderBy(m => m.Ordem));
				listView.ItemsSource = null;
				listView.ItemsSource = _modulos;

				//todo api modulos put
			};

			await Navigation.PushAsync(page);
		}

		async void AdicionarModulo_Clicked(object sender, System.EventArgs e)
		{
			var page = new EditarModuloView(new Modulo());

			page.ModuloAdicionado += (source, modulo) =>
			{
				_modulos.Add(modulo);

				//ordenar lista
				_modulos = new ObservableCollection<Modulo>(_modulos.OrderBy(m => m.Ordem));
				listView.ItemsSource = null;
				listView.ItemsSource = _modulos;

				//todo chamar api post
			};

			await Navigation.PushAsync(page);
		}
	}
}
