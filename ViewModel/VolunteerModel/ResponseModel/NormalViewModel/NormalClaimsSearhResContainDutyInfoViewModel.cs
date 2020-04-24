using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel;

namespace ViewModel.VolunteerModel.ResponseModel.NormalViewModel
{
    public class NormalClaimsSearhResContainDutyInfoViewModel
    {
        public bool isSuccess;
        public NornalContainDutyMiddle  nornalContainDutyMiddle;
        public BaseViewModel baseViewModel;
        public int TotalNum;
        public NormalClaimsSearhResContainDutyInfoViewModel()
        {
            baseViewModel = new BaseViewModel();
        }

    }
}
