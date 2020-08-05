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
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.ResponseModel;
using ViewModel.PublicViewModel;
using Dtol.Easydtol;

namespace Dto.Service.IntellVolunteer
{
    public class VHelpAreaByVService : IVHelpAreaByVService
    {
        private readonly IVHelpAreaRepository _IVHelpAreaRepository;
        private readonly IVolunteerInfoRepository _IVolunteerInfoRepository;
        private readonly IVHA_SignRepository _IVHA_SignRepository;
        private readonly IVHA_HandleRepository _IVHA_HandleRepository;
        private readonly IVAttachmentRepository _IVAttachmentRepository;
        private readonly ISQLRepository _ISQLRepository;
        private readonly IAISQLRepository _IAISQLRepository;
        private readonly IVA_HandleRepository _IVA_HandleRepository;
        private readonly IMapper _IMapper;
        private readonly IVolunteer_ScoreRepository _IVolunteer_ScoreRepository;
        private readonly IVolunteer_MessageRepository _IVolunteer_MessageRepository;
        private readonly IET_pointsRepository eT_PointsRepository;
        public VHelpAreaByVService(IVHelpAreaRepository iInfoRepository, IVolunteerInfoRepository volunteerInfo, IVHA_SignRepository vha_SignRepository, 
            IVHA_HandleRepository handleRepository, IVAttachmentRepository vAttachment, ISQLRepository SQL, IVA_HandleRepository vA_Handle, 
            IMapper mapper, IAISQLRepository aisqlRepository, IVolunteer_ScoreRepository scoreRepository, IVolunteer_MessageRepository messageRepository,
            IET_pointsRepository pointsRepository )
        {
            _IVHelpAreaRepository = iInfoRepository;
            _IVolunteerInfoRepository = volunteerInfo;
            _IVHA_SignRepository = vha_SignRepository;
            _IVHA_HandleRepository = handleRepository;
            _IVAttachmentRepository = vAttachment;
            _ISQLRepository = SQL;
            _IAISQLRepository = aisqlRepository;
            _IVA_HandleRepository = vA_Handle;
            _IMapper = mapper;
            _IVolunteer_ScoreRepository = scoreRepository;
            _IVolunteer_MessageRepository = messageRepository;
            eT_PointsRepository = pointsRepository;
        }

        //社区居民上传互助信息（名称、内容、所需擅长技能、姓名、联系方式、详细地址、可得积分）
        public BaseViewModel AddHelpArea(VHelpAreaSearchMiddle area)
        {
            BaseViewModel result = new BaseViewModel();
            VHelpArea helpArea = _IMapper.Map<VHelpAreaSearchMiddle,VHelpArea>(area);
            helpArea.ID = Guid.NewGuid().ToString();
            helpArea.CreateDate = DateTime.Now;
            helpArea.UpdateDate = DateTime.Now;
            helpArea.Status = "9";
            _IVHelpAreaRepository.Add(helpArea);
            int a = _IVHelpAreaRepository.SaveChanges();
            if (a > 0)
            {
                //提示信息：您已发布标题为XXX互助信息，等待审核
                Volunteer_MessageMiddle middle = new Volunteer_MessageMiddle();
                middle.Contents = "您已上传标题为 " + area.Title + " 互助信息，等待审核";

                Volunteer_Info volunteer_Info = _IVolunteerInfoRepository.SearchInfoByID(area.CreateUser);
                middle.Name = volunteer_Info.Name;
                middle.VID = volunteer_Info.ID;
                middle.VNO = volunteer_Info.VNO;

                Volunteer_Message message = _IMapper.Map<Volunteer_MessageMiddle, Volunteer_Message>(middle);
                message.ID = Guid.NewGuid().ToString();
                message.CreateDate = DateTime.Now;
                message.CreateUser = area.CreateUser;
                message.Status = "0";

                _IVolunteer_MessageRepository.Add(message);
                int c = _IVolunteer_MessageRepository.SaveChanges();

                result.ResponseCode = 200;
                result.Message = "上传成功，等待审核";
            }
            else
            {
                result.ResponseCode = 400;
                result.Message = "上传失败，请重新操作";
            }
            return result;
        }


