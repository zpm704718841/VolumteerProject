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

namespace Dto.Service.IntellVolunteer
{
    public class VActivity_PublicShowService : IVActivity_PublicShowService
    {
        private readonly IVActivity_PublicShowRepository _IVActivity_PublicShowRepository;
        private readonly IMapper _IMapper;
        private readonly IVAttachmentRepository _IVAttachmentRepository;
        private readonly IVA_SignRepository _IVA_SignRepository;
        private readonly IVolunteerActivityRepository _IVolunteerActivityRepository;
        private readonly IVActivity_PublicShow_GiveLikeRepository _GivelikeRepository;
        private readonly IVolunteerInfoRepository _IVolunteerInfoRepository;
        private readonly IVolunteer_MessageRepository _IVolunteer_MessageRepository;
        public VActivity_PublicShowService(IVActivity_PublicShowRepository  showRepository, IMapper mapper, IVAttachmentRepository attachmentRepository, IVA_SignRepository signRepository, IVolunteerActivityRepository activityRepository, IVActivity_PublicShow_GiveLikeRepository likeRepository, IVolunteerInfoRepository infoRepository, IVolunteer_MessageRepository messageRepository)
        {
            _IVActivity_PublicShowRepository = showRepository;
            _IMapper = mapper;
            _IVAttachmentRepository = attachmentRepository;
            _IVA_SignRepository = signRepository;
            _IVolunteerActivityRepository = activityRepository;
            _GivelikeRepository = likeRepository;
            _IVolunteerInfoRepository = infoRepository;
            _IVolunteer_MessageRepository = messageRepository;

        }

        //志愿者参与的志愿活动（已结束和进行中）(必须有签到记录) 参数 志愿者VID
        public List<VolunteerActivitySearchMiddle> GetMyAllActivity(string VID)
        {
            List<String> Infos = _IVA_SignRepository.GetMyList(VID);
            List<VolunteerActivitySearchMiddle> Searches = new List<VolunteerActivitySearchMiddle>();
 
            foreach (var item in Infos)
            {
                VolunteerActivity middle = new VolunteerActivity();
                middle = _IVolunteerActivityRepository.GetByID(item.ToString());
                
                var SearchMiddlecs = _IMapper.Map<VolunteerActivity, VolunteerActivitySearchMiddle>(middle);
                Searches.Add(SearchMiddlecs);
            }
            return Searches;

        }


