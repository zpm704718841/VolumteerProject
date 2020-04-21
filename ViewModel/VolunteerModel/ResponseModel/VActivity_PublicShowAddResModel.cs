using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;


namespace ViewModel.VolunteerModel.ResponseModel
{
    public class VActivity_PublicShowAddResModel
    {
        public VActivity_PublicShowAddResModel()
        {
            baseViewModel = new BaseViewModel();

        }

        public bool IsSuccess;
        public int AddCount;

        public BaseViewModel baseViewModel;
    }
}
