using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.VolunteerModel.RequsetModel
{
    public class VActivity_PublicShowAddModel
    {
        public string ContentID { get; set; }
        public string CommunityID { get; set; }
        public string Community { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string VID { get; set; }
        public string Experience { get; set; }
        public int isPublic { get; set; }
        public string Memo { get; set; }
        public List<VAttachmentAddViewModel> VAttachmentAddList { get; set; }

    }
}
