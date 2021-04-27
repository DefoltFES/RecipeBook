using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable

namespace RecipeBook.databaseClasses
{
    public partial class Category:ICloneable
    {
        public Category()
        {
            ListCategories = new ObservableCollection<ListCategory>();
        }

        public long IdCategory { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }

        public virtual ObservableCollection<ListCategory> ListCategories { get; set; }
        public object Clone()
        {
            return  this.MemberwiseClone();
        }
    }
}
