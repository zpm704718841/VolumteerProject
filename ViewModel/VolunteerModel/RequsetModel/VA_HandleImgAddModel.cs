using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;

namespace ViewModel.VolunteerModel.RequsetModel
{
    public class VA_HandleImgAddModel
    {
        public string ContentID { get; set; }
        public string VID { get; set; }
        public List<VAttachmentAddViewModel> VAttachmentAddList { get; set; }
    }
}
