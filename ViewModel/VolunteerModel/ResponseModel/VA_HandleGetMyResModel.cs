using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;

namespace ViewModel.VolunteerModel.ResponseModel
{
    public class VA_HandleGetMyResModel
    {
        public List<VAttachmentAddViewModel> VAttachmentList;
        public string SignUpTime;
        public string SignUpAddress;
        public string SignOutTime;
        public string SignOutAddress;
    }
}
