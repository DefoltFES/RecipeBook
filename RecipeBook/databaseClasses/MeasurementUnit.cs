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
            RecipeIngridients = new HashSet<RecipeIngridient>();
        }

        public long IdMeasurement { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RecipeIngridient> RecipeIngridients { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
