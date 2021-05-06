using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Annotations;
using RecipeBook.databaseClasses;
using RecipeBook.service;


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
        RelayCommand deleteCommand;
        RelayCommand addCommand;
        RelayCommand editCommand;

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

        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??= new RelayCommand((o) =>
                {
                    CreateOrEditRecipe addRecipeWindow = new CreateOrEditRecipe(new Recipe());
                    if (addRecipeWindow.ShowDialog() == true)
                    {
                      
                        Recipe recipe = new Recipe()
                        {
                            Image = addRecipeWindow.Context.Image,
                            Description = addRecipeWindow.Context.Description,
                            CookTime = addRecipeWindow.Context.CookTime,
                            Name = addRecipeWindow.Context.Name,
                            NumService = addRecipeWindow.Context.NumService,
                            Instructions = addRecipeWindow.Context.Instructions,
                            RecipeIngridients = addRecipeWindow.Context.Ingridients
                        };
                        
                        Recipes.Add(recipe);
                        App.dbContext.Recipes.Add(recipe);
                        App.dbContext.SaveChanges();
                    }
                });
            }
        }


        public RelayCommand DeleteCommand
        {
            get
            {
                return deleteCommand ??
                       (deleteCommand = new RelayCommand((selectedItem) =>
                       {
                           if (selectedItem == null)
                           {
                               return;
                           }

                           var recipe = selectedItem as Recipe;
                           var message = MessageBox.Show("Вы хотите удалить рецепт ?", "Предупреждение", MessageBoxButton.OKCancel);
                           var path = System.AppDomain.CurrentDomain.BaseDirectory + recipe.Image;
                           if (message == MessageBoxResult.Cancel) { return; }
                           Recipes.Remove(recipe);
                           if (recipe.Image != null)
                           {
                               File.Delete(path);
                           }
                           foreach (var image in recipe.Instructions)
                           {
                               if (image.ImageStep != null)
                               {
                                   File.Delete(System.AppDomain.CurrentDomain.BaseDirectory+image.ImageStep);
                               } 
                           }
                           App.dbContext.RecipeIngridients.RemoveRange(recipe.RecipeIngridients);
                           App.dbContext.Instructions.RemoveRange(recipe.Instructions);
                           App.dbContext.ListCategories.RemoveRange(recipe.ListCategories);
                           App.dbContext.BookRecipes.RemoveRange(recipe.Book);
                           App.dbContext.Recipes.Remove(recipe);
                           App.dbContext.SaveChanges();
                       }));
            }
        }

        public RelayCommand EditCommand
        {
            get
            {
                return editCommand ?? (editCommand = new RelayCommand((selectedItem) =>
                {
                    if (selectedItem == null) return;
                    Recipe recipe = selectedItem as Recipe;
                    
                    int newIndex = Recipes.IndexOf((Recipe)selectedItem);

                    Recipe vm = (Recipe)recipe.Clone();
                    CreateOrEditRecipe editRecipe = new CreateOrEditRecipe(vm);
                    if (editRecipe.ShowDialog() == true)
                    {
                        Recipes.Remove((Recipe)selectedItem);

                        recipe = App.dbContext.Recipes.Find(editRecipe.Context.Recipe.IdRecipe);

                        if (recipe != null)
                        {
                            if ( editRecipe.Context.Image!=recipe.Image)
                            {
                                var path = System.AppDomain.CurrentDomain.BaseDirectory + recipe.Image;
                                File.Delete(path);
                            }
                            foreach (var image in recipe.Instructions)
                            {
                                foreach (var imageInstruction in editRecipe.Context.Instructions)
                                {
                                    if (imageInstruction.ImageStep != image.ImageStep)
                                    {
                                        File.Delete(System.AppDomain.CurrentDomain.BaseDirectory + image.ImageStep);
                                    }
                                }
                                
                            }
                            recipe.NumService = editRecipe.Context.NumService;
                            recipe.Image = editRecipe.Context.Image;
                            recipe.Instructions = editRecipe.Context.Instructions.ToList();
                            recipe.RecipeIngridients = editRecipe.Context.Ingridients.ToList();
                            recipe.Description = editRecipe.Context.Description;
                            recipe.ListCategories = editRecipe.Context.Categories.ToList();
                            recipe.CookTime = editRecipe.Context.CookTime;
                            recipe.Name = editRecipe.Context.Name;
                            Recipes.Remove((Recipe)selectedItem);
                            Recipes.Add(recipe);
                            int oldIndex = Recipes.IndexOf(recipe);
                            Recipes.Move(oldIndex, newIndex);
                            App.dbContext.Update(recipe);
                            App.dbContext.SaveChanges();
                        }
                    }
                }));
            }

        }

     

    }
}
