using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;

namespace ViewModel.VolunteerBackground.ResponseModel
{
    public class VolunteerActivityAddResModel
    {
        public bool IsSuccess;
        public int ResultCount;
        public BaseViewModel baseViewModel;
        public VolunteerActivityAddResModel()
        {
            baseViewModel = new BaseViewModel();
        }

    }
}
