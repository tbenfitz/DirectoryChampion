using DirectoryAPI.Controllers;
using DirectoryOperations.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Moq;
using System;
using Xunit;

namespace DirectoryAPI_Tests
{
    public class DirectoryControllerTest
    {
        [Fact]
        public void DefaultGetTest_Pass()
        {
            // Arrange
            var mockEnvironment = new Mock<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();
           
            mockEnvironment
                .Setup(m => m.EnvironmentName)
                .Returns("Hosting:UnitTestEnvironment");

            var mockDirectoryList =
                new Mock<IDirectoryList>();

            var mockDirectoryOperations = 
                new Mock<IDirectoryOperations>();

            DirectoryController controller =
                new DirectoryController(mockEnvironment, 
                                        mockDirectoryList, 
                                        mockDirectoryOperations);

            ////IActionResult result = sut.Index();

            ////Assert.IsType<ViewResult>(result);

            // Act

            // Assert
        }
    }
}
