using AutoMapper;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel;
using ViewModel.VolunteerModel.RequsetModel.NormalViewModel;

namespace Dto.Service.AutoMapper.Normalization
{
    public class NormalizationMapper : Profile
    {
        public NormalizationMapper()
        {
            CreateMap<Normalization_Info, NornalOndutySearchMiddlecs>();

            CreateMap<OndutyClaims_Info, ContainOnDutyMiddleMiddle>();

            CreateMap<MydutyClaim_Info, ContainOnDutyMiddleMiddle>();

            CreateMap<Normalization_Info, NornalContainDutyMiddle>()
                
                .ForMember(dto => dto.containOnDutyMiddleMiddles, (map) => map.MapFrom(m => m.ondutyClaims_Infos))
  
                
                ;

            CreateMap<Normalization_Info, NornalContainDutyMiddle>()
               .ForMember(dto => dto.containOnDutyMiddleMiddles, (map) => map.MapFrom(m => m.ondutyClaims_Infos));



            CreateMap<MydutyClaim_Info, MydutyClaimInfoSearchMiddleModel>()
                .ForMember(dto => dto.title, (map) => map.MapFrom(m => m.OndutyClaims_Info.Normalization_Info.title))
                .ForMember(dto => dto.XiaoCommunityName, (map) => map.MapFrom(m => m.OndutyClaims_Info.Normalization_Info.XiaoCommunityName))
                ;



            CreateMap<MydutyClaimInfoUpdateViewModel, MydutyClaim_Info>();


            CreateMap<MydutyClaimInfoAddViewModel, MydutyClaim_Info>();




            CreateMap<MydutyClaim_Info, MydutyClaim_Info>();

        }
    }
}
