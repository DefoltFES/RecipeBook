using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using RecipeBook.Annotations;

#nullable disable

namespace RecipeBook.databaseClasses
{
    public partial class Instruction:ICloneable,INotifyPropertyChanged
    {
        private string descriptionStep;
        private string imageStep;
        public long IdRecipe { get; set; }
        public long IdInstruction { get; set; }

        public string DescriptionStep
        {
            get => descriptionStep;
            set
            {
                descriptionStep = value;
                OnPropertyChanged();
            }
        }

        public string ImageStep
        {
            get => imageStep;
            set
            {
                imageStep = value;
                OnPropertyChanged();
            }
        }

        public virtual Recipe Recipe { get; set; }
        public object Clone()
        {
          return  this.MemberwiseClone();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
