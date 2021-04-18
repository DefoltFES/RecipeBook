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
            Books.ItemsSource = new List<int>() {5,53,44,4,4,4,4,4,4,4,4,4 };
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
