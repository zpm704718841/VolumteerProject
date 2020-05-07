using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerBackground.MiddleModel;
using ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.RequsetModel.NormalViewModel;
using ViewModel.VolunteerModel.ResponseModel.NormalViewModel;

namespace Dto.IService.IntellVolunteer
{
    public  interface INormalizationInfoService
    {
        void AddNormalizationInfoService(NormalAddViewModel normalAddViewModel);
        List<NornalOndutySearchMiddlecs> NormalizationSeachService(NormalSearchViewModel normalSearchViewModel);
        NornalContainDutyMiddle NormalizationContainDutyService(NormalizationContainSearchViewModel normalizationContainSearchViewModel);

        //获取 小区信息
        List<UserDepartSearchMidModel> GetDepartList(CodeViewModel code);

        // (小程序端接口)  获取 该值班信息的认领人信息
        List<MydutyClaimInfoSearchMiddleModel> GetDutyListByID(CodeViewModel code);
    }
}
