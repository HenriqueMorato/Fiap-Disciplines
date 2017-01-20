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

		private ObservableCollection<GroupedDisciplines> _disciplinas = new ObservableCollection<GroupedDisciplines>();

		void CreateGroup ()
		{
			var group = new GroupedDisciplines();
			foreach(Modulo m in _modulos)
			{
				group = new GroupedDisciplines() { Descricao = m.Descricao };
				foreach(Disciplina d in _modulos.SelectMany(x => x.Disciplinas))
				{
					group.Add(d);
				}
				_disciplinas.Add(group);
			}

		}

		public CursoDetalheView(int cursoId)
		{
			InitializeComponent();

			var _curso = _service.GetCurso(cursoId);
			_modulos = new ObservableCollection<Modulo>(_curso.Modulos);
			CreateGroup();
			listView.ItemsSource = _disciplinas;

			//_disciplinas = new List<Disciplina>(_modulos.SelectMany(x => x.Disciplinas));
			//BindingContext = _modulos;
			//var dataTemplate = new DataTemplate(typeof(TextCell));
			//dataTemplate.SetBinding(TextCell.TextProperty, "Modulos.Descricao");


			//var listView = new ListView()
			//{
			//	//IsGroupingEnabled = true,
			//	GroupDisplayBinding = new Binding("Descricao"),
			//	ItemsSource = _disciplinas,
			//	ItemTemplate = dataTemplate
			//};

			//Content = listView;
		}
	}
}
