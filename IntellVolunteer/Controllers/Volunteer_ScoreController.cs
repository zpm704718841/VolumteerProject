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

namespace IntellVolunteer.Controllers
{
    [Route("VolunteerManageApi/[controller]/[action]")]
    [ApiController]
    public class Volunteer_ScoreController : ControllerBase
    {
        private readonly IVolunteer_ScoreService _ScoreService;
        //private readonly ILogger _ILogger;


        public Volunteer_ScoreController(IVolunteer_ScoreService  scoreService)
        {
            _ScoreService = scoreService;
        }


        /// <summary>
        /// 单独获取该志愿者积分情况 （志愿者VID）  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string  GetMyScore(SearchByVIDModel model)
        {

            string score = _ScoreService.GetMyScores(model);
            
            return score;
        }


        /// <summary>
        /// (小程序端接口)  获取该志愿者积分 和 排名情况 （志愿者VID）
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public VScoreRankMiddle GetMyScoreRank(SearchByVIDModel model)
        {
            VScoreRankMiddle mMiddle = new VScoreRankMiddle();
            List<VScoreRankMiddle> score = _ScoreService.GetMyScoreRanks(model);
            if (score.Count > 0)
            {
                return score.First();
            }
            else
            {
                return mMiddle;
            }
            
        }


        /// <summary>
        /// (小程序端接口)  获取所有志愿者积分排名情况  
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public List<VScoreRankMiddle> GetAllScoreRank()
        {

            List <VScoreRankMiddle> result = _ScoreService.GetScoreRanks();

            return result;
        }

    }
}
