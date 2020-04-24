using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel;
using ViewModel.VolunteerModel.RequsetModel.NormalViewModel;

namespace Dto.IRepository.IntellVolunteer
{
    public interface INormalizationInfoRepository : IRepository<Normalization_Info>
    {
        List<Normalization_Info> NormalizationSearch(NormalSearchViewModel normalSearchViewModel);
        Normalization_Info NormalizationContainDuty(NormalizationContainSearchViewModel normalizationContainSearchViewModel);


    }
}
