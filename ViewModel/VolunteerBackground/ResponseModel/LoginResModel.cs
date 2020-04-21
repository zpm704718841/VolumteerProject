using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerBackground.MiddleModel;

namespace ViewModel.VolunteerBackground.ResponseModel
{
    public class LoginResModel
    {
        public bool IsSuccess;
        public LoginMidModel user_session;
        public BaseViewModel baseViewModel;

        /// <summary>
        /// token
        /// </summary>
        public TokenViewModel tokenViewModel { get; set; }
        public string token;

        public LoginResModel()
        {
            //tokenViewModel = new TokenViewModel();
            baseViewModel = new BaseViewModel();
        }
    }
}
