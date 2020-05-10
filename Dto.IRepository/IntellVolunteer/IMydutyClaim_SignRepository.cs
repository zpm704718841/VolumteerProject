using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel.NormalViewModel;

namespace Dto.IRepository.IntellVolunteer
{
    public interface IMydutyClaim_SignRepository : IRepository<MydutyClaim_Sign>
    {
        //根据条件查询
        List<MydutyClaim_Sign> GetByParas(SearchByIDAnduidModel model);
    }
}
