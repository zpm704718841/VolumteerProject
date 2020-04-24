using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel
{
    public  class OndutyClaimsSearchMiddle
    {
        public string id { get; set; }
        public int hasReportNum { get; set; }
        public int TotalReportNum { get; set; }
        public DateTime? ClaimTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Normalization_InfoId { get; set; }
    }
}
