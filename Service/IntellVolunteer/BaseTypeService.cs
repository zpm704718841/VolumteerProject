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

namespace Dto.Service.IntellVolunteer
{
    public class BaseTypeService : IBaseTypeService
    {
        private readonly IBaseTypeRepository _IBaseTypeRepository;
        private readonly IMapper _IMapper;


        public BaseTypeService(IBaseTypeRepository itypeRepository, IMapper mapper)
        {
            _IBaseTypeRepository = itypeRepository;
            _IMapper = mapper;
        }

        //添加用户
        public int BaseType_Add(BaseTypeAddViewModel TypeAddViewModel)
        {

            var user_Info = _IMapper.Map<BaseTypeAddViewModel, VBaseType>(TypeAddViewModel);
            _IBaseTypeRepository.Add(user_Info);
            return _IBaseTypeRepository.SaveChanges();
        }

        public List<BaseTypeSearchMiddle> BaseType_Search()
        {
            List<VBaseType> Searches = _IBaseTypeRepository.SearchInfoByWhere();

            var Infos =  _IMapper.Map<List<VBaseType>, List<BaseTypeSearchMiddle>>(Searches);

            List<BaseTypeSearchMiddle> result = new List<BaseTypeSearchMiddle>();
            result.AddRange(Infos.Where(p => p.ParentCode == "0").OrderBy(o => o.Sort).ToList());

            foreach (var item in result)
            {
                AddPrimission(Infos, item);
            }

            return result;
        }

        private void AddPrimission(List<BaseTypeSearchMiddle> list, BaseTypeSearchMiddle curPrimission)
        {
            List<BaseTypeSearchMiddle> primission = list.Where(p => p.ParentCode == curPrimission.Code.ToString()).OrderBy(o => o.Sort).ToList();
            curPrimission.TypeChildren = primission;
            foreach (var p in primission)
            {
                AddPrimission(list, p);
            }
        }


    }
}
