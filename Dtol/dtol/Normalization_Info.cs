using System;
using System.Collections.Generic;
using System.Text;

namespace Dtol.dtol
{
    //管控信息
    public class Normalization_Info
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string title { get; set; }
        public string CommunityName { get; set; }
        public string CommunityNameCode { get; set; }
        public string XiaoCommunityName { get; set; }
        public string XiaoCommunityNameeCode { get; set; }
        public string PointsEarned { get; set; }
        public string CheckInTime { get; set; }
        public string CheckOutTime { get; set; }
        //值班认领的时间范围
        public DateTime? DutyStartTime { get; set; }
        public DateTime? DutyEndTime { get; set; }
        public string ServiceContent { get; set; }
        public DateTime? CreateaDate { get; set; } = DateTime.Now;
        public string status { get; set; }

        public List<OndutyClaims_Info> ondutyClaims_Infos { get; set; }


    }
}
