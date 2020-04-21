using AutoMapper;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace Dto.Service.AutoMapper.VolunteerMapper.VolunteerResponseMapper
{
    public class Volunteer_Relate_TypeMapper: Profile
    {
        public Volunteer_Relate_TypeMapper()
        {
            CreateMap<Volunteer_Relate_TypeMiddle,Volunteer_Relate_Type>();
            CreateMap<Volunteer_Relate_Type,Volunteer_Relate_TypeMiddle>();
        }
    }
}