        //首页 发布互助入口（验证是否是区内用户，区外用户不能发布）
        public BaseViewModel CheckRights(SearchByVIDModel model)
        {
            BaseViewModel result = new BaseViewModel();
            var VolunteerInfo = _IVolunteerInfoRepository.SearchInfoByID(model.VID);
            //判断是否是 泰达街社区居民或机关单位工作人员 发布
            if (VolunteerInfo.CommunityID == "D6E82522-7D21-44EF-BC81-31CAC7DA19CE" && VolunteerInfo.UnitID == "")
            {
                result.Message = "您没有权限发布互助信息";
                result.ResponseCode = 400;
            }
            else
            {
                result.Message = "您可以发布互助信息";
                result.ResponseCode = 200;
            }
            return result;
        }


        //获取我发布的 互助信息   
        public List<VHelpAreaSearchMiddle> VHelpArea_MyAll(string VID, string status)
        {
            List<VHelpArea> Infos = _IVHelpAreaRepository.GetMyPublishInfo(VID, status);
            foreach (var itme in Infos)
            {
                var num = _IVHA_SignRepository.GetByContentID(itme.ID, "1");
                if (num == null)
                {
                    itme.bak1 = "0";
                }
                else
                {
                    //统计每条互助信息 的认领人数
                    itme.bak1 = num.Count.ToString();
                }
                
            }
            List<VHelpAreaSearchMiddle> Searches = new List<VHelpAreaSearchMiddle>();
            Searches = _IMapper.Map<List<VHelpArea>, List<VHelpAreaSearchMiddle>>(Infos);
            return Searches;
        }


        // 获取 查看该互助信息对应的认领人列表  
        public PublishHelpAreaResModel GetVList(string ContentID)
        {
            PublishHelpAreaResModel resModel = new PublishHelpAreaResModel();
            List<VHA_Sign> list = _IVHA_SignRepository.GetByContentID(ContentID,"1");
            foreach (var itme in list)
            {
                var num = (_IVolunteerInfoRepository.SearchInfoByID(itme.VID));
                // 获取认领人姓名
                itme.bak1 = num.Name;
            }

            resModel.vha_SignMiddle = _IMapper.Map<List<VHA_Sign>, List<VHA_SignSearchMiddle>>(list); 
            return resModel;

        }

        // 获取 查看该互助信息对应的认领人 上传结果列表 
        public VHA_Sign GetVHandleList(string ContentID,string status)
        {
            VHA_Sign resModel = new VHA_Sign();
            List<VHA_Sign> list = _IVHA_SignRepository.GetByContentID(ContentID, status);
 
            if (list.Count > 0)
            {
                resModel = list.First();
            }
            return resModel;

        }



        // 获取 查看该互助信息对应的认领人 的具体信息 
        public VHA_SignInfoMiddle GetVDetail(string VID)
        {
            VHA_SignInfoMiddle resModel = new VHA_SignInfoMiddle();
            var user_Infos = _IVolunteerInfoRepository.SearchInfoByID(VID);
            resModel.VID = user_Infos.ID;
            resModel.Name = user_Infos.Name;
            resModel.VNO = user_Infos.VNO;
            resModel.Community = user_Infos.Community;
            resModel.Mobile = user_Infos.Mobile;
            resModel.RegisteTime = user_Infos.CreateDate.ToString();

  

            //显示 服务领域
            string Services = _ISQLRepository.GetVServices(VID);
            resModel.Services= Services.Substring(0, (Services.Length - 1));

            //显示 擅长技能
            string Skills = _ISQLRepository.GetVSkills(VID);
            resModel.Skills = Skills.Substring(0, (Skills.Length-1));
            //获取志愿者 擅长技能资质
            List<VAttachment> VAttachmentList = _IVAttachmentRepository.GetMyList(VID);
            resModel.VAttachmentAddList = _IMapper.Map<List<VAttachment>, List<VAttachmentAddViewModel>>(VAttachmentList);

            resModel.SkillandFilelist = _ISQLRepository.GetSkillandFiles(VID);

            //参与活动时长
            resModel.VA_SignHours = _ISQLRepository.GetVA_SignHours(VID);
            //参与活动时长
            resModel.VA_SignTimes = _IVA_HandleRepository.GetMyInTimes(VID);

            //参与互助信息
            resModel.MyVHASignList = _ISQLRepository.GetVHA_Signs(VID);

            return resModel;

        }


