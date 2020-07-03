using AutoMapper;
using Dto.IRepository.IntellVolunteer;
using Dto.IService.IntellVolunteer;
using Dtol.dtol;
using Dtol.Easydtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SystemFilter.PublicFilter;
using ViewModel.PublicViewModel;
using ViewModel.VolunteerBackground.MiddleModel;
using ViewModel.VolunteerModel.MiddleModel.OndutyClaimsMiddleModel;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.RequsetModel.NormalViewModel;
using ViewModel.VolunteerModel.ResponseModel.NormalViewModel;

namespace Dto.Service.IntellVolunteer
{
    public class MydutyClaimInfoService: IMydutyClaimInfoService
    {

        private readonly IMydutyClaimInfoRepository _IMydutyClaimInfoRepository;
        private readonly IMapper _IMapper;
        private readonly IMydutyClaim_SignRepository _mydutyClaim_Sign;
        private readonly IVAttachmentRepository _vAttachmentRepository;
        private readonly IVolunteerActivityRepository _IVolunteerActivityRepository;
        private readonly IVolunteerInfoRepository _IVolunteerInfoRepository;
        private readonly IOndutyClaimsInfoRepository  _claimsInfoRepository;
        private readonly IMydutyClaimInfoRepository _mydutyClaimInfo;
        private readonly INormalizationInfoRepository _normalizationInfo;
        private readonly IOndutyClaimsInfoRepository _ondutyClaimsInfoRepository;
        private readonly IVolunteer_ScoreRepository _IVolunteer_ScoreRepository;
        private readonly IET_pointsRepository eT_PointsRepository;

        public MydutyClaimInfoService(IMydutyClaimInfoRepository iMydutyClaimInfoRepository, IMapper iMapper, IMydutyClaim_SignRepository mydutyClaim_Sign, 
            IVAttachmentRepository vAttachment, IVolunteerActivityRepository volunteerActivity, IVolunteerInfoRepository volunteerInfo, 
            IOndutyClaimsInfoRepository ondutyClaimsInfo, IMydutyClaimInfoRepository mydutyClaimInfo, INormalizationInfoRepository normalizationInfoRepository,
            IOndutyClaimsInfoRepository claimsInfoRepository, IVolunteer_ScoreRepository volunteer_Score, IET_pointsRepository pointsRepository)
        {
            _IMydutyClaimInfoRepository = iMydutyClaimInfoRepository;
            _IMapper = iMapper;
            _mydutyClaim_Sign = mydutyClaim_Sign;
            _vAttachmentRepository = vAttachment;
            _IVolunteerActivityRepository = volunteerActivity;
            _IVolunteerInfoRepository = volunteerInfo;
            _claimsInfoRepository = ondutyClaimsInfo;
            _mydutyClaimInfo = mydutyClaimInfo;
            _normalizationInfo = normalizationInfoRepository;
            _ondutyClaimsInfoRepository = claimsInfoRepository;
            _IVolunteer_ScoreRepository = volunteer_Score;
            eT_PointsRepository = pointsRepository;
        }

