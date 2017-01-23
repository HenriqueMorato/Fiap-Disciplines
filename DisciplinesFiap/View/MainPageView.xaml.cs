using System;
using System.Collections.Generic;

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
			var item = e.SelectedItem as OpcoesMenu;
			if (item != null)
			{
				if(item.TargetType == typeof(CursosView))
					Detail = new NavigationPage(await CursosView.Create());
				else
					Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
				menuPage.ListView.SelectedItem = null;
				IsPresented = false;
			}
		}
	}
}
