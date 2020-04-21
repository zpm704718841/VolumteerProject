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
    public class VAttachmentController : ControllerBase
    {
        private readonly IVAttachmentService _IVAttachmentService;

        public VAttachmentController(IVAttachmentService Service)
        {
            _IVAttachmentService = Service;
        }

        /// <summary>
        ///    增添附件信息
        /// </summary>
        /// <param name="AddViewModel"></param>
        /// <returns></returns>

        [HttpPost]
        [ValidateModel]
        public ActionResult<VAttachmentAddResModel> VAttachment_add(VAttachmentAddViewModel AddViewModel)
        {
            int Add_Count;
            VAttachmentAddResModel AddResModel = new VAttachmentAddResModel();
            Add_Count = _IVAttachmentService.VAttachment_Add(AddViewModel);
            if (Add_Count > 0)
            {
                AddResModel.IsSuccess = true;
                AddResModel.AddCount = Add_Count;
                AddResModel.baseViewModel.Message = "添加成功";
                AddResModel.baseViewModel.ResponseCode = 200;
                return Ok(AddResModel);
            }
            else
            {
                AddResModel.IsSuccess = false;
                AddResModel.AddCount = 0;
                AddResModel.baseViewModel.Message = "添加失败";
                AddResModel.baseViewModel.ResponseCode = 200;
                return Ok(AddResModel);
            }
        }
    }
}
