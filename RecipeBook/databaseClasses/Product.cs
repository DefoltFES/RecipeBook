using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RecipeBook.Annotations;

#nullable disable

namespace RecipeBook.databaseClasses
{
    public partial class Product:ICloneable,INotifyPropertyChanged
    {
        public Product()
        {
            RecipeIngridients = new HashSet<RecipeIngridient>();
        }

        private long idProduct;
        private string name;

        public long IdProduct
        {
            get => idProduct;
            set
            {
                idProduct = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

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
