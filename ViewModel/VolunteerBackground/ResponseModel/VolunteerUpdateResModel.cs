using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;

namespace ViewModel.VolunteerBackground.ResponseModel
{
    public class VolunteerUpdateResModel
    {
        public bool IsSuccess;
        public int ResultCount;
        public BaseViewModel baseViewModel;
        public VolunteerUpdateResModel()
        {
            baseViewModel = new BaseViewModel();
        }
    }
}
