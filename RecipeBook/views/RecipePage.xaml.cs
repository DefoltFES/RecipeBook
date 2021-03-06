using System;
using System.Collections.Generic;
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
using RecipeBook.databaseClasses;
//using RecipeBook.databaseClasses;
using RecipeBook.viewModels;
using RecipeBook.views;

namespace RecipeBook.pages
{
    /// <summary>
    /// Interaction logic for RecipePage.xaml
    /// </summary>
    public partial class RecipePage : Page
    {


        public RecipePage(Category category)
        {
            InitializeComponent();
            DataContext = new RecipePageViewModel(category);
        }
        public RecipePage()
        {
            InitializeComponent();
            DataContext = new RecipePageViewModel();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void ListRecipes_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListRecipes.SelectedItem != null)
            {
                this.NavigationService.Navigate(new FullRecipePage(ListRecipes.SelectedItem as Recipe));
            }
        }
    }
}
