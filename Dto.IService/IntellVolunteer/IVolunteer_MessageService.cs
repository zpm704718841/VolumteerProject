using Dtol.dtol;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;

namespace Dto.IService.IntellVolunteer
{
    public interface IVolunteer_MessageService
    {
        BaseViewModel AddMessages(Volunteer_MessageMiddle model);
        BaseViewModel UpdateMessagesStatus(MessageIDandVIDModel model);
        List<Volunteer_Message> GetMyMessages(SearchByVIDModel model);

        BaseViewModel GetMyUnReadMessageNum(SearchByVIDModel model);
    }
}
