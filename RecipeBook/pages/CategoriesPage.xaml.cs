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
        public CategoriesPageViewModel categoriesPage { get; private set; }
        public CategoriesPage(CategoriesPageViewModel categories)
        {
            categoriesPage = categories;
            DataContext = categoriesPage;
            InitializeComponent();
            
            
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CreateButton_OnClick(object sender, RoutedEventArgs e)
        {
            
            CreateOrEditCategory categoryWindow = new CreateOrEditCategory(new CategoryViewModel(new Category()));
            if (categoryWindow.ShowDialog() == true)
            {
                CategoryViewModel category = categoryWindow.Category;
                categoriesPage.Categories.Add(new Category(category.Name, category.Image));
                App.dbContext.Categories.Add(new Category(category.Name,category.Image));
                App.dbContext.SaveChanges();

            }
        }

        private void DeleteItemButton_OnClick(object sender, RoutedEventArgs e)
        {
            var item = ListCategories.SelectedItem as Category;
            var message = MessageBox.Show("Вы хотите удалить категорию ?", "Предупреждение", MessageBoxButton.OKCancel);
            if (item == null || message == MessageBoxResult.Cancel) { return; }

            App.dbContext.Categories.Local.Remove(item);
            categoriesPage.Categories.Remove(item);
            App.dbContext.SaveChanges();

        }
    }
}
