using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;

namespace ViewModel.VolunteerModel.RequsetModel
{
    public class VolunteerSearchViewModel
    {
        /// <summary>
        /// 志愿者编号
        /// </summary>
        public string VNO { get; set; }
        /// <summary>
        /// 志愿者姓名
        /// </summary>
        public string Name { get; set; }
     
        /// <summary>
        /// 证件编号
        /// </summary>
        public string CertificateID { get; set; }
     
        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobile { get; set; }
      

    }
}
