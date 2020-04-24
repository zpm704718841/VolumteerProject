using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel
{
    public  class MydutyClaimInfoSearchMiddleModel
    {

        public string title { get; set; }

        public string XiaoCommunityName { get; set; }

        public string id { get; set; } 
        /// <summary>
        /// 用户名
        /// </summary>
        public String Userid { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public String UserName { get; set; }

        /// <summary>
        /// 值班结束时间
        /// </summary>
        public DateTime? StartDutyTime { get; set; }
        /// <summary>
        /// 值班时间
        /// </summary>
        public DateTime? EndDutyTime { get; set; }

       /// <summary>
       /// 状态
       /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 认领时间
        /// </summary>
        public DateTime? CreateDate { get; set; } 
    }
}
