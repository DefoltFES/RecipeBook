using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable

namespace RecipeBook.databaseClasses
{
    public partial class MeasurementUnit
    {
        public MeasurementUnit()
        {
            RecipeIngridients = new ObservableCollection<RecipeIngridient>();
        }

        public long IdMeasurement { get; set; }
        public string Name { get; set; }

        public  ObservableCollection<RecipeIngridient> RecipeIngridients { get; set; }
    }
}
