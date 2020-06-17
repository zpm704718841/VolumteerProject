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
        /// (小程序端接口) 测试文件上传 1
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UploadAsync(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

           
            var filepath = Directory.GetCurrentDirectory() + "\\file";
            var newpath = String.Empty;
            UploadFileAddResModel model = new UploadFileAddResModel();

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    //获取文件后缀
                    string extensionName = System.IO.Path.GetExtension(file.FileName);
                    //存储时生成新文件名
                    model.internalName = System.Guid.NewGuid().ToString("N") + extensionName;

                    var filePath = System.IO.Path.Combine(filepath, model.internalName).Replace("\\", "/");
                    newpath = filePath;
 
                    //文件名
                    model.fileName = file.FileName;
                    model.length = file.Length.ToString();                

                    model.path = newpath;
                    //获取临时文件相对完整路径
                    model.url = System.IO.Path.Combine("https://bhteda.com/Volunteer/file", model.internalName).Replace("\\", "/");                   

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }
            }

            return Ok(Newtonsoft.Json.JsonConvert.SerializeObject(model));
        }


        /// <summary>
        /// (小程序端接口) 测试文件上传 2
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
        /// (小程序端接口) 测试文件上传 3
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult uploadfile()
        {
            var files = Request.Form.Files;
            String RandFileName = "";

            if (files.Count == 0)
            {
                throw new ArgumentException("找不到上传的文件");
            }
            foreach (var formFile in files)
            {
                RandFileName = _FileUploadService.UploadFile(formFile);
            }
            return Ok(RandFileName);
        }




        /// <summary>
        /// (小程序端接口) 测试文件上传 4
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
