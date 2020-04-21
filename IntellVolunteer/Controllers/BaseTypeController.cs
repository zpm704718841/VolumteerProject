using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dto.IService.IntellVolunteer;
using Dtol.dtol;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SystemFilter.PublicFilter;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.ResponseModel;
using System.IO;

namespace IntellVolunteer.Controllers
{
    [Route("VolunteerManageApi/[controller]/[action]")]
    [ApiController]
    public class BaseTypeController : ControllerBase
    {
        private readonly IBaseTypeService _BaseTypeService;




        public BaseTypeController(IBaseTypeService Typeservice)
        {
            _BaseTypeService = Typeservice;
        }
        /// <summary>
        /// 增添信息
        /// </summary>
        /// <param name="TypeAddViewModel"></param>
        /// <returns></returns>

        [HttpPost]
        [ValidateModel]
        public ActionResult<BaseTypeAddViewModel> BaseType_add(BaseTypeAddViewModel TypeAddViewModel)
        {


            int Type_Add_Count;
            BaseTypeAddResModel typeAddResModel = new BaseTypeAddResModel();
            Type_Add_Count = _BaseTypeService.BaseType_Add(TypeAddViewModel);
            if (Type_Add_Count > 0)
            {
                typeAddResModel.IsSuccess = true;
                typeAddResModel.AddCount = Type_Add_Count;
                typeAddResModel.baseViewModel.Message = "添加成功";
                typeAddResModel.baseViewModel.ResponseCode = 200;
                return Ok(typeAddResModel);
            }
            else
            {
                typeAddResModel.IsSuccess = false;
                typeAddResModel.AddCount = 0;
                typeAddResModel.baseViewModel.Message = "添加失败";
                typeAddResModel.baseViewModel.ResponseCode = 200;
                return Ok(typeAddResModel);
            }
        }


        /// <summary>
        /// (小程序端接口) 查询 擅长技能、服务领域 等基础 信息  (无参数)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BaseTypeSearchResModel> Manage_Search()
        {
            BaseTypeSearchResModel SearchResModel = new BaseTypeSearchResModel();
            var SearchResult = _BaseTypeService.BaseType_Search();


            SearchResModel.List = SearchResult;
            SearchResModel.isSuccess = true;
            SearchResModel.baseViewModel.Message = "查询成功";
            SearchResModel.baseViewModel.ResponseCode = 200;
            SearchResModel.TotalNum = 1;
            return Ok(SearchResModel);

        }
    }
}
