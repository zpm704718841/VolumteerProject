using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerBackground.MiddleModel;

namespace ViewModel.VolunteerBackground.ResponseModel
{
    public class VloginInfoResModel
    {
        public bool IsSuccess;
        public VolunteerBackgroundInfoModel userInfo;
        public BaseViewModel baseViewModel;
        public VloginInfoResModel()
        {
            baseViewModel = new BaseViewModel();
        }
    }
}
