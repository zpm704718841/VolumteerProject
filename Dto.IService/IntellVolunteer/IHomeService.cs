using Dtol.dtol;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;
using System.Threading.Tasks;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.ResponseModel;
using ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel;

namespace Dto.IService.IntellVolunteer
{
    public interface IHomeService
    {
        //获取当前用户 今天以后的报名活动情况 日期列表
        List<string> GetMyAllDate(SearchByVIDModel vidModel);

        ///获取当前用户 根据活动时间显示具体 活动情况列表 20200527
        List<VolunteerActivitySearchMiddle> GetMyAllByDate(VolunteerActivitySearchByDateModel vidModel);

        ///获取当前用户 根据时间显示具体常态化管控认领 列表 20200617
        List<MydutyClaimInfoMiddleModel> GetMyDutyAllByDate(VolunteerActivitySearchByDateModel vidModel);
    }
}
