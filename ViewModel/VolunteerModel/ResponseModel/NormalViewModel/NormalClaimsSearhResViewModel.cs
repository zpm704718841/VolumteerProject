using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel;

namespace ViewModel.VolunteerModel.ResponseModel.NormalViewModel
{
    public class NormalClaimsSearhResViewModel
    {
        public bool isSuccess;
        public List<NornalOndutySearchMiddlecs>   nornalOndutySearchMiddlecs;
        public BaseViewModel baseViewModel;
        public int TotalNum;
        public NormalClaimsSearhResViewModel()
        {
            baseViewModel = new BaseViewModel();
        }

    }
}
