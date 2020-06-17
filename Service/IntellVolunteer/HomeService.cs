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
    public class HomeService: IHomeService
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

        public HomeService(IVolunteerActivityRepository iInfoRepository, IMapper mapper, IVActivity_Relate_TypeRepository relate_TypeRepository, IVolunteerInfoRepository infoRepository, IVA_SignRepository va_SignRepository, IVolunteer_Relate_TypeRepository volunteer_Relate_TypeRepository, IBaseTypeRepository baseTypeRepository, IVA_HandleRepository va_HandleRepository, IVAttachmentRepository AttachmentRepository, ISQLRepository sqlRepository, IVolunteer_ScoreRepository scoreRepository, IAISQLRepository aisqlRepository, IVolunteer_MessageRepository messageRepository)
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

        //获取当前用户 今天以后的报名活动情况 日期列表
        public List<string> GetMyAllDate(SearchByVIDModel vidModel)
        {
            List<string> list = new List<string>();

            List<string> Infos = _IVA_SignRepository.GetMyList(vidModel.VID);
            List<VolunteerActivitySearchMiddle> Searches = new List<VolunteerActivitySearchMiddle>();

            List<VolunteerActivity> middle = _IVolunteerActivityRepository.GetByIDList(Infos);
            Searches = _IMapper.Map<List<VolunteerActivity>, List<VolunteerActivitySearchMiddle>>(middle);

            ////获取当前用户 今天以后的报名活动情况 日期列表
            Searches = Searches.Where(o => o.Stime >= DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"))).OrderBy(o => o.Stime).ToList();


            foreach (var item in Searches)
            {
                string date = DateTime.Parse(item.Stime.ToString()).ToString("yyyy-MM-dd");
                if (!list.Contains(date))
                {
                    list.Add(date);
                }
            }

            return list;

        }


        ///获取当前用户 根据活动时间显示具体 活动情况列表 20200527
        public List<VolunteerActivitySearchMiddle> GetMyAllByDate(VolunteerActivitySearchByDateModel vidModel)
        {
            List<String> Infos = _IVA_SignRepository.GetMyList(vidModel.VID);
            List<VolunteerActivitySearchMiddle> Searches = new List<VolunteerActivitySearchMiddle>();

            List<VolunteerActivity> middle = _IVolunteerActivityRepository.GetByIDList(Infos);
            Searches = _IMapper.Map<List<VolunteerActivity>, List<VolunteerActivitySearchMiddle>>(middle);


            if (!String.IsNullOrEmpty(vidModel.Date))
            {
                Searches = Searches.Where(o => DateTime.Parse(o.Stime.ToString()).ToString("yyyy-MM-dd") == vidModel.Date).OrderBy(o => o.Stime).ToList();
            }
            
            foreach (var item in Searches)
            {
                //bak3  显示 该用户针对活动的状态
                var handle = _Va_HandleRepository.GetNewSign(vidModel.VID, item.ID);
                if(handle!=null && handle.ID !=null)
                {
                    if (handle.type == "in")
                    {
                        item.bak3 = "上传现场图片";
                    }
                    if (handle.type == "img")
                    {
                        item.bak3 = "待签退";
                    }
                    if (handle.type == "out")
                    {
                        item.bak3 = "已完结";
                        //已经完结的 事项不再显示，只显示待办信息 20200609
                        Searches.Remove(item);
                    }
                }
                else
                {
                    item.bak3 = "待签到";
                }

            }

            return Searches;
        }


    }
}
