using Dto.IService.IntellWeChat;
using Microsoft.Extensions.Options;
using System;
using Dtol.dtol;
using Dtol.WGWdtol;
using Dto.IRepository.IntellWeChat;
using System.Collections.Generic;
using System.Text;
using ViewModel.WeChatViewModel.MiddleModel;
using ViewModel.WeChatViewModel.ResponseModel;
using ViewModel.WeChatViewModel.RequestViewModel;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using ViewModel.VolunteerModel.MiddleModel;
using ViewModel.VolunteerModel.RequsetModel;
using ViewModel.VolunteerModel.ResponseModel;
using ViewModel.PublicViewModel;
using System.Net;

namespace Dto.Service.IntellWeChat
{
    public class WeChatHttpClientService: IWeChatHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private IOptions<WeChartTokenMiddles> _IOptions;
        private readonly IWeChatClientRepository _IWeChatClientRepository;
        private readonly IMapper _IMapper;
        private readonly ISQLRepository _ISQLRepository;
        public WeChatHttpClientService(IOptions<WeChartTokenMiddles> iOptions, IHttpClientFactory httpClientFactory, IWeChatClientRepository repository, IMapper mapper, ISQLRepository sqlRepository)
        {
            _IOptions = iOptions;
            _httpClientFactory = httpClientFactory;
            _IWeChatClientRepository = repository;
            _ISQLRepository = sqlRepository;
            _IMapper = mapper;
        }


        ///
        public async Task<WeChatTokenResModel> getWeChatTokenAsync()
        {

            var client = _httpClientFactory.CreateClient("WeChatToken");//必须和services.AddHttpClient()中指定的名称对应
            string content = "?grant_type=" + _IOptions.Value.grant_type + "&appid=" + _IOptions.Value.appid + "&secret=" + _IOptions.Value.secret;

            var uri = new Uri(client.BaseAddress, content);//重新组合url
            var response = client.GetAsync(uri);//调用
            var result = await response.Result.Content.ReadAsStringAsync();

            var WeChartTokenRes = JsonConvert.DeserializeObject<WeChatTokenResModel>(result);
         
            return WeChartTokenRes;
           
        }

        public async Task<string> GetWeChartToken()
        {
            //token为空,表示已过期；token不为空 则未过期可继续使用该token
            string token = _IWeChatClientRepository.getToken();

            if (string.IsNullOrEmpty(token))
            {
                WeChatTokenResModel TokenResModel = new WeChatTokenResModel();
                TokenResModel = await getWeChatTokenAsync();

                V_GetTokenAddModel model = new V_GetTokenAddModel();
                model.ID = Guid.NewGuid().ToString();
                model.token = TokenResModel.access_token;
                model.addtime = DateTime.Now;
                AddToken(model);

                token = model.token;

            }

         
            return token;
        }

        /// <summary>
        /// 用户初次进入自愿者小程序验证用户是否是微官网已注册用户，如果是返回微官网用户中心ID，如果不是返回空
        /// </summary>
        public WeChatUserCheckResModel GetWeChartInfo(WeChatCodeModel  codeModel)
        {
            WeChatUserCheckResModel result = _IWeChatClientRepository.Decrypt(codeModel, _IOptions.Value.appid, _IOptions.Value.secret);

            return result;
        }

        /// <summary>
        /// 用户初次进入自愿者小程序验证用户是否是微官网已注册用户，如果是返回微官网用户中心信息，如果不是返回空
        /// </summary>
        public WeChatWGWUserResModel GetWeChartUserInfo(string code)
        {
            UserInfo user_Infos = _IWeChatClientRepository.WGWDecrypt(code, _IOptions.Value.appid, _IOptions.Value.secret);
            WeChatWGWUserResModel result = _IMapper.Map<UserInfo, WeChatWGWUserResModel>(user_Infos);

            result.CommunityID = _ISQLRepository.GetIDByName(result.Community);
            result.subdistrictID = _ISQLRepository.GetIDByName(result.subdistrict);
            return result;
        }


        public int AddToken(V_GetTokenAddModel AddViewModel)
        {

            var Info = _IMapper.Map<V_GetTokenAddModel, V_GetToken>(AddViewModel);
            _IWeChatClientRepository.Add(Info);
            return _IWeChatClientRepository.SaveChanges();
        }

        public async Task<BaseViewModel> SendMessageService(SendMessageModel model)
        {
            string postData = "";
            string result = "";
            string url = "https://api.weixin.qq.com/cgi-bin/message/subscribe/send?access_token=";
            BaseViewModel res = new BaseViewModel();
            string token = _IWeChatClientRepository.getToken();
            if (string.IsNullOrEmpty(token))
            {
                token = await GetWeChartToken(); 
            }

            url = url + token;
            postData = JsonConvert.SerializeObject(model);
            result = GetPage(url, postData);

            res.Message = result;
            res.ResponseCode = 200;
            return res;
        }


        public string GetPage(string posturl, string postData)
        {
            Stream outstream = null;
            Stream instream = null;
            StreamReader sr = null;
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            Encoding encoding = Encoding.UTF8;
            byte[] data = encoding.GetBytes(postData);
            // 准备请求...
            try
            {
                // 设置参数
                request = WebRequest.Create(posturl) as HttpWebRequest;
                CookieContainer cookieContainer = new CookieContainer();
                request.CookieContainer = cookieContainer;
                request.AllowAutoRedirect = true;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;
                outstream = request.GetRequestStream();
                outstream.Write(data, 0, data.Length);
                outstream.Close();
                //发送请求并获取相应回应数据
                try
                {
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    response = (HttpWebResponse)ex.Response;
                }
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                instream = response.GetResponseStream();
                sr = new StreamReader(instream, encoding);
                //返回结果网页（html）代码
                string content = sr.ReadToEnd();
                string err = string.Empty;
                return content;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
