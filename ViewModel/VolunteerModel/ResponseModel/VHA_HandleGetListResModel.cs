using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace ViewModel.VolunteerModel.ResponseModel
{
    public class VHA_HandleGetListResModel
    {
        public bool isSuccess;
        public VHA_SignInfoMiddle middle;
        public List<VHA_HandleGetMyResModel> List;
        public BaseViewModel baseViewModel;
        public int TotalNum;
        public DateTime Signtime;
        public VHA_HandleGetListResModel()
        {
            baseViewModel = new BaseViewModel();
        }
    }
}
