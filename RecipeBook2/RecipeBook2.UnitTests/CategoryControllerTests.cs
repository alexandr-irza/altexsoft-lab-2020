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
    public class CategoryControllerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private CategoryController _controller;

        public CategoryControllerTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _controller = new CategoryController(_unitOfWorkMock.Object);
        }

        [Fact(DisplayName = "Create Category")]
        public async Task CreateCategory_Should_Create_New_Category()
        {
            // Arrange
            var categoryToAdd = new Category { Name = "Category 1", ParentId = null };

            var repositoryMock = new Mock<ICategoryRepository>();
            repositoryMock.Setup(r => r.SingleOrDefaultAsync(x => x.Name == categoryToAdd.Name && x.ParentId == categoryToAdd.ParentId)).ReturnsAsync((Category)null);
            repositoryMock.Setup(r => r.Add(categoryToAdd));

            _unitOfWorkMock.SetupGet(uow => uow.Categories).Returns(repositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync());

            // Act
            var result = await _controller.CreateCategoryAsync(categoryToAdd);

            // Assert
            Assert.Equal(result, categoryToAdd);

            repositoryMock.Verify(r => r.SingleOrDefaultAsync(x => x.Name == categoryToAdd.Name && x.ParentId == categoryToAdd.ParentId), Times.Once);
            repositoryMock.Verify(r => r.Add(categoryToAdd), Times.Once);
            _unitOfWorkMock.VerifyGet(uow => uow.Categories, Times.AtLeastOnce);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }
        [Fact(DisplayName = "Create category, should throw when null")]
        public void CreateCategory_Should_Throw_When_CategoryNull()
        {
            // Arrange
            Category categoryToAdd = null;
            _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync());

            // Act & Assert
            var result = Assert.ThrowsAsync<ArgumentNullException>(async () => categoryToAdd = await _controller.CreateCategoryAsync(null));
            Assert.Null(categoryToAdd);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
        }
        [Fact(DisplayName = "Create duplicate category, should throw EntityAlreadyExistsException")]
        public async Task CreateCategory_Should_Throw_When_CategoryExists()
        {
            // Arrange
            var categoryToAdd = new Category { Name = "Category 1", ParentId = null };
            var repositoryMock = new Mock<ICategoryRepository>();
            repositoryMock.Setup(r => r.SingleOrDefaultAsync(x => x.Name == categoryToAdd.Name && x.ParentId == categoryToAdd.ParentId)).ReturnsAsync(categoryToAdd);

            _unitOfWorkMock.SetupGet(uow => uow.Categories).Returns(repositoryMock.Object);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<EntityAlreadyExistsException>(async () => _ = await _controller.CreateCategoryAsync(categoryToAdd));
            Assert.EndsWith("already exists.", exception.Message);
            repositoryMock.Verify(r => r.SingleOrDefaultAsync(x => x.Name == categoryToAdd.Name && x.ParentId == categoryToAdd.ParentId), Times.Once);
            _unitOfWorkMock.VerifyGet(uow => uow.Categories, Times.AtLeastOnce);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
        }
        [Fact(DisplayName = "Create category with empty name, should throw EmptyFieldException")]
        public async Task CreateCategory_Should_Throw_When_CategoryNameEmpty()
        {
            // Arrange
            _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync());
            // Act & Assert
            var exception = await Assert.ThrowsAsync<EmptyFieldException>(async () => _ = await _controller.CreateCategoryAsync(new Category { }));
            Assert.EndsWith("cannot be empty.", exception.Message);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
        }
        [Fact(DisplayName = "Remove category, throw exception")]
        public async Task RemoveCategory_Should_Throw_When_CategoryDoesNotExist()
        {
            // Arrange
            var idToRemove = 2;
            var categoryToRemove = new Category { Id = 1, Name = "Category 1", ParentId = null };

            var repositoryMock = new Mock<ICategoryRepository>();
            repositoryMock.Setup(r => r.Remove(categoryToRemove));
            repositoryMock.Setup(r => r.GetAsync(categoryToRemove.Id)).ReturnsAsync(categoryToRemove);

            _unitOfWorkMock.SetupGet(uow => uow.Categories).Returns(repositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync());

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(async () => await _controller.RemoveCategoryAsync(idToRemove));
            Assert.EndsWith("not found.", exception.Message);
            repositoryMock.Verify(r => r.GetAsync(idToRemove), Times.Once);
            _unitOfWorkMock.VerifyGet(uow => uow.Categories, Times.AtLeastOnce);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Never);
        }
        [Fact(DisplayName = "Remove category, success")]
        public async Task RemoveCategory_Should_Be_Removed()
        {
            // Arrange
            var idToRemove = 1;
            var categoryToRemove = new Category { Id = 1, Name = "Category 1", ParentId = null };

            var repositoryMock = new Mock<ICategoryRepository>();
            repositoryMock.Setup(r => r.Remove(categoryToRemove));
            repositoryMock.Setup(r => r.GetAsync(categoryToRemove.Id)).ReturnsAsync(categoryToRemove);

            _unitOfWorkMock.SetupGet(uow => uow.Categories).Returns(repositoryMock.Object);
            _unitOfWorkMock.Setup(uow => uow.SaveChangesAsync());

            // Act & Assert
            await _controller.RemoveCategoryAsync(idToRemove);
            repositoryMock.Verify(r => r.Remove(categoryToRemove), Times.Once);
            repositoryMock.Verify(r => r.GetAsync(idToRemove), Times.Once);
            _unitOfWorkMock.VerifyGet(uow => uow.Categories, Times.AtLeastOnce);
            _unitOfWorkMock.Verify(uow => uow.SaveChangesAsync(), Times.Once);
        }

    }
}
