using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;

namespace Dto.IRepository.IntellVolunteer
{
    public interface ILoginType_LogRepository : IRepository<LoginType_Log>
    {
        LoginType_Log SearchByUIDModel(SearchByVIDModel user);
    }
}
