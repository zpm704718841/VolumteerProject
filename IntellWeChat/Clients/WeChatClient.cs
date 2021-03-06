﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntellWeChat.Clients
{
    public class WeChatClient
    {
        public HttpClient Client { get; private set; }

        public WeChatClient(HttpClient httpClient)
        {
            string grant_type = "client_credential";
            string appid = "wx9948f9141aa016bf";
            string secret = "119f73843585cdda94422c81277f34a1";
            var content = "https://api.weixin.qq.com/cgi-bin/token?grant_type="+ grant_type + "&appid="+ appid + "&secret="+ secret;
            String tokenUrl = "25_DqUWKQ1e1KRaylonR5LSle1Fe38IinNSbB-I08Kf7adA8XLHRkJ02U6c7qsDpH11BQFK-E_junCXwd8TdYTf4qtUUkKznr0o87sjej6ECCMQU7Mm5anqpxqe6r5fujahB-4wh79AilBx51s8PCOaABAGTF";
            httpClient.BaseAddress = new Uri(tokenUrl);
            httpClient.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "HttpClientFactory-Sample");
            Client = httpClient;
        }

    }
}
