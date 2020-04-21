using AutoMapper;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;

namespace Dto.Service.AutoMapper.VolunteerMapper.VolunteerRequestMapper
{
    public class VHelpAreaReqMapper : Profile
    {
        public VHelpAreaReqMapper()
        {
            CreateMap<VHelpArea, VHelpAreaSearchMiddle>();
            CreateMap<VHelpArea, VHelpArea>();
            CreateMap<VHelpAreaSearchMiddle, VHelpArea>();
        }
    }
}
