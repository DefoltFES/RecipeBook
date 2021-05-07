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
    /// Interaction logic for AddRecipeBook.xaml
    /// </summary>
    public partial class AddRecipeBook : Window
    {
        public AddRecipeBookViewModel Context { get; private set; }
        public AddRecipeBook()
        {
            InitializeComponent();
            Context = new AddRecipeBookViewModel();
            DataContext = Context;
        }
    }
}
