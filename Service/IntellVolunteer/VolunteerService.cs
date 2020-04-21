using AutoMapper;
using Dto.IRepository.IntellVolunteer;
using Dto.IService.IntellVolunteer;
using Dtol.dtol;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ViewModel.VolunteerBackground.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerBackground.MiddleModel;
using ViewModel.VolunteerBackground.ResponseModel;
using ViewModel.PublicViewModel;


namespace Dto.Service.IntellVolunteer
{
    public class VolunteerService : IVolunteerService
    {

        private readonly IVolunteerInfoRepository _IVolunteerInfoRepository;
        private readonly IVAttachmentRepository _IVAttachmentRepository;
        private readonly IVolunteer_Relate_TypeRepository _IVolunteer_Relate_TypeRepository;
        private readonly IMapper _IMapper;
 

        public VolunteerService(IVolunteerInfoRepository iuserInfoRepository,  IMapper mapper, IVolunteer_Relate_TypeRepository Relate_TypeRepository, IVAttachmentRepository AttachmentRepository)
        {
            _IVolunteerInfoRepository = iuserInfoRepository;
            _IVolunteer_Relate_TypeRepository = Relate_TypeRepository;
            _IVAttachmentRepository = AttachmentRepository;
            _IMapper = mapper;
        }

        //添加用户
        public int User_Add(VolunteerAddViewModel VuserAddViewModel)
        {
            //再次获取  志愿者编号以免提交时出现重复编号
            var vno = GetNewVNO();
            var user_Info = _IMapper.Map<VolunteerAddViewModel, Volunteer_Info>(VuserAddViewModel);
            user_Info.VNO = vno;
            user_Info.Status = "0";
            //保存基本信息
            _IVolunteerInfoRepository.Add(user_Info);
            int a = _IVolunteerInfoRepository.SaveChanges();

            //保存完善信息（擅长技能、服务领域）
            List<Volunteer_Relate_TypeMiddle> Relate_Types = VuserAddViewModel.RelateUserIDandTypeIDList;
            var TypeInfo = _IMapper.Map<List<Volunteer_Relate_TypeMiddle>, List<Volunteer_Relate_Type>>(Relate_Types);
          
            foreach (var itme in TypeInfo)
            {
                _IVolunteer_Relate_TypeRepository.Add(itme);
                int b = _IVolunteer_Relate_TypeRepository.SaveChanges();
            }

            //保存 资质文件信息
            List<VAttachmentAddViewModel> VAttachmentAdds = VuserAddViewModel.VAttachmentAddList;
            var AttachmentInfo = _IMapper.Map<List<VAttachmentAddViewModel>, List<VAttachment>>(VAttachmentAdds);

            foreach (var item in AttachmentInfo)
            {
                item.ID = Guid.NewGuid().ToString();
                item.formid = VuserAddViewModel.ID;
                item.type = "ZZFJ";
                item.Status = "0";
                item.CreateUser = VuserAddViewModel.ID;
                item.CreateDate = DateTime.Now;
                _IVAttachmentRepository.Add(item);
                int c = _IVAttachmentRepository.SaveChanges();
            }

            return a;
        }
       
        //查询用户
        public List<VolunteerSearchMiddle> Volunteer_Search(VolunteerSearchViewModel VSearchViewModel)
        {
            List<Volunteer_Info> user_Infos = _IVolunteerInfoRepository.SearchInfoByWhere(VSearchViewModel);
            List<VolunteerSearchMiddle> userSearches = new List<VolunteerSearchMiddle>();

            foreach (var item in user_Infos)
            {
                var UserSearchMiddlecs = _IMapper.Map<Volunteer_Info, VolunteerSearchMiddle>(item);
                userSearches.Add(UserSearchMiddlecs);
            }
            return userSearches.Count > 0 ? userSearches : null;
        }


        public List<VolunteerSearchMiddle> Volunteer_SearchForBG(VolunteerInfoSearchViewModel VSearchViewModel)
        {
            List<Volunteer_Info> user_Infos = _IVolunteerInfoRepository.SearchInfoByWhereForBackGround(VSearchViewModel);

            List<VolunteerSearchMiddle> userSearches = new List<VolunteerSearchMiddle>();

            foreach (var item in user_Infos)
            {
                var UserSearchMiddlecs = _IMapper.Map<Volunteer_Info, VolunteerSearchMiddle>(item);
                userSearches.Add(UserSearchMiddlecs);

            }
            return userSearches.Count > 0 ? userSearches : null;
        }

        public int GetAllCount(VolunteerInfoSearchViewModel searchModel)
        {
            int result = _IVolunteerInfoRepository.GetVolunteerAll(searchModel).Count();
            return result;
        }

        public string GetNewVNO()
        {
            string vno = _IVolunteerInfoRepository.GetMaxVNO();

            if (vno == "" || vno == null)
            {
                vno = "BHTEDA00000001";
            }
            else
            {
                string temNO = vno.Substring(6, 8);
                int tNO = int.Parse(temNO.TrimStart('0'));
                string no = (tNO + 1).ToString();

                switch (no.Length)
                {
                    case 1:
                        no = "0000000" + no;
                        break;
                    case 2:
                        no = "000000" + no;
                        break;
                    case 3:
                        no = "00000" + no;
                        break;
                    case 4:
                        no = "0000" + no;
                        break;
                    case 5:
                        no = "000" + no;
                        break;
                    case 6:
                        no = "00" + no;
                        break;
                    case 7:
                        no = "0" + no;
                        break;
                    default:
                        break;
                }

                vno = vno.Substring(0, 6) + no;
            }


            return vno;
        }

