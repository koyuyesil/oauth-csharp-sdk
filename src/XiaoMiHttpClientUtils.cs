using System;
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
    class XiaoMiHttpClientUtils
    {
        static long localTimeDiff = dateDiff(DateTime.Now, getServerTime());
        static private Random ran = new Random(Dns.GetHostName().GetHashCode() + (int)DateTime.Now.Ticks);//根据主机名的hashcode和现在时间得到随即种子;
        static long dateDiff(DateTime DateTimeStart, DateTime DateTimeEnd)
        {
            long dateDiff = 0;
            TimeSpan end = new TimeSpan(DateTimeEnd.Ticks);
            TimeSpan start = new TimeSpan(DateTimeStart.Ticks);
            TimeSpan diff = end.Subtract(start).Duration();
            dateDiff = (diff.Days * 24 + diff.Hours);
            dateDiff = dateDiff * 60 + diff.Minutes;
            //Console.WriteLine("get the timediff " + dateDiff);
            return dateDiff;
        }
        static public String createHeader(string nonce, string mac, string accessToken)
        {
            string header = string.Format("MAC access_token=\"{0}\",nonce=\"{1}\",mac=\"{2}\"", Uri.EscapeDataString(accessToken), Uri.EscapeDataString(nonce), Uri.EscapeDataString(mac));
            return header;
        }
        static public String createCiphertext(string nonce, string query, string method, string localPath, string macKey)
        {
            method = method.ToUpperInvariant();
            string ciphertext = nonce + "\n" + method + "\n" + XiaoMiHttpClientConst.hostURI + "\n" + localPath + "\n" + query + "\n";
            HMACSHA1 hmacsha1 = new HMACSHA1();
            hmacsha1.Key = Encoding.UTF8.GetBytes(macKey);
            byte[] dataBuffer = Encoding.UTF8.GetBytes(ciphertext);
            byte[] hashBytes = hmacsha1.ComputeHash(dataBuffer);
            ciphertext = Convert.ToBase64String(hashBytes);
            return ciphertext;
        }
        static public DateTime getServerTime()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(XiaoMiHttpClientConst.apiURI);
            request.Method = "GET";
            HttpWebResponse response;
            Console.WriteLine("waiting for getting the server time");
            Console.WriteLine("");
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }
            if (response == null) Console.WriteLine("error： can not get the server time ,please check the network connections");
            return response.LastModified;
        }
        static public string getNonce()
        {
            //产生随机数过程
            int randKey = ran.Next();
            string nonce = randKey + ":" + (dateDiff(new DateTime(1970, 1, 1, 8, 0, 0), DateTime.Now) + localTimeDiff);
            return nonce;
        }
    }
}
