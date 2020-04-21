using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace ViewModel.VolunteerModel.ResponseModel
{
    public class VHelpAreaSearchResModel
    {
        public bool isSuccess;
        public List<VHelpAreaSearchMiddle> VHelpArea;
        public BaseViewModel baseViewModel;
        public int TotalNum;
        public VHelpAreaSearchResModel()
        {
            baseViewModel = new BaseViewModel();
        }
    }
}
