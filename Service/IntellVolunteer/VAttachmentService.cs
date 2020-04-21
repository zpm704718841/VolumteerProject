using AutoMapper;
using Dto.IRepository.IntellVolunteer;
using Dto.IService.IntellVolunteer;
using Dtol.dtol;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;

namespace Dto.Service.IntellVolunteer
{
    public class VAttachmentService : IVAttachmentService
    {
        private readonly IVAttachmentRepository _IVAttachmentRepository;
        private readonly IMapper _IMapper;


        public VAttachmentService(IVAttachmentRepository itypeRepository, IMapper mapper)
        {
            _IVAttachmentRepository = itypeRepository;
            _IMapper = mapper;
        }

        //添加用户
        public int VAttachment_Add(VAttachmentAddViewModel AddViewModel)
        {
            var Info = _IMapper.Map<VAttachmentAddViewModel, VAttachment>(AddViewModel);
            _IVAttachmentRepository.Add(Info);
            return _IVAttachmentRepository.SaveChanges();
        }

    }
}
