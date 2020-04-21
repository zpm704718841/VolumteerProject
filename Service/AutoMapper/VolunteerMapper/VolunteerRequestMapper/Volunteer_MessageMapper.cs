using AutoMapper;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;

namespace Dto.Service.AutoMapper.VolunteerMapper.VolunteerRequestMapper
{
    public class Volunteer_MessageMapper : Profile
    {
        public Volunteer_MessageMapper()
        {
            CreateMap<Volunteer_Message, Volunteer_MessageMiddle>();
            CreateMap<Volunteer_MessageMiddle, Volunteer_Message>();
        }
    }
}
