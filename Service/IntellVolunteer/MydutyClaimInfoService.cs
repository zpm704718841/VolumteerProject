using AutoMapper;
using Dto.IRepository.IntellVolunteer;
using Dto.IService.IntellVolunteer;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;
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

        public MydutyClaimInfoService(IMydutyClaimInfoRepository iMydutyClaimInfoRepository, IMapper iMapper, IMydutyClaim_SignRepository mydutyClaim_Sign, 
            IVAttachmentRepository vAttachment)
        {
            _IMydutyClaimInfoRepository = iMydutyClaimInfoRepository;
            _IMapper = iMapper;
            _mydutyClaim_Sign = mydutyClaim_Sign;
            _vAttachmentRepository = vAttachment;
        }

        public void getMydutyInfoAddService(MydutyClaimInfoAddViewModel mydutyClaimInfoAddViewModel)
        {

            if (mydutyClaimInfoAddViewModel.StartDutyTime >= DateTime.Now)
            {
                var result = _IMapper.Map<MydutyClaimInfoAddViewModel, MydutyClaim_Info>(mydutyClaimInfoAddViewModel);
                _IMydutyClaimInfoRepository.Add(result);
                _IMydutyClaimInfoRepository.SaveChanges();
            }
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

        public void getMydutyInfoUpdateService(MydutyClaimInfoUpdateViewModel mydutyClaimInfoUpdateViewModel)
        {
            var searchresult= _IMydutyClaimInfoRepository.GetInfoById(mydutyClaimInfoUpdateViewModel.id);
            var updatemodel= _IMapper.Map<MydutyClaimInfoUpdateViewModel, MydutyClaim_Info>(mydutyClaimInfoUpdateViewModel, searchresult);//mapper没配置
            updatemodel.UpdateDate = DateTime.Now;
            updatemodel.UpdateUser = updatemodel.Userid;

            _IMydutyClaimInfoRepository.Update(updatemodel);
            _IMydutyClaimInfoRepository.SaveChanges();
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

        //获取该认领信息具体情况  包括签到、签退 现场图片等
        public MydutyClaim_InfoResModel GetMydutyDetail(SearchByIDAnduidModel viewModel)
        {
            MydutyClaim_InfoResModel MyResModel = new MydutyClaim_InfoResModel();

            //获取基本信息
            var model1 = _IMydutyClaimInfoRepository.GetInfoById(viewModel.MydutyClaim_InfoID);
            MyResModel.MiddleModel = _IMapper.Map<MydutyClaim_Info, MydutyClaimInfoSearchMiddleModel>(model1);

            //获取 签到签退图片等信息
            List<MydutyClaim_Sign> handleList = _mydutyClaim_Sign.GetByParas(viewModel);
            MydutyClaim_SignInfo signInfo = new MydutyClaim_SignInfo();
            foreach (var itme in handleList)
            {
                //签到
                if (itme.type == "in")
                {
                    signInfo.SignUpTime = itme.CheckTime.ToString();
                }
                //上传现场图片
                if (itme.type == "img")
                {

                    var list = _vAttachmentRepository.GetMyList(itme.id);
                    var newlist = _IMapper.Map<List<VAttachment>, List<VAttachmentAddViewModel>>(list);
                    signInfo.VAttachmentList = newlist;
                }
                //签退
                if (itme.type == "out")
                {
                    signInfo.SignOutTime = itme.CheckTime.ToString();
                }
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

    }
}
