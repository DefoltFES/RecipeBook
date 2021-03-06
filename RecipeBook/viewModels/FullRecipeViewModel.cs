using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RecipeBook.databaseClasses;

namespace RecipeBook.viewModels
{
    public class FullRecipeViewModel:ViewModel
    {
        private Recipe recipe;

        public FullRecipeViewModel(Recipe r)
        {
            App.dbContext.Recipes.Where(x => x.IdRecipe == r.IdRecipe).Include(b => b.Instructions).Include(x => x.RecipeIngridients).ThenInclude(b => b.Product).Load();
            recipe = r;
        }

        public string Name
        {
            get { return recipe.Name; }
            set
            {
                recipe.Name = value;
                OnPropertyChanged();
            }
        }

        public int? CookTime
        {
            get {return recipe.CookTime;}
            set
            {
                
                recipe.CookTime = value;
                OnPropertyChanged();
            }
        }

        public int? NumService
        {
            get { return recipe.NumService; }
            set
            {
                recipe.NumService = value;
                OnPropertyChanged();
            }
        }

        public ICollection<ListCategory> Categories
        {
            get { return recipe.ListCategories; }
            set
            {
                recipe.ListCategories = value;
                OnPropertyChanged();
            }
        }

        public string Image
        {
            get { return recipe.Image; }
            set
            {
                recipe.Image = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return recipe.Description; }
            set
            {
                recipe.Description = value;
                OnPropertyChanged();
            }
        }

        public ICollection<RecipeIngridient> Ingridients
        {
            get { return recipe.RecipeIngridients;}
            set
            {
                recipe.RecipeIngridients = value;
                OnPropertyChanged();
            }
        }

        public ICollection<Instruction> Instructions
        {
            get { return recipe.Instructions;}
            set
            {
                recipe.Instructions = value;
                OnPropertyChanged();
            }
        }

    }
}
