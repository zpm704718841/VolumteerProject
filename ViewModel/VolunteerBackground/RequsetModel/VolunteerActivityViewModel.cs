using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel.VolunteerBackground.RequsetModel
{
    public partial class VolunteerActivityViewModel
    {
        public string ID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string TypeIDs { get; set; }
        public string CommunityID { get; set; }
        public string Community { get; set; }
        public string Page { get; set; }
        public DateTime? Stime { get; set; }
        public DateTime? Etime { get; set; }
        public string Contact { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string Note { get; set; }
        public string Score { get; set; }
        public DateTime? SignStime { get; set; }
        public DateTime? SignEtime { get; set; }
        public DateTime? SignUpStime { get; set; }
        public DateTime? SignUpEtime { get; set; }
        public DateTime? SignOutStime { get; set; }
        public DateTime? SignOutEtime { get; set; }
        public string CreateUser { get; set; }
        public DateTime? CreateDate { get; set; }
        public string UpdateUser { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