        //(小程序端接口) 志愿者上传 公益秀（志愿活动ID，活动标题，活动地址，参与感受，图片列表 等等）
        public VActivity_PublicShowAddResModel AddPublicShow(VActivity_PublicShowAddModel showAddModel)
        {
            VActivity_PublicShowAddResModel result = new VActivity_PublicShowAddResModel();
            VActivity_PublicShow show = _IMapper.Map<VActivity_PublicShowAddModel, VActivity_PublicShow>(showAddModel);

            var VolunteerInfo = _IVolunteerInfoRepository.SearchInfoByID(showAddModel.VID);
            if (VolunteerInfo == null)
            {
                result.AddCount = 0;
                result.IsSuccess = false;
                result.baseViewModel.Message = "上传公益秀基本信息失败";
                result.baseViewModel.ResponseCode = 500;
                return result;
            }

            show.ID = Guid.NewGuid().ToString();
            show.NickName = VolunteerInfo.Nickname;
            show.Headimgurl = VolunteerInfo.Headimgurl;
            show.CreateUser = showAddModel.VID;
            show.CreateDate = DateTime.Now;
            show.UpdateUser = showAddModel.VID;
            show.UpdateDate = DateTime.Now;
            show.Status = "0";
            _IVActivity_PublicShowRepository.Add(show);
            int a = _IVActivity_PublicShowRepository.SaveChanges();
            if (a > 0)
            {
                int c = 0;
                //保存 公益秀图片
                var AttachmentInfo = _IMapper.Map<List<VAttachmentAddViewModel>, List<VAttachment>>(showAddModel.VAttachmentAddList);
                foreach (var item in AttachmentInfo)
                {
                    item.ID = Guid.NewGuid().ToString();
                    item.formid = show.ID;
                    item.type = "PSTP";
                    item.Status = "0";
                    item.CreateUser = showAddModel.VID;
                    item.CreateDate = DateTime.Now;
                    _IVAttachmentRepository.Add(item);
                    c = _IVAttachmentRepository.SaveChanges() + c;
                }
                if (c == showAddModel.VAttachmentAddList.Count)
                {
                    //提示信息：您成功上传公益秀，等待审核。
                    Volunteer_MessageMiddle middle = new Volunteer_MessageMiddle();
                    middle.Contents = "您上传标题为 "+ showAddModel.Title + " 公益秀，等待审核。";

                    Volunteer_Info volunteer_Info = _IVolunteerInfoRepository.SearchInfoByID(showAddModel.VID);
                    middle.Name = volunteer_Info.Name;
                    middle.VID = volunteer_Info.ID;
                    middle.VNO = volunteer_Info.VNO;

                    Volunteer_Message message = _IMapper.Map<Volunteer_MessageMiddle, Volunteer_Message>(middle);
                    message.ID = Guid.NewGuid().ToString();
                    message.CreateDate = DateTime.Now;
                    message.CreateUser = showAddModel.VID;
                    message.Status = "0";

                    _IVolunteer_MessageRepository.Add(message);
                    int f = _IVolunteer_MessageRepository.SaveChanges();



                    result.AddCount = a;
                    result.IsSuccess = true;
                    result.baseViewModel.Message = "成功上传公益秀，等待审核。";
                    result.baseViewModel.ResponseCode = 200;
                }
                else
                {
                    result.AddCount = 0;
                    result.IsSuccess = false;
                    result.baseViewModel.Message = "上传公益秀图片失败";
                    result.baseViewModel.ResponseCode = 300;
                }
            }
            else
            {
                result.AddCount = 0;
                result.IsSuccess = false;
                result.baseViewModel.Message = "上传公益秀基本信息失败";
                result.baseViewModel.ResponseCode = 400;
            }
            return result;
        }


        //展示公所有益秀信息（志愿活动ID，活动标题，活动地址，参与感受，图片列表 等等）
        public List<VActivity_PublicShowMiddle> GetPublicShowList()
        {
            List<VActivity_PublicShowMiddle> Searches = new List<VActivity_PublicShowMiddle>();
            List<VActivity_PublicShow> list = _IVActivity_PublicShowRepository.GetPublicShowList();
            // 只显示   isPublic=0; 选择公开  
            // 只显示   status=1;审核通过信息  
            list = list.Where(p => p.Status.Equals("1")).ToList();
            list = list.Where(o => o.isPublic.Equals(0)).ToList();

            Searches = _IMapper.Map<List<VActivity_PublicShow>, List<VActivity_PublicShowMiddle>>(list);

            foreach (var item in Searches)
            {
                //计算发布时间  
                DateTime oldDate = DateTime.Parse(item.CreateDate.ToString());
                DateTime newDate = DateTime.Now;
                TimeSpan ts = newDate - oldDate;
                int differenceInDays = ts.Days;
                int differenceInHours = ts.Hours;
                int differenceInMins = ts.Minutes;
           

                if (differenceInDays > 5)
                {
                    item.bak1 = oldDate.ToString("yyyy-MM-dd");
                }
                else if (differenceInDays <= 5 && differenceInDays > 0)
                {
                    item.bak1 = differenceInDays.ToString() + "天前";
                }
                else if (differenceInDays <= 0 && differenceInHours>0)
                {
                    item.bak1 = differenceInHours.ToString() + "小时前";
                }
                else if (differenceInHours <= 0 && differenceInMins > 0)
                {
                    item.bak1 = differenceInMins.ToString() + "分钟前";
                }
                else if (differenceInMins <= 0 )
                {
                    item.bak1 = "刚刚";
                }


                //绑定对应的图片信息
                List<VAttachment> attlist = _IVAttachmentRepository.GetMyList(item.ID);
                List<VAttachmentAddViewModel> newlist = _IMapper.Map<List<VAttachment>, List<VAttachmentAddViewModel>>(attlist);
                item.VAttachmentAddList = newlist;

                //绑定对应的点赞信息
                List<VActivity_PublicShow_GiveLike> likelist = _GivelikeRepository.GetLikeList(item.ID);
                List<VActivity_PublicShow_GiveLikeMiddle> newlikes = _IMapper.Map<List<VActivity_PublicShow_GiveLike>, List<VActivity_PublicShow_GiveLikeMiddle>>(likelist);
                item.GiveLikeList = newlikes;
            }
            return Searches;
        }