        public BaseViewModel getMydutyInfoAddService(MydutyClaimInfoAddViewModel mydutyClaimInfoAddViewModel)
        {
            BaseViewModel baseView = new BaseViewModel();
            if (mydutyClaimInfoAddViewModel.StartDutyTime >= DateTime.Now)
            {
                var result = _IMapper.Map<MydutyClaimInfoAddViewModel, MydutyClaim_Info>(mydutyClaimInfoAddViewModel);

               
                //获取已该值班信息 认领人数
                int num = _IMydutyClaimInfoRepository.GetByParasNum(mydutyClaimInfoAddViewModel.OndutyClaims_InfoId,
                    mydutyClaimInfoAddViewModel.StartDutyTime, mydutyClaimInfoAddViewModel.EndDutyTime);
                //获取 可值班人数
                OndutyClaims_Info ondutyClaims = _ondutyClaimsInfoRepository.GetByID(mydutyClaimInfoAddViewModel.OndutyClaims_InfoId);

                //查询是否已经 被认领（已满员）  20200703
                if (ondutyClaims.TotalReportNum<= num)
                {
                    baseView.ResponseCode = 4;
                    baseView.Message = "已经被认领，无法再次认领。";
                }
                else
                {
                    //判断 是否已经认领同时段 信息
                    if (_IMydutyClaimInfoRepository.GetByParas(mydutyClaimInfoAddViewModel.Userid, "",
                    mydutyClaimInfoAddViewModel.StartDutyTime, mydutyClaimInfoAddViewModel.EndDutyTime))
                    {
                        baseView.ResponseCode = 5;
                        baseView.Message = "您已认领同时段值班任务，无法再次认领。";
                    }
                    else
                    {
                        //查询是否本人是否已经认领过 20200623
                        if (_IMydutyClaimInfoRepository.GetByParas(mydutyClaimInfoAddViewModel.Userid, mydutyClaimInfoAddViewModel.OndutyClaims_InfoId,
                            mydutyClaimInfoAddViewModel.StartDutyTime, mydutyClaimInfoAddViewModel.EndDutyTime))
                        {
                            baseView.ResponseCode = 3;
                            baseView.Message = "您已经认领无需再次认领。";
                        }
                        else
                        {
                            _IMydutyClaimInfoRepository.Add(result);
                            int i = _IMydutyClaimInfoRepository.SaveChanges();
                            if (i > 0)
                            {
                                baseView.ResponseCode = 0;
                                baseView.Message = "认领成功";
                            }
                            else
                            {
                                baseView.ResponseCode = 1;
                                baseView.Message = "认领失败";
                            }
                        }
                    }
                   
                }         
            }
            else
            {
                baseView.ResponseCode = 2;
                baseView.Message = "无法认领已失效值班信息";

            }
            return baseView;
        }

        public List<MydutyClaimInfoSearchMiddleModel> getMydutyInfoService(MydutyClaimInfoSearchViewModel  mydutyClaimInfoSearchViewModel)
        {
            var searchresult = _IMydutyClaimInfoRepository.getMydutyInfo(mydutyClaimInfoSearchViewModel);
            List<MydutyClaimInfoSearchMiddleModel> lists = new List<MydutyClaimInfoSearchMiddleModel>();
           
            foreach (var item in searchresult)
            {
                var res = new MydutyClaimInfoSearchMiddleModel();
                res.id = item.id;
                res.StartDutyTime = item.StartDutyTime;
                res.EndDutyTime = item.EndDutyTime;
                res.CreateDate = item.CreateDate;
                res.status = item.status;
                res.title = item.OndutyClaims_Info.Normalization_Info.title;
                res.Userid = item.Userid;
                res.UserName = item.UserName;
                res.XiaoCommunityName = item.OndutyClaims_Info.Subdistrict;
                lists.Add(res);
            }
            return lists;

        }

        public BaseViewModel getMydutyInfoUpdateService(MydutyClaimInfoUpdateViewModel mydutyClaimInfoUpdateViewModel)
        {
            BaseViewModel baseView = new BaseViewModel();
            var searchresult= _IMydutyClaimInfoRepository.GetInfoById(mydutyClaimInfoUpdateViewModel.id);
            var updatemodel= _IMapper.Map<MydutyClaimInfoUpdateViewModel, MydutyClaim_Info>(mydutyClaimInfoUpdateViewModel, searchresult);//mapper没配置
            updatemodel.UpdateDate = DateTime.Now;
            updatemodel.UpdateUser = updatemodel.Userid;

            _IMydutyClaimInfoRepository.Update(updatemodel);
            int i=_IMydutyClaimInfoRepository.SaveChanges();
            if (i > 0)
            {
                baseView.ResponseCode = 0;
                baseView.Message = "更新成功";
            }
            else
            {
                baseView.ResponseCode = 1;
                baseView.Message = "更新失败";

            }
            return baseView;
        }


        // 值班认领 上传现场服务照片
        public int SubmitImg(MyDutySignImgAddModel AddViewModel)
        {
            int c = 0;
            MydutyClaim_Sign model = new MydutyClaim_Sign();

            model.id = Guid.NewGuid().ToString();
            model.Userid = AddViewModel.uid;
            model.UserName = AddViewModel.name;
            model.type = "img";
            model.CheckTime = DateTime.Now;
            model.CreateUser = AddViewModel.uid;
            model.CreateDate = DateTime.Now;
            model.MydutyClaim_InfoID = AddViewModel.MydutyClaim_InfoID;
            model.OndutyClaims_InfoId = AddViewModel.OndutyClaims_InfoId;
            _mydutyClaim_Sign.Add(model);
            int count = _mydutyClaim_Sign.SaveChanges();

            var AttachmentInfo = _IMapper.Map<List<VAttachmentAddViewModel>, List<VAttachment>>(AddViewModel.VAttachmentAddList);
            foreach (var item in AttachmentInfo)
            {
                item.ID = Guid.NewGuid().ToString();
                item.formid = model.id;
                item.type = "ZBTP";//值班信息图片
                item.Status = "0";
                item.CreateUser = AddViewModel.uid;
                item.CreateDate = DateTime.Now;
                _vAttachmentRepository.Add(item);
                c = _vAttachmentRepository.SaveChanges() + c;
            }

            return c;
        }


