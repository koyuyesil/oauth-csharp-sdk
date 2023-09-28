using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace XiaoMiOauth2
{
    class XiaoMiHttpClientTest
    {
        static Task Main()
        {
            //UriBuilderImprove uriBuilder = new UriBuilderImprove("http://www.baidu.com");
            //uriBuilder.Path = "s";
            //uriBuilder.QueryString["wd"] = "小米";
            //System.Console.WriteLine(HttpClient.getResponse(uriBuilder.ToString(), "GET", null));//测试httpclient能正常返回界面

            //b: F
            //c:11
            //d: viva_global
            //daemon_version:2
            //l: en
            //m:viva
            //r:GB
            //sn:0x5a6dacdfdfbdd746ab01a6ed542d4b277de0ab3149fab11159a8180e
            //v: V13.0.11.0.RGDMIXM


            var version = "V13.0.4.0.SGDMIXM";
            var codebase = "12";
            var device_name = "viva_global";
            var model = "viva";
            var branch = "F";
            var language = "en";
            var region = "GB";
            var sn = "0x5a6dacdfdfbdd746ab01a6ed542d4b277de0ab3149fab11159a8180e";
            var romzone = "2";
            var recovery_version = "2";

            long clientId = 179887661252608;
            string clientKey = "50S2mk2MTDHQFbV6O6kMjg==";
            string clientRedirectUri = "http://xiaomi.com";


            XiaoMiHttpClient httpClient = new XiaoMiHttpClient(clientId, clientKey);

            string updateUri = httpClient.GetUpdateURL(device_name, branch, region, "", "");
            var rep = HttpClient.getResponse(updateUri, "GET", null);
            Console.WriteLine(rep);
            Console.WriteLine();



            Fullfirmware fullfirmware =  JsonSerializer.Deserialize<Fullfirmware>(rep);

            var f = fullfirmware.LatestFullRom.filename;
            var v = fullfirmware.LatestFullRom.version;
            var h = fullfirmware.LatestFullRom.md5;
            var m = fullfirmware.MirrorList[0];
            var d = fullfirmware.LatestFullRom.description;
            var ds = fullfirmware.LatestFullRom.descriptionUrl;
            var dl = $"{m}/{v}/{f}";

            Console.WriteLine($"Filename: {f}");
            Console.WriteLine($"Version: {v}");
            Console.WriteLine($"MD5: {h}");
            Console.WriteLine($"Mirror: {m}");
            Console.WriteLine($"Desc: {d}");
            Console.WriteLine($"DescUrl: {ds}");
            Console.WriteLine($"Download: {dl}");

            Latestfullrom.startDownload(dl,f);



            string getTokenURI = httpClient.GetAuthorizeURL(clientRedirectUri, "token", null, null, false);
            Console.WriteLine(getTokenURI);
            Console.WriteLine();
            //System.Console.WriteLine(HttpClient.getResponse(getTokenURI, "GET", null));//能正常返回界面
            string refresh_token = "eJxjYGAQSS_9M8X40acUv_lnpTc3sjO8O5eRzMDAwMgQDyQZ0pakngLRwTeSwDSjvaghA8Pi2TFqIB4Du6GCkYKxggmQyVuUmlaUWpwRX5KfnZoHALQ_GGY";

            string getRefreshTokenURI = httpClient.GetRefreshTokenURL(clientRedirectUri, "refresh_token", refresh_token);
            Console.WriteLine(getRefreshTokenURI);
            Console.WriteLine();
            string HTTP = "GET";
            string URI = "/user/relation";
            string mac_key = "b22OmhVgdRIVEPSWAeCyWaEA7GA";
            string token = "eJxjYGAQqVSyKaqq1FK_NFfUU2Yb99pj4uoXGBgYGBnigSRDiHOWBYgOPnMYTDPaixoyMCyeHaMG4jGwGyoYKRgrmACZzLmJyQDggRAr";

            XiaoMiApiHttpClient apiHttpClient = new XiaoMiApiHttpClient(clientId, clientKey);

            string apiResponse = apiHttpClient.apiCall(HTTP, URI, mac_key, token);
            Console.WriteLine(apiResponse);
            Console.WriteLine();
            Console.WriteLine(apiResponse.IndexOf("成功") != -1);
            Console.WriteLine();
            Console.ReadKey();
            return Task.CompletedTask;
        }
    }
}