using Dtol.dtol;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dto.IRepository.IntellVolunteer
{
    public interface ILoginTypeRepository : IRepository<LoginType>
    {
        //根据 id 获取具体登录方式
        LoginType SearchByIDModel(string id);


        //直接获取 人脸识别登录方式信息
        LoginType SearchFaceModel();

        //获取全部登录方式
        List<LoginType> GetList();

    }
}
