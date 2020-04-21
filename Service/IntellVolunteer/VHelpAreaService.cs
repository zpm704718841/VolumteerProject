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
using System.Web;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace Dto.Service.IntellVolunteer
{
  
    public class VHelpAreaService : IVHelpAreaService
    {
        private readonly IVHelpAreaRepository _IVHelpAreaRepository;
        private readonly IVolunteerInfoRepository _IVolunteerInfoRepository;
        private readonly IVolunteer_Relate_TypeRepository _IVolunteer_Relate_TypeRepository;
        private readonly IVHA_SignRepository _IVHA_SignRepository;
        private readonly IVHA_HandleRepository _IVHA_HandleRepository;
        private readonly IBaseTypeRepository _IBaseTypeRepository;
        private readonly IVAttachmentRepository _IVAttachmentRepository;
        private readonly IMapper _IMapper;
        private readonly IVolunteer_MessageRepository _IVolunteer_MessageRepository;


        public VHelpAreaService(IVHelpAreaRepository iInfoRepository, IMapper mapper, IVolunteerInfoRepository infoRepository, IVHA_SignRepository vha_SignRepository,IVolunteer_Relate_TypeRepository volunteer_Relate_TypeRepository,IBaseTypeRepository baseTypeRepository, IVAttachmentRepository AttachmentRepository, IVHA_HandleRepository handleRepository, IVolunteer_MessageRepository messageRepository)
        {
            _IVHelpAreaRepository = iInfoRepository;
            _IVolunteerInfoRepository = infoRepository;
            _IVHA_SignRepository = vha_SignRepository;
            _IVolunteer_Relate_TypeRepository = volunteer_Relate_TypeRepository;
            _IBaseTypeRepository = baseTypeRepository;
            _IVAttachmentRepository = AttachmentRepository;
            _IVHA_HandleRepository = handleRepository;
            _IMapper = mapper;
            _IVolunteer_MessageRepository = messageRepository;
        }



        //根据条件查询互助信息 （互助标题Title、所属组织架构Community,互助地址Address,擅长技能Type）
        public List<VHelpAreaSearchMiddle> VHelpArea_Search(AllSearchViewModel VSearchViewModel)
        {

            List<VHelpArea> Infos = _IVHelpAreaRepository.SearchInfoByWhere(VSearchViewModel);

            List<VHelpAreaSearchMiddle> Searches = new List<VHelpAreaSearchMiddle>();

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
                //判断 互助地址与志愿者注册详细地址间距离
                double dis = GetDistance(double.Parse(longitude), double.Parse(latitude), double.Parse(item.longitude), double.Parse(item.latitude));
                item.bak3 = dis.ToString("f2");

                //获取本志愿者针对该互助信息 所有状态
                BaseViewModel baseViewModel = GetVHAStatus(VSearchViewModel.VID, item.ID);
                item.bak2 = baseViewModel.ResponseCode.ToString();

                var SearchMiddlecs = _IMapper.Map<VHelpArea, VHelpAreaSearchMiddle>(item);
                Searches.Add(SearchMiddlecs);

            }
 
            return Searches;
        }

        //互助ID标识ContentID、
        public List<VHelpAreaSearchMiddle> GetVHelpAreaSearch(SearchByContentIDModel  model)
        {

            VHelpArea Infos = _IVHelpAreaRepository.SearchInfoByID(model.ContentID);
            List<VHelpAreaSearchMiddle> Searches = new List<VHelpAreaSearchMiddle>();
            var SearchMiddlecs = _IMapper.Map<VHelpArea, VHelpAreaSearchMiddle>(Infos);
            Searches.Add(SearchMiddlecs);
            return Searches;
        }


        //获取所有互助信息  （无参数）
        public List<VHelpAreaSearchMiddle> VHelpArea_All()
        {
            List<VHelpArea> Infos = _IVHelpAreaRepository.GetHelpAreaAll();

            List<VHelpAreaSearchMiddle> Searches = new List<VHelpAreaSearchMiddle>();

            foreach (var item in Infos)
            {
                var SearchMiddlecs = _IMapper.Map<VHelpArea, VHelpAreaSearchMiddle>(item);
                Searches.Add(SearchMiddlecs);

            }
            return Searches;
        }


        //验证是否 认领该互助信息
        public string CheckSignAdd(string VID, string ContentID)
        {
            string result = "false";
            List<String> Infos = _IVHA_SignRepository.GetMyList(VID);
            if (!Infos.Contains(ContentID))
            {
                result = "true";
            }
            return result;
        }

        //验证是否 认领、上传结果图片 该互助信息
        public string CheckSignHandleAdd(string VID, string ContentID)
        {
            string result = "false";
            List<String> Infos = _IVHA_SignRepository.GetMyList(VID);
            if (!Infos.Contains(ContentID))
            {
                result = "true";
            }
            return result;
        }


        //志愿者互助信息认领提交
        public int SignAdd(VHA_SignAddViewModel AddViewModel)
        {
            //普通志愿者  无擅长技能
            string SkillID = "AB1EC380-4D47-45AB-9C2B-F2EE19B7AE9C";
            //区外小区 
            string CommunityID = "D6E82522-7D21-44EF-BC81-31CAC7DA19CE";

            int count = 0;
            if (String.IsNullOrEmpty(AddViewModel.ContentID) || String.IsNullOrEmpty(AddViewModel.VID) || String.IsNullOrEmpty(AddViewModel.TypeID))
            {
                count = 7;
                return count;
            }


            ///发布者不能认领自己发布的互助任务
            VHelpArea area = _IVHelpAreaRepository.SearchInfoByID(AddViewModel.ContentID);
            if (area.CreateUser == AddViewModel.VID)
            {
                count = 5;
                return count;
            }

            var model = _IMapper.Map<VHA_SignAddViewModel, VHA_Sign>(AddViewModel);
            var VolunteerInfo = _IVolunteerInfoRepository.SearchInfoByID(model.VID);
            if (VolunteerInfo == null)
            {
                count = 6;
                return count;
            }
      

            //判断是否是   泰达街社区居民或机关单位工作人员  认领
            if (VolunteerInfo.CommunityID == CommunityID && VolunteerInfo.UnitID  == "")
            {
                count = 8;
                return count;
            }

            //判断 志愿类型 判断是否是 普通志愿者
            if (AddViewModel.TypeID != SkillID)
            {
                //判断 是否是专业志愿服务 则需验证 报名者是否符合
                if (_IBaseTypeRepository.CheckStatus(AddViewModel.TypeID))
                {
                    //判断是否有该类型资质认证
                    if (!_IVolunteer_Relate_TypeRepository.CheckInfo(AddViewModel.TypeID, AddViewModel.VID))
                    {
                        count = 9;
                        return count;
                    }
                }
            }
 

            model.ID = Guid.NewGuid().ToString();
            model.VNO = VolunteerInfo.VNO;
            model.Participant = VolunteerInfo.Name;
            model.Status = "0";
            model.flag = "1";
            model.CreateUser = VolunteerInfo.ID;
            model.CreateDate = DateTime.Now;
            _IVHA_SignRepository.Add(model);
            count = _IVHA_SignRepository.SaveChanges();

            //提示信息：您已认领XXX互助信息，等待审核
            Volunteer_MessageMiddle middle = new Volunteer_MessageMiddle();
            middle.Contents = "您已认领标题为 " + area.Title + " 互助信息，等待审核";
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

            return count;
        }


        //志愿者上传互助信息处理结果
        public int HandleAdd(VHA_HandleAddModel AddViewModel)
        {
            int a = 0;
            var VolunteerInfo = _IVolunteerInfoRepository.SearchInfoByID(AddViewModel.VID);

            if (VolunteerInfo == null)
            {
                a = 9;
                return a;
            }

            var model = _IMapper.Map<VHA_HandleAddModel, VHA_Handle>(AddViewModel);



            model.ID = Guid.NewGuid().ToString();
            model.VNO = VolunteerInfo.VNO;
            model.Participant = VolunteerInfo.Name;
            model.Status = "0";
            model.CreateUser = VolunteerInfo.ID;
            model.CreateDate = DateTime.Now;

            _IVHA_HandleRepository.Add(model);
            a = _IVHA_HandleRepository.SaveChanges();

            int c = 0;
            var AttachmentInfo = _IMapper.Map<List<VAttachmentAddViewModel>, List<VAttachment>>(AddViewModel.VAttachmentAddList);
            foreach (var item in AttachmentInfo)
            {
                item.ID = Guid.NewGuid().ToString();
                item.formid = model.ID;
                item.type = "HZJG";
                item.Status = "0";
                item.CreateUser = AddViewModel.VID;
                item.CreateDate = DateTime.Now;
                _IVAttachmentRepository.Add(item);
                c = _IVAttachmentRepository.SaveChanges() + c;
            }


            VHA_Sign vha_Sign = new VHA_Sign();
            vha_Sign.ID= Guid.NewGuid().ToString();
            vha_Sign.ContentID = AddViewModel.ContentID;
            vha_Sign.VID = AddViewModel.VID;
            vha_Sign.VNO = VolunteerInfo.VNO;
            vha_Sign.Participant = VolunteerInfo.Name;
            vha_Sign.Status = "0";
            vha_Sign.flag = "2";
            vha_Sign.CreateUser = VolunteerInfo.ID;
            vha_Sign.CreateDate = DateTime.Now;
            vha_Sign.bak1 = model.ID;

            _IVHA_SignRepository.Add(vha_Sign);
            int d = _IVHA_SignRepository.SaveChanges();

            ///获取本次互助任务信息
            VHelpArea area = _IVHelpAreaRepository.SearchInfoByID(AddViewModel.ContentID);
            //提示信息：您已上传标题为XXX互助信息的处理结果，等待审核
            Volunteer_MessageMiddle middle = new Volunteer_MessageMiddle();
            middle.Contents = "您已上传标题为 " + area.Title + " 互助信息的处理结果，等待审核";
            middle.Name = VolunteerInfo.Name;
            middle.VID = VolunteerInfo.ID;
            middle.VNO = VolunteerInfo.VNO;

            Volunteer_Message message = _IMapper.Map<Volunteer_MessageMiddle, Volunteer_Message>(middle);
            message.ID = Guid.NewGuid().ToString();
            message.CreateDate = DateTime.Now;
            message.CreateUser = model.VID;
            message.Status = "0";
            _IVolunteer_MessageRepository.Add(message);
            int f = _IVolunteer_MessageRepository.SaveChanges();


            return a;
        }


        //获取我的互助信息
        public List<VHelpAreaSearchMiddle> GetMyAllInfo(string VID)
        {
            List<String> Infos = _IVHA_SignRepository.GetMyList(VID);
            List<VHelpAreaSearchMiddle> Searches = new List<VHelpAreaSearchMiddle>();

            //默认地址 天津市滨海新区宏达街19号 (显示距离)
            string longitude = "117.70889";
            string latitude = "39.02749";

            var VolunteerInfo = _IVolunteerInfoRepository.SearchInfoByID(VID);
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
                VHelpArea middle = new VHelpArea();
                middle = _IVHelpAreaRepository.SearchInfoByID(item.ToString());

                //判断 互助地址与志愿者注册详细地址间距离
                double dis = GetDistance(double.Parse(longitude), double.Parse(latitude), double.Parse(middle.longitude), double.Parse(middle.latitude));
                middle.bak3 = dis.ToString("f2");

                //获取本志愿者针对该互助信息 所有状态
                BaseViewModel baseViewModel = GetVHAStatus(VID, item.ToString());
                middle.bak2 = baseViewModel.ResponseCode.ToString();

                var SearchMiddlecs = _IMapper.Map<VHelpArea, VHelpAreaSearchMiddle>(middle);
                Searches.Add(SearchMiddlecs);

            }
            return Searches;
        }


        //获取我的互助信息(志愿者ID、所属社区ID、活动类型ID、状态 等)
        public List<VHelpAreaSearchMiddle> GetMyAllInfoByWhere(GetMyListViewModel vidModel)
        {
            List<String> Infos = _IVHA_SignRepository.GetMyList(vidModel.VID);
            List<VHelpAreaSearchMiddle> Searches = new List<VHelpAreaSearchMiddle>();

            List<VHelpArea> middle = _IVHelpAreaRepository.GetByIDList(Infos);
           
            if (!String.IsNullOrEmpty(vidModel.CommunityID))
            {
                middle = middle.Where(o => o.CommunityID.Contains(vidModel.CommunityID)).ToList();
            }
            if (!String.IsNullOrEmpty(vidModel.Type))
            {
                middle = middle.Where(o => o.ProblemType.Contains(vidModel.Type)).ToList();
            }
   

            //默认地址 天津市滨海新区宏达街19号 (显示距离)
            string longitude = "117.70889";
            string latitude = "39.02749";

            var VolunteerInfo = _IVolunteerInfoRepository.SearchInfoByID(vidModel.VID);
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

            foreach (var item in middle)
            {
                //判断 互助地址与志愿者注册详细地址间距离
                double dis = GetDistance(double.Parse(longitude), double.Parse(latitude), double.Parse(item.longitude), double.Parse(item.latitude));
                item.bak3 = dis.ToString("f2");

                //获取本志愿者针对该互助信息 所有状态
                BaseViewModel baseViewModel = GetVHAStatus(vidModel.VID, item.ID);
                item.bak2 = baseViewModel.ResponseCode.ToString();
            }

            if (!String.IsNullOrEmpty(vidModel.Status))
            {
                middle = middle.Where(o => o.bak2.Contains(vidModel.Status)).ToList();
            }

            Searches = _IMapper.Map<List<VHelpArea>, List<VHelpAreaSearchMiddle>>(middle);

            return Searches;
        }



        //获取本志愿者针对该互助信息 所有状态 （互助ID标识ContentID、志愿者唯一VID、擅长技能TypeID为空不传参数 ）
        public BaseViewModel GetVHAStatus(string VID ,string ContentID)
        {
            BaseViewModel result = new BaseViewModel();

            VHA_Sign res = _IVHA_SignRepository.GetNewSign(VID, ContentID);
            if (!String.IsNullOrEmpty(res.ID))
            {
                if (res.flag.Equals("1"))
                {
                    if (res.Status.Equals("0"))
                    {
                        result.ResponseCode = 1;
                        result.Message = "认领审核中";
                    }
                    else if(res.Status.Equals("1"))
                    {
                        result.ResponseCode = 2;
                        result.Message = "认领审核通过，上传处理结果";
                    }
                    else if(res.Status.Equals("2"))
                    {
                        result.ResponseCode = 3;
                        result.Message = "认领审核不通过";
                    }
                    else if(res.Status.Equals("3"))
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
                        result.Message = "处理结果审核中";
                    }
                    else if (res.Status.Equals("1"))
                    {
                        result.ResponseCode = 6;
                        result.Message = "处理结果审核通过，任务完结";
                    }
                    else if(res.Status.Equals("2"))
                    {
                        result.ResponseCode = 7;
                        result.Message = "处理结果审核不通过，请志愿者重新上传";
                    }
                    else if(res.Status.Equals("3"))
                    {
                        if(!String.IsNullOrEmpty(res.opinion))
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
                VHelpArea item = _IVHelpAreaRepository.SearchInfoByID(ContentID);
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


        //志愿者互助信息退出功能 （互助ID标识ContentID、志愿者唯一VID、擅长技能TypeID为空不传参数）
        public string SetVHAStatusBack(string VID, string ContentID)
        {
            string result = String.Empty;
            result = _IVHA_SignRepository.SetStatusBack(VID,ContentID);
            if (result == "success")
            {
                VHelpArea res = new VHelpArea();

                //更新 互助 表状态 可重新被认领
                res = _IVHelpAreaRepository.SearchInfoByID(ContentID);
                if (res != null)
                {
                    res.Status = "0";
                    _IVHelpAreaRepository.Update(res);
                    int a = _IVHelpAreaRepository.SaveChanges();
                }
                else
                {
                    result = "fail";
                }
            }
            return result;
        }


        // 获取本志愿者针对该互助信息 上传结果信息 （互助ID标识ContentID、志愿者唯一VID、擅长技能TypeID为空不传参数 ）
        public List<VHA_HandleGetMyResModel> GetMyHandleInfo(string VID, string ContentID)
        {
            List<VHA_HandleGetMyResModel> MyList = new List<VHA_HandleGetMyResModel>();
     
            List<VHA_Handle> handleList = _IVHA_HandleRepository.GetMyHandle(VID, ContentID);
            foreach(var itme in handleList)
            {
                VHA_HandleGetMyResModel resModel = new VHA_HandleGetMyResModel();
                List<VAttachment> list = _IVAttachmentRepository.GetMyList(itme.ID);
                List<VAttachmentAddViewModel> newlist = _IMapper.Map<List<VAttachment>, List<VAttachmentAddViewModel>>(list);


                //获取审核 信息
                VHA_Sign res = _IVHA_SignRepository.GetMySign(itme.ID);
                resModel.opinion = res.opinion;
                resModel.opinionTime = res.UpdateDate.ToString();

                resModel.contents = itme.Results;
                resModel.time = itme.CreateDate.ToString();
                resModel.VAttachmentList = newlist;
                MyList.Add(resModel);
            }
            return MyList;
        }


        //判断是否是注册用户
        public bool CheckInfos(string ID)
        {
            bool result = false;
            if (!String.IsNullOrEmpty(ID))
            {
                var res = _IVolunteerInfoRepository.SearchInfoByID(ID);
                if (res != null && res.ID!=null)
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
        public List<VHelpAreaSearchMiddle> GetMyRecommendList(string VID)
        {
            List<VHelpAreaSearchMiddle> Searches = new List<VHelpAreaSearchMiddle>();
            List<VHelpArea> lists = _IVHelpAreaRepository.GetMyRecommendList();

            //无擅长技能
            string SkillID = "AB1EC380-4D47-45AB-9C2B-F2EE19B7AE9C";

            //默认地址 天津市滨海新区宏达街19号
            string longitude = "117.70889";
            string latitude = "39.02749";

            //判断是否是注册用户
            if (!CheckInfos(VID))
            {
                //不是注册用户
                foreach (var item in lists)
                {
                    //互动信息 擅长技能(无技能要求)
                    string activityType = item.TypeID;
                    if (activityType == SkillID)
                    {
                        item.bak2 = "1";
                    }

                    if (item.longitude == "" || item.longitude == null || item.latitude == "" || item.latitude == null)
                    {
                        item.bak2 = "1";
                        item.bak3 = "1";
                    }
                    else
                    {
                        //根据活动地址推荐 两公里以内
                        var status = CheckAddress(double.Parse(longitude), double.Parse(latitude), double.Parse(item.longitude), double.Parse(item.latitude), 2.0);
                        if (status)
                        {
                            item.bak2 = "1";
                        }
                        else
                        {
                            item.bak2 = "0";
                        }
                        //判断 互助地址与志愿者注册详细地址间距离
                        double dis = GetDistance(double.Parse(longitude), double.Parse(latitude), double.Parse(item.longitude), double.Parse(item.latitude));
                        item.bak3 = dis.ToString("f2");
                    }
 
                    if (item.Status == "0")
                    {
                        item.bak2 = "1";
                    }
                    else
                    {
                        item.bak2 = "0";
                    }
                }
            }
            else
            {
                var VolunteerInfo = _IVolunteerInfoRepository.SearchInfoByID(VID);
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

                //根据志愿者擅长技能推荐  与  根据志愿者服务领域推荐
                var typelist = _IVolunteer_Relate_TypeRepository.GetMyTypeList(VID);

                foreach (var item in lists)
                {
                    //互动信息 擅长技能
                    string activityType = item.TypeID;
                    if (activityType == SkillID)
                    {
                        item.bak2 = "1";
                    }
                    else
                    {
                        bool isContains = false;
                        typelist.ForEach(o => isContains |= activityType.Contains(o.TypeID));
                        if (isContains)
                        {
                            item.bak2 = "1";
                        }
                    }

                    if (item.longitude == "" || item.longitude == null || item.latitude == "" || item.latitude == null)
                    {
                        item.bak2 = "1";
                        item.bak3 = "1";
                    }
                    else
                    {
                        //根据活动地址推荐 两公里以内
                        var status = CheckAddress(double.Parse(longitude), double.Parse(latitude), double.Parse(item.longitude), double.Parse(item.latitude), 2.0);
                        if (status)
                        {
                            item.bak2 = "1";
                        }
                        else
                        {
                            item.bak2 = "0";
                        }
                        //判断 互助地址与志愿者注册详细地址间距离
                        double dis = GetDistance(double.Parse(longitude), double.Parse(latitude), double.Parse(item.longitude), double.Parse(item.latitude));
                        item.bak3 = dis.ToString("f2");
                    }

                    if (item.Status == "0")
                    {
                        item.bak2 = "1";
                    }
                    else
                    {
                        item.bak2 = "0";
                    }


                }

              

            }

            Searches = _IMapper.Map<List<VHelpArea>, List<VHelpAreaSearchMiddle>>(lists);
            Searches = Searches.OrderByDescending(o => o.bak2).ToList();
            return Searches;
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


        //志愿者成功参与的 互助次数（只有最终审核处理结果通过才算成功）
        public BaseViewModel GetMyHelpAreaNums(SearchByVIDModel vidModel)
        {
            BaseViewModel result = new BaseViewModel();
            int count = _IVHA_SignRepository.GetMyHelpAreaNums(vidModel.VID);
            result.ResponseCode = count;
            result.Message = "查询成功";
            return result;
        }

        //根据 经纬度 判断是否在 dis  千米 范围内
        public bool CheckAddress(double longitude, double latitude, double nowlon, double nowlat, double dis)
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
