﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace DisciplinesFiap
{
	public partial class CursoDetalheView : ContentPage
	{
		private CursoService _service = CursoService.getCursoService();
		private ObservableCollection<Modulo> _modulos { get; set; }

		private ObservableCollection<GroupedDisciplines> _disciplinas { get; set; }
        private int _cursoId { get; set; }

        public static async Task<CursoDetalheView> Create(int cursoId)
        {
            var myClass = new CursoDetalheView(cursoId);
            await myClass.Initialize();
            return myClass;
        }

        private async Task Initialize()
        {

            var curso = await _service.GetCurso(_cursoId);
            _modulos = new ObservableCollection<Modulo>(curso.Modulo);
            _disciplinas = GroupedDisciplines.CriarGrupo(_modulos);
            listView.ItemsSource = _disciplinas;

            Title = curso.Titulo;
        }

        private CursoDetalheView(int cursoId)
		{
            _cursoId = cursoId;
			InitializeComponent();

			//desabilitar itens de edição
			if (!App.UsuarioAutenticado)
			{
				ToolbarItems.Clear();
			}
		}

		void Handle_Appearing(object sender, System.EventArgs e)
		{
			if (!App.UsuarioAutenticado)
			{
				var text = (TextCell)sender;

				text.ContextActions.Clear();
			}
		}

		async void AdicionarDisciplina_Clicked(object sender, System.EventArgs e)
		{
            if (_modulos == null || _modulos.Count == 0)
            {
                await DisplayAlert("Erro", "Você primeiro deve criar um módulo para o curso!", "OK");
                return;
            }

			var page = new EditarDisciplinaView(new Disciplina(), _modulos.ToList());

			page.DisciplinaAdicionada += async (source, disciplina) =>
            {
                var retorno = await _service.AdicionarDisciplina(disciplina);

                if (retorno)
                {
                    await Initialize();
                }
                else
                {
                    await DisplayAlert("Erro", "Ocorreu um erro ao incluir a Disciplina!", "OK");
                    return;
                }
            };

			await Navigation.PushAsync(page);
		}

		async void EditarDisciplina_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (listView.SelectedItem == null)
				return;

			var disciplinaSelecionada = e.SelectedItem as Disciplina;
			listView.SelectedItem = null;

			if (!App.UsuarioAutenticado)
				return;

			var page = new EditarDisciplinaView(disciplinaSelecionada, _modulos.ToList());

			page.DisciplinaEditado += async (source, disciplina) =>
            {
                disciplinaSelecionada.Id = disciplina.Id;
                disciplinaSelecionada.Conteudo = disciplina.Conteudo;
                disciplinaSelecionada.Descricao = disciplina.Descricao;
                disciplinaSelecionada.Modulo_Id = disciplina.Modulo_Id;

                var retorno = await _service.EditarDisciplina(disciplinaSelecionada);

                if (retorno)
                {
                    await Initialize();
                }
                else
                {
                    await DisplayAlert("Erro", "Ocorreu um erro ao editar a Disciplina!", "OK");
                    return;
                }

            };

			await Navigation.PushAsync(page);
		}

		async void Deletar_Clicked(object sender, System.EventArgs e)
		{
			var disciplinaSelecionado = (sender as MenuItem).CommandParameter as Disciplina;

            if (await DisplayAlert("Alerta", $"Tem certeza que quer excluir a Disciplina {disciplinaSelecionado.Descricao}", "Sim", "Não"))
            {
                var retorno = await _service.RemoverDisciplina(disciplinaSelecionado);

                if (retorno)
                {
                    await Initialize();
                }
                else
                {
                    await DisplayAlert("Erro", "Ocorreu um erro ao excluir a Disciplina!", "OK");
                    return;
                }
            }
		}
	}
}
