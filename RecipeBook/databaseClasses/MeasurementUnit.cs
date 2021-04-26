using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable

namespace RecipeBook.databaseClasses
{
    public partial class MeasurementUnit:ICloneable
    {
        public MeasurementUnit()
        {
            RecipeIngridients = new ObservableCollection<RecipeIngridient>();
        }

        public long IdMeasurement { get; set; }
        public string Name { get; set; }

        public virtual ObservableCollection<RecipeIngridient> RecipeIngridients { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