        // 发布者 确认该志愿者为 认领人（其他人 状态为审核不通过）
        public BaseViewModel SetVHAStatus(string VID, string ContentID)
        {
            VHelpArea area = _IVHelpAreaRepository.SearchInfoByID(ContentID);

            BaseViewModel result = new BaseViewModel();
            //确认 认领人员状态更新为 1（审核通过）、其他人员状态更新为 2（审核不通过）；该互助信息 状态改为 1（已认领）
            List<VHA_Sign> list = _IVHA_SignRepository.GetByContentID(ContentID,"1");
            foreach (var item in list)
            {   
                //提示信息：您成功认领标题为XXX互助信息，请尽快处理
                Volunteer_MessageMiddle middle = new Volunteer_MessageMiddle();
                Volunteer_Info volunteer_Info = new Volunteer_Info();
                if (item.CreateUser == VID)
                {
                    volunteer_Info = _IVolunteerInfoRepository.SearchInfoByID(VID);

                    //确认 认领人员状态更新为 1（审核通过）
                    item.Status = "1";
                    middle.Contents = "您成功认领标题为 " + area.Title + " 互助信息，请尽快处理";
                }
                else
                {
                    volunteer_Info = _IVolunteerInfoRepository.SearchInfoByID(item.CreateUser);
    
                    //其他人员状态更新为 2（审核不通过）
                    item.Status = "2";
                    middle.Contents = "您认领标题为 " + area.Title + " 互助信息，认领不成功，已由其他志愿者处理";
                }
                item.UpdateUser = item.CreateUser;
                item.UpdateDate = DateTime.Now;

                _IVHA_SignRepository.Update(item);
                int a = _IVHA_SignRepository.SaveChanges();

                middle.Name = volunteer_Info.Name;
                middle.VID = volunteer_Info.ID;
                middle.VNO = volunteer_Info.VNO;

                Volunteer_Message message = _IMapper.Map<Volunteer_MessageMiddle, Volunteer_Message>(middle);
                message.ID = Guid.NewGuid().ToString();
                message.CreateDate = DateTime.Now;
                message.CreateUser = item.CreateUser;
                message.Status = "0";

                _IVolunteer_MessageRepository.Add(message);
                int c = _IVolunteer_MessageRepository.SaveChanges();

            }

            //该互助信息 状态改为 1（已认领）
            var modelVHA = _IVHelpAreaRepository.SearchInfoByID(ContentID);
            modelVHA.Status = "1";
            modelVHA.UpdateUser = modelVHA.CreateUser;
            modelVHA.UpdateDate = DateTime.Now;
            _IVHelpAreaRepository.Update(modelVHA);
            int b = _IVHelpAreaRepository.SaveChanges();
            if (b > 0)
            {

                result.ResponseCode = 200;
                result.Message = "操作成功";
            }
            else
            {
                result.ResponseCode = 400;
                result.Message = "操作失败";
            }

            return result;
        }


