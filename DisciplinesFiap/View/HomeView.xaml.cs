using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace DisciplinesFiap
{
	public partial class HomeView : ContentPage
	{
        public ListView ListView { get { return lstMenu; } }
        public HomeView()
		{
			InitializeComponent();

            ObservableCollection<OpcoesMenu> menuItems = new ObservableCollection<OpcoesMenu>();
            menuItems.Add(new OpcoesMenu
            {
                Opcao = "Login",
                Icone = "Door_Opened.png",
                TargetType = typeof(LoginView)
            });
            menuItems.Add(new OpcoesMenu
            {
                Opcao = "Cursos",
                Icone = "Book.png",
                TargetType = typeof(CursosView)
            });

            lstMenu.ItemsSource = menuItems;
        }

        async private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as OpcoesMenu;
            if (item != null)
            {
                if (item.TargetType == typeof(CursosView))
                    ((MainPageView)Application.Current.MainPage).Detail = new NavigationPage(await CursosView.Create());
                else
                    ((MainPageView)Application.Current.MainPage).Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
                //menuPage.ListView.SelectedItem = null;
                //IsPresented = false;
            }
        }
    }
}
