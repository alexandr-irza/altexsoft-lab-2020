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
    public class CategoryControllerTests
    {
        private List<Category> _categories;
        private Mock<ICategoryRepository> _repositoryMock;
        private Mock<IUnitOfWork> _unitOfWorkMock;

        public CategoryControllerTests()
        {
            _categories = new List<Category>();
            _repositoryMock = new Mock<ICategoryRepository>();
            _repositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(_categories);
            _repositoryMock.Setup(x => x.GetAsync(It.IsAny<int>()))
                .ReturnsAsync((int x) => _categories.SingleOrDefault(r => r.Id == x));

            _repositoryMock.Setup(x => x.Add(It.IsAny<Category>()))
                .Callback<Category>(x =>
                {
                    x.Id = _categories.Max(o => o.Id) + 1;
                    _categories.Add(x);
                });

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(x => x.Categories)
                .Returns(_repositoryMock.Object);
        }

        [Fact(DisplayName = "Create Category")]
        public async Task CreateCategory_Should_Create_New_Category()
        {
            // Arrange
            _categories.Add(new Category { Id = 1, Name = "Category 1", ParentId = null });
            _categories.Add(new Category { Id = 2, Name = "Sub Category 1", ParentId = 1 });

            var controller = new CategoryController(_unitOfWorkMock.Object);

            // Act
            var newCategory = await controller.CreateCategoryAsync(new Category { Name = "Category 2", ParentId = null });
            // Assert
            Assert.Equal(3, newCategory.Id);
            Assert.Equal(3, _categories.Count);
        }

        [Fact(DisplayName = "Remove category, throw exception")]
        public async Task RemoveCategory_Should_Throw_When_CategoryDoesNotExist()
        {
            // Arrange
            _categories.Add(new Category { Id = 1, Name = "Category 1", ParentId = null });
            _repositoryMock.Setup(x => x.Remove(It.IsAny<Category>()))
                .Callback<Category>(x =>
                {
                    _categories.RemoveAll(c => c.Id == x.Id || c.ParentId == x.Id);
                });
            var controller = new CategoryController(_unitOfWorkMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await controller.RemoveCategoryAsync(2));
            Assert.Single(_categories);
        }
        [Fact(DisplayName = "Remove category, success")]
        public async Task RemoveCategory_Should_Be_Removed()
        {
            // Arrange
            _categories.Add(new Category { Id = 1, Name = "Category 1", ParentId = null });
            _categories.Add(new Category { Id = 2, Name = "Sub Category 1", ParentId = 1 });
            _repositoryMock.Setup(x => x.Remove(It.IsAny<Category>()))
                .Callback<Category>(x =>
                {
                    _categories.RemoveAll(c => c.Id == x.Id || c.ParentId == x.Id);
                });
            var controller = new CategoryController(_unitOfWorkMock.Object);

            // Act & Assert
            await controller.RemoveCategoryAsync(1);
            Assert.Empty(_categories);
        }
        [Fact(DisplayName = "Create category, should throw when null")]
        public void CreateCategory_Should_Throw_When_CategoryNull()
        {
            // Arrange
            Category newCategory = null;
            var controller = new CategoryController(_unitOfWorkMock.Object);
            // Act & Assert
            Assert.ThrowsAsync<ArgumentNullException>(async () => newCategory = await controller.CreateCategoryAsync(null));
            Assert.Null(newCategory);
        }
        [Fact(DisplayName = "Create duplicate category, should throw EntityAlreadyExistsException")]
        public void CreateCategory_Should_Throw_When_CategoryExists()
        {
            // Arrange
            Category newCategory = null;
            _categories.Add(new Category { Id = 1, Name = "Category 1" });
            var controller = new CategoryController(_unitOfWorkMock.Object);
            // Act & Assert
            Assert.ThrowsAsync<EntityAlreadyExistsException>(async () => newCategory = await controller.CreateCategoryAsync(new Category { Name = "Category 1" }));
        }
        [Fact(DisplayName = "Create category with empty name, should throw EmptyFieldException")]
        public void CreateCategory_Should_Throw_When_CategoryNameEmpty()
        {
            // Arrange
            var controller = new CategoryController(_unitOfWorkMock.Object);
            // Act & Assert
            Assert.ThrowsAsync<EmptyFieldException>(async () => _ = await controller.CreateCategoryAsync(new Category {  }));
        }
    }
}