        //值班认领 签到、签退
        public int HandleAdd(VA_HandleAddViewModel AddViewModel)
        {
            int count = 0;
            MydutyClaim_Sign model = new MydutyClaim_Sign();
            //是否注册
            var VolunteerInfo = _IVolunteerInfoRepository.SearchInfoByID(AddViewModel.VID);
            if (VolunteerInfo == null)
            {
                count = 6;
                return count;
            }

            ////认领得 值班信息
            MydutyClaim_Info claim_Info = _IMydutyClaimInfoRepository.GetByUidandID(AddViewModel.VID, AddViewModel.ContentID);
            if (claim_Info != null)
            {
                //判断是否在值班区间内进行打卡 20200622  值班时间前后15 分钟均可签到签退
                DateTime start = DateTime.Parse(claim_Info.StartDutyTime.ToString());
                DateTime end = DateTime.Parse(claim_Info.EndDutyTime.ToString());

                if (DateTime.Now > start.AddMinutes(-15) && DateTime.Now < end.AddMinutes(15))
                {
                    OndutyClaims_Info ondutyClaims = _claimsInfoRepository.GetByID(claim_Info.OndutyClaims_InfoId);
                    if (ondutyClaims != null)
                    {
                        model.OndutyClaims_InfoId = ondutyClaims.id;
                        model.MydutyClaim_InfoID = AddViewModel.ContentID;
                        //获取小区经纬度信息
                        List<User_Depart> departAll = _IVolunteerInfoRepository.GetDepartAll();

                        var departList = _IMapper.Map<List<User_Depart>, List<UserDepartSearchMidModel>>(departAll);

                        List<UserDepartSearchMidModel> result = new List<UserDepartSearchMidModel>();
                        result.AddRange(departList.Where(p => p.Code == ondutyClaims.SubdistrictID).ToList());
                        UserDepartSearchMidModel depart = new UserDepartSearchMidModel();
                        if (result.Count > 0)
                        {
                            depart = result.First();
                        }


                        //获取 经纬度
                        if (!String.IsNullOrEmpty(AddViewModel.Checklongitude) && !String.IsNullOrEmpty(AddViewModel.Checklatitude))
                        {
                            //进行地址判断 活动地址方圆1000米可以签到
                            if (!String.IsNullOrEmpty(depart.longitude) && !String.IsNullOrEmpty(depart.latitude))
                            {
                                var checks = CheckAddress(double.Parse(depart.longitude), double.Parse(depart.latitude), double.Parse(AddViewModel.Checklongitude), double.Parse(AddViewModel.Checklatitude),1);
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


                    }

                }
                else
                {
                    count = 5;
                    return count;
                }
            }
            else
            {
                count = 4;
                return count;
            }

            DEncrypt encrypt = new DEncrypt();
            //保存签到签退信息
            model.id = Guid.NewGuid().ToString();
            model.Userid =AddViewModel.VID;
            model.UserName = encrypt.Decrypt(VolunteerInfo.Name);
            model.CheckTime = DateTime.Now;
            model.type = AddViewModel.type;
            model.CreateUser = VolunteerInfo.ID;
            model.CreateDate = DateTime.Now;
            model.CheckAddress = AddViewModel.CheckAddress;
            model.Checklongitude = AddViewModel.Checklongitude;
            model.Checklatitude = AddViewModel.Checklatitude;
          
        
            _mydutyClaim_Sign.Add(model);
            count = _mydutyClaim_Sign.SaveChanges();
            if (count == 1)
            {
                string id = Guid.NewGuid().ToString();
               
                //签退 时按时长积分继续计算
                if (AddViewModel.type == "out")
                {
                    Volunteer_Score score = new Volunteer_Score();
                    score.ID = id;
                    score.ContentID = AddViewModel.ContentID;
                    score.tableName = "MydutyClaim_Info";
                    score.VID = AddViewModel.VID;
                    score.type = "out";
                    score.Score = 1;
                    score.CreateUser = AddViewModel.VID;
                    score.CreateDate = DateTime.Now;

                    _IVolunteer_ScoreRepository.Add(score);
                    int b = _IVolunteer_ScoreRepository.SaveChanges();
                    if (b > 0)
                    {
                        //插入到  泰便利积分表  20200622
                        ET_points ipointMiddle = new ET_points();

                        ipointMiddle.ID = id;
                        ipointMiddle.uid = AddViewModel.VID;
                        ipointMiddle.points = score.Score;
                        ipointMiddle.type = "MydutySign";
                        ipointMiddle.tableName = "TedaVolunteerDB.dbo.Volunteer_Score";
                        ipointMiddle.CreateUser = AddViewModel.VID;
                        ipointMiddle.CreateDate = DateTime.Now;
                        eT_PointsRepository.Add(ipointMiddle);
                        int j = eT_PointsRepository.SaveChanges();
                       
                    }
                }
            }

            return count;
        }


        //获取该认领信息具体情况  包括签到、签退 现场图片等
        public MydutyClaim_InfoResModel GetMydutyDetail(SearchByIDAnduidModel viewModel)
        {
            MydutyClaim_InfoResModel MyResModel = new MydutyClaim_InfoResModel();

            //获取基本信息
            var model1 = _IMydutyClaimInfoRepository.GetInfoById(viewModel.MydutyClaim_InfoID);
            MyResModel.MiddleModel = _IMapper.Map<MydutyClaim_Info, MydutyClaimInfoMiddleModel>(model1);

            var dutyInfo = _ondutyClaimsInfoRepository.GetByID(model1.OndutyClaims_InfoId);
            if (dutyInfo != null)
            {
                //获取 社区、小区
                MyResModel.MiddleModel.Subdistrict = dutyInfo.Subdistrict;
                var normalInfo = _normalizationInfo.NormalizationByID(dutyInfo.Normalization_InfoId);
                if (normalInfo != null)
                {
                    MyResModel.MiddleModel.Community = normalInfo.CommunityName;
                }

            }


            //获取 签到签退图片等信息

            List<MydutyClaim_Sign> handlelist = _mydutyClaim_Sign.GetByParas(viewModel);
            MydutyClaim_SignInfo signInfo = new MydutyClaim_SignInfo();
            foreach (var item in handlelist)
            {
                //签到
                if (item.type == "in")
                {
                    signInfo.SignUpTime = item.CheckTime.ToString();
                }
                //上传现场图片
                if (item.type == "img")
                {

                    var list = _vAttachmentRepository.GetMyList(item.id);
                    var newlist = _IMapper.Map<List<VAttachment>, List<VAttachmentAddViewModel>>(list);
                    signInfo.VAttachmentList = newlist;
                }
                //签退
                if (item.type == "out")
                {
                    signInfo.SignOutTime = item.CheckTime.ToString();
                }
            }

            //状态
            MydutyClaim_Sign handle = _mydutyClaim_Sign.GetByParasOne(viewModel);
            if (handle!=null)
            {
                //签到
                if (handle.type == "in")
                {
                    MyResModel.MiddleModel.status = "img";
                }
                //上传现场图片
                if (handle.type == "img")
                {
                    MyResModel.MiddleModel.status = "out";
                }
                //签退
                if (handle.type == "out")
                {
                    MyResModel.MiddleModel.status = "done";
                } 
            }
            else
            {
                MyResModel.MiddleModel.status = "in";
            }

            MyResModel.claim_SignInfo = signInfo;


            if (MyResModel.MiddleModel!=null && MyResModel.claim_SignInfo!=null)
            {
                MyResModel.isSuccess = true;
                MyResModel.baseViewModel.Message = "查询成功";
                MyResModel.baseViewModel.ResponseCode = 200;
                MyResModel.TotalNum = 1;
            }
            else
            {
                MyResModel.isSuccess = false;
                MyResModel.baseViewModel.Message = "查询失败";
                MyResModel.baseViewModel.ResponseCode = 400;
                MyResModel.TotalNum = 0;
            }

            return MyResModel;

        }


        //根据 经纬度 判断是否在 dis  (1千米  20200703)千米 范围内
        public virtual bool CheckAddress(double longitude, double latitude, double nowlon, double nowlat, double dis)
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


    }
}
