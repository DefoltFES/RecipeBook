using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RecipeBook.databaseClasses;
using RecipeBook.viewModels;

namespace RecipeBook.views
{
    /// <summary>
    /// Interaction logic for FullBookPage.xaml
    /// </summary>
    public partial class FullBookPage : Page
    {
        
        public FullBookPage(Book book)
        {
            InitializeComponent();
            DataContext =new FullBookViewModel(book);
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private void PrintButton_OnClick(object sender, RoutedEventArgs e)
        {
           
            this.NavigationService.Navigate(new PrintBookPage(this.DataContext as FullBookViewModel));
        }

        private void Recipes_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Recipes.SelectedItem !=null)
            {
                var item = Recipes.SelectedItem as BookRecipe;
                this.NavigationService.Navigate(new FullRecipePage(item.Recipe));
            }
         
        }
    }
}
