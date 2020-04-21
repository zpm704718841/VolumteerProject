using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.VolunteerModel.MiddleModel
{
    public class BaseTypeSearchMiddle
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string ParentCode { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public int Sort { get; set; }
        public string Memo { get; set; }

        public List<BaseTypeSearchMiddle> TypeChildren { get; set; }
    }
}
