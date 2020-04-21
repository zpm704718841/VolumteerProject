using System;
using Dtol.dtol;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace Dto.IRepository.IntellVolunteer
{
    public interface IVActivity_Relate_TypeRepository : IRepository<VActivity_Relate_Type>
    {
        List<VActivity_Relate_Type> GetRelateList(string id);

        int GetSum(string ContentID, string typeID);
    }
}
