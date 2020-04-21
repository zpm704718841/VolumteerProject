using Dtol;
using Dtol.dtol;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using ViewModel.VolunteerModel.RequsetModel;
using System.Linq.Expressions;
using Dtol.EfCoreExtion;
using ViewModel.VolunteerModel.MiddleModel;
using Dto.IRepository.IntellVolunteer;


namespace Dto.IRepository.IntellVolunteer
{
    public interface IVolunteer_MessageRepository : IRepository<Volunteer_Message>
    {
        Volunteer_Message GetByID(string id, string VID);

        List<Volunteer_Message> GetByVID(string VID);
    }
}
