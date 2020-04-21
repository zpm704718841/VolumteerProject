using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.MiddleModel;
namespace ViewModel.VolunteerModel.ResponseModel
{
    public class VolunteerGetNoResModel
    {
        public bool isSuccess;
        public string  VNO;
        public BaseViewModel baseViewModel;
        public int TotalNum;
        public VolunteerGetNoResModel()
        {
            baseViewModel = new BaseViewModel();
        }
    }
}
