using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable

namespace RecipeBook.databaseClasses
{
    public partial class Recipe:ICloneable
    {
        public Recipe()
        {
            BookRecipes = new ObservableCollection<BookRecipe>();
            Instructions = new ObservableCollection<Instruction>();
            ListCategories = new ObservableCollection<ListCategory>();
            RecipeIngridients = new ObservableCollection<RecipeIngridient>();
        }

        public long IdRecipe { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public virtual ObservableCollection<BookRecipe> BookRecipes { get; set; }
        public virtual ObservableCollection<Instruction> Instructions { get; set; }
        public virtual ObservableCollection<ListCategory> ListCategories { get; set; }
        public virtual ObservableCollection<RecipeIngridient> RecipeIngridients { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
