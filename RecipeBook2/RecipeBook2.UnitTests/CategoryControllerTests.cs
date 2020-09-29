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
                    x.Id = _categories.Max(x => x.Id) + 1;
                    _categories.Add(x);
                });
            _repositoryMock.Setup(x => x.Remove(It.IsAny<Category>()))
                .Callback<Category>(x =>
                {
                    _categories.RemoveAll(c => c.Id == x.Id || c.ParentId == x.Id);
                });

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _unitOfWorkMock.Setup(x => x.Categories)
                .Returns(_repositoryMock.Object);
        }

        [Fact(DisplayName = "Create Category")]
        public async Task CreateCategoryAsync()
        {
            _categories.Clear();
            _categories.Add(new Category { Id = 1, Name = "Category 1", ParentId = null });
            _categories.Add(new Category { Id = 2, Name = "Sub Category 1", ParentId = 1 });

            var controller = new CategoryController(_unitOfWorkMock.Object);

            var newCategory = await controller.CreateCategoryAsync(new Category { Name = "Category 2", ParentId = null });

            Assert.Equal(3, newCategory.Id);
            Assert.Equal(3, _categories.Count);
        }

        [Fact(DisplayName = "Remove category")]
        public async Task RemoveCategoryAsync()
        {
            _categories.Clear();
            _categories.Add(new Category { Id = 1, Name = "Category 1", ParentId = null });
            _categories.Add(new Category { Id = 2, Name = "Sub Category 1", ParentId = 1 });
            _categories.Add(new Category { Id = 3, Name = "Category 2", ParentId = null });

            var controller = new CategoryController(_unitOfWorkMock.Object);

            await controller.RemoveCategoryAsync(1);

            Assert.Single(_categories);

            await Assert.ThrowsAsync<NotFoundException>(async () => await controller.RemoveCategoryAsync(1));
            Assert.Single(_categories);
        }

        [Fact]
        public void CreateCategoryNullShouldThrow()
        {
            Category newCategory = null;
            var controller = new CategoryController(_unitOfWorkMock.Object);
            Assert.ThrowsAsync<ArgumentNullException>(async () => newCategory = await controller.CreateCategoryAsync(null));
            Assert.Null(newCategory);
        }
        [Fact(DisplayName = "Create duplicate category, should throw EntityAlreadyExistsException")]
        public void CreateCategoryDuplicateShouldThrow()
        {
            Category newCategory = null;
            _categories.Clear();
            _categories.Add(new Category { Id = 1, Name = "Category 1" });
            var controller = new CategoryController(_unitOfWorkMock.Object);
            Assert.ThrowsAsync<EntityAlreadyExistsException>(async () => newCategory = await controller.CreateCategoryAsync(new Category { Name = "Category 1" }));
        }
        [Fact(DisplayName = "Create category with empty name, should throw EmptyFieldException")]
        public void CreateCategoryEmptyNameShouldThrow()
        {
            var controller = new CategoryController(_unitOfWorkMock.Object);
            Assert.ThrowsAsync<EmptyFieldException>(async () => _ = await controller.CreateCategoryAsync(new Category {  }));
        }
    }
}
