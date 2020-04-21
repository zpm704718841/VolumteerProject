using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;

namespace ViewModel.VolunteerModel.ResponseModel
{
    public class VHA_HandleGetMyResModel
    {
        public List<VAttachmentAddViewModel> VAttachmentList;
        public string contents;
        public string time;
        public string opinion;
        public string opinionTime;
    }
}
