using AutoMapper;
using Dto.IRepository.IntellVolunteer;
using Dto.IService.IntellVolunteer;
using Dtol.dtol;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.ResponseModel;

namespace Dto.Service.IntellVolunteer
{
    public class FileUploadService: IFileUploadService
    {
        public FileUploadService()
        {
             
        }

        public string UploadFile(IFormFile formFile)
        {
            string filePath = "";//上传文件的路径
            string RandName = "";
            string[] fileTail = formFile.FileName.Split('.');
            RandName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + "." + fileTail[1];
            filePath = Directory.GetCurrentDirectory() + "\\files\\" + RandName;
            if (formFile.Length > 0)
            {
                //using (var stream = new FileStream(filePath, FileMode.Create))
                //{
                //    formFile.CopyTo(stream);
                //}
            }
            return RandName;
        }

        public string PostFile(IFormFile file)
        {
            var newpath = String.Empty;
            var filepath = Directory.GetCurrentDirectory() + "\\file\\";
            UploadFileAddResModel model = new UploadFileAddResModel();
            ////20200531  判断上传得文件是否是 图片类型文件，如果不是则不保存
            string typenew = file.ContentType;
            if (typenew.Contains("Image") || typenew.Contains("image"))
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


                IFormFile current = file;
                using (FileStream fileStream = new FileStream(newpath, FileMode.Create, FileAccess.Write))
                {
                    current.CopyTo((Stream)fileStream);
                    ((Stream)fileStream).Flush();
                }

            }
            else
            {
                model = null;
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(model);

        }

        public string PostFileNew(IFormFile file,string TypeID)
        {
            var newpath = String.Empty;
            var filepath = Directory.GetCurrentDirectory() + "\\file\\";
            UploadFileAddResModel model = new UploadFileAddResModel();
            ////20200531  判断上传得文件是否是 图片类型文件，如果不是则不保存
            string typenew = file.ContentType;
            if (typenew.Contains("Image") || typenew.Contains("image"))
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
                model.bak1 = TypeID;

                IFormFile current = file;
                using (FileStream fileStream = new FileStream(newpath, FileMode.Create, FileAccess.Write))
                {
                    current.CopyTo((Stream)fileStream);
                    ((Stream)fileStream).Flush();
                }
            }
            else
            {
                model = null;
            }
               


            return Newtonsoft.Json.JsonConvert.SerializeObject(model);
        }


    }
}
