using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel
{
    public class NornalOndutySearchMiddlecs
    {
        public string id { get; set; }
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
        public DateTime? CreateaDate { get; set; }
        public string status { get; set; }
    }
}
