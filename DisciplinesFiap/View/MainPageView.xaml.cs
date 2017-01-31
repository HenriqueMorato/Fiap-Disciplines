using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DisciplinesFiap
{
	public partial class MainPageView : MasterDetailPage
	{
		public MainPageView()
		{
			InitializeComponent();

			menuPage.ListView.ItemSelected += ListView_ItemSelected;
		}

		async private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{

            var cancelSrc = new CancellationTokenSource();
            var config = new ProgressDialogConfig()
                .SetTitle("Loading")
                .SetIsDeterministic(false)
                .SetMaskType(MaskType.Black)
                .SetCancel(onCancel: cancelSrc.Cancel);

            using (UserDialogs.Instance.Progress(config))
            {
                try
                {
                    var item = e.SelectedItem as OpcoesMenu;
                    if (item != null)
                    {
                        if (item.TargetType == typeof(CursosView))
                            Detail = new NavigationPage(await CursosView.Create());
                        else
                            Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                        menuPage.ListView.SelectedItem = null;
                        IsPresented = false;
                    }
                }
                catch { }
            }
		}
	}
}
