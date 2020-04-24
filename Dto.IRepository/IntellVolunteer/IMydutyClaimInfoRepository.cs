using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel.NormalViewModel;
using ViewModel.VolunteerModel.ResponseModel.NormalViewModel;

namespace Dto.IRepository.IntellVolunteer
{
    public interface IMydutyClaimInfoRepository : IRepository<MydutyClaim_Info>
    {
        List<MydutyClaim_Info> getMydutyInfo(MydutyClaimInfoSearchViewModel  mydutyClaimInfoSearchViewModel);
        MydutyClaim_Info GetInfoById(string id);
    }
}
