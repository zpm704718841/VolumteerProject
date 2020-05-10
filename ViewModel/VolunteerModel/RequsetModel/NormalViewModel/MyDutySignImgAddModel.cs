using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.VolunteerModel.RequsetModel.NormalViewModel
{
    public class MyDutySignImgAddModel
    {
        public string MydutyClaim_InfoID { get; set; }//认领值班ID
        public string OndutyClaims_InfoId { get; set; }//值班ID
        public string uid { get; set; }
        public string name { get; set; }
        public List<VAttachmentAddViewModel> VAttachmentAddList { get; set; }
    }
}
