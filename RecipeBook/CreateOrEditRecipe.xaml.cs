using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using RecipeBook.databaseClasses;
using RecipeBook.viewModels;
using Path = System.IO.Path;

namespace RecipeBook
{
    /// <summary>
    /// Interaction logic for CreateOrEditRecipe.xaml
    /// </summary>
    public partial class CreateOrEditRecipe : Window
    {
        public CreateOrEditRecipeViewModel Context { get; private set; }
        public CreateOrEditRecipe(Recipe recipe)
        {
            InitializeComponent();
            Context = new CreateOrEditRecipeViewModel(recipe);
            DataContext = Context;
        }

        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
          
            this.DialogResult = true;
        }


        private void OnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        
           

     

       
    }
}
