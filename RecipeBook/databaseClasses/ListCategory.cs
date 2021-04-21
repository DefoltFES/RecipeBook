using System;
using System.Collections.Generic;

#nullable disable

namespace RecipeBook.databaseClasses
{
    public partial class ListCategory
    {
        public long IdRecipe { get; set; }
        public long IdCategory { get; set; }

        public virtual Category Category { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}
