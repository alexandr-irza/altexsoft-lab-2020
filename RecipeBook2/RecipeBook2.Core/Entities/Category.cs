﻿using RecipeBook2.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBook2.Core.Entities
{
    public class Category: BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public virtual Category Parent { get; set; }
        public virtual List<Category> Categories { get; set; }
        public virtual List<Recipe> Recipes { get; set; }
        public override string ToString()
        {
            return $"Category {Name}";
        }
    }
}
