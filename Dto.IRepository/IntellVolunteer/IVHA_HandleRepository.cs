using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace Dto.IRepository.IntellVolunteer
{
    public interface IVHA_HandleRepository : IRepository<VHA_Handle>
    {
        List <VHA_Handle> GetMyHandle(string VID, string ContentID);

        VHA_Handle GetVolunteerHandle(string ID);
    }
}