        //获取我的益秀信息（志愿活动ID，活动标题，活动地址，参与感受，图片列表 等等）
        public List<VActivity_PublicShowMiddle> GetMyPublicShowList(string VID)
        {
            List<VActivity_PublicShowMiddle> Searches = new List<VActivity_PublicShowMiddle>();
            List<VActivity_PublicShow> list = _IVActivity_PublicShowRepository.GetPublicShowList();
            //只显示我上传的公益秀
            list = list.Where(o => o.VID.Equals(VID)).ToList();

            Searches = _IMapper.Map<List<VActivity_PublicShow>, List<VActivity_PublicShowMiddle>>(list);

            foreach (var item in Searches)
            {
                //计算发布时间  
                DateTime oldDate = DateTime.Parse(item.CreateDate.ToString());
                DateTime newDate = DateTime.Now;
                TimeSpan ts = newDate - oldDate;
                int differenceInDays = ts.Days;
                int differenceInHours = ts.Hours;
                int differenceInMins = ts.Minutes;

                if (differenceInDays > 5)
                {
                    item.bak1 = oldDate.ToString("yyyy-MM-dd");
                }
                else if (differenceInDays <= 5 && differenceInDays > 0)
                {
                    item.bak1 = differenceInDays.ToString() + "天前";
                }
                else if (differenceInDays <= 0 && differenceInHours > 0)
                {
                    item.bak1 = differenceInHours.ToString() + "小时前";
                }
                else if (differenceInHours <= 0 && differenceInMins > 0)
                {
                    item.bak1 = differenceInMins.ToString() + "分钟前";
                }
                else if (differenceInMins <= 0)
                {
                    item.bak1 = "刚刚";
                }
                //绑定对应的
                List<VAttachment> attlist = _IVAttachmentRepository.GetMyList(item.ID);
                List<VAttachmentAddViewModel> newlist = _IMapper.Map<List<VAttachment>, List<VAttachmentAddViewModel>>(attlist);
                item.VAttachmentAddList = newlist;

                //绑定对应的点赞信息
                List<VActivity_PublicShow_GiveLike> likelist = _GivelikeRepository.GetLikeList(item.ID);
                List<VActivity_PublicShow_GiveLikeMiddle> newlikes = _IMapper.Map<List<VActivity_PublicShow_GiveLike>, List<VActivity_PublicShow_GiveLikeMiddle>>(likelist);
                item.GiveLikeList = newlikes;
            }
            return Searches;
        }

