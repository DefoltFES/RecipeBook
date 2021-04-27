using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;
using Microsoft.EntityFrameworkCore;
using RecipeBook.databaseClasses;


namespace RecipeBook.viewModels
{
    class RecipePageViewModel:ViewModel
    {
        public string Title { get; set; }

        public ObservableCollection<Recipe> Recipes { get; set;}

        public RecipePageViewModel(Category category)
        {
            Title = $"Рецепты категории: {category.Name}";
            Recipes = new ObservableCollection<Recipe>(App.dbContext.Categories.Find(category.IdCategory).ListCategories.Select(x => x.Recipe).ToList());

        }

        public RecipePageViewModel()
        {
            Title = "Все рецепты";
          var  item=  App.dbContext.Recipes.Include(x => x.ListCategories).ThenInclude(x => x.Category).ToList();
            Recipes = new ObservableCollection<Recipe>(App.dbContext.Recipes.ToList());
        }
    }
}
