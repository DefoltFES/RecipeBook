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
using RecipeBook.pages;

namespace RecipeBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
                       
        }
        

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
                MainFrame.Content=new MainPage();
        }

        private void Categories_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CategoriesPage());
        }

        private void AllRecipes_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new BooksPage());
        }

        private void CreateCategory_OnClick(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CreateCategory());
        }
    }
}