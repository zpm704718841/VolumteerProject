using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerBackground.MiddleModel;
using ViewModel.VolunteerBackground.RequsetModel;

namespace Dto.IService.IntellVolunteerBackground
{
    public interface IVolunteerActivityService
    {
        int Add(VolunteerActivityViewModel activityAddModel);
        int Update(VolunteerActivityViewModel activityAddModel);
        int Delete(Guid id);
        List<VolunteerActivityMidModel> SearchList(VolunteerActivitySearchViewModel searchViewModel);
        bool ActivityDistinct(VolunteerActivityViewModel model);
        bool ActivityDistinctForUpdate(VolunteerActivityViewModel model);
        int GetAllCount(VolunteerActivitySearchViewModel searchModel);
        List<UserDepartSearchMidModel> GetDepartList();
        //int DeleteByID(string id);
        int DeleteByID(VolunteerActivityDeleteViewModel viewModel);
    }
}
