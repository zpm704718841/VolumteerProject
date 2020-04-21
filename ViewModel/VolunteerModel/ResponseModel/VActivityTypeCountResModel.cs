using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace ViewModel.VolunteerModel.ResponseModel
{
    public class VActivityTypeCountResModel
    {
        
        public bool isSuccess;
        public List<VActivityTypeCountMiddle> VActivityTypeCount;
        public BaseViewModel baseViewModel;
        public int TotalNum;
        public VActivityTypeCountResModel()
        {
            baseViewModel = new BaseViewModel();
        }
    }
}
