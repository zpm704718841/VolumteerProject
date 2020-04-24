using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel;

namespace ViewModel.VolunteerModel.ResponseModel.NormalViewModel
{
     public  class MydutyClaimInfoSearchResModel
    {
        public bool isSuccess;
        public List<MydutyClaimInfoSearchMiddleModel>  mydutyClaimInfoSearchMiddleModel;
        public BaseViewModel baseViewModel;
        public int TotalNum;
        public MydutyClaimInfoSearchResModel()
        {
            baseViewModel = new BaseViewModel();
        }
    }
}