        //查看 认领人上传的 处理结果信息 
        public List<VHA_HandleGetMyResModel> GetMyHandleInfo(string VID, string ContentID)
        {
            List<VHA_HandleGetMyResModel> MyList = new List<VHA_HandleGetMyResModel>();

            List<VHA_Handle> handleList = _IVHA_HandleRepository.GetMyHandle(VID, ContentID);
            //获取 志愿者 上传的 处理结果的信息进行审核
            List<VHA_Sign> listSign = _IVHA_SignRepository.GetByContentID(ContentID, "2");
            List<VHA_Sign> newSign = new List<VHA_Sign>();
            foreach (var itme in handleList)
            {
                newSign = listSign.Where(o => o.bak1.Equals(itme.ID)).ToList();
                VHA_Sign middle = new VHA_Sign();
                if (newSign.Count > 0)
                {
                    middle = newSign.First();
                }
            
                VHA_HandleGetMyResModel resModel = new VHA_HandleGetMyResModel();
                List<VAttachment> list = _IVAttachmentRepository.GetMyList(itme.ID);
                List<VAttachmentAddViewModel> newlist = _IMapper.Map<List<VAttachment>, List<VAttachmentAddViewModel>>(list);
                resModel.contents = itme.Results;
                resModel.time = itme.CreateDate.ToString();
                if (middle == null || middle.ID == null)
                {
                    resModel.opinion = "";
                    resModel.opinionTime = "";
                }
                else
                {
                    resModel.opinion = middle.opinion;
                    resModel.opinionTime = middle.UpdateDate.ToString();
                }
 
                resModel.VAttachmentList = newlist;
                MyList.Add(resModel);
            }
            return MyList;
        }


        //发布者审核 认领人上传的 处理结果信息    审核通过任务完结
        public BaseViewModel SetSignPass(ContentIDandVIDModel model)
        {
            BaseViewModel result = new BaseViewModel();
            var VolunteerInfo = _IVolunteerInfoRepository.SearchInfoByID(model.VID);
            if (VolunteerInfo == null)
            {
                result.ResponseCode = 400;
                result.Message = "操作失败";
                return result;
            }

            //该互助信息  更新状态为 已完结  2
            VHelpArea area = _IVHelpAreaRepository.SearchInfoByID(model.ContentID);
            //获取 志愿者 上传的 处理结果的信息进行审核
            List<VHA_Sign> list = _IVHA_SignRepository.GetByContentID(model.ContentID, "2");
            VHA_Sign item = list.First();

            item.opinion = "审核通过，任务完结";
            item.Status = "1";
            item.UpdateUser = area.CreateUser;
            item.UpdateDate = DateTime.Now;
            _IVHA_SignRepository.Update(item);
            int a = _IVHA_SignRepository.SaveChanges();

            //本次处理结果 审核信息通过 
            VHA_Handle modelHandle = _IVHA_HandleRepository.GetVolunteerHandle(item.bak1);
            modelHandle.Status = "1";
            modelHandle.UpdateUser = area.CreateUser;
            modelHandle.UpdateDate = DateTime.Now;
            _IVHA_HandleRepository.Update(modelHandle);
            int b = _IVHA_HandleRepository.SaveChanges();

          
            area.Status = "2";
            area.UpdateUser = area.CreateUser;
            area.UpdateDate = DateTime.Now;
            _IVHelpAreaRepository.Update(area);
            int c = _IVHelpAreaRepository.SaveChanges();

            if (c > 0)
            {
                string id = Guid.NewGuid().ToString();
                //插入积分表
                Volunteer_Score score = new Volunteer_Score();
                score.ID = id;
                score.ContentID = model.ContentID;
                score.tableName = "VHelpArea";
                score.VID = model.VID;
                score.type = "done";
                score.Score = double.Parse(area.Score);
                score.CreateUser = model.VID;
                score.CreateDate = DateTime.Now;

                _IVolunteer_ScoreRepository.Add(score);
                int n = _IVolunteer_ScoreRepository.SaveChanges();
                if (n > 0)
                {
                    //插入到 泰便利积分表 20200622
                    ET_points ipointMiddle = new ET_points();

                    ipointMiddle.ID = id;
                    ipointMiddle.uid = VolunteerInfo.ID;
                    ipointMiddle.points = double.Parse(area.Score);
                    ipointMiddle.type = "VolunteerHelp";
                    ipointMiddle.tableName = "TedaVolunteerDB.dbo.Volunteer_Score";
                    ipointMiddle.CreateUser = VolunteerInfo.ID;
                    ipointMiddle.CreateDate = DateTime.Now;
                    eT_PointsRepository.Add(ipointMiddle);
                    int j = eT_PointsRepository.SaveChanges();

                }


                //提示信息：您认领标题为XXX互助信息，互助信息，处理审核通过，任务完结
                Volunteer_MessageMiddle middle = new Volunteer_MessageMiddle();
                middle.Contents = "您认领标题为 " + area.Title + " 互助信息，处理审核通过，任务完结";

                Volunteer_Info volunteer_Info = _IVolunteerInfoRepository.SearchInfoByID(model.VID);
                middle.Name = volunteer_Info.Name;
                middle.VID = volunteer_Info.ID;
                middle.VNO = volunteer_Info.VNO;

                Volunteer_Message message = _IMapper.Map<Volunteer_MessageMiddle, Volunteer_Message>(middle);
                message.ID = Guid.NewGuid().ToString();
                message.CreateDate = DateTime.Now;
                message.CreateUser = model.VID;
                message.Status = "0";

                _IVolunteer_MessageRepository.Add(message);
                int f = _IVolunteer_MessageRepository.SaveChanges();


                result.ResponseCode = 200;
                result.Message = "操作成功";
            }
            else
            {
                result.ResponseCode = 400;
                result.Message = "操作失败";
            }

            return result;
        }


