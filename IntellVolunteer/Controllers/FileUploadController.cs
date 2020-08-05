using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.IService.IntellVolunteer;
using Dtol.dtol;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemFilter.PublicFilter;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.ResponseModel;
using System.IO;
using System.Web;
using Serilog;
 



namespace IntellVolunteer.Controllers
{
    [Route("VolunteerManageApi/[controller]/[action]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadService _FileUploadService;
        //private readonly ILogger _ILogger;

        public FileUploadController(IFileUploadService uploadService )
        {
            _FileUploadService = uploadService;
            //_ILogger = logger;
        }



        /// <summary>
        /// (小程序端接口) 文件上传
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public string PostFile([FromForm] IFormCollection formCollection)
        {
            var files = formCollection.Files;
          
            String RandFileName = "";

            if (files.Count == 0)
            {
                RandFileName = "找不到上传的文件";
            }
            foreach (var formFile in files)
            {
                RandFileName = _FileUploadService.PostFile(formFile);
            }
 
            return RandFileName; 
        }



        /// <summary>
        /// (小程序端接口) 文件上传
        /// </summary>
        /// <param name="formCollection"></param>
        /// <returns></returns>
        [HttpPost]
        public string PostFileNew([FromForm] IFormCollection formCollection, string TypeID)
        {
            var files = formCollection.Files;

            String RandFileName = "";

            if (files.Count == 0)
            {
                RandFileName = "找不到上传的文件";
            }
            foreach (var formFile in files)
            {
                RandFileName = _FileUploadService.PostFileNew(formFile, TypeID);
            }

            return RandFileName;
        }


    }
}
