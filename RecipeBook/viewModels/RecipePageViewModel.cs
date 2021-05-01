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

        private ObservableCollection<Recipe> recipes;

        public ObservableCollection<Recipe> Recipes
        {
            get => recipes;
            set
            {
                recipes = value;
                OnPropertyChanged();
            }
        }

        public RecipePageViewModel(Category category)
        {

            if (category.IdCategory==1)
            {
                Title = $"Рецепты без категории";
                Recipes =new ObservableCollection<Recipe>(App.dbContext.Recipes.Where(x=>x.ListCategories.Count==0).ToList());
            }
            else
            {
                Title = $"Рецепты категории: {category.Name}";
                Recipes = new ObservableCollection<Recipe>(App.dbContext.Categories.Find(category.IdCategory).ListCategories.Select(x => x.Recipe).ToList());
            }
            
        }

        public RecipePageViewModel()
        {
            Title = "Все рецепты";
            var  item=  App.dbContext.Recipes.Include(x => x.ListCategories).ThenInclude(x => x.Category).ToList();
            Recipes = new ObservableCollection<Recipe>(App.dbContext.Recipes.ToList());
        }
    }
}
