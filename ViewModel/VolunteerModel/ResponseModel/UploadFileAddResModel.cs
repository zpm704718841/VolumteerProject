using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.VolunteerModel.ResponseModel
{
    public class UploadFileAddResModel
    {
         
        /// <summary>
        /// 文件上传时名称，包括扩展名
        /// </summary>
        public string fileName { set; get; }
        /// <summary>
        /// 文件上传成功后生产的名称，包括扩展名
        /// </summary>
        public string internalName { set; get; }
        /// <summary>
        /// 浏览路径
        /// </summary>
        public string url { set; get; }
        /// <summary>
        /// 物理路径
        /// </summary>
        public string path { set; get; }


        /// <summary>
        /// 上传的图片的大小
        /// </summary>
        public string length { get; set; }
        public string bak1 { get; set; }
    }
}
