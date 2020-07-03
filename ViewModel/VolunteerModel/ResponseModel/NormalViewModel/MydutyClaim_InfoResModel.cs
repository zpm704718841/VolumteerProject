using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel;

namespace ViewModel.VolunteerModel.ResponseModel.NormalViewModel
{
    public class MydutyClaim_InfoResModel
    {
        public bool isSuccess;
        public MydutyClaimInfoMiddleModel MiddleModel;
        public MydutyClaim_SignInfo claim_SignInfo;
        public BaseViewModel baseViewModel;
        public int TotalNum;
        public MydutyClaim_InfoResModel()
        {
            baseViewModel = new BaseViewModel();
        }
    }
}
