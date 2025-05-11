using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using DirectoryOperations.Abstractions;
using DirectoryOperations.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Abstractions;
using Models.Models;
using Moq;

namespace DirectoryOperations.Tests
{
    /// <summary>
    /// Unit tests for DirectoryList class
    /// Uses MOQ and System.IO.Abstractions to create a MockFileSystem
    ///   ensuring the tests do not actually touch the actual file system
    /// </summary>
    [TestClass]
    public class DirectoryListTests
    {
        [TestMethod]
        public void GetDirectoryTest_Pass()
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

            var mockDirectoryModel
                = new Mock<IDirectoryModel>();

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();

            var pathToDirectory = @"c:\";

            var component =
                new DirectoryList(fileSystem,
                                  mockDirectoryModel.Object,
                                  mockLogger.Object,
                                  mockExceptionHandler.Object);

            // Act
            var actual =
                component.GetDirectory(pathToDirectory);

            // Assert
            Assert.IsNotNull(actual);
            Assert.IsInstanceOfType(actual, typeof(IDirectoryModel));
        }

        [TestMethod]
        public void GetDirectoryTest_Fail()
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

            var mockDirectoryModel
                = new Mock<IDirectoryModel>();

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();

            var pathToDirectory = @"c:\doesnotexist";

            var component =
                new DirectoryList(fileSystem,
                                  mockDirectoryModel.Object,
                                  mockLogger.Object,
                                  mockExceptionHandler.Object);

