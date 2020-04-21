using AutoMapper;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;

namespace Dto.Service.AutoMapper.VolunteerMapper.VolunteerRequestMapper
{
    public class VA_SignReqMapper : Profile
    {
        public VA_SignReqMapper()
        {
            CreateMap<VA_SignAddViewModel, VA_Sign>();
      
        }
    }
}
