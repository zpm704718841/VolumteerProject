using AutoMapper;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace Dto.Service.AutoMapper.VolunteerMapper.VolunteerResponseMapper
{
    public class BaseTypeAddResMapper : Profile
    {
        public BaseTypeAddResMapper()
        {
            CreateMap<BaseTypeAddViewModel, VBaseType>();
            CreateMap<VBaseType, BaseTypeSearchMiddle>();
        }
    }
}
