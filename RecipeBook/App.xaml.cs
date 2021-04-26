using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using RecipeBook.databaseClasses;

//using RecipeBook.databaseClasses;

namespace RecipeBook
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static RecipeDatabaseContext dbContext=new RecipeDatabaseContext();
    }
}
