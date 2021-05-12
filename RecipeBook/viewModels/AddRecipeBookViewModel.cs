using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeBook.databaseClasses;
using RecipeBook.service;

namespace RecipeBook.viewModels
{
    public class AddRecipeBookViewModel:ViewModel
    {
        private ObservableCollection<Recipe> recipeCategory;

        public ObservableCollection<Recipe> RecipesCategory
        {
            get => recipeCategory;
            set
            {
                recipeCategory = value;
                OnPropertyChanged();
            }
        }

        public ICollection<Recipe> AddRecipes { get; set; }
        public ICollection<Category> AllCategories { get; set; }

        public AddRecipeBookViewModel()
        {
            AllCategories = App.dbContext.Categories.ToList();
            AddRecipes=new List<Recipe>();
            RecipesCategory=new ObservableCollection<Recipe>();
        }

        private RelayCommand changeCategory;


        public RelayCommand ChangeCategory
        {
            get
            {
                return changeCategory ??= new RelayCommand(selectedItem =>
                {
                    var item=  selectedItem as Category;
                    if (item.IdCategory == 1)
                    {
                        RecipesCategory = new ObservableCollection<Recipe>(App.dbContext.Recipes.Where(x => x.ListCategories.Count == 0));
                    }
                    else
                    {
                        RecipesCategory = new ObservableCollection<Recipe>(App.dbContext.Categories.Find(item.IdCategory).ListCategories
                            .Select(x => x.Recipe).ToList());
                    }
                });
            }
        }

      

    }
}
