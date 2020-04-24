using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel;
using ViewModel.VolunteerModel.RequsetModel.NormalViewModel;

namespace Dto.IService.IntellVolunteer
{
    public  interface IMydutyClaimInfoService
    {
        List<MydutyClaimInfoSearchMiddleModel> getMydutyInfoService(MydutyClaimInfoSearchViewModel  mydutyClaimInfoSearchViewModel);
        void getMydutyInfoUpdateService(MydutyClaimInfoUpdateViewModel mydutyClaimInfoUpdateViewModel);
        void getMydutyInfoAddService(MydutyClaimInfoAddViewModel mydutyClaimInfoAddViewModel);
    }
}
