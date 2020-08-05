using Dtol.dtol;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerBackground.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerBackground.MiddleModel;

namespace Dto.IService.IntellVolunteer
{
    public interface IFileUploadService
    {
     

        string PostFile(IFormFile formFile);

        string PostFileNew(IFormFile formFile,string TypeID);
    }
}
