using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.IRepository.IntellVolunteer
{
    public interface  IOndutyClaimsInfoRepository : IRepository<OndutyClaims_Info>
    {
        OndutyClaims_Info GetByID(string id);
    }
}
