using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.IService.IntellVolunteer;
using Dtol.dtol;
using Serilog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemFilter.PublicFilter;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.ResponseModel;
using System.IO;
using ViewModel.VolunteerBackground.RequsetModel;
using ViewModel.VolunteerBackground.ResponseModel;
using ViewModel.PublicViewModel;

namespace IntellVolunteer.Controllers
{
    [Route("VolunteerManageApi/[controller]/[action]")]
    [ApiController]
    public class Volunteer_MessageController
    {
        private readonly IVolunteer_MessageService _MessageService;
        private readonly ILogger _ILogger;


        public Volunteer_MessageController(IVolunteer_MessageService  messageService)
        {
            _MessageService = messageService;
        }

        /// <summary>
        /// (微信小程序接口)   获取该用户的推送消息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<Volunteer_Message> GetMyMessages(SearchByVIDModel model)
        {
            List<Volunteer_Message> list = new List<Volunteer_Message>();
            list = _MessageService.GetMyMessages(model);
            return list;
        }


        /// <summary>
        /// (微信小程序接口)   插入推送消息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseViewModel AddMessages(Volunteer_MessageMiddle model)
        {
            BaseViewModel res = new BaseViewModel();
            res = _MessageService.AddMessages(model);
            return res;
        }

        /// <summary>
        /// (微信小程序接口)   更新消息状态 未读\已读
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseViewModel UpdateMessagesStatus(MessageIDandVIDModel model)
        {
            BaseViewModel res = new BaseViewModel();
            res = _MessageService.UpdateMessagesStatus(model);
            return res;
        }

        /// <summary>
        /// (微信小程序接口)   获取该用户未读消息数
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public BaseViewModel GetMyUnReadMessageNum(SearchByVIDModel model)
        {
            BaseViewModel res = new BaseViewModel();
            res = _MessageService.GetMyUnReadMessageNum(model);
            return res;
        }



    }
}
