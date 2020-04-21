using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerBackground.MiddleModel;

namespace ViewModel.VolunteerBackground.ResponseModel
{
    public class UserDepartResModel
    {
        public bool IsSuccess;
        public BaseViewModel baseViewModel;
        public List<UserDepartSearchMidModel> UserDepart;

        public UserDepartResModel()
        {
            baseViewModel = new BaseViewModel();
        }
    }
}
