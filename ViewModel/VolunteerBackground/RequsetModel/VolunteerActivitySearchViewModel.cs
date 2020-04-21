using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;

namespace ViewModel.VolunteerBackground.RequsetModel
{
    public class VolunteerActivitySearchViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string TypeIDs { get; set; }
        public string CommunityID { get; set; }
        public PageViewModel pageViewModel { get; set; }

        public VolunteerActivitySearchViewModel()
        {
            pageViewModel = new PageViewModel();
        }
    }
}
