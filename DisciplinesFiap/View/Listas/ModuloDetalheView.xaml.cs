﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DisciplinesFiap
{
	public partial class ModuloDetalheView : ContentPage
	{
		private CursoService _service = CursoService.getCursoService();
		private ObservableCollection<Modulo> _modulos { get; set; }
        private int _cursoId { get; set; }

        private ModuloDetalheView(int cursoId)
		{
            _cursoId = cursoId;
			InitializeComponent();
		}

        public static async Task<ModuloDetalheView> Create(int cursoId)
        {
            var myClass = new ModuloDetalheView(cursoId);
            await myClass.Initialize();
            return myClass;
        }

        private async Task Initialize()
        {
            var curso = await _service.GetCurso(_cursoId);
            _modulos = new ObservableCollection<Modulo>(curso.Modulo.OrderBy(m => m.Ordem));
            listView.ItemsSource = _modulos;

            Title = curso.Titulo;
        }

        async void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (listView.SelectedItem == null)
				return;

			var moduloSelecionado = e.SelectedItem as Modulo;

			listView.SelectedItem = null;

			var page = new EditarModuloView(moduloSelecionado);

			page.ModuloEditado += async (source, modulo) =>
            {


                moduloSelecionado.Id = modulo.Id;
                moduloSelecionado.Descricao = modulo.Descricao;
                moduloSelecionado.Ordem = modulo.Ordem;
                moduloSelecionado.Carga = modulo.Carga;
                moduloSelecionado.Disciplina = modulo.Disciplina;
                moduloSelecionado.Curso_Id = _cursoId;

                var retorno = await _service.EditarModulo(moduloSelecionado);

                if (retorno)
                {
                    await Initialize();
                }
                else
                {
                    await DisplayAlert("Erro", "Ocorreu um erro ao editar o Módulo!", "OK");
                    return;
                }
            };

			await Navigation.PushAsync(page);
		}

		async void AdicionarModulo_Clicked(object sender, System.EventArgs e)
		{
			var page = new EditarModuloView(new Modulo());

			page.ModuloAdicionado += async (source, modulo) =>
            {
                modulo.Curso_Id = _cursoId;

                var retorno = await _service.AdicionarModulo(modulo);

                if (retorno)
                {
                    await Initialize();
                }
                else
                {
                    await DisplayAlert("Erro", "Ocorreu um erro ao incluir o Módulo!", "OK");
                    return;
                }
            };

			await Navigation.PushAsync(page);
		}

		async void Deletar_Clicked(object sender, System.EventArgs e)
		{
			var moduloSelecionado = (sender as MenuItem).CommandParameter as Modulo;

			if (await DisplayAlert("Alerta", $"Tem certeza que quer excluir o Módulo {moduloSelecionado.Descricao}", "Sim", "Não"))
			{
                var retorno = await _service.RemoverModulo(moduloSelecionado);

                if (retorno)
                {
                    await Initialize();
                }
                else
                {
                    await DisplayAlert("Erro", "Ocorreu um erro ao excluir o Módulo!", "OK");
                    return;
                }
            }
		}
	}
}