        //志愿者针对一条公益秀点赞   参数志愿者VID，公益秀ID
        public BaseViewModel PublicShow_GiveLike(PublicShowIDandVID showDandVid)
        {
            BaseViewModel result = new BaseViewModel();

            var VolunteerInfo = _IVolunteerInfoRepository.SearchInfoByID(showDandVid.VID);
            if (VolunteerInfo == null)
            {
                result.ResponseCode = 400;
                result.Message = "操作失败";
                return result;
            }
            VActivity_PublicShow_GiveLike giveLike = new VActivity_PublicShow_GiveLike();
            string id = Guid.NewGuid().ToString();
            giveLike.ID = id;
            giveLike.PublicShowID = showDandVid.PublicShowID;
            giveLike.VID = showDandVid.VID;
            giveLike.NickName = VolunteerInfo.Nickname;
            giveLike.Headimgurl = VolunteerInfo.Headimgurl;
            giveLike.CreateUser = showDandVid.VID;
            giveLike.CreateDate = DateTime.Now;
            giveLike.UpdateUser = showDandVid.VID;
            giveLike.UpdateDate = DateTime.Now;

            _GivelikeRepository.Add(giveLike);
            int a = _GivelikeRepository.SaveChanges();
            if (a > 0)
            {
                result.ResponseCode = 200;
                result.Message = "操作成功";
            }
            return result;
        }


        //志愿者针对一条公益秀 取消点赞   参数志愿者VID，公益秀ID
        public BaseViewModel PublicShow_CancelLike(PublicShowIDandVID showDandVid)
        {
            BaseViewModel result = new BaseViewModel();

            var VolunteerInfo = _IVolunteerInfoRepository.SearchInfoByID(showDandVid.VID);
            if (VolunteerInfo == null)
            {
                result.ResponseCode = 400;
                result.Message = "操作失败";
                return result;
            }
            VActivity_PublicShow_GiveLike giveLike = _GivelikeRepository.GetLike(showDandVid.VID, showDandVid.PublicShowID);

            if (giveLike.ID != null)
            {
                _GivelikeRepository.RemoveNew(giveLike);
                int a = _GivelikeRepository.SaveChanges();
                if (a > 0)
                {
                    result.ResponseCode = 200;
                    result.Message = "操作成功";
                }
            }
            else
            {
                result.ResponseCode = 300;
                result.Message = "操作失败";
            }
           
            return result;
        }


        //志愿者删除该公益秀（自己发布的）   参数志愿者VID，公益秀ID
        public BaseViewModel PublicShow_Delete(PublicShowIDandVID showDandVid)
        {
            BaseViewModel result = new BaseViewModel();

            var PublicShow = _IVActivity_PublicShowRepository.SearchInfoByID(showDandVid.PublicShowID);
            if (PublicShow == null)
            {
                result.ResponseCode = 400;
                result.Message = "操作失败";
                return result;
            }
            else
            {
                if (PublicShow.VID == showDandVid.VID)
                {
                    PublicShow.Status = "3";
                    _IVActivity_PublicShowRepository.Update(PublicShow);
                    int a = _IVActivity_PublicShowRepository.SaveChanges();
                    if (a > 0)
                    {
                        result.ResponseCode = 200;
                        result.Message = "操作成功";
                    }
                    else
                    {
                        result.ResponseCode = 300;
                        result.Message = "操作失败";
                    }
                }
                else
                {
                    result.ResponseCode = 500;
                    result.Message = "操作失败";
                }
            }

            return result;
        }

        //验证该志愿者是否 已经点赞该公益秀 20200608
        public BaseViewModel CheckIsGiveLike(PublicShowIDandVID showIDandVID)
        {
            BaseViewModel result = new BaseViewModel();
            try
            {
                VActivity_PublicShow_GiveLike itme = _GivelikeRepository.GetLike(showIDandVID.VID, showIDandVID.PublicShowID);
                //不为空 则点赞过
                if (!string.IsNullOrEmpty(itme.PublicShowID))
                {
                    result.ResponseCode = 0;
                    result.Message = "已点赞";
                }
                else
                {
                    result.ResponseCode = 1;
                    result.Message = "未点赞";
                }
            }
            catch(Exception ex)
            {
                result.ResponseCode = 3;
                result.Message = "出现异常";
            }
             
            return result;

        }
    }
}
