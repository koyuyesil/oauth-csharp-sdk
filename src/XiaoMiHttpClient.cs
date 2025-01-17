﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.IO;
using System.Data;
using System.Net.Security;
using System.Security.Cryptography;

namespace XiaoMiOauth2
{
    class XiaoMiHttpClient
    {
        readonly long clientId;
        readonly string clientKey;
        /**
         * 构造方法
         * @param clientId为客户端
         * @param clientKey为id密码
         * @return 
         */
        public XiaoMiHttpClient(long clientId, String clientKey)
        {
            this.clientId = clientId;
            this.clientKey = clientKey;
        }
        /**
         * 获得权限的uri
         * @param redirectUri返回uri
         * @param responseType返回类型填token或code
         * @param scope 权限，没有为null
         * @param state 可选传参，没有为null
         * @param skipConfirm是否为黄页，默认为false
         * @return 权限的uri
         */
        public String GetAuthorizeURL(String redirectUri, String responseType, String scope, String state, bool skipConfirm)
        {
            UriBuilderImprove urlBuilder = new UriBuilderImprove(XiaoMiHttpClientConst.authorizeURI);
            urlBuilder.QueryString[XiaoMiHttpClientConst.clientIdName] = clientId.ToString();
            urlBuilder.QueryString[XiaoMiHttpClientConst.redirectUriName] = redirectUri;
            urlBuilder.QueryString[XiaoMiHttpClientConst.responseTypeName] = responseType;
            if (scope != null) urlBuilder.QueryString[XiaoMiHttpClientConst.scopeeName] = scope;
            if (state != null) urlBuilder.QueryString[XiaoMiHttpClientConst.stateName] = state;
            if (skipConfirm) urlBuilder.QueryString[XiaoMiHttpClientConst.skipConfirmName] = skipConfirm.ToString();
            return urlBuilder.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="redirectUri"></param>
        /// <param name="grantType"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public String GetRefreshTokenURL(String redirectUri, String grantType, String val)
        {
            UriBuilderImprove urlBuilder = new UriBuilderImprove(XiaoMiHttpClientConst.tokenURI);
            urlBuilder.QueryString[XiaoMiHttpClientConst.clientIdName] = clientId.ToString();
            urlBuilder.QueryString[XiaoMiHttpClientConst.redirectUriName] = redirectUri;
            urlBuilder.QueryString[XiaoMiHttpClientConst.clientSecretName] = clientKey;
            if (grantType.Equals(XiaoMiHttpClientConst.refreshTokenName))
            {
                urlBuilder.QueryString[XiaoMiHttpClientConst.grantTypeName] = XiaoMiHttpClientConst.refreshTokenName;
                urlBuilder.QueryString[XiaoMiHttpClientConst.refreshTokenName] = val;
            }
            else
            {
                urlBuilder.QueryString[XiaoMiHttpClientConst.grantTypeName] = XiaoMiHttpClientConst.authorizationCodeName;
                urlBuilder.QueryString[XiaoMiHttpClientConst.codeName] = val;
            }
            return urlBuilder.ToString();
        }

        /// <summary>
        /// http://update.miui.com/updates/miota-fullrom.php?d=vili_eea_global&b=F&r=eea&n=&l=en_US
        /// http://update.miui.com/updates/miota-fullrom.php?d=vili_global&b=F&r=global&n=&l=en_US
        /// </summary>
        /// <param name="device"></param>
        /// <param name="branch"></param>
        /// <param name="region"></param>
        /// <param name="n"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public String GetUpdateURL(String device, String branch, String region, String n, String lang)
        {

            UriBuilderImprove urlBuilder = new UriBuilderImprove(XiaoMiHttpClientConst.updateURI);
            urlBuilder.Path = "updates/miota-fullrom.php";
            urlBuilder.QueryString["d"] = device;
            urlBuilder.QueryString["b"] = branch.ToUpper();
            urlBuilder.QueryString["r"] = region.ToLower().Replace("gb","global").Replace("cn", "china");
            urlBuilder.QueryString["n"] = n;
            urlBuilder.QueryString["l"] = lang;
            return urlBuilder.ToString();
        }

        /// <summary>
        /// https://update.intl.miui.com/updates/miotaV3.php?d=joyeuse_global&b=F&r=global&n=&l=en_GB
        /// </summary>
        /// <param name="device"></param>
        /// <param name="branch"></param>
        /// <param name="region"></param>
        /// <param name="n"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        public String GetMiotaV3UpdateURL(String device, String branch, String region, String n, String lang)
        {
            UriBuilderImprove urlBuilder = new UriBuilderImprove(XiaoMiHttpClientConst.updateURIv3);
            urlBuilder.Path = "/updates/miotaV3.php";
            urlBuilder.QueryString["d"] = device;
            urlBuilder.QueryString["b"] = branch;
            urlBuilder.QueryString["r"] = region;
            urlBuilder.QueryString["n"] = n;
            urlBuilder.QueryString["l"] = lang;
            return urlBuilder.ToString();
        }

    }
}
