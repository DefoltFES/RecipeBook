using System;
using System.Collections.Generic;

#nullable disable

namespace RecipeBook.databaseClasses
{
    public partial class RecipeIngridient:ICloneable
    {
        public long IdRecipe { get; set; }
        public long IdProduct { get; set; }
        public long? IdMeasurement { get; set; }
        public string Amount { get; set; }
        public string Note { get; set; }

        public virtual MeasurementUnit MeasurementUnit { get; set; }
        public virtual Product Product { get; set; }
        public virtual Recipe Recipe { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
