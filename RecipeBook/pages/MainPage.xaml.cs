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
            
            var list = new List<int>() { 1, 23, 45,4 };
            
            var suka = App.dbContext.Categories.ToList();
            PopularCategories.ItemsSource = suka;
        }


        private void DeleteMenuItem(object sender, RoutedEventArgs e)
        {
            if (PopularCategories.SelectedItem is Category category)
            {
                App.dbContext.Categories.Remove(category);
            }

            App.dbContext.SaveChanges();
            NavigationService.Refresh();
        }

        private void EditingMenuButton(object sender, RoutedEventArgs e)
        {
            if (PopularCategories.SelectedItem is Category category)
            {
                NavigationService.Navigate(new CreateCategory(category,true));

            }
           
        }
    }
}