        //发布者审核 认领人上传的 处理结果信息   审核不通过，志愿者重新上传处理结果
        public BaseViewModel SetSignReturn(ContentIDandVIDModel model)
        {
            BaseViewModel result = new BaseViewModel();

            //该互助信息   
            VHelpArea area = _IVHelpAreaRepository.SearchInfoByID(model.ContentID);
            //获取 志愿者 上传的 处理结果的信息进行审核
            List<VHA_Sign> list = _IVHA_SignRepository.GetByContentID(model.ContentID, "2");
            VHA_Sign item = list.First();

            item.opinion = "审核不通过，志愿者重新上传处理结果";
            item.Status = "2";
            item.UpdateUser = area.CreateUser;
            item.UpdateDate = DateTime.Now;
            _IVHA_SignRepository.Update(item);
            int a = _IVHA_SignRepository.SaveChanges();

            //本次处理结果 审核信息通过 
            VHA_Handle modelHandle = _IVHA_HandleRepository.GetVolunteerHandle(item.bak1);
            modelHandle.Status = "2";
            modelHandle.UpdateUser = area.CreateUser;
            modelHandle.UpdateDate = DateTime.Now;
            _IVHA_HandleRepository.Update(modelHandle);
            int b = _IVHA_HandleRepository.SaveChanges();

            if (b > 0)
            {

                //提示信息：您认领标题为XXX互助信息，互助信息，处理审核不通过，请重新上传处理结果
                Volunteer_MessageMiddle middle = new Volunteer_MessageMiddle();
                middle.Contents = "您认领标题为 " + area.Title + " 互助信息，处理审核不通过，请重新上传处理结果";

                Volunteer_Info volunteer_Info = _IVolunteerInfoRepository.SearchInfoByID(model.VID);
                middle.Name = volunteer_Info.Name;
                middle.VID = volunteer_Info.ID;
                middle.VNO = volunteer_Info.VNO;

                Volunteer_Message message = _IMapper.Map<Volunteer_MessageMiddle, Volunteer_Message>(middle);
                message.ID = Guid.NewGuid().ToString();
                message.CreateDate = DateTime.Now;
                message.CreateUser = model.VID;
                message.Status = "0";

                _IVolunteer_MessageRepository.Add(message);
                int f = _IVolunteer_MessageRepository.SaveChanges();

                result.ResponseCode = 200;
                result.Message = "操作成功";
            }
            else
            {
                result.ResponseCode = 400;
                result.Message = "操作失败";
            }

            return result;
        }


