using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerBackground.MiddleModel;

namespace ViewModel.VolunteerBackground.ResponseModel
{
    public class VolunteerActivitySearchResModel
    {
        public bool IsSuccess;
        public BaseViewModel baseViewModel;
        public List<VolunteerActivityMidModel> Activity;
        public int TotalNum;
        public VolunteerActivitySearchResModel()
        {
            baseViewModel = new BaseViewModel();
        }
    }
}
