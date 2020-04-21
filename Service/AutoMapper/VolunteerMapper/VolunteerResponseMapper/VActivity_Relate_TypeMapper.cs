using AutoMapper;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;
namespace Dto.Service.AutoMapper.VolunteerMapper.VolunteerResponseMapper
{
    public class VActivity_Relate_TypeMapper : Profile
    {
        public VActivity_Relate_TypeMapper()
        {
            CreateMap<VActivity_Relate_Type, VActivity_Relate_TypeMiddle>();

        }
    }
}