        //发布者审核 认领人上传的 处理结果信息   退回至互助信息，志愿者可重新认领
        public BaseViewModel SetSignBack(ContentIDandVIDModel model)
        {
            BaseViewModel result = new BaseViewModel();
            //该互助信息  更新状态为 未认领状态  0
            VHelpArea area = _IVHelpAreaRepository.SearchInfoByID(model.ContentID);
            //获取 志愿者 上传的 处理结果的信息进行审核
            List<VHA_Sign> list = _IVHA_SignRepository.GetByContentID(model.ContentID, "2");
            VHA_Sign item = list.First();

            //本次处理结果 审核信息被退回(记录表)
            item.opinion = "审核不通过，退回至互助信息，可重新认领";
            item.Status = "3";
            item.UpdateUser = area.CreateUser;
            item.UpdateDate = DateTime.Now;
            _IVHA_SignRepository.Update(item);
            int a = _IVHA_SignRepository.SaveChanges();

            //本次处理结果 审核信息不通过 
            VHA_Handle modelHandle = _IVHA_HandleRepository.GetVolunteerHandle(item.bak1);
            modelHandle.Status = "2";
            modelHandle.UpdateUser = area.CreateUser;
            modelHandle.UpdateDate = DateTime.Now;
            _IVHA_HandleRepository.Update(modelHandle);
            int b = _IVHA_HandleRepository.SaveChanges();


            area.Status = "0";
            area.UpdateUser = area.CreateUser;
            area.UpdateDate = DateTime.Now;
            _IVHelpAreaRepository.Update(area);
            int c = _IVHelpAreaRepository.SaveChanges();

            if (c > 0)
            {
                //提示信息：您认领标题为XXX互助信息，互助信息，处理审核不通过，退回至互助信息
                Volunteer_MessageMiddle middle = new Volunteer_MessageMiddle();
                middle.Contents = "您认领标题为 " + area.Title + " 互助信息，处理审核不通过，退回至互助信息";

                Volunteer_Info volunteer_Info = _IVolunteerInfoRepository.SearchInfoByID(model.VID);
                middle.Name = volunteer_Info.Name;
                middle.VID = volunteer_Info.ID;
                middle.VNO = volunteer_Info.VNO;

                Volunteer_Message message = _IMapper.Map<Volunteer_MessageMiddle, Volunteer_Message>(middle);
                message.ID = Guid.NewGuid().ToString();
                message.CreateDate = DateTime.Now;
                message.CreateUser = model.VID;
                message.Status = "0";

                _IVolunteer_MessageRepository.Add(message);
                int f = _IVolunteer_MessageRepository.SaveChanges();

                result.ResponseCode = 200;
                result.Message = "操作成功";
            }
            else
            {
                result.ResponseCode = 400;
                result.Message = "操作失败";
            }


            return result;

        }


        // (小程序端接口) 认领人针对本次认领任务的所有状态
        public BaseViewModel GetVHAByVStatus(ContentIDandVIDModel model)
        {
            BaseViewModel result = new BaseViewModel();
            VHA_Sign res = _IVHA_SignRepository.GetNewSign(model.VID, model.ContentID);
            if (!String.IsNullOrEmpty(res.ID))
            {
                if (res.flag.Equals("1"))
                {
                    if (res.Status.Equals("0"))
                    {
                        result.ResponseCode = 1;
                        result.Message = "选择认领人";
                    }
                    else if (res.Status.Equals("1"))
                    {
                        result.ResponseCode = 2;
                        result.Message = "等待志愿者上传处理结果";
                    }
                    else if (res.Status.Equals("2"))
                    {
                        result.ResponseCode = 3;
                        result.Message = "未被选择为认领人";
                    }
                    else if (res.Status.Equals("3"))
                    {
                        result.ResponseCode = 4;
                        result.Message = "认领审核通过，志愿者主动退回，该志愿者本次互助任务终止";
                    }
                }
                else if (res.flag.Equals("2"))
                {
                    if (res.Status.Equals("0"))
                    {
                        result.ResponseCode = 5;
                        result.Message = "审核处理结果";
                    }
                    else if (res.Status.Equals("1"))
                    {
                        result.ResponseCode = 6;
                        result.Message = "处理结果审核通过，任务完结";
                    }
                    else if (res.Status.Equals("2"))
                    {
                        result.ResponseCode = 7;
                        result.Message = "处理结果审核不通过，请志愿者重新上传";
                    }
                    else if (res.Status.Equals("3"))
                    {
                        if (!String.IsNullOrEmpty(res.opinion))
                        {
                            result.ResponseCode = 8;
                            result.Message = "处理结果审核不通过，发布者退回，该志愿者本次互助任务终止";
                        }
                        else
                        {
                            result.ResponseCode = 9;
                            result.Message = "处理结果审核不通过，志愿者主动退回，该志愿者本次互助任务终止";
                        }
                    }
                }
            }
            else
            {
                VHelpArea item = _IVHelpAreaRepository.SearchInfoByID(model.ContentID);
                if (!String.IsNullOrEmpty(item.ID))
                {
                    if (item.Status.Equals("0"))
                    {
                        result.ResponseCode = 11;
                        result.Message = "未认领";
                    }
                    else if (item.Status.Equals("1"))
                    {
                        result.ResponseCode = 12;
                        result.Message = "已认领";
                    }
                    else if (item.Status.Equals("2"))
                    {
                        result.ResponseCode = 13;
                        result.Message = "已完结";
                    }
                }
                else
                {
                    result.ResponseCode = 10;
                    result.Message = "该互助信息有问题，请联系志愿者后台调整。";
                }

            }
            return result;
        }



