using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.VolunteerBackground.MiddleModel
{
    public class UserDepartSearchMidModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string Code { get; set; }
        public string Remark { get; set; }
        public int? Sort { get; set; }

        public List<UserDepartSearchMidModel> DepartChildren { get; set; }
    }
}
