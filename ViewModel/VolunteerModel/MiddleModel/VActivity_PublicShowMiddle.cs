using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;

namespace ViewModel.VolunteerModel.MiddleModel
{
    public class VActivity_PublicShowMiddle
    {
        public string ID { get; set; }
        public string ContentID { get; set; }
        public string CommunityID { get; set; }
        public string Community { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string VID { get; set; }
        public string NickName { get; set; }
        public string Headimgurl { get; set; }
        public string Experience { get; set; }
        public int isPublic { get; set; }
        public string Memo { get; set; }
        public string Status { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string bak1 { get; set; }
        public string bak2 { get; set; }
        public string bak3 { get; set; }
        public string bak4 { get; set; }
        public string bak5 { get; set; }

        public List<VAttachmentAddViewModel> VAttachmentAddList { get; set; }
        public List<VActivity_PublicShow_GiveLikeMiddle> GiveLikeList { get; set; }
    }
}
