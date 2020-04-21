using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;

namespace ViewModel.VolunteerBackground.RequsetModel
{
    public partial class VolunteerInfoSearchViewModel
    {
        public string VNO { get; set; }
        public string Name { get; set; }
        public string CommunityID { get; set; }
        public string Gender { get; set; }
        public string Political { get; set; }

        public PageViewModel pageViewModel { get; set; }

        public VolunteerInfoSearchViewModel()
        {
            pageViewModel = new PageViewModel();
        }
    }
}
