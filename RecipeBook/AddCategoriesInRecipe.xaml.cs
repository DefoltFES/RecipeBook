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
using System.Windows.Shapes;
using RecipeBook.databaseClasses;
using RecipeBook.viewModels;

namespace RecipeBook
{
    /// <summary>
    /// Interaction logic for AddCategoriesInRecipe.xaml
    /// </summary>
    public partial class AddCategoriesInRecipe : Window
    {
        public AddCategoriesInRecipeViewModel context { get; private set; }
        public AddCategoriesInRecipe()
        {
            InitializeComponent();
            context= new AddCategoriesInRecipeViewModel();

            DataContext = context;
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            foreach (var item in ListCategories.SelectedItems)
            {
                context.AddCategories.Add(item as Category);
            }

            this.DialogResult = true;
        }
    }
}
