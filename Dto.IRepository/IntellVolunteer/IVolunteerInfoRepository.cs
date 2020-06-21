using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerBackground.RequsetModel;

namespace Dto.IRepository.IntellVolunteer
{
    public interface IVolunteerInfoRepository : IRepository<Volunteer_Info>
    {

        //根据用户id查询
        IQueryable<Volunteer_Info> GetInfoByVON(string VON);

        List<User_Depart> GetDepartAll();
        User_Depart GetDepartByCode(string code);
        // 根据条件查角色
        List<Volunteer_Info> SearchInfoByWhere(VolunteerSearchViewModel VSearchViewModel);
        //查询用户数量
        IQueryable<Volunteer_Info> GetVolunteerAll(VolunteerSearchViewModel VSearchViewModel);
        List<Volunteer_Info> SearchInfoByWhereForBackGround(VolunteerInfoSearchViewModel VSearchViewModel);
        IQueryable<Volunteer_Info> GetVolunteerAll(VolunteerInfoSearchViewModel VSearchViewModel);

        void UpdateByModel(VolunteerInfoUpdateViewModel viewModel);

        string GetMaxVNO();

        Volunteer_Info SearchInfoByID(string id);

        void EditInfo(Volunteer_Info delmodel);

    }
}
