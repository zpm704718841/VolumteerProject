using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel
{
    public class ContainOnDutyMiddleMiddle
    {
        public string Id { get; set; }
        public int ReportNum { get; set; }
        public int TotalReportNum { get; set; }
        public DateTime? ClaimTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string status { get; set; }

        public string isClaim { get; set; }
    }
}
