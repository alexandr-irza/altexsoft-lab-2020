using Moq;
using RecipeBook2.Core.Controllers;
using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using RecipeBook2.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RecipeBook2.UnitTests
{
    public class RecipeControllerTests
    {
        private List<Recipe> _recipes;
        private Mock<IRecipeRepository> _repositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        public RecipeControllerTests()
        {
            _recipes = new List<Recipe>();
            _repositoryMock = new Mock<IRecipeRepository>();

            _repositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(_recipes);
            _repositoryMock.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((int x) => _recipes.SingleOrDefault(r => r.Id == x));

            _repositoryMock.Setup(x => x.Add(It.IsAny<Recipe>()))
                .Callback<Recipe>(x =>
                {
                    x.Id = _recipes.Max(o => o.Id) + 1;
                    _recipes.Add(x);
                });

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(x => x.Recipes)
                .Returns(_repositoryMock.Object);
        }
        [Fact(DisplayName = "Create recipe")]
        public async Task CreateRecipe_Should()
        {
            // Arrange
            _recipes.Add(new Recipe { Id = 1, Name = "Recipe 1", CategoryId = null });
            _recipes.Add(new Recipe { Id = 2, Name = "Recipe 2", CategoryId = null });

            var controller = new RecipeController(_unitOfWorkMock.Object);

            // Act
            var newRecipe = await controller.CreateRecipeAsync(new Recipe { Name = "Recipe 3" });

            // Assert
            Assert.Equal(3, newRecipe.Id);
            Assert.Equal(3, _recipes.Count);
        }

        [Fact]
        public void CreateRecipeNull_Should_Throw()
        {
            // Arrange
            Recipe newRecipe = null;
            var controller = new RecipeController(_unitOfWorkMock.Object);

            // Act
            Assert.ThrowsAsync<ArgumentNullException>(async () => newRecipe = await controller.CreateRecipeAsync(null));

            // Assert
            Assert.Null(newRecipe);
        }
        [Fact(DisplayName = "Create duplicate recipe, should throw EntityAlreadyExistsException")]
        public void CreateRecipDuplicate_Should_Throw()
        {
            // Arrange
            Recipe newRecipe = null;
            _recipes.Add(new Recipe { Id = 1, Name = "Recipe 1", CategoryId = null });
            var controller = new RecipeController(_unitOfWorkMock.Object);
            
            // Act
            Assert.ThrowsAsync<EntityAlreadyExistsException>(async () => newRecipe = await controller.CreateRecipeAsync(new Recipe { Name = "Recipe 2" }));

            // Assert
        }
        [Fact(DisplayName = "Create recipe with empty name, should throw EmptyFieldException")]
        public void CreateRecipeEmptyName_Should__Throw()
        {
            // Arrange

            // Act
            var controller = new RecipeController(_unitOfWorkMock.Object);
            Assert.ThrowsAsync<EmptyFieldException>(async () => _ = await controller.CreateRecipeAsync(new Recipe { }));

            // Assert
        }
        [Fact(DisplayName = "Remove recipe")]
        public async Task RemoveRecipe_Should_Throw()
        {
            // Arrange
            _repositoryMock.Setup(x => x.Remove(It.IsAny<Recipe>()))
                .Callback<Recipe>(x =>
                {
                    _recipes.Remove(x);
                });

            _recipes.Add(new Recipe { Id = 1, Name = "Recipe 1", CategoryId = null });
            _recipes.Add(new Recipe { Id = 2, Name = "Recipe 2", CategoryId = null });

            var controller = new RecipeController(_unitOfWorkMock.Object);
            // Act 
            await controller.RemoveRecipeAsync(1);

            // Assert
            Assert.Single(_recipes);

            await Assert.ThrowsAsync<NotFoundException>(async () => await controller.RemoveRecipeAsync(1));
            Assert.Single(_recipes);
        }
    }
}
