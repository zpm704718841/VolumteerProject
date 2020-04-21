using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.WeChatViewModel.ResponseModel
{
    public class WeChatInfoModel
    {
        /// <summary>
        /// openid
        /// </summary>
        public String openid { get; set; }
        public String session_key { get; set; }

        public String unionid { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public String errcode { get; set; }
        /// <summary>
        /// 错误详细信息
        /// </summary>
        public String errmsg { get; set; }
    }
}
