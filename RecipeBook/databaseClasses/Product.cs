using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable

namespace RecipeBook.databaseClasses
{
    public partial class Product:ICloneable
    {
        public Product()
        {
            RecipeIngridients = new HashSet<RecipeIngridient>();
        }

        public long IdProduct { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RecipeIngridient> RecipeIngridients { get; set; }
        public object Clone()
        {
           return this.MemberwiseClone();
        }
    }
}
