using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace Dto.IRepository.IntellVolunteer
{
    public interface IVHelpAreaRepository : IRepository<VHelpArea>
    {
        //根据条件查询互助信息 （互助ID标识ContentID、互助标题Title、所属组织架构CommunityID,互助地址Address,擅长技能TypeID）
        List<VHelpArea> SearchInfoByWhere(AllSearchViewModel VSearchViewModel);


        //获取所有互助信息  （无参数）
         List<VHelpArea> GetHelpAreaAll();


        VHelpArea SearchInfoByID(string id);

        //获取我的互助信息(志愿者ID、所属社区ID、活动类型ID、状态 等)
        List<VHelpArea> GetByIDList(List<string> ids);

        List<VHelpArea> SearchMyInfoByWhere(GetMyListViewModel VSearchModel);

        //获取我发布的 互助信息
        List<VHelpArea> GetMyPublishInfo(string VID, string status);

        //首页推荐 排除已结束、已认领的互助信息  
        List<VHelpArea> GetMyRecommendList();
    }
}
