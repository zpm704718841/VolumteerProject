using System;
using System.Collections.Generic;
using System.Text;

namespace Dtol.dtol
{
    //个人值班认领
    public  class MydutyClaim_Info
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public String Userid { get; set; }
        public String UserName { get; set; }
        public DateTime? StartDutyTime { get; set; }
        public DateTime? EndDutyTime { get; set; }

        public string status { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public string OndutyClaims_InfoId { get; set; }
        public OndutyClaims_Info OndutyClaims_Info { get; set; }
    }
}
