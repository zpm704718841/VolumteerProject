using AutoMapper;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;

namespace Dto.Service.AutoMapper.VolunteerMapper.VolunteerRequestMapper
{
    public class VA_HandleReqMapper : Profile
    {
        public VA_HandleReqMapper()
        {
            CreateMap<VA_HandleAddViewModel, VA_Handle>();
            CreateMap<VA_HandleImgAddModel, VA_Handle>();

        }
    }
}
