using System;
using Dtol.dtol;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;
namespace Dto.IRepository.IntellVolunteer
{
    public interface IVolunteer_Relate_TypeRepository : IRepository<Volunteer_Relate_Type>
    {
        bool CheckInfo(string typeID, string VID);

        List<Volunteer_Relate_Type> GetMyTypeList(string VID);

        void RemoveAll(string userid);
    }
}
