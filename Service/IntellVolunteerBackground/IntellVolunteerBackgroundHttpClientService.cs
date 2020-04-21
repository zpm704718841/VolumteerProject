using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ViewModel.VolunteerBackground.MiddleModel;

namespace Dto.Service.IntellVolunteerBackground
{
    public class IntellVolunteerBackgroundHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private IOptions<VolunteerBackgroundTokenModel> _IOptions;
        public IntellVolunteerBackgroundHttpClientService(IOptions<VolunteerBackgroundTokenModel> iOptions, IHttpClientFactory httpClientFactory)
        {
            _IOptions = iOptions;
            _httpClientFactory = httpClientFactory;
        }
        ///
        public async Task<VolunteerBackgroundTokenModel> getWeChatTokenAsync()
        {
            var client = _httpClientFactory.CreateClient("VolunteerBackgroundTokenModel");//必须和services.AddHttpClient()中指定的名称对应

            string content = "?grant_type=" + _IOptions.Value.grant_type + "&appid=" + _IOptions.Value.appid + "&secret=" + _IOptions.Value.secret;

            var uri = new Uri(client.BaseAddress, content);//重新组合url
            var response = client.GetAsync(uri);//调用
            var result = await response.Result.Content.ReadAsStringAsync();

            var weChartTokenMiddles = JsonConvert.DeserializeObject<VolunteerBackgroundTokenModel>(result);
            return weChartTokenMiddles;
        }
    }
}