            try
            {
                // Act
                var actual =
                    component.GetDirectory(pathToDirectory);
            }
            catch (DirectoryNotFoundException)
            {
                // Assert                
                return;
            }            
        }

        [TestMethod]
        public void GetSubDirectoryAndFilePathsTest_Fail()
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

            var mockDirectoryModel
                = new Mock<IDirectoryModel>();

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();

            var testPath =
                @"c:\demo\jQuery_SHOULD_NOT_EXIST.js";

            var component =
                new DirectoryList(fileSystem,
                                  mockDirectoryModel.Object,
                                  mockLogger.Object,
                                  mockExceptionHandler.Object);
            try
            {
                // Act
                var actual =
                    component.GetSubDirectoryAndFilePaths(testPath);
            }
            catch (DirectoryNotFoundException)
            {
                // Assert                
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod]
        public void GetSubDirectoryAndFilePathsTest_Pass()
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

            var mockDirectoryModel
                = new Mock<IDirectoryModel>();

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();

            var component =
                new DirectoryList(fileSystem,
                                  mockDirectoryModel.Object,
                                  mockLogger.Object,
                                  mockExceptionHandler.Object);
            // Act
            var actual =
                component.GetSubDirectoryAndFilePaths(@"c:\demo");

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(2, actual.Length);            
        }

        [TestMethod]
        public void GetSubDirectoryAndFilePathsAsListTest_Fail()
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

            var mockDirectoryModel
                = new Mock<IDirectoryModel>();

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();

            var testPath =
                 @"c:\demo\jQuery_SHOULD_NOT_EXIST.js";
            
            var component =
                new DirectoryList(fileSystem,
                                  mockDirectoryModel.Object,
                                  mockLogger.Object,
                                  mockExceptionHandler.Object);
            try
            {
                // Act
                var actual =
                    component.GetSubDirectoryAndFilePaths(testPath);
            }
            catch (DirectoryNotFoundException)
            {
                // Assert                
                return;
            }

            Assert.Fail("The expected exception was not thrown.");
        }

        [TestMethod]
        public void GetSubDirectoryAndFilePathsAsListTest_Pass()
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

            var mockDirectoryModel
                = new Mock<IDirectoryModel>();

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();

            var component =
                new DirectoryList(fileSystem,
                                  mockDirectoryModel.Object,
                                  mockLogger.Object,
                                  mockExceptionHandler.Object);
            var actual =
                component.GetSubDirectoryAndFilePaths(@"c:\demo");

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(2, actual.Length);
        }

        [TestMethod]
        public void GetSubDirectoryPathsTest_Pass()
        {
            // Arrange           
            var fileSystem =
                new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\demo\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\demo\jQuery.js", new MockFileData("some js") },
                {
                    @"c:\demo\test\image.gif",
                    new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 })
                }
            });

            var mockDirectoryModel
                = new Mock<IDirectoryModel>();

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();

            var component =
                new DirectoryList(fileSystem,
                                  mockDirectoryModel.Object,
                                  mockLogger.Object,
                                  mockExceptionHandler.Object);
            var actual =
                component.GetSubDirectoryAndFilePaths(@"c:\demo");

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Length);
        }

        [TestMethod]
        public void GetSubDirectoryPathsTest_Fail()
        {
            // Arrange           
            var fileSystem =
                new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\demo\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\demo\jQuery.js", new MockFileData("some js") },
                {
                    @"c:\demo\test\image.gif",
                    new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 })
                }
            });

            var mockDirectoryModel
                = new Mock<IDirectoryModel>();

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();

            var component =
                new DirectoryList(fileSystem,
                                  mockDirectoryModel.Object,
                                  mockLogger.Object,
                                  mockExceptionHandler.Object);
            var actual =
                component.GetSubDirectoryAndFilePaths(@"c:\demo");

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreNotEqual(4, actual.Length);
        }

        [TestMethod]
        public void GetSubDirectoryPathsAsListTest_Pass()
        {
            // Arrange           
            var fileSystem =
                new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\demo\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\demo\jQuery.js", new MockFileData("some js") },
                {
                    @"c:\demo\test\image.gif",
                    new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 })
                }
            });

            var mockDirectoryModel
                = new Mock<IDirectoryModel>();

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();

            var component =
                new DirectoryList(fileSystem,
                                  mockDirectoryModel.Object,
                                  mockLogger.Object,
                                  mockExceptionHandler.Object);
            var actual =
                component.GetSubDirectoryAndFilePaths(@"c:\demo");

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Length);
        }

        [TestMethod]
        public void GetSubDirectoryPathsAsListTest_Fail()
        {
            // Arrange           
            var fileSystem =
                new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\demo\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\demo\jQuery.js", new MockFileData("some js") },
                {
                    @"c:\demo\test\image.gif",
                    new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 })
                }
            });

            var mockDirectoryModel
                = new Mock<IDirectoryModel>();

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();

            var component =
                new DirectoryList(fileSystem,
                                  mockDirectoryModel.Object,
                                  mockLogger.Object,
                                  mockExceptionHandler.Object);
            var actual =
                component.GetSubDirectoryAndFilePaths(@"c:\demo");

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreNotEqual(4, actual.Length);
        }        

        [TestMethod]
        public void GetDirectoryFilePathsTest_Pass()
        {
            // Arrange           
            var fileSystem =
                new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\demo\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\demo\jQuery.js", new MockFileData("some js") },
                {
                    @"c:\demo\test\image.gif",
                    new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 })
                }
            });

            var mockDirectoryModel
                = new Mock<IDirectoryModel>();

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();

            var component =
                new DirectoryList(fileSystem,
                                  mockDirectoryModel.Object,
                                  mockLogger.Object,
                                  mockExceptionHandler.Object);
            var actual =
                component.GetSubDirectoryAndFilePaths(@"c:\demo");

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Length);
        }

        [TestMethod]
        public void GetDirectoryFilePathsTest_Fail()
        {
            // Arrange           
            var fileSystem =
                new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\demo\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\demo\jQuery.js", new MockFileData("some js") },
                {
                    @"c:\demo\test\image.gif",
                    new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 })
                }
            });

            var mockDirectoryModel
                = new Mock<IDirectoryModel>();

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();

            var component =
                new DirectoryList(fileSystem,
                                  mockDirectoryModel.Object,
                                  mockLogger.Object,
                                  mockExceptionHandler.Object);
            var actual =
                component.GetSubDirectoryAndFilePaths(@"c:\demo");

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreNotEqual(4, actual.Length);
        }

        [TestMethod]
        public void GetDirectoryFilePathsAsListTest_Pass()
        {
            // Arrange           
            var fileSystem =
                new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\demo\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\demo\jQuery.js", new MockFileData("some js") },
                {
                    @"c:\demo\test\image.gif",
                    new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 })
                }
            });

            var mockDirectoryModel
                = new Mock<IDirectoryModel>();

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();

            var component =
                new DirectoryList(fileSystem,
                                  mockDirectoryModel.Object,
                                  mockLogger.Object,
                                  mockExceptionHandler.Object);
            var actual =
                component.GetSubDirectoryAndFilePaths(@"c:\demo");

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(3, actual.Length);
        }

        [TestMethod]
        public void GetDirectoryFilePathsAsListTest_Fail()
        {
            // Arrange           
            var fileSystem =
                new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\demo\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\demo\jQuery.js", new MockFileData("some js") },
                {
                    @"c:\demo\test\image.gif",
                    new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 })
                }
            });

            var mockDirectoryModel
                = new Mock<IDirectoryModel>();

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();

            var component =
                new DirectoryList(fileSystem,
                                  mockDirectoryModel.Object,
                                  mockLogger.Object,
                                  mockExceptionHandler.Object);
            var actual =
                component.GetSubDirectoryAndFilePaths(@"c:\demo");

            // Assert
            Assert.IsNotNull(actual);
            Assert.AreNotEqual(4, actual.Length);
        }
    }    
}


