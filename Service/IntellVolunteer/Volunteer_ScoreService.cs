﻿using AutoMapper;
using Dto.IRepository.IntellVolunteer;
using Dto.IService.IntellVolunteer;
using Dtol.dtol;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SystemFilter.PublicFilter;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;

namespace Dto.Service.IntellVolunteer
{
    public class Volunteer_ScoreService: IVolunteer_ScoreService
    {
        private readonly IVolunteer_ScoreRepository _IScoreRepository;
        private readonly IMapper _IMapper;
        private readonly ISQLRepository _SqlRepository;

        public Volunteer_ScoreService(IVolunteer_ScoreRepository itypeRepository, IMapper mapper, ISQLRepository sqlRepository)
        {
            _IScoreRepository = itypeRepository;
            _IMapper = mapper;
            _SqlRepository = sqlRepository;
        }


        public string GetMyScores(SearchByVIDModel searchModel)
        {
            string result = _IScoreRepository.GetMyScore(searchModel);
            return result;
        }

        public List<VScoreRankMiddle> GetMyScoreRanks(SearchByVIDModel searchModel)
        {
            DEncrypt encrypt = new DEncrypt();
            List<VScoreRankMiddle> result = _SqlRepository.GetScoreRank();
            foreach (var item in result)
            {
                item.Name = encrypt.Decrypt(item.Name);
            }
            List<VScoreRankMiddle> middle = result.Where(o => o.VID.Equals(searchModel.VID)).ToList();
           
            return middle ;
        }


        public List<VScoreRankMiddle> GetScoreRanks()
        {
            DEncrypt encrypt = new DEncrypt();
            List<VScoreRankMiddle> result = _SqlRepository.GetScoreRank();
            foreach (var item in result)
            {
                item.Name = encrypt.Decrypt(item.Name);
                
            }
            return result;
        }

    }
}
