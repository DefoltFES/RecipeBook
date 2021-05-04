using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RecipeBook.Annotations;

#nullable disable

namespace RecipeBook.databaseClasses
{
    public partial class Recipe:ICloneable,INotifyPropertyChanged
    {
        public Recipe()
        {
            Book = new HashSet<BookRecipe>();
            Instructions = new HashSet<Instruction>();
            ListCategories = new HashSet<ListCategory>();
            RecipeIngridients = new HashSet<RecipeIngridient>();
        }

        private string image;
        public long IdRecipe { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Image
        {
            get => image;
            set
            {
                image = value;
                OnPropertyChanged();
            }
        }

        public int? CookTime { get; set; }
        public int? NumService { get; set; }

        public virtual ICollection<BookRecipe> Book { get; set; }
        public virtual ICollection<Instruction> Instructions { get; set; }
        public virtual ICollection<ListCategory> ListCategories { get; set; }
        public virtual ICollection<RecipeIngridient> RecipeIngridients { get; set; }

        

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
