using Dto.IRepository.IntellWeChat;
using Dtol;
using Dtol.dtol;
using Dtol.WGWdtol;
using Dtol.EfCoreExtion;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Net.Http;
using ViewModel.WeChatViewModel.RequestViewModel;
using ViewModel.WeChatViewModel.ResponseModel;
using ViewModel.WeChatViewModel.MiddleModel;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Dto.Repository.IntellWeChat
{
    public class WeChatClientRepository : IWeChatClientRepository
    {
        //志愿者数据库
        protected readonly DtolContext Db;
        protected readonly DbSet<V_GetToken> DbSet;

        //微官网数据库 20191030
        protected readonly WGWDtolContext WGWDb;
        protected readonly DbSet<Dtol.WGWdtol.UserInfo> WGWDbSet;


        //泰便利Easy 数据库 20200510
        protected readonly EasyDtolContext EasyDb;
        protected readonly DbSet<Dtol.Easydtol.UserInfo> EasyDbSet;


        public WeChatClientRepository(DtolContext context, WGWDtolContext WGWcontext, EasyDtolContext easyDtol)
        {
            Db = context;
            DbSet = Db.Set<V_GetToken>();

            WGWDb = WGWcontext;
            WGWDbSet= WGWDb.Set<Dtol.WGWdtol.UserInfo>();

            EasyDb = easyDtol;
            EasyDbSet = EasyDb.Set<Dtol.Easydtol.UserInfo>();
        }

        public IQueryable<V_GetToken> GetAll()
        {
            throw new NotImplementedException();
        }

        public string getToken()
        {
            string token = "";
            var predicate = WhereExtension.True<V_GetToken>();

            var result = DbSet.Where(predicate)
                .OrderByDescending(o => o.addtime).ToList();

            if (result.Count != 0)
            {
                token = result.First().token;
                DateTime time = result.First().addtime;

                //token 有效期为2小时，超过2小时 token过期，返回值为空
                if (DateTime.Now >= time.AddHours(2))
                {
                    token = string.Empty;
                }
            }
            else
            {
                token = string.Empty;
            }
            
            return token;
        }



        public virtual void Add(V_GetToken obj)
        {
            DbSet.Add(obj);
        }
 
        public virtual V_GetToken GetById(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual void Update(V_GetToken obj)
        {
            DbSet.Update(obj);
        }

        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

 
        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }


        public virtual void Remove(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
        }


        /// <summary>  
        /// 获取OpenId和SessionKey的Json数据包  
        /// </summary>  
        /// <param name="code">客户端发来的code</param>  
        /// <returns>Json数据包</returns>  
        public string GetOpenIdAndSessionKeyString(string code,string appId,string appSecret)
        {
            string temp = "https://api.weixin.qq.com/sns/jscode2session?" +
                "appid=" + appId
                + "&secret=" + appSecret
                + "&js_code=" + code
                + "&grant_type=authorization_code";

            return GetPage(temp);

        }


        /// <summary>  
        /// 根据微信小程序平台提供的解密算法解密数据，推荐直接使用此方法  
        /// </summary>  
        /// <param name="codeModel">登陆信息</param>  
        /// <returns>用户信息</returns>  
        public WeChatUserCheckResModel Decrypt(WeChatCodeModel codeModel, string appId, string appSecret)
        {
            WeChatUserCheckResModel userInfoCheck = new WeChatUserCheckResModel();

            if (codeModel == null)
                return null;

            if (String.IsNullOrEmpty(codeModel.code))
                return null;
            WeChatInfoModel oiask = JsonConvert.DeserializeObject<WeChatInfoModel>(GetOpenIdAndSessionKeyString(codeModel.code, appId, appSecret));

            userInfoCheck = CheckUser(oiask.unionid);

            return userInfoCheck;
        }

        /// <summary>
        /// 用户初次进入自愿者小程序验证用户是否是微官网已注册用户，如果是返回微官网用户中心信息，如果不是返回空
        /// </summary>
        public Dtol.WGWdtol.UserInfo WGWDecrypt(string  code, string appId, string appSecret)
        {
            Dtol.WGWdtol.UserInfo WGWuserInfo = new Dtol.WGWdtol.UserInfo();

            if (code == null || code=="")
                return null;

            if (String.IsNullOrEmpty(code))
                return null;
            WeChatInfoModel oiask = JsonConvert.DeserializeObject<WeChatInfoModel>(GetOpenIdAndSessionKeyString(code, appId, appSecret));

            WGWuserInfo = GetUser(oiask.unionid);
            if(WGWuserInfo.ID != null)
            {
                string photopath = WGWuserInfo.PhotoUrl;
                if (!photopath.Contains("https"))
                {
                    photopath = photopath.Substring(2, (photopath.Length - 2));
                    WGWuserInfo.PhotoUrl = "http://wgw.bhteda.com" + photopath;
                }
            }
       
            return WGWuserInfo;
        }


        /// <summary>
        /// 用户初次进入自愿者小程序验证用户是否是泰便利已注册用户，如果是返回泰便利用户中心信息，如果不是返回空  20200510
        /// </summary>
        public Dtol.Easydtol.UserInfo EasyDecrypt(string code, string appId, string appSecret)
        {
            Dtol.Easydtol.UserInfo userInfo = new Dtol.Easydtol.UserInfo();

            if (code == null || code == "")
                return null;

            if (String.IsNullOrEmpty(code))
                return null;
            WeChatInfoModel oiask = JsonConvert.DeserializeObject<WeChatInfoModel>(GetOpenIdAndSessionKeyString(code, appId, appSecret));

            userInfo = GetEasyUser(oiask.unionid);
            return userInfo;
        }

        /// <summary>  
        /// 根据微信小程序平台提供的签名验证算法验证用户发来的数据是否有效  
        /// </summary>  
        /// <param name="rawData">公开的用户资料</param>  
        /// <param name="signature">公开资料携带的签名信息</param>  
        /// <param name="sessionKey">从服务端获取的SessionKey</param>  
        /// <returns>True：资料有效，False：资料无效</returns> 
        public virtual bool VaildateUserInfo(string rawData, string signature, string sessionKey)
        {
            //创建SHA1签名类  
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            //编码用于SHA1验证的源数据  
            byte[] source = Encoding.UTF8.GetBytes(rawData + sessionKey);
            //生成签名  
            byte[] target = sha1.ComputeHash(source);
            //转化为string类型，注意此处转化后是中间带短横杠的大写字母，需要剔除横杠转小写字母  
            string result = BitConverter.ToString(target).Replace("-", "").ToLower();
            //比对，输出验证结果  
            return signature == result;
        }


        public virtual WechatUserInfoResModel Decrypt(string encryptedData, string iv, string sessionKey)
        {
            WechatUserInfoResModel userInfo;
                //创建解密器生成工具实例  
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                //设置解密器参数  
                aes.Mode = CipherMode.CBC;
                aes.BlockSize = 128;
                aes.Padding = PaddingMode.PKCS7;
                //格式化待处理字符串  
                byte[] byte_encryptedData = Convert.FromBase64String(encryptedData);
                byte[] byte_iv = Convert.FromBase64String(iv);
                byte[] byte_sessionKey = Convert.FromBase64String(sessionKey);

                aes.IV = byte_iv;
                aes.Key = byte_sessionKey;
                //根据设置好的数据生成解密器实例  
                ICryptoTransform transform = aes.CreateDecryptor();

                //解密  
                byte[] final = transform.TransformFinalBlock(byte_encryptedData, 0, byte_encryptedData.Length);

                //生成结果  
                string result = Encoding.UTF8.GetString(final);

                userInfo = JsonConvert.DeserializeObject<WechatUserInfoResModel>(result);

                return userInfo;

        }

        //注册志愿者时 验证用户是否是 微官网用户
        public virtual WeChatUserCheckResModel CheckUser(string unionid)
        {
            WeChatUserCheckResModel userInfoCheck = new WeChatUserCheckResModel();

            var predicate = WhereExtension.True<UserInfo>();
            predicate = predicate.And(p => p.unionid.Contains(unionid));
            predicate = predicate.And(p => !String.IsNullOrEmpty(p.Mobile));

            var result = WGWDbSet.Where(predicate);

            if (result.Count() > 0 )
            {
                userInfoCheck.ID = result.First().ID;
                userInfoCheck.unionId = result.First().unionid;
                userInfoCheck.nickName = result.First().nickname;
            }
            return userInfoCheck;
        }

        //注册志愿者时 获取 微官网的用户信息
        public virtual Dtol.WGWdtol.UserInfo GetUser(string unionid)
        {
            UserInfo WGWuserInfo = new UserInfo();
            var predicate = WhereExtension.True<UserInfo>();
            predicate = predicate.And(p => p.unionid.Contains(unionid));
            predicate = predicate.And(p => !String.IsNullOrEmpty(p.Mobile));

            var result = WGWDbSet.Where(predicate);

            if (result.Count() > 0)
            {
                WGWuserInfo = result.First();
            }

            return WGWuserInfo;
        }


        //注册志愿者时 获取 泰便利用户信息 20200510
        public virtual Dtol.Easydtol.UserInfo GetEasyUser(string unionid)
        {
            Dtol.Easydtol.UserInfo userInfo = new Dtol.Easydtol.UserInfo();
            var predicate = WhereExtension.True<Dtol.Easydtol.UserInfo>();
            predicate = predicate.And(p => p.Status.Equals("1"));
            predicate = predicate.And(p => p.unionid.Equals(unionid));
        

            var result = EasyDbSet.Where(predicate);

            if (result.Count() > 0)
            {
                userInfo = result.First();
            }

            return userInfo;
        }



        public virtual string GetPage(string posturl)
        {
          
            HttpWebResponse response = null;
            HttpWebRequest request = WebRequest.Create(posturl) as HttpWebRequest;

            CookieContainer cookieContainer = new CookieContainer();
            request.CookieContainer = cookieContainer;
            request.AllowAutoRedirect = true;
            request.Method = "GET";
               
            //发送请求并获取相应回应数据
            response = (HttpWebResponse)request.GetResponse();
            Stream instream = response.GetResponseStream();

            //返回结果网页（html）代码
            StreamReader sr = new StreamReader(instream);
            //返回结果网页（html）代码
            string content = sr.ReadToEnd();
            string err = string.Empty;
            return content;

        }

    
    }
}
