using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel;

namespace ViewModel.VolunteerModel.ResponseModel.NormalViewModel
{
    public class MyDutyListResModel
    {
        public bool isSuccess;
        public List<MydutyClaimInfoMiddleModel>   mydutyClaims;
        public BaseViewModel baseViewModel;
        public int TotalNum;
        public MyDutyListResModel()
        {
            baseViewModel = new BaseViewModel();
        }
    }
}
