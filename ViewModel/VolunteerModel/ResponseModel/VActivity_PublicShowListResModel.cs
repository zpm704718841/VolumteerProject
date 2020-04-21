using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;

namespace ViewModel.VolunteerModel.ResponseModel
{
    public class VActivity_PublicShowListResModel
    {
        public bool isSuccess;
        public List<VActivity_PublicShowMiddle>  List;
        public BaseViewModel baseViewModel;
        public int TotalNum;
        public VActivity_PublicShowListResModel()
        {
            baseViewModel = new BaseViewModel();
        }
    }
}
