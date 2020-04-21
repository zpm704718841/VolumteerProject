using AutoMapper;
using Dto.IRepository.IntellVolunteerBackground;
using Dto.IService.IntellVolunteerBackground;
using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModel.VolunteerBackground.MiddleModel;
using ViewModel.VolunteerBackground.RequsetModel;
using ViewModel.VolunteerBackground.ResponseModel;
using ViewModel.VolunteerModel.MiddleModel;

namespace Dto.Service.IntellVolunteerBackground
{
    public class VolunteerActivityService : IVolunteerActivityService
    {
        private readonly IMapper _IMapper;
        private readonly IVolunteerActivityRepository _volunteerActivityRepository;
        public VolunteerActivityService(IMapper mapper, IVolunteerActivityRepository volunteerActivityRepository)
        {
            _IMapper = mapper;
            _volunteerActivityRepository = volunteerActivityRepository;
        }

        public int Add(VolunteerActivityViewModel activityAddModel)
        {
            var activityModel = _IMapper.Map<VolunteerActivityViewModel, VolunteerActivity>(activityAddModel);
            activityModel.Status = "1";
            _volunteerActivityRepository.Add(activityModel);
            return _volunteerActivityRepository.SaveChanges();

        }

        public int Update(VolunteerActivityViewModel activityAddModel)
        {
            var activityModel = _IMapper.Map<VolunteerActivityViewModel, VolunteerActivity>(activityAddModel);
            _volunteerActivityRepository.Update(activityModel);
            return _volunteerActivityRepository.SaveChanges();
        }

        public int Delete(Guid id)
        {
            var activityModel = _volunteerActivityRepository.GetById(id);
            activityModel.Status = "0";
            _volunteerActivityRepository.Update(activityModel);
            return _volunteerActivityRepository.SaveChanges();

        }

        //public int DeleteByID(string id)
        //{
        //    _volunteerActivityRepository.Delete(id);
        //    return _volunteerActivityRepository.SaveChanges();
        //}

        public int DeleteByID(VolunteerActivityDeleteViewModel viewModel)
        {
            _volunteerActivityRepository.Delete(viewModel);
            return _volunteerActivityRepository.SaveChanges();
        }
        public List<VolunteerActivityMidModel> SearchList(VolunteerActivitySearchViewModel searchViewModel)
        {
            List<VolunteerActivity> model = _volunteerActivityRepository.SearchInfoByWhere(searchViewModel);
            List<VolunteerActivityMidModel> searchModelList = new List<VolunteerActivityMidModel>();
            foreach (var item in model)
            {
                var searchModel = _IMapper.Map<VolunteerActivity, VolunteerActivityMidModel>(item);
                searchModelList.Add(searchModel);
            }
            return searchModelList;
        }

        public bool ActivityDistinct(VolunteerActivityViewModel model)
        {
            IQueryable<VolunteerActivity> activityModel = _volunteerActivityRepository.GetVolunteerActivityByTitle(model.Title);
            return (activityModel.Count() < 1) ? true : false;
        }

        public bool ActivityDistinctForUpdate(VolunteerActivityViewModel model)
        {
            IQueryable<VolunteerActivity> activityModel = _volunteerActivityRepository.GetVolunteerActivityForUpdate(model.Title, model.ID);
            return (activityModel.Count() < 1) ? true : false;
        }

        public int GetAllCount(VolunteerActivitySearchViewModel searchModel)
        {
            int result = _volunteerActivityRepository.GetVolunteerAll(searchModel).Count();
            return result;
        }

        private void AddPrimission(List<UserDepartSearchMidModel> list, UserDepartSearchMidModel curPrimission)
        {
            List<UserDepartSearchMidModel> primission = list.Where(p => p.ParentId == curPrimission.Code.ToString()).ToList();
            curPrimission.DepartChildren = primission;
            foreach (var p in primission)
            {
                AddPrimission(list, p);
            }
        }

        public List<UserDepartSearchMidModel> GetDepartList()
        {
            UserDepartResModel returnDepart = new UserDepartResModel();

            List<User_Depart> departAll = _volunteerActivityRepository.GetDepartAll();

            var departList = _IMapper.Map<List<User_Depart>, List<UserDepartSearchMidModel>>(departAll);

            List<UserDepartSearchMidModel> result = new List<UserDepartSearchMidModel>();
            result.AddRange(departList.Where(p => p.ParentId == "0").ToList());
            foreach (var el in result)
            {
                AddPrimission(departList, el);
            }

            return result;
        }

    }
}
