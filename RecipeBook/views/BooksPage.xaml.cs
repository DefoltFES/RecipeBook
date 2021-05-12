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
using RecipeBook.views;

namespace RecipeBook.pages
{
    /// <summary>
    /// Interaction logic for BooksPage.xaml
    /// </summary>
    public partial class BooksPage : Page
    {
        public BooksPage()
        {
            InitializeComponent();
            DataContext=new BookPageViewModel();
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
            
        }

        private void Books_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (Books.SelectedItem != null)
            {
                this.NavigationService.Navigate(new FullBookPage(Books.SelectedItem as Book));
            }
        }
    }
}
