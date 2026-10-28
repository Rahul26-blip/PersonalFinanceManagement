
using System;
using System.Collections.Generic;
using Moq;
using Xunit;

using PFM.Application.DTO;
using PFM.Application.Services;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;

namespace PFM.XTests
{
    public class CategoryServicesTests
    {
        private readonly Mock<ICategorieRepository> _mockRepository;
        private readonly CategorieServices _categorieServices;

        public CategoryServicesTests()
        {
            // Create a fake repository.
            // These tests will not connect to SQL Server.
            _mockRepository = new Mock<ICategorieRepository>();

            // Pass the fake repository to the service.
            _categorieServices = new CategorieServices(
                _mockRepository.Object);
        }

        // TEST 1: GetAll returns categories.
        [Fact]
        public void GetAll_WhenCategoriesExist_ReturnsCategories()
        {
            // Arrange
            var categories = new List<Categories>
            {
                new Categories
                {
                    CategoriesId = 1,
                    UserId = 10,
                    CategoriesName = "Food",
                    CategoriesCode = "FOOD",
                    IsActive = true,
                    CreatedOn = new DateTime(2026, 1, 1)
                },
                new Categories
                {
                    CategoriesId = 2,
                    UserId = 10,
                    CategoriesName = "Travel",
                    CategoriesCode = "TRAVEL",
                    IsActive = true,
                    CreatedOn = new DateTime(2026, 1, 2)
                }
            };

            _mockRepository.Setup(r => r.GetAll()).Returns(categories);

            // Act
            var result = _categorieServices.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Food", result[0].CategoriesName);
            Assert.Equal("Travel", result[1].CategoriesName);

            _mockRepository.Verify(r => r.GetAll(), Times.Once);
        }

        // TEST 2: GetAll returns an empty list.
        [Fact]
        public void GetAll_WhenNoCategoriesExist_ReturnsEmptyList()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAll()).Returns(new List<Categories>());

            // Act
            var result = _categorieServices.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // TEST 3: GetAll correctly maps category properties.
        [Fact]
        public void GetAll_MapsCategoryPropertiesCorrectly()
        {
            // Arrange
            var createdOn = new DateTime(2026, 2, 10);
            var updatedOn = new DateTime(2026, 2, 15);

            var categories = new List<Categories>
            {
                new Categories
                {
                    CategoriesId = 5,
                    UserId = 20,
                    CategoriesName = "Shopping",
                    CategoriesCode = "SHOP",
                    IsActive = true,
                    CreatedOn = createdOn,
                    UpdatedOn = updatedOn
                }
            };

            _mockRepository.Setup(r => r.GetAll()).Returns(categories);

            // Act
            var result = _categorieServices.GetAll();

            // Assert
            var category = Assert.Single(result);

            Assert.Equal(5, category.CategoriesId);
            Assert.Equal(20, category.UserId);
            Assert.Equal("Shopping", category.CategoriesName);
            Assert.Equal("SHOP", category.CategoriesCode);
            Assert.True(category.IsActive);
            Assert.Equal(createdOn, category.CreatedOn);
            Assert.Equal(updatedOn, category.UpdatedOn);
        }

        // TEST 4: GetById returns an existing category.
        [Fact]
        public void GetById_WhenCategoryExists_ReturnsCategory()
        {
            // Arrange
            var category = new Categories
            {
                CategoriesId = 1,
                UserId = 10,
                CategoriesName = "Food",
                CategoriesCode = "FOOD",
                IsActive = true,
                CreatedOn = new DateTime(2026, 1, 1)
            };

            _mockRepository
                .Setup(r => r.GetById(1))
                .Returns(category);

            // Act
            var result = _categorieServices.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.CategoriesId);
            Assert.Equal("Food", result.CategoriesName);
            Assert.Equal("FOOD", result.CategoriesCode);

