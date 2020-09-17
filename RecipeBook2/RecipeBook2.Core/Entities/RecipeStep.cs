using RecipeBook2.SharedKernel;

namespace RecipeBook2.Core.Entities
{
    public class RecipeStep: BaseEntity
    {
        public int RecipeId { get; set; }
        public int StepNumber { get; set; }
        public string StepInstruction { get; set; }
    }
}
