using System;
using System.Collections.Generic;
using System.Text;

namespace Dtol.dtol
{
    public class MydutyClaim_Sign
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public String Userid { get; set; }
        public String UserName { get; set; }
        public DateTime? CheckTime { get; set; }//签到签退时间
        public string type { get; set; }//in=签到；img=上次图片；out=签退
        public string CheckAddress { get; set; }
        public string Checklongitude { get; set; }
        public string Checklatitude { get; set; }
        public String CreateUser { get; set; }
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? UpdateDate { get; set; }
        public String UpdateUser { get; set; }
        public string OndutyClaims_InfoId { get; set; }//值班信息ID
        public string MydutyClaim_InfoID { get; set; }//认领值班信息ID
        public OndutyClaims_Info OndutyClaims_Info { get; set; }//值班信息
        public string bak1 { get; set; }
        public string bak2 { get; set; }
        public string bak3 { get; set; }
        public string bak4 { get; set; }
        public string bak5 { get; set; }
    }
}
