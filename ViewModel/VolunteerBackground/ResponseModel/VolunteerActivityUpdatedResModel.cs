using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;

namespace ViewModel.VolunteerBackground.ResponseModel
{
   public partial class VolunteerActivityUpdatedResModel
    {
        public bool IsSuccess;
        public int ResultCount;
        public BaseViewModel baseViewModel;
        public VolunteerActivityUpdatedResModel()
        {
            baseViewModel = new BaseViewModel();
        }
    }
}
