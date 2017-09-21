using System;
using BethanysPieShop.Models;
using Xunit;

namespace BethanysPieShop.Tests
{
    public class SampleTests
    {
        [Fact]
        public void CanUpdatePiePrice()
        {
            // Arrange
            var pie = new Pie { Name = "Sample pie", Price = 12.95M };
            // Act
            pie.Price = 20M;
            //Assert
            Assert.Equal(20M, pie.Price);
        }

        [Fact]
        public void CanUpdatePieName()
        {
            // Arrange
            var pie = new Pie { Name = "Sample pie", Price = 12.95M };
            // Act
            pie.Name = "Another pie";
            //Assert
            Assert.Equal("Another pie", pie.Name);
        }
    }
}