            _mockRepository.Verify(r => r.GetById(1), Times.Once);
        }

        // TEST 5: GetById returns null when the category is missing.
        [Fact]
        public void GetById_WhenCategoryDoesNotExist_ReturnsNull()
        {
            // Arrange
            _mockRepository .Setup(r => r.GetById(99)).Returns((Categories)null);

            // Act
            var result = _categorieServices.GetById(99);

            // Assert
            Assert.Null(result);
        }

        // TEST 6: Create sends the correct data to the repository.
        [Fact]
        public void Create_WhenCalled_CreatesCategoryInRepository()
        {
            // Arrange
            var dto = new CategorieDTO
            {
                UserId = 10,
                CategoriesName = "Food",
                CategoriesCode = "FOOD",
                IsActive = true
            };

            Categories savedCategory = null;

            _mockRepository.Setup(r => r.Create(It.IsAny<Categories>()))
                .Callback<Categories>(c => savedCategory = c);

            // Act
            _categorieServices.Create(dto);

            // Assert
            Assert.NotNull(savedCategory);
            Assert.Equal(10, savedCategory.UserId);
            Assert.Equal("Food", savedCategory.CategoriesName);
            Assert.Equal("FOOD", savedCategory.CategoriesCode);
            Assert.True(savedCategory.IsActive);
            Assert.True(savedCategory.CreatedOn <= DateTime.Now);
            Assert.Null(savedCategory.UpdatedOn);

            _mockRepository.Verify(r => r.Create(It.IsAny<Categories>()),Times.Once);
        }

        // TEST 7: Update changes an existing category.
        [Fact]
        public void Update_WhenCategoryExists_UpdatesCategory()
        {
            // Arrange
            var existingCategory = new Categories
            {
                CategoriesId = 1,
                UserId = 10,
                CategoriesName = "Old Name",
                CategoriesCode = "OLD",
                IsActive = true,
                CreatedOn = new DateTime(2026, 1, 1)
            };

            var dto = new CategorieDTO
            {
                CategoriesId = 1,
                UserId = 10,
                CategoriesName = "New Name",
                CategoriesCode = "NEW",
                IsActive = false
            };

            _mockRepository.Setup(r => r.GetById(1)).Returns(existingCategory);

            // Act
            _categorieServices.Update(dto);

            // Assert
            Assert.Equal("New Name", existingCategory.CategoriesName);
            Assert.Equal("NEW", existingCategory.CategoriesCode);
            Assert.False(existingCategory.IsActive);
            Assert.NotNull(existingCategory.UpdatedOn);

            _mockRepository.Verify(r => r.Update(existingCategory),Times.Once);
        }

        // TEST 8: Update exposes the missing-category issue.
        [Fact]
        public void Update_WhenCategoryDoesNotExist_ThrowsNullReferenceException()
        {
            // Arrange
            var dto = new CategorieDTO
            {
                CategoriesId = 99,
                UserId = 10,
                CategoriesName = "Food",
                CategoriesCode = "FOOD",
                IsActive = true
            };

            _mockRepository.Setup(r => r.GetById(99)).Returns((Categories)null);

            // Act and Assert
            Assert.Throws<NullReferenceException>(() => _categorieServices.Update(dto));

            _mockRepository.Verify(r => r.Update(It.IsAny<Categories>()),Times.Never);
        }

        // TEST 9: Delete passes the correct ID to the repository.
        [Fact]
        public void Delete_WhenCalled_PassesCorrectIdToRepository()
        {
            // Arrange
            int categoryId = 5;

            // Act
            _categorieServices.Delete(categoryId);

            // Assert
            _mockRepository.Verify(r => r.Delete(categoryId),Times.Once);
        }

        // TEST 10: Delete does not call the repository more than once.
        [Fact]
        public void Delete_WhenCalled_CallsRepositoryOnce()
        {
            // Arrange
            int categoryId = 10;

            // Act
            _categorieServices.Delete(categoryId);

            // Assert
            _mockRepository.Verify( r => r.Delete(It.IsAny<int>()),Times.Once);
        }
    }
}
