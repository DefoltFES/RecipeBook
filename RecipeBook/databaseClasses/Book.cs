using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable

namespace RecipeBook.databaseClasses
{
    public partial class Book
    {
        public Book()
        {
            Recipes = new HashSet<BookRecipe>();
        }

        public long IdBook { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public virtual ICollection<BookRecipe> Recipes { get; set; }
    }
}
