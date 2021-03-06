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
using RecipeBook.viewModels;

namespace RecipeBook.pages
{
    /// <summary>
    /// Interaction logic for CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : Page
    {
        
        public CategoriesPage()
        {
            
            DataContext = new CategoriesPageViewModel();
            InitializeComponent();
            
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
            
        }


      
        private void ListCategories_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ListCategories.SelectedItem as Category;
            this.NavigationService.Navigate(new RecipePage(item));
        }
    }
}
