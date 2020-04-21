using AutoMapper;
using Dtol.WGWdtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.WeChatViewModel.ResponseModel;

namespace Dto.Service.AutoMapper.WeChatMapper.WeChatResponseMapper
{
    public class WeChatUserInfoResMapper : Profile
    {
        public WeChatUserInfoResMapper()
        {
            CreateMap<UserInfo, WeChatWGWUserResModel>();
        }
    }
}
