using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace ViewModel.VolunteerModel.ResponseModel
{
    public class VolunteerActivitySearchResModel
    {
        public bool isSuccess;
        public List<VolunteerActivitySearchMiddle> Volunteer_Activity;
        public BaseViewModel baseViewModel;
        public int TotalNum;
        public VolunteerActivitySearchResModel()
        {
            baseViewModel = new BaseViewModel();
        }
    }
}
