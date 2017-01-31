using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DisciplinesFiap
{
	public partial class CursosView : ContentPage
	{
		private CursoService _cursoService = CursoService.getCursoService();
		private ObservableCollection<Curso> _cursos { get; set; }

        public static async Task<CursosView> Create()
        {
            var myClass = new CursosView();
            await myClass.Initialize();
            return myClass;
        }

        private async Task Initialize()
        {
            _cursos = new ObservableCollection<Curso>(await _cursoService.GetAllCurso());
			BindingContext = _cursos;
            listView.ItemsSource = _cursos;
        }

        private CursosView()
		{
			InitializeComponent();

			//desabilitar itens de edição
			if (!App.UsuarioAutenticado)
			{
				ToolbarItems.Clear();
			}

			NavigationPage.SetHasBackButton(this, false);		
		}

		void Handle_Appearing(object sender, System.EventArgs e)
		{
			if(!App.UsuarioAutenticado)
			{
				var text = (TextCell)sender;

				text.ContextActions.Clear();	
			}
		}

		void Busca_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
		{
			listView.ItemsSource = _cursoService.BuscarCursoPorNome(e.NewTextValue);
		}

		async void Curso_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return;

			var selecaoCurso = e.SelectedItem as Curso;

			listView.SelectedItem = null;

			await Navigation.PushAsync(await CursoDetalheView.Create(selecaoCurso.Id));
		}

		async void AdicionarCurso_Clicked(object sender, System.EventArgs e)
		{
			var page = new EditarCursoView(new Curso());

			page.CursoAdicionado += async (source, curso) =>
            {
                var retorno = await _cursoService.AdicionaCurso(curso);

                if (retorno)
                {
                    await Initialize();
                }
                else
                {
                    await DisplayAlert("Erro", "Ocorreu um erro ao incluir o Curso!", "OK");
                    return;
                }
            };

			await Navigation.PushAsync(page);
		}

		async void EditarCurso_Clicked(object sender, System.EventArgs e)
		{
			var cursoSelecionado = (sender as MenuItem).CommandParameter as Curso;

			var page = new EditarCursoView(cursoSelecionado);

			page.CursoEditado += async (source, curso) =>
            {
                cursoSelecionado.Id = curso.Id;
                cursoSelecionado.Titulo = curso.Titulo;
                cursoSelecionado.Local = curso.Local;
                cursoSelecionado.Inicio = curso.Inicio;
                cursoSelecionado.Duracao = curso.Duracao;
                cursoSelecionado.Dias = curso.Dias;
                cursoSelecionado.Horario = curso.Horario;
                cursoSelecionado.Investimento = curso.Investimento;

                var retorno = await _cursoService.EditarCurso(cursoSelecionado);

                if (retorno)
                {
                    await Initialize();
                }
                else
                {
                    await DisplayAlert("Erro", "Ocorreu um erro ao editar o Curso!", "OK");
                    return;
                }
            };


			await Navigation.PushAsync(page);
		}

		async void EditarModulo_Clicked(object sender, System.EventArgs e)
		{
			var cursoSelecionado = (sender as MenuItem).CommandParameter as Curso;

			var page = await ModuloDetalheView.Create(cursoSelecionado.Id);

			await Navigation.PushAsync(page);
		}

		async void Deletar_Clicked(object sender, System.EventArgs e)
		{
			var cursoSelecionado = (sender as MenuItem).CommandParameter as Curso;

			if(await DisplayAlert("Alerta", $"Tem certeza que quer excluir o curso {cursoSelecionado.Titulo} ?", "Sim", "Não"))
			{
				var retorno = await _cursoService.RemoverCurso(cursoSelecionado);

                if (retorno)
                {
                    await Initialize();
                }
                else
                {
                    await DisplayAlert("Erro", "Ocorreu um erro ao excluir o Curso!", "OK");
                    return;
                }
            }
		}

		//Desabilitar back button dispositivos android
		protected override bool OnBackButtonPressed()
		{
			return true;
		}
	}
}
