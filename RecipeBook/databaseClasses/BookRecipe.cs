using System;
using System.Collections.Generic;

#nullable disable

namespace RecipeBook.databaseClasses
{
    public partial class BookRecipe
    {
        public long IdBook { get; set; }
        public long IdRecipe { get; set; }

        public virtual Book Book { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
