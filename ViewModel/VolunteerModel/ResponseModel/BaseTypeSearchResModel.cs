using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace ViewModel.VolunteerModel.ResponseModel
{
    public class BaseTypeSearchResModel
    {
        public bool isSuccess;
        public List<BaseTypeSearchMiddle> List;
        public BaseViewModel baseViewModel;
        public int TotalNum;
        public BaseTypeSearchResModel()
        {
            baseViewModel = new BaseViewModel();
        }
    }
}
