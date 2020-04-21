using AutoMapper;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
namespace Dto.Service.AutoMapper.VolunteerMapper.VolunteerResponseMapper
{
    public class VolunteerAddResMapper : Profile
    {
        public VolunteerAddResMapper()
        {
            CreateMap<VolunteerAddViewModel, Volunteer_Info>();

        }
    }
}
