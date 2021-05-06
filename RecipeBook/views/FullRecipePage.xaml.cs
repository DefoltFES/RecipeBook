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

namespace RecipeBook.views
{
    /// <summary>
    /// Interaction logic for FullRecipePage.xaml
    /// </summary>
    public partial class FullRecipePage : Page
    {
        
        public FullRecipePage(Recipe recipe)
        {
            InitializeComponent();
            DataContext=new FullRecipeViewModel(recipe);
        }

        private void PrintButtonClick(object sender, RoutedEventArgs e)
        {
            PrintButton.Visibility = Visibility.Hidden;
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(Print,"Рецепт");
            }
            PrintButton.Visibility = Visibility.Visible;
        }

        private void BackButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
