using AutoMapper;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerBackground.MiddleModel;
using ViewModel.VolunteerBackground.RequsetModel;

namespace Dto.Service.AutoMapper.VolunteerActivityMapper.ActivityMapperReqestMapper
{
    public class ActivityReqMapper : Profile
    {
        public ActivityReqMapper()
        {
            CreateMap<VolunteerActivityViewModel,VolunteerActivity > ();
            CreateMap<VolunteerActivityMidModel, VolunteerActivity>();
            CreateMap<VolunteerActivity, VolunteerActivityMidModel>();
            CreateMap<User_Depart, UserDepartSearchMidModel>();
        }

    }
}
