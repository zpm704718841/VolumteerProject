using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel;
using ViewModel.VolunteerModel.RequsetModel.NormalViewModel;
using ViewModel.VolunteerModel.ResponseModel.NormalViewModel;

namespace Dto.IService.IntellVolunteer
{
    public  interface INormalizationInfoService
    {
        void AddNormalizationInfoService(NormalAddViewModel normalAddViewModel);
        List<NornalOndutySearchMiddlecs> NormalizationSeachService(NormalSearchViewModel normalSearchViewModel);
        NornalContainDutyMiddle NormalizationContainDutyService(NormalizationContainSearchViewModel normalizationContainSearchViewModel);
    
    }
}
