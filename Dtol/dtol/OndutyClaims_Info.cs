using System;
using System.Collections.Generic;
using System.Text;

namespace Dtol.dtol
{
    //值班认领
    public class OndutyClaims_Info
    {
        public string id { get; set; } = Guid.NewGuid().ToString();

        public DateTime? ClaimTime { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int TotalReportNum { get; set; }
        public string Normalization_InfoId { get; set; }
        public Normalization_Info Normalization_Info { get; set; }
        public List<MydutyClaim_Info>  mydutyClaim_Infos { get; set; }
    }
}
