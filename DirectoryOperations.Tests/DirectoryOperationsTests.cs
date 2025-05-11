using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using DirectoryOperations.Abstractions;
using DirectoryOperations.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Models;
using Moq;

namespace DirectoryOperations.Tests
{
    [TestClass]
    public class DirectoryOperationsTests
    {
        [TestMethod]
        public void CreateDirectoryTest_Pass()
        {
            // Arrange
            var fileSystem =
                new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\demo\jQuery.js", new MockFileData("some js") },
                {
                    @"c:\demo\image.gif",
                    new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 })
                }
            });
          
            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();            

            var pathToDirectory = @"c:\test";

            var component =
                new DirectoryOperations.Classes
                    .DirectoryOperations(fileSystem,
                                         mockLogger.Object, 
                                         mockExceptionHandler.Object);

            // Act
            var actual =
                component.CreateDirectory(pathToDirectory);

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void MoveDirectoryTest_Pass()
        {
            // Arrange
            var fileSystem =
                new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\demo\jQuery.js", new MockFileData("some js") },
                {
                    @"c:\demo\image.gif",
                    new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 })
                }
            });

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();

            var destinationPath = @"c:\demo";
            var sourcePath = @"c:\demo2";

            var component =
                new DirectoryOperations.Classes
                    .DirectoryOperations(fileSystem,
                                         mockLogger.Object,
                                         mockExceptionHandler.Object);

            // Act
            var actual =
                component.MoveDirectory(destinationPath, sourcePath);

            // Assert
            Assert.IsTrue(actual);
        }    

        [TestMethod]
        public void CopyDirectoryTest_Pass()
        {
            // Arrange
            var fileSystem =
                new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\demo\jQuery.js", new MockFileData("some js") },
                {
                    @"c:\demo\image.gif",
                    new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 })
                }
            });

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();

            var destinationPath = @"c:\demo";
            var sourcePath = @"c:\demo2";

            var component =
                new DirectoryOperations.Classes
                    .DirectoryOperations(fileSystem,
                                         mockLogger.Object,
                                         mockExceptionHandler.Object);
            
            // Act
            var actual =
                component.CopyDirectory(destinationPath, sourcePath);

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void DeleteDirectoryTest_Fail()
        {
            // Arrange
            var fileSystem =
                new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\demo\jQuery.js", new MockFileData("some js") },
                {
                    @"c:\demo\image.gif",
                    new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 })
                }
            });

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();

            var path = @"c:\demo";

            var component =
                new DirectoryOperations.Classes
                    .DirectoryOperations(fileSystem,
                                         mockLogger.Object,
                                         mockExceptionHandler.Object);

            // Act
            var actual =
                component.DeleteDirectory(path);

            // Assert                
            Assert.IsFalse(actual);

        }

        [TestMethod]
        public void DeleteDirectoryTest_Pass()
        {
            // Arrange
            var fileSystem =
                new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\demo\jQuery.js", new MockFileData("some js") }
            });

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();
            
            var directoryComponent =
                new DirectoryOperations.Classes
                    .DirectoryOperations(fileSystem,
                                         mockLogger.Object,
                                         mockExceptionHandler.Object);

            var fileComponent =
                new FileOperations(fileSystem,
                                   mockLogger.Object,
                                   mockExceptionHandler.Object);

            // Act
            fileComponent.DeleteFile(@"c:\demo\jQuery.js");

            var actual =
                directoryComponent.DeleteDirectory(@"c:\demo");

            // Assert                
            Assert.IsTrue(actual);
        }
    }
}
