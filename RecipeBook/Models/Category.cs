using System.Text.Json.Serialization;

namespace RecipeBook.Models
{
    public class Category : BaseModel
    {
        public string ParentId { get; set; }
        [JsonIgnore]
        public Category Parent { get; set; }
        public override string ToString()
        {
            return $"Category {Name}";
        }
    }
}
