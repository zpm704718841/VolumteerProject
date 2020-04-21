using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
namespace ViewModel.VolunteerModel.ResponseModel
{
    public class VA_SignAddResModel
    {
        public VA_SignAddResModel()
        {
            baseViewModel = new BaseViewModel();

        }

        public bool IsSuccess;
        public int AddCount;

        public BaseViewModel baseViewModel;
    }
}