        //根据志愿者ID 获取志愿者信息
        public VolunteerSearchMiddle SearchMiddle(string id)
        {
            Volunteer_Info user_Infos = _IVolunteerInfoRepository.SearchInfoByID(id);
            VolunteerSearchMiddle userSearches = _IMapper.Map<Volunteer_Info, VolunteerSearchMiddle>(user_Infos);
            return userSearches;
        }


        public int ChangeVolunteer(VolunteerInfoUpdateViewModel updateViewModel)
        {
            _IVolunteerInfoRepository.UpdateByModel(updateViewModel);
            return _IVolunteerInfoRepository.SaveChanges();
        }


        private void AddPrimission(List<UserDepartSearchMidModel> list, UserDepartSearchMidModel curPrimission)
        {
            List<UserDepartSearchMidModel> primission = list.Where(p => p.ParentId == curPrimission.Code.ToString()).ToList();
            curPrimission.DepartChildren = primission;
            foreach (var p in primission)
            {
                AddPrimission(list, p);
            }
        }

        public List<UserDepartSearchMidModel> GetDepartList()
        {
            UserDepartResModel returnDepart = new UserDepartResModel();

            List<User_Depart> departAll = _IVolunteerInfoRepository.GetDepartAll();

            var departList = _IMapper.Map<List<User_Depart>, List<UserDepartSearchMidModel>>(departAll);

            List<UserDepartSearchMidModel> result = new List<UserDepartSearchMidModel>();
            result.AddRange(departList.Where(p => p.ParentId == "0").ToList());
            foreach (var el in result)
            {
                AddPrimission(departList, el);
            }

            return result;
        }

        //验证是否注册用户
        public string CheckInfos(string ID)
        {
            string result = string.Empty;
            if (!String.IsNullOrEmpty(ID))
            {
                var res = _IVolunteerInfoRepository.SearchInfoByID(ID);
                if (res != null && res.ID != null)
                {
                    result = "true";
                }
                else
                {
                    result = "false";
                }
            }

            return result;
        }


        public BaseViewModel CheckInfosNew(string ID)
        {
            BaseViewModel result = new BaseViewModel();
            if (!String.IsNullOrEmpty(ID))
            {
                var res = _IVolunteerInfoRepository.SearchInfoByID(ID);
                if (res != null)
                {
                    if (res.Status.Equals("0"))
                    {
                        result.ResponseCode = 1;
                        result.Message = "审核中";
                    }
                    else if (res.Status.Equals("1"))
                    {
                        result.ResponseCode = 2;
                        result.Message = "审核通过";
                    }
                    else if (res.Status.Equals("2"))
                    {
                        result.ResponseCode = 3;
                        result.Message = "审核不通过";
                    }
             
                }
                else
                {
                    result.ResponseCode = 0;
                    result.Message = "未注册";

                }
            }

            return result;
        }




        public VolunteerAddViewModel GetMyInfos(SearchByVIDModel vidModel)
        {
            VolunteerAddViewModel model = new VolunteerAddViewModel();

            Volunteer_Info info = _IVolunteerInfoRepository.SearchInfoByID(vidModel.VID);

            if (info != null && info.ID !=null)
            {
                model = _IMapper.Map<Volunteer_Info, VolunteerAddViewModel>(info);

                List<Volunteer_Relate_Type> Relate_Types = _IVolunteer_Relate_TypeRepository.GetMyTypeList(vidModel.VID);
                model.RelateUserIDandTypeIDList = _IMapper.Map<List<Volunteer_Relate_Type>, List<Volunteer_Relate_TypeMiddle>>(Relate_Types);

                List<VAttachment> VAttachmentList = _IVAttachmentRepository.GetMyList(vidModel.VID);
                model.VAttachmentAddList = _IMapper.Map<List<VAttachment>, List<VAttachmentAddViewModel>>(VAttachmentList);

            }


            return model;
        }



        public int User_Edit(VolunteerAddViewModel VuserAddViewModel)
        {
            var user_Info = _IMapper.Map<VolunteerAddViewModel, Volunteer_Info>(VuserAddViewModel);
            _IVolunteerInfoRepository.EditInfo(user_Info);
            int a = _IVolunteerInfoRepository.SaveChanges();

            //保存完善信息（擅长技能、服务领域）   先删除原有信息在添加
            _IVolunteer_Relate_TypeRepository.RemoveAll(user_Info.ID);

            List<Volunteer_Relate_TypeMiddle> Relate_Types = VuserAddViewModel.RelateUserIDandTypeIDList;
            var TypeInfo = _IMapper.Map<List<Volunteer_Relate_TypeMiddle>, List<Volunteer_Relate_Type>>(Relate_Types);

            foreach (var itme in TypeInfo)
            {
                _IVolunteer_Relate_TypeRepository.Add(itme);
                int b = _IVolunteer_Relate_TypeRepository.SaveChanges();
            }


            //保存 资质文件信息 先删除原有信息在添加

            _IVAttachmentRepository.RemoveAll(user_Info.ID,"ZZFJ");
            List<VAttachmentAddViewModel> VAttachmentAdds = VuserAddViewModel.VAttachmentAddList;
            var AttachmentInfo = _IMapper.Map<List<VAttachmentAddViewModel>, List<VAttachment>>(VAttachmentAdds);

            foreach (var item in AttachmentInfo)
            {
                item.ID = Guid.NewGuid().ToString();
                item.formid = VuserAddViewModel.ID;
                item.type = "ZZFJ";
                item.Status = "0";
                item.CreateUser = VuserAddViewModel.ID;
                item.CreateDate = DateTime.Now;
                _IVAttachmentRepository.Add(item);
                int c = _IVAttachmentRepository.SaveChanges();
            }

            return a;
        }

    }
}
