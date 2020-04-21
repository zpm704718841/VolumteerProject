using AutoMapper;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;
namespace Dto.Service.AutoMapper.VolunteerMapper.VolunteerRequestMapper
{
    public class VHA_SignReqMapper: Profile
    {
        public VHA_SignReqMapper()
        {
            CreateMap<VHA_SignAddViewModel, VHA_Sign>();
            CreateMap<VHA_Sign,VHA_SignSearchMiddle> ();

        }
    }
}
