using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace Dto.IRepository.IntellVolunteer
{
    public interface IVAttachmentRepository : IRepository<VAttachment>
    {
        List<VAttachment> GetMyList(string formid);

        void RemoveAll(string formid, string type);
    }
}
