using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace ViewModel.VolunteerModel.ResponseModel
{
    public  class VolunteerSearchResModel
    {
        public bool isSuccess;
        public List<VolunteerSearchMiddle> Volunteer_Infos;
        public BaseViewModel baseViewModel;
        public int TotalNum;
        public VolunteerSearchResModel()
        {
            baseViewModel = new BaseViewModel();
        }
    }
}
