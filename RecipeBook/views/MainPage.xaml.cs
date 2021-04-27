using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using RecipeBook.databaseClasses;
using RecipeBook.pages;
using RecipeBook.viewModels;

namespace RecipeBook
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
   
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MainPageViewModel();
        }

        private void LastBooks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void PopularCategories_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = PopularCategories.SelectedItem as Category;
            this.NavigationService.Navigate(new RecipePage(item));
        }
    }
}
