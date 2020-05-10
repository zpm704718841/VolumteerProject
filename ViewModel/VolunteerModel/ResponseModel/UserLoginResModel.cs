using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.VolunteerModel.ResponseModel
{
    public class UserLoginResModel
    {
        /// <summary>
        /// uid
        /// </summary>
        public string uid { get; set; }
        /// <summary>
        /// 登陆方式
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 登陆方式id
        /// </summary>
        public string typeid { get; set; }
        /// <summary>
        /// 有效天数
        /// </summary>
        public string days { get; set; }
        /// <summary>
        /// 有效小时数
        /// </summary>
        public string hours { get; set; }
    }
}
