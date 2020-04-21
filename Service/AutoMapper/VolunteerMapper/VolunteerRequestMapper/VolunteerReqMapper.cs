using AutoMapper;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerBackground.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;

namespace Dto.Service.AutoMapper.VolunteerMapper.VolunteerRequestMapper
{
   public  class VolunteerReqMapper : Profile
    {
        public VolunteerReqMapper()
        {
            CreateMap<VolunteerAddViewModel, Volunteer_Info>();
            CreateMap<Volunteer_Info, VolunteerAddViewModel>();
            CreateMap<Volunteer_Info, VolunteerSearchMiddle>();
            CreateMap<Volunteer_Info, Volunteer_Info>();
            CreateMap<Volunteer_Info, VolunteerInfoSearchViewModel>();
        }
    }
}
