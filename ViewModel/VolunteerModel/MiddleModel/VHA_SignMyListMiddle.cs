using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.VolunteerModel.MiddleModel
{
    public class VHA_SignMyListMiddle
    {
        //互助标题
        public string Title { get; set; }
        //发布社区
        public string Community { get; set; }
        //积分
        public string Score { get; set; }
        //发布时间
        public DateTime? CreateDate { get; set; }
    }
}
