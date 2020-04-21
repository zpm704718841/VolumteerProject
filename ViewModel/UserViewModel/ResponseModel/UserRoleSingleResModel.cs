﻿using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.PublicViewModel;

namespace ViewModel.UserViewModel.ResponseModel
{
    public class UserRoleSingleResModel
    {
        public bool IsSuccess;
        public BaseViewModel baseViewModel;
        public UserRoleSingleResModel()
        {
            baseViewModel = new BaseViewModel();
        }
    }
}
