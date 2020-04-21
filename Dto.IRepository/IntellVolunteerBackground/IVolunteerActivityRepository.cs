using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerBackground.RequsetModel;

namespace Dto.IRepository.IntellVolunteerBackground
{
    public interface IVolunteerActivityRepository:IRepository<VolunteerActivity>
    {
        IQueryable<VolunteerActivity> GetVolunteerAll(VolunteerActivitySearchViewModel VSearchViewModel);
        List<VolunteerActivity> SearchInfoByWhere(VolunteerActivitySearchViewModel VSearchViewModel);
        IQueryable<VolunteerActivity> GetVolunteerActivityByTitle(string title);
        IQueryable<VolunteerActivity> GetVolunteerActivityForUpdate(string title, string id);
        IQueryable<VolunteerActivity> GetAll(VolunteerActivitySearchViewModel VSearchViewModel);
        List<User_Depart> GetDepartAll();
        int DeleteByUseridList(List<string> IdList);
        //void Delete(string id);
        void Delete(VolunteerActivityDeleteViewModel delmodel);
    }
}
