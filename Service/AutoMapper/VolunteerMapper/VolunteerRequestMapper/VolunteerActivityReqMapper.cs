using AutoMapper;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;

namespace Dto.Service.AutoMapper.VolunteerMapper.VolunteerRequestMapper
{
    public class VolunteerActivityReqMapper : Profile
    {
        public VolunteerActivityReqMapper()
        {
            CreateMap<VolunteerActivity, VolunteerActivitySearchMiddle>();
            CreateMap<VolunteerActivity, VolunteerActivity>();
        }
    }
}
