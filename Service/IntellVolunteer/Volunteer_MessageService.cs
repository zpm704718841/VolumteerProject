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
using ViewModel.PublicViewModel;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;

namespace Dto.Service.IntellVolunteer
{
    public class Volunteer_MessageService : IVolunteer_MessageService
    {
        private readonly IVolunteer_MessageRepository _IVolunteer_MessageRepository;
        private readonly IMapper _IMapper;
        private readonly ISQLRepository _SqlRepository;

        public Volunteer_MessageService(IVolunteer_MessageRepository volunteer_Messag , IMapper mapper, ISQLRepository sqlRepository)
        {
            _IVolunteer_MessageRepository = volunteer_Messag;
            _IMapper = mapper;
            _SqlRepository = sqlRepository;
        }


        public BaseViewModel AddMessages(Volunteer_MessageMiddle model)
        {
            BaseViewModel res = new BaseViewModel();
          
            Volunteer_Message message = _IMapper.Map<Volunteer_MessageMiddle,Volunteer_Message>(model);
            
            message.ID = Guid.NewGuid().ToString();
            message.CreateDate = DateTime.Now;
            message.CreateUser = model.VID;
            message.Status = "0";

            _IVolunteer_MessageRepository.Add(message);
            int a = _IVolunteer_MessageRepository.SaveChanges();
            if (a > 0)
            {
                res.ResponseCode = 200;
                res.Message = "消息推送成功";
            }
            else
            {
                res.ResponseCode = 400;
                res.Message = "消息推送失败";
            }
            return res;

        }


        public BaseViewModel UpdateMessagesStatus(MessageIDandVIDModel model)
        {
            BaseViewModel res = new BaseViewModel();
            Volunteer_Message message = _IVolunteer_MessageRepository.GetByID(model.MessageID, model.VID);
            message.Status ="1";
            message.UpdateUser = model.VID;
            message.UpdateDate = DateTime.Now;
            _IVolunteer_MessageRepository.Update(message);
            int a = _IVolunteer_MessageRepository.SaveChanges();
            if (a > 0)
            {
                res.ResponseCode = 200;
                res.Message = "消息推送成功";
            }
            else
            {
                res.ResponseCode = 400;
                res.Message = "消息推送失败";
            }
            return res;

        }

        public List<Volunteer_Message> GetMyMessages(SearchByVIDModel model)
        {
            List<Volunteer_Message> list = new List<Volunteer_Message>();
            list = _IVolunteer_MessageRepository.GetByVID(model.VID);
            return list;
        }

        public BaseViewModel GetMyUnReadMessageNum(SearchByVIDModel model)
        {
            BaseViewModel res = new BaseViewModel();
            List<Volunteer_Message> list = new List<Volunteer_Message>();
            list = _IVolunteer_MessageRepository.GetByVID(model.VID);
            list = list.Where(o => o.Status == "0").ToList();
            res.ResponseCode = list.Count;
            res.Message = "未读消息数";
            return res;
        }

    }
}
