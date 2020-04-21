using AutoMapper;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;
namespace Dto.Service.AutoMapper.VolunteerMapper.VolunteerRequestMapper
{
    public class VHA_HandleReqMapper: Profile
    {
        public VHA_HandleReqMapper()
        {
            CreateMap<VHA_HandleAddModel, VHA_Handle>();
        }
    }
}
