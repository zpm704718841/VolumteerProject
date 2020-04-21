using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;
namespace ViewModel.VolunteerModel.RequsetModel
{
    public class VHA_HandleAddModel
    {
        public string ContentID { get; set; }
        public string VID { get; set; }
        public string Results { get; set; }
        public List<VAttachmentAddViewModel> VAttachmentAddList { get; set; }
    }
}
