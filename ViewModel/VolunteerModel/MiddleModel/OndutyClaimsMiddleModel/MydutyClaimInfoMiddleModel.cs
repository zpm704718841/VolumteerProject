using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel
{
    public  class MydutyClaimInfoMiddleModel
    {
        public string id { get; set; } 
        public String Userid { get; set; }
        public String UserName { get; set; }
        public DateTime? StartDutyTime { get; set; }
        public DateTime? EndDutyTime { get; set; }

        public string status { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public string OndutyClaims_InfoId { get; set; }
    }
}
