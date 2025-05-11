using DirectoryOperations.Abstractions;
using DirectoryOperations.Classes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryOperations.Tests
{
    [TestClass]
    public class FileOperationsTests
    {
        [TestMethod]
        public void MoveFileTestNoOverwrite_Fail()
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

            var destinationPath = @"c:\myfile.txt";
            var sourcePath = @"c:\demo\jQuery.js";

            var component =
                new FileOperations(fileSystem,
                                   mockLogger.Object,
                                   mockExceptionHandler.Object);

            // Act -- File Exists
            var actual =
                    component.MoveFile(sourcePath, destinationPath, false);

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void MoveFileTestNoOverwrite_Pass()
        {
            // Arrange
            var fileSystem =
                new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\myfile.txt", new MockFileData("Testing is meh.") },
                { @"c:\demo\\test\extra\jQuery.js", new MockFileData("some js") },
                {
                    @"c:\demo\image.gif",
                    new MockFileData(new byte[] { 0x12, 0x34, 0x56, 0xd2 })
                }
            });

            var mockLogger
                = new Mock<ILogger>();

            var mockExceptionHandler =
                new Mock<IException>();

            var sourcePath = @"c:\myfile.txt";
            var destinationPath = @"c:\demo\test\extra\myfile.txt";

            var component =
                new FileOperations(fileSystem,
                                   mockLogger.Object,
                                   mockExceptionHandler.Object);

            // Act
            var actual = component.MoveFile(sourcePath, destinationPath, false);

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void MoveFileTestOverwrite_Pass()
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

            var sourcePath = @"c:\myfile.txt";
            var destinationPath = @"c:\demo\jQuery.js";

            var component =
                new FileOperations(fileSystem,
                                   mockLogger.Object,
                                   mockExceptionHandler.Object);

            // Act
            var actual = component.MoveFile(sourcePath, destinationPath, true);

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CopyFileTestNoOverwriteExists_Pass()
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

            var sourcePath = @"c:\myfile.txt";
            var destinationPath = @"c:\demo\jQuery.js";

            var component =
                new FileOperations(fileSystem,
                                   mockLogger.Object,
                                   mockExceptionHandler.Object);

            // Act
            var actual = component.CopyFile(sourcePath, destinationPath, false);

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CopyFileTestNoOverwrite_Pass()
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

            var sourcePath = @"c:\myfile.txt";
            var destinationPath = @"c:\demo\myfile.txt";

            var component =
                new FileOperations(fileSystem,
                                   mockLogger.Object,
                                   mockExceptionHandler.Object);

            // Act
            var actual = component.CopyFile(sourcePath, destinationPath, false);

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CopyFileTestOverwrite_Pass()
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

            var sourcePath = @"c:\myfile.txt";
            var destinationPath = @"c:\demo\jQuery.js";

            var component =
                new FileOperations(fileSystem,
                                   mockLogger.Object,
                                   mockExceptionHandler.Object);

            // Act
            var actual = component.CopyFile(sourcePath, destinationPath, true);

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void DeleteFileTest_Fail()
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

            var path = @"c:\doesntexsist.txt";

            var component =
                new FileOperations(fileSystem,
                                   mockLogger.Object,
                                   mockExceptionHandler.Object);

            // Act
            var actual = component.DeleteFile(path);

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void DeleteFileTest_Pass()
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

            var path = @"c:\demo\image.gif";

            var component =
                new FileOperations(fileSystem,
                                   mockLogger.Object,
                                   mockExceptionHandler.Object);

            // Act
            var actual = component.DeleteFile(path);

            // Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void Exists_Pass()
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

            var path = @"c:\demo\image.gif";

            var component =
                new FileOperations(fileSystem,
                                   mockLogger.Object,
                                   mockExceptionHandler.Object);

            // Act
            var actual = component.Exists(path);

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void Exists_Fail()
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

            var path = @"c:\demo\doesnotexist.gif";

            var component =
                new FileOperations(fileSystem,
                                   mockLogger.Object,
                                   mockExceptionHandler.Object);

            // Act
            var actual = component.Exists(path);

            // Assert
            Assert.IsFalse(actual);
        }
    }
}
