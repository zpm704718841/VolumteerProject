using AutoMapper;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;

namespace Dto.Service.AutoMapper.VolunteerMapper.VolunteerRequestMapper
{
    public class BaseTypeReqMapper : Profile
    {
        public BaseTypeReqMapper()
        {
            CreateMap<BaseTypeAddViewModel, VBaseType>();
            CreateMap<VBaseType, VBaseType>();
        }
    }
}
