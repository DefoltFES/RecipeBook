using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Microsoft.Win32;
using RecipeBook.databaseClasses;
using RecipeBook.viewModels;
using Path = System.IO.Path;

namespace RecipeBook
{
    /// <summary>
    /// Interaction logic for CreateOrEditCategory.xaml
    /// </summary>
    public partial class CreateOrEditCategory : Window
    {
        public CreateOrEditCategoryViewModel Context { get; private set; }
        public CreateOrEditCategory(Category categories)
        {
            InitializeComponent();
            Context = new CreateOrEditCategoryViewModel(categories);
            this.DataContext = Context;

        }
      
        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Length > 0)
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Название категории не может быть пустым");
                return;
            }
        }



        
    }
}
