using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RecipeBook.Annotations;

#nullable disable

namespace RecipeBook.databaseClasses
{
    public partial class Category:ICloneable
    {
       
        private string name;
        private byte[] image;
        public Category()
        {
            ListCategories = new ObservableCollection<ListCategory>();
        }

        public Category(string NameCategory,byte [] ImageCategory):this()
        {
            Name = NameCategory;
            Image = ImageCategory;
        }
        public long IdCategory { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }

        public byte[] Image
        {
            get { return image; }
            set
            {
                image = value;
            }
        }

        public virtual  ObservableCollection<ListCategory> ListCategories { get; set; }

       
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
