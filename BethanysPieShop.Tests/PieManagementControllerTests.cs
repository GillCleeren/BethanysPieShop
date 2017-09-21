using System.Collections.Generic;
using System.Linq;
using BethanysPieShop.Controllers;
using BethanysPieShop.Models;
using BethanysPieShop.Tests.Model;
using BethanysPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace BethanysPieShop.Tests
{
    public class PieManagementControllerTests
    {
        [Fact]
        public void Index_ReturnsAViewResult_ContainsAllPies()
        {
            //arrange
            var mockCategoryRepository = RepositoryMocks.GetCategoryRepository();
            var mockPieRepository = RepositoryMocks.GetPieRepository();

            var pieManagementController = new PieManagementController(mockPieRepository.Object, mockCategoryRepository.Object);

            //act
            var result = pieManagementController.Index();

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var pies = Assert.IsAssignableFrom<IEnumerable<Pie>>(viewResult.ViewData.Model);
            Assert.Equal(10, pies.Count());
        }

        [Fact]
        public void AddPie_Redirects_ValidPieViewModel()
        {
            //arrange
            var pieEditViewModel = new PieEditViewModel();
            var mockPieRepository = RepositoryMocks.GetPieRepository();
            var pie = mockPieRepository.Object.GetPieById(1);
            pieEditViewModel.Pie = pie;
            pieEditViewModel.CategoryId = 1;

            var mockCategoryRepository = new Mock<ICategoryRepository>();

            var pieManagementController = new PieManagementController(mockPieRepository.Object, mockCategoryRepository.Object);

            //act
            var result = pieManagementController.AddPie(pieEditViewModel);

            //assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public void AddPie_ReturnsViewResultWithViewModel_InvalidPieViewModel()
        {
            //arrange
            var pieEditViewModel = new PieEditViewModel();
            var mockPieRepository = RepositoryMocks.GetPieRepository();
            var pie = mockPieRepository.Object.GetPieById(1);
            pie.IsPieOfTheWeek = true;
            pie.InStock = false;
            pieEditViewModel.Pie = pie;
            pieEditViewModel.CategoryId = 1;

            var mockCategoryRepository = new Mock<ICategoryRepository>();

            var pieManagementController = new PieManagementController(mockPieRepository.Object, mockCategoryRepository.Object);

            //act
            var result = pieManagementController.AddPie(pieEditViewModel);

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.Model);
            Assert.True(string.IsNullOrEmpty(viewResult.ViewName));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public void AddPie_ReturnsViewResultWithViewModel_InvalidPieViewModel_NegativePrice(int price)
        {
            //arrange
            var pieEditViewModel = new PieEditViewModel();
            var mockPieRepository = RepositoryMocks.GetPieRepository();
            var pie = mockPieRepository.Object.GetPieById(1);
            pie.IsPieOfTheWeek = true;
            pie.InStock = false;
            pie.Price = price;
            pieEditViewModel.Pie = pie;
            pieEditViewModel.CategoryId = 1;

            var mockCategoryRepository = new Mock<ICategoryRepository>();

            var pieManagementController = new PieManagementController(mockPieRepository.Object, mockCategoryRepository.Object);

            //act
            var result = pieManagementController.AddPie(pieEditViewModel);

            //assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.NotNull(viewResult.Model);
            Assert.True(string.IsNullOrEmpty(viewResult.ViewName));

        }
    }
}