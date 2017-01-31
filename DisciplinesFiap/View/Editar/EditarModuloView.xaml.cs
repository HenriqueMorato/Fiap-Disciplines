using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Threading;
using Xamarin.Forms;

namespace DisciplinesFiap
{
	public partial class EditarModuloView : ContentPage
	{
		public event EventHandler<Modulo> ModuloEditado;
		public event EventHandler<Modulo> ModuloAdicionado;

		public EditarModuloView(Modulo modulo)
		{
			if (modulo == null)
				throw new ArgumentNullException(nameof(modulo));

			InitializeComponent();

			BindingContext = new Modulo
			{
				Id = modulo.Id,
				Descricao = modulo.Descricao,
				Ordem = modulo.Ordem,
				Carga = modulo.Carga,
				Disciplina = modulo.Disciplina
			};

            Title = modulo.Descricao;
		}

		async void Save_Clicked(object sender, System.EventArgs e)
		{
			var modulo = BindingContext as Modulo;

			if(string.IsNullOrWhiteSpace(modulo.Descricao))
			{
				await DisplayAlert("Erro", "Por favor, preencha a descrição.", "OK");
				return; 
			}

            var cancelSrc = new CancellationTokenSource();
            var config = new ProgressDialogConfig()
                .SetTitle("Loading")
                .SetIsDeterministic(false)
                .SetMaskType(MaskType.Black)
                .SetCancel(onCancel: cancelSrc.Cancel);

            using (UserDialogs.Instance.Progress(config))
            {

                if (modulo.Id == 0)
                    ModuloAdicionado?.Invoke(this, modulo);
                else
                    ModuloEditado?.Invoke(this, modulo);

            }

			await Navigation.PopAsync();
		}
	}
}
