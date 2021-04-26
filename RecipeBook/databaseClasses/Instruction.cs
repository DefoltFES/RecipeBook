using System;
using System.Collections.Generic;

#nullable disable

namespace RecipeBook.databaseClasses
{
    public partial class Instruction:ICloneable
    {
        public long IdRecipe { get; set; }
        public long IdStep { get; set; }
        public string DescriptionStep { get; set; }
        public byte[] ImageStep { get; set; }

        public virtual Recipe Recipe { get; set; }
        public object Clone()
        {
          return  this.MemberwiseClone();
        }
    }
}
