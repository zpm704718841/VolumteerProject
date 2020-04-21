using AutoMapper;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.ResponseModel;

namespace Dto.Service.AutoMapper.VolunteerMapper.VolunteerRequestMapper
{
    public class VActivity_PublicShowReqMapper : Profile
    {
        public VActivity_PublicShowReqMapper()
        {
            CreateMap<VActivity_PublicShowAddModel, VActivity_PublicShow>();
            CreateMap<VActivity_PublicShow, VActivity_PublicShowAddModel>();
            CreateMap<VActivity_PublicShow, VActivity_PublicShowMiddle>();
            CreateMap<VActivity_PublicShowMiddle, VActivity_PublicShow>();
            CreateMap<VActivity_PublicShow_GiveLike, VActivity_PublicShow_GiveLikeMiddle>();
        }
    }
}
