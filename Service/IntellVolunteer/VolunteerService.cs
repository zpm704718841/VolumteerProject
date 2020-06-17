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
using Serilog;
using SystemFilter.PublicFilter;


namespace Dto.Service.IntellVolunteer
{
    public class VolunteerService : IVolunteerService
    {

        private readonly IVolunteerInfoRepository _IVolunteerInfoRepository;
        private readonly IVAttachmentRepository _IVAttachmentRepository;
        private readonly IVolunteer_Relate_TypeRepository _IVolunteer_Relate_TypeRepository;
        private readonly IMapper _IMapper;
        private readonly IUserLogin_LogRepository _userLogin_Log;
        private readonly ILoginType_LogRepository _loginType_Log;
        private readonly ILoginTypeRepository _loginTypeRepository;
        //private readonly ILogger _ILogger;


        public VolunteerService(IVolunteerInfoRepository iuserInfoRepository,  IMapper mapper, IVolunteer_Relate_TypeRepository Relate_TypeRepository,
            IVAttachmentRepository AttachmentRepository, IUserLogin_LogRepository userLogin_Log, ILoginType_LogRepository loginType_Log,
            ILoginTypeRepository loginType )
        {
            _IVolunteerInfoRepository = iuserInfoRepository;
            _IVolunteer_Relate_TypeRepository = Relate_TypeRepository;
            _IVAttachmentRepository = AttachmentRepository;
            _IMapper = mapper;
            _userLogin_Log = userLogin_Log;
            _loginType_Log = loginType_Log;
            _loginTypeRepository = loginType;
            //_ILogger = logger;
        }

        //添加用户
        public int User_Add(VolunteerAddViewModel VuserAddViewModel)
        {

            //判断  （擅长技能、服务领域） 是否为空 20200602
            if (VuserAddViewModel.RelateUserIDandTypeIDList.Count == 0)
            {
                return 0;
            }
 
            DEncrypt encrypt = new DEncrypt();
            //再次获取  志愿者编号以免提交时出现重复编号
            var vno = GetNewVNO();
            var user_Info = _IMapper.Map<VolunteerAddViewModel, Volunteer_Info>(VuserAddViewModel);
            user_Info.VNO = vno;
            user_Info.Status = "0";
            // 字段加密 20200521
            user_Info.Name = encrypt.Encrypt(user_Info.Name);
            user_Info.CertificateID = encrypt.Encrypt(user_Info.CertificateID);
            user_Info.Mobile = encrypt.Encrypt(user_Info.Mobile);

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

            //用户注册后 默认登录成功  20200511
            //保存登录时间 20200511
            SaveLoginInfo(VuserAddViewModel.ID);
            //保存登录方式 20200511
            SaveLoginTypeInfo(VuserAddViewModel.ID);


            return a;
        }




        //记录 用户登录 时间（参数：uid）
        public BaseViewModel SaveLoginInfo(string uid)
        {
            BaseViewModel baseView = new BaseViewModel();
            if (uid == "")
            {
                baseView.Message = "参数为空";
                baseView.ResponseCode = 2;
            }
            else
            {
                try
                {
                    UserLogin_Log userLogin = new UserLogin_Log();
                    userLogin.ID = Guid.NewGuid().ToString();
                    userLogin.uid = uid;
                    userLogin.Action = "登录系统";
                    userLogin.status = "true";
                    userLogin.CreateDate = DateTime.Now;
                    _userLogin_Log.Add(userLogin);
                    int a = _userLogin_Log.SaveChanges();
                    if (a > 0)
                    {
                        baseView.Message = "保存成功";
                        baseView.ResponseCode = 0;
                    }
                    else
                    {
                        baseView.Message = "保存失败";
                        baseView.ResponseCode = 1;
                    }
                }
                catch (Exception ex)
                {
                    baseView.Message = "出现异常";
                    baseView.ResponseCode = 3;
                    //_ILogger.Information("记录用户登录时间出现异常" + ex.Message + ex.StackTrace + ex.Source);
                }
            }
            return baseView;
        }


        //记录 用户选择登录方式（参数：uid）(默认人脸识别登录)
        public BaseViewModel SaveLoginTypeInfo(string uid)
        {
            BaseViewModel baseView = new BaseViewModel();
            if (uid == "")
            {
                baseView.Message = "参数为空";
                baseView.ResponseCode = 2;
            }
            else
            {
                try
                {
                    //获取人脸登录方式  默认该用户选用人脸识别登录
                    LoginType LoginType = _loginTypeRepository.SearchFaceModel();

                    LoginType_Log log = new LoginType_Log();
                    log.ID = Guid.NewGuid().ToString();
                    log.uid = uid;
                    log.typeid = LoginType.ID;
                    log.status = "true";
                    log.CreateDate = DateTime.Now;
                    _loginType_Log.Add(log);
                    int a = _loginType_Log.SaveChanges();
                    if (a > 0)
                    {
                        baseView.Message = "保存成功";
                        baseView.ResponseCode = 0;
                    }
                    else
                    {
                        baseView.Message = "保存失败";
                        baseView.ResponseCode = 1;
                    }
                }
                catch (Exception ex)
                {
                    baseView.Message = "出现异常";
                    baseView.ResponseCode = 3;
                    //_ILogger.Information("记录用户选择登录方式出现异常" + ex.Message + ex.StackTrace + ex.Source);
                }
            }
            return baseView;
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
            DEncrypt encrypt = new DEncrypt();
            VolunteerAddViewModel model = new VolunteerAddViewModel();

            Volunteer_Info info = _IVolunteerInfoRepository.SearchInfoByID(vidModel.VID);

           

            if (info != null && info.ID !=null)
            {
                model = _IMapper.Map<Volunteer_Info, VolunteerAddViewModel>(info);

                List<Volunteer_Relate_Type> Relate_Types = _IVolunteer_Relate_TypeRepository.GetMyTypeList(vidModel.VID);
                model.RelateUserIDandTypeIDList = _IMapper.Map<List<Volunteer_Relate_Type>, List<Volunteer_Relate_TypeMiddle>>(Relate_Types);

                List<VAttachment> VAttachmentList = _IVAttachmentRepository.GetMyList(vidModel.VID);
                model.VAttachmentAddList = _IMapper.Map<List<VAttachment>, List<VAttachmentAddViewModel>>(VAttachmentList);

                model.Name = encrypt.Decrypt(model.Name);
                model.CertificateID = encrypt.Decrypt(model.CertificateID);
                model.Mobile = encrypt.Decrypt(model.Mobile);
            }

            return model;
        }



        public int User_Edit(VolunteerAddViewModel VuserAddViewModel)
        {
            DEncrypt encrypt = new DEncrypt();
            var user_Info = _IMapper.Map<VolunteerAddViewModel, Volunteer_Info>(VuserAddViewModel);

            // 字段加密 20200521
            user_Info.Name = encrypt.Encrypt(user_Info.Name);
            user_Info.CertificateID = encrypt.Encrypt(user_Info.CertificateID);
            user_Info.Mobile = encrypt.Encrypt(user_Info.Mobile);

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
