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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.ResponseModel;
using ViewModel.PublicViewModel;
using System.Web;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace Dto.Service.IntellVolunteer
{
    public class VolunteerActivityService : IVolunteerActivityService
    {
        private readonly IVolunteerActivityRepository _IVolunteerActivityRepository;
        private readonly IVActivity_Relate_TypeRepository _IVActivity_Relate_TypeRepository;
        private readonly IVolunteerInfoRepository _IVolunteerInfoRepository;
        private readonly IVA_SignRepository _IVA_SignRepository;
        private readonly IVolunteer_Relate_TypeRepository _IVolunteer_Relate_TypeRepository;
        private readonly IBaseTypeRepository _IBaseTypeRepository;
        private readonly IVA_HandleRepository _Va_HandleRepository;
        private readonly IVAttachmentRepository _IVAttachmentRepository;
        private readonly IMapper _IMapper;
        private readonly ISQLRepository _ISQLRepository;
        private readonly IAISQLRepository _IAISQLRepository;
        private readonly IVolunteer_ScoreRepository _IVolunteer_ScoreRepository;
        private readonly IVolunteer_MessageRepository _IVolunteer_MessageRepository;

        public VolunteerActivityService(IVolunteerActivityRepository iInfoRepository, IMapper mapper, IVActivity_Relate_TypeRepository relate_TypeRepository, IVolunteerInfoRepository infoRepository, IVA_SignRepository va_SignRepository, IVolunteer_Relate_TypeRepository volunteer_Relate_TypeRepository, IBaseTypeRepository baseTypeRepository, IVA_HandleRepository va_HandleRepository, IVAttachmentRepository AttachmentRepository, ISQLRepository sqlRepository, IVolunteer_ScoreRepository scoreRepository, IAISQLRepository aisqlRepository, IVolunteer_MessageRepository messageRepository)
        {
            _IVolunteerActivityRepository = iInfoRepository;
            _IVActivity_Relate_TypeRepository = relate_TypeRepository;
            _IVolunteerInfoRepository = infoRepository;
            _IVA_SignRepository = va_SignRepository;
            _IVolunteer_Relate_TypeRepository = volunteer_Relate_TypeRepository;
            _IBaseTypeRepository = baseTypeRepository;
            _Va_HandleRepository = va_HandleRepository;
            _IVAttachmentRepository = AttachmentRepository;
            _IMapper = mapper;
            _ISQLRepository = sqlRepository;
            _IAISQLRepository = aisqlRepository;
            _IVolunteer_ScoreRepository = scoreRepository;
            _IVolunteer_MessageRepository = messageRepository;

        }



        //查询信息
        public List<VolunteerActivitySearchMiddle> VolunteerActivity_Search(AllSearchViewModel VSearchViewModel)
        {
            List<VolunteerActivity> Infos = _IVolunteerActivityRepository.SearchInfoByWhere(VSearchViewModel);

            List<VolunteerActivitySearchMiddle> Searches = new List<VolunteerActivitySearchMiddle>();


            //默认地址 天津市滨海新区宏达街19号 (显示距离)
            string longitude = "117.70889";
            string latitude = "39.02749";

            var VolunteerInfo = _IVolunteerInfoRepository.SearchInfoByID(VSearchViewModel.VID);
            //根据活动地址推荐 两公里以内
            string address = VolunteerInfo.Address;

            if (address != "")
            {
                string content = GetFunction(address);
                JObject jo = (JObject)JsonConvert.DeserializeObject(content);//或者JObject jo = JObject.Parse(jsonText);
                if (jo["status"].ToString() == "0")
                {
                    longitude = jo["result"]["location"]["lng"].ToString();
                    latitude = jo["result"]["location"]["lat"].ToString();
                }
            }

            foreach (var item in Infos)
            {
                var id = item.ID;
                List<VActivity_Relate_Type> RelateList = _IVActivity_Relate_TypeRepository.GetRelateList(id);

                //计算 已报名人数
                int nums = _IVA_SignRepository.GetSingNum(item.ID);
                item.bak4 = nums.ToString();

                //活动报名人数上限
                int totalnum = _IVActivity_Relate_TypeRepository.GetSum(item.ID, "");
                item.bak5 = totalnum.ToString();

                //判断 互助地址与志愿者注册详细地址间距离
                double dis = GetDistance(double.Parse(longitude), double.Parse(latitude), double.Parse(item.longitude), double.Parse(item.latitude));
                item.bak3 = dis.ToString("f2");

                var model = _IMapper.Map<List<VActivity_Relate_Type>, List<VActivity_Relate_TypeMiddle>>(RelateList);

                var SearchMiddlecs = _IMapper.Map<VolunteerActivity, VolunteerActivitySearchMiddle>(item);

                SearchMiddlecs.VTypeList = model;

                Searches.Add(SearchMiddlecs);

            }

            return Searches.Count > 0 ? Searches : null;
        }

        //根据ID 获取 活动信息
        public List<VolunteerActivitySearchMiddle> GetVolunteerActivity(SearchByContentIDModel model)
        {
            List<VolunteerActivitySearchMiddle> Searches = new List<VolunteerActivitySearchMiddle>();
            VolunteerActivity activity = _IVolunteerActivityRepository.GetByID(model.ContentID);

            var id = activity.ID;


            //计算 已报名人数
            int nums = _IVA_SignRepository.GetSingNum(activity.ID);
            activity.bak4 = nums.ToString();

            //活动报名人数上限
            int totalnum = _IVActivity_Relate_TypeRepository.GetSum(activity.ID, "");
            activity.bak5 = totalnum.ToString();


            List<VActivity_Relate_Type> RelateList = _IVActivity_Relate_TypeRepository.GetRelateList(id);
            var typelist = _IMapper.Map<List<VActivity_Relate_Type>, List<VActivity_Relate_TypeMiddle>>(RelateList);

            var SearchMiddlecs = _IMapper.Map<VolunteerActivity, VolunteerActivitySearchMiddle>(activity);
            SearchMiddlecs.VTypeList = typelist;
            Searches.Add(SearchMiddlecs);
            return Searches;
        }

        //查询所有信息
        public List<VolunteerActivitySearchMiddle> VolunteerActivity_All()
        {
            List<VolunteerActivity> Infos = _IVolunteerActivityRepository.GetVolunteerActivityAll();

            List<VolunteerActivitySearchMiddle> Searches = new List<VolunteerActivitySearchMiddle>();

            foreach (var item in Infos)
            {
                //计算 已报名人数
                int nums = _IVA_SignRepository.GetSingNum(item.ID);
                item.bak4 = nums.ToString();

                //活动报名人数上限
                int totalnum = _IVActivity_Relate_TypeRepository.GetSum(item.ID, "");
                item.bak5 = totalnum.ToString();

                var SearchMiddlecs = _IMapper.Map<VolunteerActivity, VolunteerActivitySearchMiddle>(item);
                Searches.Add(SearchMiddlecs);

            }
            return Searches.Count > 0 ? Searches : null;
        }


        //获取不同类型的活动数
        public int GetTypeCounts(VolunteerActivityTypeModel typeModel)
        {
            int count = _IVolunteerActivityRepository.GetTypeCount(typeModel);
            return count;
        }

        //验证 是否 报名 同时段活动
        public async Task<string> CheckSigns(string VID, string ContentID)
        {
            string ss = await _ISQLRepository.CheckSign(VID, ContentID);
            return ss;
        }


        //验证 是否 报名 同时段活动
        public string CheckSignsNew(string VID, string ContentID)
        {
            string ss =  _ISQLRepository.CheckSignNew(VID, ContentID);
            return ss;
        }

        //验证是否 报名该活动
        public string CheckSignAdd(string VID, string ContentID)
        {
            string result = "false";
            List<String> Infos = _IVA_SignRepository.GetMyList(VID);
            if (!Infos.Contains(ContentID))
            {
                result = "true";
            }
            return result;
        }


        // 志愿者活动报名
        public int SignAdd(VA_SignAddViewModel AddViewModel)
        {
            //普通志愿者  无擅长技能
            string SkillID = "AB1EC380-4D47-45AB-9C2B-F2EE19B7AE9C";

            int count = 0;
            if (String.IsNullOrEmpty(AddViewModel.ContentID) || String.IsNullOrEmpty(AddViewModel.VID) || String.IsNullOrEmpty(AddViewModel.TypeID))
            {
                count = 7;
                return count;
            }

            var model = _IMapper.Map<VA_SignAddViewModel, VA_Sign> (AddViewModel);
            var VolunteerInfo = _IVolunteerInfoRepository.SearchInfoByID(model.VID);
            if (VolunteerInfo == null)
            {
                count = 6;
                return count;
            }

            //20191108 判断 是否报名同时段内志愿活动
            string ss = CheckSignsNew(AddViewModel.VID, AddViewModel.ContentID);
            if (ss.ToString() == "false")
            {
                count = 5; 
                return count;
            }



            //审核状态  只有上传资质类型的志愿需要审核，其他志愿者无需审核，直接定义为审核通过
            string status = String.Empty;

            //判断是否还有 名额进行报名 （或 按审核通过的报名人数进行 比对 待确认？？？ zpm 20191106  ----目前不会有专业志愿者、都是普通志愿者，不需要审核）
            int sumCount = _IVA_SignRepository.GetCount(AddViewModel.ContentID, AddViewModel.TypeID);

            int sumNum = 0;
           
            //判断 志愿类型 判断是否是 普通志愿者 
            if (AddViewModel.TypeID != SkillID)
            {
                sumNum = _IVActivity_Relate_TypeRepository.GetSum(AddViewModel.ContentID, AddViewModel.TypeID);
                if (sumCount >= sumNum)
                {
                    count = 8;
                    return count;
                }
                //判断 是否是专业志愿服务 则需验证 报名者是否符合
                if (_IBaseTypeRepository.CheckStatus(AddViewModel.TypeID))
                {
                    status = "0";
                    //判断是否有该类型资质认证
                    if (!_IVolunteer_Relate_TypeRepository.CheckInfo(AddViewModel.TypeID, AddViewModel.VID))
                    {
                        count = 9;
                        return count;
                    }
                }
                else
                {
                    status = "1";
                }
            }
            else
            {
                #region 注释 普通志愿者时 活动信息表（volunteerActivity） number字段与 志愿活动与擅长技能关系表（VActivity_Relate_Type） Count字段相同
                //VolunteerActivity volunteerActivity = _IVolunteerActivityRepository.GetByID(AddViewModel.ContentID);
                //if(!String.IsNullOrEmpty(volunteerActivity.Number))
                //{
                //    sumNum = int.Parse(volunteerActivity.Number);
                //    if (sumCount >= sumNum)
                //    {
                //        count = 8;
                //        return count;
                //    }
                //}
                #endregion

                status = "1";

                sumNum = _IVActivity_Relate_TypeRepository.GetSum(AddViewModel.ContentID, AddViewModel.TypeID);
                if (sumCount >= sumNum)
                {
                    count = 8;
                    return count;
                }
            }
            VolunteerActivity activity = _IVolunteerActivityRepository.GetByID(AddViewModel.ContentID);

            model.ID = Guid.NewGuid().ToString();
            model.VNO = VolunteerInfo.VNO;
            model.Participant = VolunteerInfo.Name;
            model.Status = status;
            model.CreateUser = VolunteerInfo.ID;
            model.CreateDate = DateTime.Now;
            model.ramark = AddViewModel.TypeID;
            _IVA_SignRepository.Add(model);
            count = _IVA_SignRepository.SaveChanges();

            if (count > 0)
            {
                int sumCount2 = _IVA_SignRepository.GetCount(AddViewModel.ContentID, AddViewModel.TypeID);
                int sumNum2 = _IVActivity_Relate_TypeRepository.GetSum(AddViewModel.ContentID, AddViewModel.TypeID);
                //判断是否 名额已满，如果名额已满更新状态位
                if (sumCount >= sumNum)
                {
                    VolunteerActivity volunteerActivity = _IVolunteerActivityRepository.GetByID(AddViewModel.ContentID);

                    volunteerActivity.Status = "8";
                    _IVolunteerActivityRepository.Update(volunteerActivity);

                }

                //提示信息：您已报名XXX活动，请按时参加
                Volunteer_MessageMiddle middle = new Volunteer_MessageMiddle();
                middle.Contents = "您已报名标题为 "+ activity.Title + " 活动，请按时参加";
                middle.Name = VolunteerInfo.Name;
                middle.VID = VolunteerInfo.ID;
                middle.VNO = VolunteerInfo.VNO;

                Volunteer_Message message = _IMapper.Map<Volunteer_MessageMiddle, Volunteer_Message>(middle);
                message.ID = Guid.NewGuid().ToString();
                message.CreateDate = DateTime.Now;
                message.CreateUser = model.VID;
                message.Status = "0";

                _IVolunteer_MessageRepository.Add(message);
                int a = _IVolunteer_MessageRepository.SaveChanges();
            }

            return count;
        }


        // 最新获取不同类型的活动数 倒序排 活动数是 0 不显示
        public List<VActivityTypeCountMiddle> GetTypeCounts()
        {

            List<VActivityTypeCountMiddle> Searches = new List<VActivityTypeCountMiddle>();

            List<VBaseType> baselist = _IBaseTypeRepository.SearchInfoByWhere("2");

            foreach (var its in baselist)
            {
                 VActivityTypeCountMiddle middle = new VActivityTypeCountMiddle();
                 var counts = _IVolunteerActivityRepository.GetTypeCounts(its.ID);
                 middle.ID = its.ID;
                 middle.type = its.Name;
                 middle.count = counts;
                 Searches.Add(middle);
            }

            Searches = Searches.OrderByDescending(o => o.count).ToList();
            Searches.RemoveAll(o => o.count == 0);



            return Searches;
        }


        //志愿者活动  签到、签退接口
        public int HandleAdd(VA_HandleAddViewModel AddViewModel)
        {
            int count = 0;
            var model = _IMapper.Map<VA_HandleAddViewModel, VA_Handle>(AddViewModel);
            var VolunteerInfo = _IVolunteerInfoRepository.SearchInfoByID(model.VID);
            if (VolunteerInfo == null)
            {
                count = 6;
                return count;
            }

            var VolunteerActivity = _IVolunteerActivityRepository.GetByID(model.ContentID);

            if (!String.IsNullOrEmpty(model.Checklongitude) && !String.IsNullOrEmpty(model.Checklatitude))
            {
                //进行地址判断 活动地址方圆500米可以签到
                if (!String.IsNullOrEmpty(VolunteerActivity.longitude) && !String.IsNullOrEmpty(VolunteerActivity.latitude))
                {
                    var checks = CheckAddress(double.Parse(VolunteerActivity.longitude), double.Parse(VolunteerActivity.latitude), double.Parse(model.Checklongitude), double.Parse(model.Checklatitude), 0.5);
                    if (!checks)
                    {
                        count = 9;
                        return count;
                    }
                }
                else
                {
                    count = 8;
                    return count;
                }
            }
            else
            {
                count = 7;
                return count;
            }


            model.ID = Guid.NewGuid().ToString();
            model.VNO = VolunteerInfo.VNO;
            model.Participant = VolunteerInfo.Name;
            model.CheckTime = DateTime.Now;
            model.CreateUser = VolunteerInfo.ID;
            model.CreateDate = DateTime.Now;
            _Va_HandleRepository.Add(model);
            count = _Va_HandleRepository.SaveChanges();
            if(count==1)
            {
                string id = Guid.NewGuid().ToString();
                //获得相应积分 (签到积分)
                if (AddViewModel.type == "in")
                {
                    Volunteer_Score score = new Volunteer_Score();
                    score.ID = id;
                    score.ContentID = AddViewModel.ContentID;
                    score.tableName = "VolunteerActivity";
                    score.VID = AddViewModel.VID;
                    score.type = "in";
                    score.Score = int.Parse(VolunteerActivity.Score);
                    score.CreateUser = AddViewModel.VID;
                    score.CreateDate = DateTime.Now;

                    _IVolunteer_ScoreRepository.Add(score);
                    int b = _IVolunteer_ScoreRepository.SaveChanges();
                    if(b>0)
                    {
                        //插入到 微官网积分表
                        AIpointMiddle ipointMiddle = new AIpointMiddle();

                        ipointMiddle.ID = id;
                        ipointMiddle.UserID = VolunteerInfo.ID;
                        ipointMiddle.unionid = VolunteerInfo.unionid;
                        ipointMiddle.points = VolunteerActivity.Score;
                        ipointMiddle.type = "VolunteerActivitySignIn";
                        ipointMiddle.tableName = "TedaVolunteerDB.dbo.Volunteer_Score";
                        ipointMiddle.mobile = VolunteerInfo.Mobile;

                        int m = _IAISQLRepository.InsertPoints(ipointMiddle);
                    }
                }
                //签退 时按时长积分继续计算
                else if (AddViewModel.type == "out")
                {
                    DateTime d1 = DateTime.Now;
                    DateTime d2 = DateTime.Parse(VolunteerActivity.Stime.ToString());
                    int Hours = (d1 - d2).Hours;//小时数差
                    int jf = int.Parse(VolunteerActivity.PerMinScore) * Hours;
                    Volunteer_Score score = new Volunteer_Score();
                    score.ID = id;
                    score.ContentID = AddViewModel.ContentID;
                    score.tableName = "VolunteerActivity";
                    score.VID = AddViewModel.VID;
                    score.type = "out";
                    score.Score = int.Parse(VolunteerActivity.PerMinScore) * Hours;
                    score.CreateUser = AddViewModel.VID;
                    score.CreateDate = DateTime.Now;

                    _IVolunteer_ScoreRepository.Add(score);
                    int b = _IVolunteer_ScoreRepository.SaveChanges();
                    if(b>0 && jf>0)
                    {
                        //插入到 微官网积分表
                        AIpointMiddle ipointMiddle = new AIpointMiddle();

                        ipointMiddle.ID = id;
                        ipointMiddle.UserID = VolunteerInfo.ID;
                        ipointMiddle.unionid = VolunteerInfo.unionid;
                        ipointMiddle.points = (jf).ToString();
                        ipointMiddle.type = "VolunteerActivitySignOut";
                        ipointMiddle.tableName = "TedaVolunteerDB.dbo.Volunteer_Score";
                        ipointMiddle.mobile = VolunteerInfo.Mobile;

                        int m = _IAISQLRepository.InsertPoints(ipointMiddle);

                    }
                }
            }

            return count;
        }


        //根据 经纬度 判断是否在 dis  千米 范围内
        public bool CheckAddress(double longitude, double latitude, double nowlon, double nowlat ,double dis)
        {
            var res = false;
            double r = 6371;//地球半径千米  
            //double dis = 0.5;// 0.5千米距离  
            double dlng = 2 * Math.Asin(Math.Sin(dis / (2 * r)) / Math.Cos(latitude * Math.PI / 180));
            dlng = dlng * 180 / Math.PI;//角度转为弧度  
            double dlat = dis / r;
            dlat = dlat * 180 / Math.PI;
            double minlat = latitude - dlat;
            double maxlat = latitude + dlat;
            double minlng = longitude - dlng;
            double maxlng = longitude + dlng;

            if (nowlon > minlng && nowlon < maxlng && minlat < nowlat && maxlat > nowlat)
            {
                res = true;
            }
            return res;
        }


        // 志愿者活动 活动中 上传现场服务照片
        public int SubmitImg(VA_HandleImgAddModel AddViewModel)
        {
            int c = 0;

            var model = _IMapper.Map<VA_HandleImgAddModel, VA_Handle>(AddViewModel);
            var VolunteerInfo = _IVolunteerInfoRepository.SearchInfoByID(model.VID);
            if (VolunteerInfo == null)
            {
                c = 9;
                return c;
            }


            model.ID = Guid.NewGuid().ToString();
            model.VNO = VolunteerInfo.VNO;
            model.Participant = VolunteerInfo.Name;
            model.type = "img";
            model.CheckTime = DateTime.Now;
            model.CreateUser = VolunteerInfo.ID;
            model.CreateDate = DateTime.Now;
            _Va_HandleRepository.Add(model);
            int count = _Va_HandleRepository.SaveChanges();


            var AttachmentInfo = _IMapper.Map<List<VAttachmentAddViewModel>,List<VAttachment>>(AddViewModel.VAttachmentAddList);
            foreach (var item in AttachmentInfo)
            {
                item.ID = Guid.NewGuid().ToString();
                item.formid = model.ID;
                item.type = "HDTP";
                item.Status = "0";
                item.CreateUser = model.VID;
                item.CreateDate = DateTime.Now;
                _IVAttachmentRepository.Add(item);
                c = _IVAttachmentRepository.SaveChanges() + c;
            }

            return c;
        }


        //获取我的活动信息
        public List<VolunteerActivitySearchMiddle> GetMyAllInfo(string VID)
        {
            List<String> Infos = _IVA_SignRepository.GetMyList(VID);
            List<VolunteerActivitySearchMiddle> Searches = new List<VolunteerActivitySearchMiddle>();
            //默认地址 天津市滨海新区宏达街19号
            string longitude = "117.70889";
            string latitude = "39.02749";
            foreach (var item in Infos)
            {
                VolunteerActivity middle = new VolunteerActivity();
                middle = _IVolunteerActivityRepository.GetByID(item.ToString());
                //计算 已报名人数
                int nums = _IVA_SignRepository.GetSingNum(middle.ID);
                middle.bak4 = nums.ToString();

                //活动报名人数上限
                int totalnum = _IVActivity_Relate_TypeRepository.GetSum(middle.ID, "");
                middle.bak5 = totalnum.ToString();
                var SearchMiddlecs = _IMapper.Map<VolunteerActivity, VolunteerActivitySearchMiddle>(middle);
                Searches.Add(SearchMiddlecs);
            }
            return Searches;
        }


        ///获取我的活动信息(志愿者ID、所属社区ID、活动类型ID、状态 等)
        public List<VolunteerActivitySearchMiddle> GetMyAllInfoByWhere(GetMyListViewModel vidModel)
        {
            List<String> Infos = _IVA_SignRepository.GetMyList(vidModel.VID);
            List<VolunteerActivitySearchMiddle> Searches = new List<VolunteerActivitySearchMiddle>();

            List<VolunteerActivity> middle = _IVolunteerActivityRepository.GetByIDList(Infos);
            Searches = _IMapper.Map<List<VolunteerActivity>, List<VolunteerActivitySearchMiddle>>(middle);


            if (!String.IsNullOrEmpty(vidModel.CommunityID))
            {
                Searches = Searches.Where(o => o.CommunityID.Contains(vidModel.CommunityID)).ToList();
            }
            if (!String.IsNullOrEmpty(vidModel.TypeIDs))
            {
                Searches = Searches.Where(o => o.TypeIDs.Contains(vidModel.TypeIDs)).ToList();
            }
            if (!String.IsNullOrEmpty(vidModel.Status))
            {
                Searches = Searches.Where(o => o.Status.Contains(vidModel.Status)).ToList();
            }
            foreach (var item in Searches)
            {
  
                //计算 已报名人数
                int nums = _IVA_SignRepository.GetSingNum(item.ID);
                item.bak4 = nums.ToString();

                //活动报名人数上限
                int totalnum = _IVActivity_Relate_TypeRepository.GetSum(item.ID, "");
                item.bak5 = totalnum.ToString();
   
            }

            return Searches;
        }


        /// 获取该志愿者针对该活动 签到签退信息 ( 活动ID,志愿者ID)
        public VA_HandleGetMyResModel GetMyAHandleInfo(ContentIDandVIDModel model)
        {
            VA_HandleGetMyResModel MyResModel = new VA_HandleGetMyResModel();
            List<VA_Handle> handleList = _Va_HandleRepository.GetMySign(model.VID, model.ContentID);

            if (handleList.Count > 0)
            {
                foreach (var itme in handleList)
                {
                    if (!string.IsNullOrEmpty(itme.type))
                    {
                        //签到
                        if (itme.type == "in")
                        {
                            MyResModel.SignUpTime = itme.CheckTime.ToString();
                            MyResModel.SignUpAddress = itme.CheckAddress;
                        }
                        //上传现场图片
                        if (itme.type == "img")
                        {
                            List<VAttachment> list = _IVAttachmentRepository.GetMyList(itme.ID);
                            List<VAttachmentAddViewModel> newlist = _IMapper.Map<List<VAttachment>, List<VAttachmentAddViewModel>>(list);
                            MyResModel.VAttachmentList = newlist;
                        }
                        //签退
                        if (itme.type == "out")
                        {
                            MyResModel.SignOutTime = itme.CheckTime.ToString();
                            MyResModel.SignOutAddress = itme.CheckAddress;
                        }
                    }
                    else
                    {
                        //签到
                        if (itme.type == "in")
                        {
                            MyResModel.SignUpTime = "";
                            MyResModel.SignUpAddress = "";
                        }
                        //上传现场图片
                        if (itme.type == "img")
                        {
                            MyResModel.VAttachmentList = null;
                        }
                        //签退
                        if (itme.type == "out")
                        {
                            MyResModel.SignOutTime = "";
                            MyResModel.SignOutAddress = "";
                        }
                    }

                }
            }
            else
            {

                    MyResModel.SignUpTime = "";
                    MyResModel.SignUpAddress = "";
                    MyResModel.VAttachmentList = null;
                    MyResModel.SignOutTime = "";
                    MyResModel.SignOutAddress = "";
               
            }

         
            return MyResModel;
        }


        //首页 签到 定位当前时段活动 返回活动ID
        public string HomeGetContentID(string VID)
        {
            var result = string.Empty;
            result = _ISQLRepository.GetNowContent(VID);
            return result;
        }


        //获取本志愿者针对该活动信息 所有状态 （活动ID标识ContentID、志愿者唯一VID、擅长技能TypeID为空不传参数 ）
        public BaseViewModel GetVAStatus(string VID, string ContentID)
        {
            BaseViewModel result = new BaseViewModel();

            //获取 该活动信息
            VolunteerActivity item = _IVolunteerActivityRepository.GetByID(ContentID);
            //判断是否报名
            VA_Sign va_Sign = _IVA_SignRepository.GetNewSign(VID, ContentID);
            //是否 已经开始签到
            VA_Handle res = _Va_HandleRepository.GetNewSign(VID, ContentID);

            if (!String.IsNullOrEmpty(item.ID))
            {
                //判断 当前时间 与 报名开始时间 比较
                if (item.SignStime != null && DateTime.Now < item.SignStime)
                {
                    result.ResponseCode = 11;
                    result.Message = "活动报名时间为："+ item.SignStime.ToString() + " 至 "+ item.SignEtime.ToString() +"，敬请期待。";
                }
                if (item.SignStime != null &&  item.SignEtime != null && DateTime.Now > item.SignStime && DateTime.Now < item.SignEtime)
                {
                    if (!String.IsNullOrEmpty(va_Sign.ID))
                    {
                        //已报名
                        result.ResponseCode = 1;
                        result.Message = "已报名";
                    }
                    else
                    {
                        result.ResponseCode = 12;
                        result.Message = "我要报名";
                    }
                }
                if (item.SignEtime != null && item.Stime != null && DateTime.Now > item.SignEtime && DateTime.Now < item.Stime)
                {
                    if (!String.IsNullOrEmpty(va_Sign.ID))
                    {
                        //已报名
                        result.ResponseCode = 1;
                        result.Message = "已报名";
                    }
                    else
                    {
                        result.ResponseCode = 13;
                        result.Message = "报名已截止";
                    }
                }
                if (item.Stime != null && item.Etime != null && DateTime.Now > item.Stime && DateTime.Now < item.Etime)
                {
                    //判断是否报名
                    if (!String.IsNullOrEmpty(va_Sign.ID))
                    {
                        if (!String.IsNullOrEmpty(res.ID))
                        {
                            if (res.type.Equals("in"))
                            {
                                result.ResponseCode = 3;
                                result.Message = "上传现场图片";
                            }
                            else if (res.type.Equals("img"))
                            {
                                result.ResponseCode = 4;
                                result.Message = "签退";
                                //有签退 时间限制
                                if (item.SignOutStime != null && item.SignOutEtime != null && (DateTime.Now < item.SignOutStime))
                                {
                                    //判断是否正常 执行到签退操作
                                    if (result.ResponseCode == 4)
                                    {
                                        result.ResponseCode = 41;
                                        result.Message = "未到签退时间请稍后。";
                                    }
                                }
                                //有签退 时间限制
                                if (item.SignOutStime != null && item.SignOutEtime != null && (DateTime.Now > item.SignOutEtime))
                                {
                                    //判断是否正常 执行到签退操作
                                    if (result.ResponseCode == 4)
                                    {
                                        result.ResponseCode = 42;
                                        result.Message = "已过签退时间，无法签退。";
                                    }
                                    else
                                    {
                                        result.ResponseCode = 31;
                                        result.Message = "已到签退时间您未上传现场图片，无法进行签退操作。";
                                    }
                                }
                           
                            }
                            else if (res.type.Equals("out"))
                            {
                                result.ResponseCode = 5;
                                result.Message = "本次活动已完成，谢谢参与";
                            }
                        }
                        else
                        {


                            //有签到 时间限制
                            if (item.SignUpStime != null && item.SignUpEtime != null && (DateTime.Now < item.SignUpStime))
                            {
                                result.ResponseCode = 21;
                                result.Message = "未到签到时间请稍后。";
                            }
                            //有签到 时间限制
                            else if (item.SignUpStime != null && item.SignUpEtime != null && (DateTime.Now > item.SignUpEtime))
                            {
                                result.ResponseCode = 22;
                                result.Message = "已过签到时间，无法签到，后续操作亦无法执行。";
                            }
                            else
                            {
                                //正常签到
                                result.ResponseCode = 2;
                                result.Message = "签到";
                            }
                        }
                    }
                    else
                    {
                        result.ResponseCode = 14;
                        result.Message = "报名进行中";
                    }
                }
                if (item.Etime != null && DateTime.Now > item.Etime)
                {
                    result.ResponseCode = 15;
                    result.Message = "活动已结束";
                }
            }
            else
            {
                result.ResponseCode = 10;
                result.Message = "该志愿活动信息有问题，请联系志愿者后台调整。";
            }
            return result;
        }

        //判断是否是注册用户
        public bool CheckInfos(string ID)
        {
            bool result = false;
            if (!String.IsNullOrEmpty(ID))
            {
                var res = _IVolunteerInfoRepository.SearchInfoByID(ID);
                if (res != null && res.ID != null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }

            return result;
        }

        //首页 志愿活动推荐( 志愿者VID )
        //首页 志愿活动推荐( 志愿者VID )
        public List<VolunteerActivitySearchMiddle> GetMyRecommendList(string VID)
        {
            //无擅长技能
            string SkillID = "AB1EC380-4D47-45AB-9C2B-F2EE19B7AE9C";

            List<VolunteerActivitySearchMiddle> Searches = new List<VolunteerActivitySearchMiddle>();
            List<VolunteerActivity> lists = _IVolunteerActivityRepository.GetMyRecommendList();
            //默认地址 天津市滨海新区宏达街19号
            string longitude = "117.70889";
            string latitude = "39.02749";

            //判断是否是注册用户
            if (CheckInfos(VID))
            {  
                //根据志愿者擅长技能推荐  与  根据志愿者服务领域推荐
                var typelist = _IVolunteer_Relate_TypeRepository.GetMyTypeList(VID);
                List<string> typeIDs = new List<string>();
                foreach (var item in lists)
                {
                    //判断该志愿者是否已经报名
                    VA_Sign Sign = _IVA_SignRepository.GetNewSign(VID, item.ID);
                    if (Sign != null && Sign.ID != null)
                    {
                        //已报名
                        item.bak1 = "1";
                    }
                    else
                    {
                        //未报名
                        item.bak1 = "0";
                    }

                    //判断是否已经活动开始
                    if (item.Stime <= DateTime.Now)
                    {
                        item.bak1 = "2";
                    }

                    //判断是否已经活动结束
                    if (item.Etime <= DateTime.Now)
                    {
                        item.bak1 = "3";
                    }
                    //根据志愿者擅长技能推荐  与  根据志愿者服务领域推荐
                    //活动服务领域、擅长技能
                    var activityType = _IVActivity_Relate_TypeRepository.GetRelateList(item.ID);
                    foreach (var type in activityType)
                    {
                        typeIDs.Add(type.TypeID);
                    }
                    if (typeIDs.Contains(SkillID))
                    {
                        item.bak2 = "3";
                    }
                    else
                    {
                        bool isContains = false;
                        typelist.ForEach(o => isContains |= typeIDs.Contains(o.TypeID));
                        if (isContains)
                        {
                            item.bak2 = "3";
                        }
                    }

                    var VolunteerInfo = _IVolunteerInfoRepository.SearchInfoByID(VID);
                    //根据活动地址推荐 两公里以内
                    string address = "天津市滨海新区开发区" + VolunteerInfo.Subdistrict + VolunteerInfo.Address;

                    if (address != "" && address != null)
                    {
                        string content = GetFunction(address);
                        JObject jo = (JObject)JsonConvert.DeserializeObject(content);//或者JObject jo = JObject.Parse(jsonText);
                        if (jo["status"].ToString() == "0")
                        {
                            longitude = jo["result"]["location"]["lng"].ToString();
                            latitude = jo["result"]["location"]["lat"].ToString();
                        }
                    }


                    //根据活动地址推荐 两公里以内
                    var status = CheckAddress(double.Parse(longitude), double.Parse(latitude), double.Parse(item.longitude), double.Parse(item.latitude), 2.0);
                    if (status)
                    {
                        item.bak2 = "4";
                    }
                    else
                    {
                        item.bak2 = "0";
                    }

                    //判断 活动地址与志愿者注册详细地址间距离
                    double dis = GetDistance(double.Parse(longitude), double.Parse(latitude), double.Parse(item.longitude), double.Parse(item.latitude));
                    item.bak3 = dis.ToString("f2");


                    //计算 已报名人数
                    int nums = _IVA_SignRepository.GetSingNum(item.ID);
                    item.bak4 = nums.ToString();

                    //活动报名人数上限
                    int totalnum = _IVActivity_Relate_TypeRepository.GetSum(item.ID, "");
                    item.bak5 = totalnum.ToString();

                    if (item.Status == "1")
                    {
                        item.bak2 = "5";
                    }
                    else
                    {
                        item.bak2 = "0";
                    }

                }
            }
            else
            {
                

                //根据志愿者擅长技能推荐  与  根据志愿者服务领域推荐
                var typelist = _IVolunteer_Relate_TypeRepository.GetMyTypeList(VID);


                List<string> typeIDs = new List<string>();
                foreach (var item in lists)
                {
                    
                    //活动服务领域、擅长技能
                    var activityType = _IVActivity_Relate_TypeRepository.GetRelateList(item.ID);
                    foreach (var type in activityType)
                    {
                        typeIDs.Add(type.TypeID);
                    }
                    if (typeIDs.Contains(SkillID))
                    {
                        item.bak2 = "3";
                    }
                    else
                    {
                        bool isContains = false;
                        typelist.ForEach(o => isContains |= typeIDs.Contains(o.TypeID));
                        if (isContains)
                        {
                            item.bak2 = "3";
                        }
                    }


                    //根据活动地址推荐 两公里以内
                    var status = CheckAddress(double.Parse(longitude), double.Parse(latitude), double.Parse(item.longitude), double.Parse(item.latitude), 2.0);
                    if (status)
                    {
                        item.bak2 = "4";
                    }
                    else
                    {
                        item.bak2 = "0";
                    }

                    //判断 活动地址与志愿者注册详细地址间距离
                    double dis = GetDistance(double.Parse(longitude), double.Parse(latitude), double.Parse(item.longitude), double.Parse(item.latitude));
                    item.bak3 = dis.ToString("f2");


                    //计算 已报名人数
                    int nums = _IVA_SignRepository.GetSingNum(item.ID);
                    item.bak4 = nums.ToString();

                    //活动报名人数上限
                    int totalnum = _IVActivity_Relate_TypeRepository.GetSum(item.ID, "");
                    item.bak5 = totalnum.ToString();

                    //未报名
                    item.bak1 = "0";
                    //判断是否已经活动开始
                    if (item.Stime <= DateTime.Now)
                    {
                        item.bak1 = "2";
                    }
                    //判断是否已经活动结束
                    if (item.Etime <= DateTime.Now)
                    {
                        item.bak1 = "3";
                    }

                    //活动状态
                    if (item.Status == "1")
                    {
                        item.bak2 = "5";
                    }
                    else
                    {
                        item.bak2 = "0";
                    }

                }
            }


            Searches = _IMapper.Map<List<VolunteerActivity>, List<VolunteerActivitySearchMiddle>>(lists);
            Searches = Searches.OrderBy(o=>o.bak1).ThenByDescending(o => o.bak2).ThenBy(o => o.bak3).ToList();
            return Searches;
        }


        //志愿者取消 志愿活动报名( 志愿者VID ,活动ID)
        public BaseViewModel CancelVASign(ContentIDandVIDModel AddViewModel)
        {
            BaseViewModel result = new BaseViewModel();
            //活动开始前2小时 就不能取消
            //获取 该活动信息
            VolunteerActivity item = _IVolunteerActivityRepository.GetByID(AddViewModel.ContentID);
            if (item.Stime > DateTime.Now.AddHours(2))
            {
                //删除 报名记录
                VA_Sign sign = _IVA_SignRepository.GetNewSign(AddViewModel.VID, AddViewModel.ContentID);
                _IVA_SignRepository.RemoveNew(sign);
                int a = _IVA_SignRepository.SaveChanges();
                if (a > 0)
                {
                    var VolunteerInfo = _IVolunteerInfoRepository.SearchInfoByID(AddViewModel.VID);
                    //提示信息：您已取消标题为 XXX 活动 
                    Volunteer_MessageMiddle middle = new Volunteer_MessageMiddle();
                    middle.Contents = "您已取消标题为 " + item.Title + " 活动。";
                    middle.Name = VolunteerInfo.Name;
                    middle.VID = VolunteerInfo.ID;
                    middle.VNO = VolunteerInfo.VNO;

                    Volunteer_Message message = _IMapper.Map<Volunteer_MessageMiddle, Volunteer_Message>(middle);
                    message.ID = Guid.NewGuid().ToString();
                    message.CreateDate = DateTime.Now;
                    message.CreateUser = VolunteerInfo.ID;
                    message.Status = "0";

                    _IVolunteer_MessageRepository.Add(message);
                    int c = _IVolunteer_MessageRepository.SaveChanges();

                    result.Message = " 取消报名操作成功。";
                    result.ResponseCode = 200;
                }
                else
                {
                    result.Message = "取消报名操作失败。";
                    result.ResponseCode = 300;
                }
            }
            else
            {
                result.Message = "活动开始前2小时,不能取消报名。";
                result.ResponseCode = 400;
            }


            return result;
        }


        //根据详细地址获取 经纬度
        public string GetFunction(string address)
        {
            string url = "http://apis.map.qq.com/ws/geocoder/v1/?address=" + address + "&key=2S7BZ-KVGKG-QKHQ5-I6L44-5M7BT-FIBJA";
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            myRequest.Method = "GET";
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string content = reader.ReadToEnd();
            reader.Close();
            return content;

        }


        //志愿者成功参与的 志愿活动数（只有签到过才算成功）
        public BaseViewModel GetMyActivityNums(SearchByVIDModel vidModel)
        {
            BaseViewModel result = new BaseViewModel();
            string times = _Va_HandleRepository.GetMyInTimes(vidModel.VID);
            result.ResponseCode = int.Parse(times);
            result.Message = "查询成功";
            return result;
        }


        /// <summary>
        /// 计算两点位置的距离，返回两点的距离，单位：米
        /// 该公式为GOOGLE提供，误差小于0.2米
        /// </summary>
        /// <param name="lng1">第一点经度</param>
        /// <param name="lat1">第一点纬度</param>        
        /// <param name="lng2">第二点经度</param>
        /// <param name="lat2">第二点纬度</param>
        /// <returns></returns>
        public static double GetDistance(double lng1, double lat1, double lng2, double lat2)
        {
            double EARTH_RADIUS = 6378137;
            double radLat1 = Rad(lat1);
            double radLng1 = Rad(lng1);
            double radLat2 = Rad(lat2);
            double radLng2 = Rad(lng2);
            double a = radLat1 - radLat2;
            double b = radLng1 - radLng2;
            double result = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) + Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2))) * EARTH_RADIUS;
            result = result / 1000;
            return result;
        }

        /// <summary>
        /// 经纬度转化成弧度
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        private static double Rad(double d)
        {
            return (double)d * Math.PI / 180d;
        }


    }
}
