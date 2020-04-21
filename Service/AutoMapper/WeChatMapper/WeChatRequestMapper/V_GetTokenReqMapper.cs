using AutoMapper;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.WeChatViewModel.MiddleModel;
using ViewModel.WeChatViewModel.RequestViewModel;


namespace Dto.Service.AutoMapper.WeChatMapper.WeChatRequestMapper
{
    public class V_GetTokenReqMapper : Profile
    {
        public V_GetTokenReqMapper()
        {
            CreateMap<V_GetTokenAddModel, V_GetToken>();
            CreateMap<V_GetToken, V_GetToken>();
        }
    }
}
