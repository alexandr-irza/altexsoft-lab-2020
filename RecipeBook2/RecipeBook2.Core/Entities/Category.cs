using RecipeBook2.SharedKernel;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook2.Core.Entities
{
    public class Category: BaseEntity
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        [NotMapped]
        public Category Parent { get; set; }
        public override string ToString()
        {
            return $"Category {Name}";
        }
    }
}
