using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace Dto.IRepository.IntellVolunteer
{
    public interface IVolunteerActivityRepository : IRepository<VolunteerActivity>
    {
        // 根据条件查角色
        List<VolunteerActivity> SearchInfoByWhere(AllSearchViewModel VSearchViewModel);
        //查询用户数量
        List<VolunteerActivity> GetVolunteerActivityAll();

        int GetTypeCount(VolunteerActivityTypeModel typeModel);

        VolunteerActivity GetByID(string id);

        List<VolunteerActivity> GetByIDList(List<string> ids);

        int GetTypeCounts(string typeID);

        List<VolunteerActivity> SearchMyInfoByWhere(GetMyListViewModel VSearchModel);


        //首页推荐 排除已结束、已满员的 活动
        List<VolunteerActivity> GetMyRecommendList();

 
    }
}
