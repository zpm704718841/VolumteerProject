using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace Dto.IRepository.IntellVolunteer
{
    public interface IVolunteer_ScoreRepository : IRepository<Volunteer_Score>
    {
        string GetMyScore(SearchByVIDModel searchModel);

 


    }
}
