using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ViewModel.VolunteerModel.MiddleModel;

namespace Dto.IRepository.IntellWeChat
{
    public interface ISQLRepository
    {
        string GetIDByName(string name);
    }
}