        // (小程序端接口) 获取该互助信息的所有状态
        public BaseViewModel GetVHAStatusAll(SearchByContentIDModel model)
        {
            BaseViewModel result = new BaseViewModel();
            VHA_Sign res = _IVHA_SignRepository.GetNewSign("", model.ContentID);
            if (!String.IsNullOrEmpty(res.ID))
            {
                if (res.flag.Equals("1"))
                {
                    if (res.Status.Equals("0"))
                    {
                        result.ResponseCode = 1;
                        result.Message = "选择认领人";
                    }
                    else if (res.Status.Equals("1"))
                    {
                        result.ResponseCode = 2;
                        result.Message = "等待志愿者上传处理结果";
                    }
                    else if (res.Status.Equals("2"))
                    {
                        result.ResponseCode = 3;
                        result.Message = "未被选择为认领人";
                    }
                    else if (res.Status.Equals("3"))
                    {
                        result.ResponseCode = 4;
                        result.Message = "认领审核通过，志愿者主动退回，该志愿者本次互助任务终止";
                    }
                }
                else if (res.flag.Equals("2"))
                {
                    if (res.Status.Equals("0"))
                    {
                        result.ResponseCode = 5;
                        result.Message = "审核处理结果";
                    }
                    else if (res.Status.Equals("1"))
                    {
                        result.ResponseCode = 6;
                        result.Message = "处理结果审核通过，任务完结";
                    }
                    else if (res.Status.Equals("2"))
                    {
                        result.ResponseCode = 7;
                        result.Message = "处理结果审核不通过，请志愿者重新上传";
                    }
                    else if (res.Status.Equals("3"))
                    {
                        if (!String.IsNullOrEmpty(res.opinion))
                        {
                            result.ResponseCode = 8;
                            result.Message = "处理结果审核不通过，发布者退回，该志愿者本次互助任务终止";
                        }
                        else
                        {
                            result.ResponseCode = 9;
                            result.Message = "处理结果审核不通过，志愿者主动退回，该志愿者本次互助任务终止";
                        }
                    }
                }
            }
            else
            {
                VHelpArea item = _IVHelpAreaRepository.SearchInfoByID(model.ContentID);
                if (!String.IsNullOrEmpty(item.ID))
                {
                    if (item.Status.Equals("0"))
                    {
                        result.ResponseCode = 11;
                        result.Message = "未认领";
                    }
                    else if (item.Status.Equals("1"))
                    {
                        result.ResponseCode = 12;
                        result.Message = "已认领";
                    }
                    else if (item.Status.Equals("2"))
                    {
                        result.ResponseCode = 13;
                        result.Message = "已完结";
                    }
                }
                else
                {
                    result.ResponseCode = 10;
                    result.Message = "该互助信息有问题，请联系志愿者后台调整。";
                }

            }
            return result;
        }

    }
}
