using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            Title = $"Рецепты категории:{category.Name}";
            var item = App.dbContext.Categories.Find(category.IdCategory);
            App.dbContext.Entry(item).Reference("Recipes").Load();

        }

        public RecipePageViewModel()
        {
            Title = "Все рецепты";
            App.dbContext.Recipes.Load();
            Recipes = App.dbContext.Recipes.Local.ToObservableCollection();
        }
    }
}
