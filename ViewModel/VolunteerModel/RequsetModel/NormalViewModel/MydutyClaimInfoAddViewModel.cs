using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.VolunteerModel.RequsetModel.NormalViewModel
{
    public  class MydutyClaimInfoAddViewModel
    {
        public String Userid { get; set; }
        public String UserName { get; set; }
        public DateTime? StartDutyTime { get; set; }
        public DateTime? EndDutyTime { get; set; }
        public string status { get; set; }
        public string OndutyClaims_InfoId { get; set; }
    }
}
