using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;
using DirectoryAPI.Abstractions;
using DirectoryAPI.Utilities;
using DirectoryAPI.ViewModels;
using DirectoryOperations.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Abstractions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DirectoryAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DirectoryController : Controller
    {
        public DirectoryController(IHostingEnvironment hostingEnvironment,
                                   IDirectoryList directoryList,
                                   IDirectoryOperations directoryOperations,
                                   IFileOperations fileOperations)
        {
            _hostingEnvironment = hostingEnvironment;
            _directoryList = directoryList;
            _directoryOperations = directoryOperations;
            _fileOperations = fileOperations;
        }

        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IDirectoryList _directoryList;
        private readonly IDirectoryOperations _directoryOperations;
        private readonly IFileOperations _fileOperations;

        // GET: api/<controller>
        [HttpGet]
        public JsonResult Get()
        {
            string contentRootPath = _hostingEnvironment.ContentRootPath;

            return Json(_directoryList.GetSubDirectoryAndFilePaths(contentRootPath));
        }

        // GET api/<controller>/{driveLetter}
        [HttpGet("{driveLetter}")]
        public JsonResult Get(string driveLetter)
        {
            DirectoryViewModel viewModel =
                new DirectoryViewModel
                {
                    DriveLetter = driveLetter,

                    DirectoryPath = String.Empty,

                    SubDirectories =
                         _directoryList.GetSubDirectoryPaths($"{ driveLetter }:\\"),

                    Files =
                        _directoryList.GetDirectoryFilePaths($"{ driveLetter }:\\")
                };

            return Json(viewModel);
        }

        // GET api/<controller>/{driveLetter}/{path}
        [HttpGet("{driveLetter}/{**path}")]
        public JsonResult Get(string driveLetter, string path)
        {
            DirectoryViewModel viewModel =
                new DirectoryViewModel
                {
                    DriveLetter = driveLetter,

                    DirectoryPath = "\\" + path.Replace('/', '\\'),

                    SubDirectories =
                         _directoryList.GetSubDirectoryPaths($"{ driveLetter }:\\{ path.Replace('/', '\\') }"),

                    Files =
                        _directoryList.GetDirectoryFilePaths($"{ driveLetter }:\\{ path.Replace('/', '\\') }")
                };

            return Json(viewModel);
        }

        // POST api/<controller>/search
        [HttpPost("search")]
        public JsonResult Search([FromBody] DirectoryViewModel data)
        {
            string directoryPath = data.DirectoryPath;

            if (data.SearchTerms == null ||
                data.SearchTerms.Trim() == String.Empty)
            {
                data.SubDirectories =
                    _directoryList.GetSubDirectoryPaths(directoryPath);

                data.Files =
                    _directoryList.GetDirectoryFilePaths(directoryPath);
            }
            else
            {
                data.SubDirectories =
                    _directoryList.GetSubDirectoryPaths(directoryPath, data.SearchTerms);

                data.Files =
                    _directoryList.GetDirectoryFilePaths(directoryPath, data.SearchTerms);
            }

            return Json(data);
        }

        // POST api/<controller>/upload
        [HttpPost("uploadfile")]
        public JsonResult UploadFile()
        {
            bool uploadSuccess = false;

            ////Byte[] fileData = Convert.FromBase64String(data.FileToUpload);

            return Json(uploadSuccess);
        }

        ////[HttpPost("uploadfile")]
        ////public JsonResult UploadFile([FromBody] DirectoryViewModel data)
        ////{
        ////    bool uploadSuccess = false;

        ////    ////Byte[] fileData = Convert.FromBase64String(data.FileToUpload);

        ////    return Json(uploadSuccess);
        ////}        
        ////[HttpPost]
        ////public async Task<string> Upload()
        ////{
        ////    var provider = new MultipartMemoryStreamProvider();
        ////    await Request.Content.ReadAsMultipartAsync(provider);

        ////    // extract file name and file contents
        ////    var fileNameParam = provider.Contents[0].Headers.ContentDisposition.Parameters
        ////        .FirstOrDefault(p => p.Name.ToLower() == "filename");
        ////    string fileName = (fileNameParam == null) ? "" : fileNameParam.Value.Trim('"');
        ////    byte[] file = await provider.Contents[0].ReadAsByteArrayAsync();

        ////    // Here you can use EF with an entity with a byte[] property, or
        ////    // an stored procedure with a varbinary parameter to insert the
        ////    // data into the DB

        ////    var result
        ////        = string.Format("Received '{0}' with length: {1}", fileName, file.Length);
        ////    return result;
        ////}

        [HttpPost]
        [Route("ReadStringDataManual")]
        public async Task<string> ReadStringDataManual([FromBody] DirectoryViewModel data)
        {
            var test =  await Request.GetRawBodyStringAsync();

            return test;
        }

        [HttpPost]
        [Route("ReadBinaryDataManual")]
        public async Task<byte[]> RawBinaryDataManual([FromBody] DirectoryViewModel data)
        {
            var test =  await Request.GetRawBodyBytesAsync();

            return test;
        }

        // PUT api/<controller>/create/newDirectoryName
        [HttpPut("create/{newDirectoryName}")]
        public JsonResult Create(string newDirectoryName, 
                                 [FromBody] DirectoryViewModel data)
        {
            bool result =
                _directoryOperations.CreateDirectory($"{ data.DirectoryPath }\\{ newDirectoryName }");

            return Json(new { returnvalue = result });
        }

        // PUT api/<controller>/movedirectory/destinationPath
        [HttpPut("movedirectory/{destinationPath}")]
        public JsonResult MoveDirectory(string destinationPath, 
                                       [FromBody] DirectoryViewModel data)
        {
            bool result = 
                    _directoryOperations.MoveDirectory(data.SelectedDirectoryPath, destinationPath);
            
            return Json(new { returnvalue = result });
        }

        // PUT api/<controller>/movedirectory/destinationPath
        [HttpPut("movefile/{destinationPath}")]
        public JsonResult MoveFile(string destinationPath,
                                   [FromBody] DirectoryViewModel data)
        {
            bool result =
                    _fileOperations.MoveFile(data.SelectedFilePath, destinationPath, false);

            return Json(new { returnvalue = result });
        }

        // PUT api/<controller>/copydirectory/destinationPath
        [HttpPut("copydirectory/{destinationPath}")]
        public JsonResult CopyDirectory(string destinationPath,
                                        [FromBody] DirectoryViewModel data)
        {
            bool result =
                    _directoryOperations.CopyDirectory(data.SelectedDirectoryPath, destinationPath);            

            return Json(new { returnvalue = result });
        }

        // PUT api/<controller>/copydirectory/destinationPath
        [HttpPut("copyfile/{destinationPath}")]
        public JsonResult CopyFile(string destinationPath,
                                   [FromBody] DirectoryViewModel data)
        {
            bool result =
                    _fileOperations.CopyFile(data.SelectedFilePath, destinationPath, false);

            return Json(new { returnvalue = result });
        }        

        // DELETE api/<controller>/deletedirectory/
        [HttpDelete("deletedirectory")]
        public JsonResult DeleteDirectory([FromBody] DirectoryViewModel data)
        {
            bool result =
                    _directoryOperations.DeleteDirectory(data.SelectedDirectoryPath);

            return Json(new { returnvalue = result });
        }

        // DELETE api/<controller>/deletefile/
        [HttpDelete("deletefile")]
        public JsonResult DeleteFile([FromBody] DirectoryViewModel data)
        {
            bool result =
                    _fileOperations.DeleteFile(data.SelectedFilePath);

            return Json(new { returnvalue = result });
        }
    }
}
