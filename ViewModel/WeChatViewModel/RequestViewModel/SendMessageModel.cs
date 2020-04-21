using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.WeChatViewModel.RequestViewModel
{
    public class SendMessageModel
    {

 
        //接收者（用户）的 openid
        public string touser { get; set; }
        //所需下发的订阅模板id
        public string template_id { get; set; }
        //点击模板卡片后的跳转页面，仅限本小程序内的页面。支持带参数,（示例index?foo=bar）。该字段不填则模板无跳转。  非必填
        public string page { get; set; }
        //模板内容，格式形如 { "key1": { "value": any }, "key2": { "value": any } }
        public string data { get; set; }

    }
}
