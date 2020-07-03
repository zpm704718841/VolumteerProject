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

        List<MydutyClaim_Info> GetByOndutyClaims_InfoID(string id);

        List<MydutyClaim_Info> GetByUid(string uid);
        MydutyClaim_Info GetByUidandID(string uid, string id);

        bool GetByParas(string uid, string OndutyClaims_InfoId, DateTime? StartDutyTime, DateTime? EndDutyTime);

        //获取 该值班信息 认领人数
        int GetByParasNum(string OndutyClaims_InfoId, DateTime? StartDutyTime, DateTime? EndDutyTime);
    }
}
