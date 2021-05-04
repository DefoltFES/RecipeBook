using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RecipeBook.Annotations;

#nullable disable

namespace RecipeBook.databaseClasses
{
    public partial class RecipeIngridient:ICloneable,INotifyPropertyChanged
    {
        private string amount;
        private string note;
        public long IdRecipe { get; set; }
        public long IdProduct { get; set; }
        public long? IdMeasurement { get; set; }

        public string Amount
        {
            get => amount;
            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }

        public string Note
        {
            get => note;
            set
            {
                note = value;
                OnPropertyChanged();
            }
        }

        public virtual MeasurementUnit MeasurementUnit { get; set; }
        public virtual Product Product { get; set; }
        public virtual Recipe Recipe { get; set; }

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
