using Moq;
using RecipeBook2.Controllers;
using RecipeBook2.Core.Entities;
using RecipeBook2.Core.Interfaces;
using RecipeBook2.SharedKernel;
using System;
using System.Collections.Generic;
using Xunit;

namespace RecipeBook2.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var repositoryMock = new Mock<ICategoryRepository>();
            repositoryMock.Setup(x => x.GetAll()).Returns(new List<Category> {
                new Category { Id = 1, ParentId = null },
                new Category { Id = 2, ParentId = 1}
            });

            repositoryMock.Setup(x => x.Add(It.IsAny<Category>())).Returns((Category x) => x);

            var uow = new Mock<IUnitOfWork>();

            uow.Setup(x => x.Categories).Returns(repositoryMock.Object);

            var cont = new CategoryController(uow.Object);

            var newCategory = cont.CreateCategory(new Category { Id = 1, ParentId = null});


            Assert.Equal(4, newCategory.Id);
        }
    }
}
