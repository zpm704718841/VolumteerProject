using Dtol.dtol;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;

namespace Dto.IService.IntellVolunteer
{
    public interface IVAttachmentService
    {
        int VAttachment_Add(VAttachmentAddViewModel  AddViewModel);
    }
}
