using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace Dto.IRepository.IntellVolunteer
{
    public interface IBaseTypeRepository : IRepository<VBaseType>
    {
        List<VBaseType> SearchInfoByWhere();

        bool CheckStatus(string TypeID);

        List<VBaseType> SearchInfoByWhere(string parentCode);
    }
}
