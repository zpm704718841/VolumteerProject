using AutoMapper;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;
namespace Dto.Service.AutoMapper.VolunteerMapper.VolunteerResponseMapper
{
    public class VAttachmentAddResMapper : Profile
    {
        public VAttachmentAddResMapper()
        {
            CreateMap<VAttachmentAddViewModel, VAttachment>();
            CreateMap<VAttachment, VAttachmentAddViewModel>();
        }
    }
}
