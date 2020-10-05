using Moq;
using RecipeBook2.Core.Controllers;
using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using RecipeBook2.Core.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace RecipeBook2.UnitTests
{
    public class RecipeControllerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private RecipeController _controller;

        public RecipeControllerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _controller = new RecipeController(_unitOfWorkMock.Object);
        }
        [Fact(DisplayName = "Create recipe")]
        public async Task CreateRecipe_Should_Be_Created_New_Recipe()
        {
            // Arrange
            var recipeToAdd = new Recipe { Name = "Recipe 1" };

            var repositoryMock = new Mock<IRecipeRepository>();
            repositoryMock.Setup(r => r.SingleOrDefaultAsync(x => x.Name == recipeToAdd.Name)).ReturnsAsync((Recipe)null);
            repositoryMock.Setup(r => r.Add(recipeToAdd));

            _unitOfWorkMock.SetupGet(uow => uow.Recipes).Returns(repositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync());

            // Act
            var result = await _controller.CreateRecipeAsync(recipeToAdd);

            // Assert
            Assert.Equal(result, recipeToAdd);

            repositoryMock.Verify(r => r.SingleOrDefaultAsync(x => x.Name == recipeToAdd.Name), Times.Once);
            repositoryMock.Verify(r => r.Add(recipeToAdd), Times.Once);
            _unitOfWorkMock.VerifyGet(uow => uow.Recipes, Times.AtLeastOnce);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }

        [Fact(DisplayName ="Create recipe, should throw when null")]
        public void CreateRecipe_Should_Throw_When_RecipeNull()
        {
            // Arrange
            Recipe newRecipe = null;
            _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync());

            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => newRecipe = await _controller.CreateRecipeAsync(null));
            Assert.Null(newRecipe);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
        }
        [Fact(DisplayName = "Create duplicate recipe, should throw EntityAlreadyExistsException")]
        public async Task CreateRecip_Should_Throw_When_RecipeExists()
        {
            // Arrange
            var recipeToAdd = new Recipe { Id = 1, Name = "Recipe 1", CategoryId = null };
            var repositoryMock = new Mock<IRecipeRepository>();
            repositoryMock.Setup(r => r.SingleOrDefaultAsync(x => x.Name == recipeToAdd.Name)).ReturnsAsync(recipeToAdd);

            _unitOfWorkMock.SetupGet(uow => uow.Recipes).Returns(repositoryMock.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<EntityAlreadyExistsException>(async () => _ = await _controller.CreateRecipeAsync(recipeToAdd));
            Assert.EndsWith("already exists.", exception.Message);
            repositoryMock.Verify(r => r.SingleOrDefaultAsync(x => x.Name == recipeToAdd.Name), Times.Once);
            _unitOfWorkMock.VerifyGet(uow => uow.Recipes, Times.AtLeastOnce);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
        }
        [Fact(DisplayName = "Create recipe with empty name, should throw EmptyFieldException")]
        public async Task CreateRecipe_Should_Throw_When_RecipeNameEmpty()
        {
            // Arrange
            var recipeToAdd = new Recipe { };
            Recipe newRecipe = null;
            _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync());
            // Act & Assert
            var controller = new RecipeController(_unitOfWorkMock.Object);
            var exception = await Assert.ThrowsAsync<EmptyFieldException>(async () => newRecipe = await controller.CreateRecipeAsync(recipeToAdd));
            Assert.EndsWith("cannot be empty.", exception.Message);
            Assert.Null(newRecipe);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
        }
        [Fact(DisplayName = "Remove recipe, throw exception")]
        public async Task RemoveRecipe_Should_Throw_When_RecipeDoesNotExist()
        {
            // Arrange
            var idToRemove = 2;
            var recipeToRemove = new Recipe { Id = 1, Name = "Recipe 1", CategoryId = null };

            var repositoryMock = new Mock<IRecipeRepository>();
            repositoryMock.Setup(r => r.Remove(recipeToRemove));
            repositoryMock.Setup(r => r.GetAsync(recipeToRemove.Id)).ReturnsAsync(recipeToRemove);

            _unitOfWorkMock.SetupGet(uow => uow.Recipes).Returns(repositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync());

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.RemoveRecipeAsync(idToRemove));
            Assert.EndsWith("not found.", exception.Message);
            repositoryMock.Verify(r => r.GetAsync(idToRemove), Times.Once);
            _unitOfWorkMock.VerifyGet(uow => uow.Recipes, Times.AtLeastOnce);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
        }
        [Fact(DisplayName = "Remove recipe, success")]
        public async Task RemoveRecipe_Should_Be_Removed()
        {
            // Arrange
            var idToRemove = 1;
            var recipeToRemove = new Recipe { Id = 1, Name = "Recipe 1", CategoryId = null };

            var repositoryMock = new Mock<IRecipeRepository>();
            repositoryMock.Setup(r => r.Remove(recipeToRemove));
            repositoryMock.Setup(r => r.GetAsync(recipeToRemove.Id)).ReturnsAsync(recipeToRemove);

            _unitOfWorkMock.SetupGet(uow => uow.Recipes).Returns(repositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync());

            // Act & Assert
            await _controller.RemoveRecipeAsync(idToRemove);
            repositoryMock.Verify(r => r.Remove(recipeToRemove), Times.Once);
            repositoryMock.Verify(r => r.GetAsync(idToRemove), Times.Once);
            _unitOfWorkMock.VerifyGet(uow => uow.Recipes, Times.AtLeastOnce);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }
    }
}
