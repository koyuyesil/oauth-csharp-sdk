using System;
using System.Net;

namespace XiaoMiOauth2
{
    /*
    {
        "latestfullrom": {
            "type": "fullrom",
                            "device": "viva_global",
                            "description": "MIUI刷机包",
                            "descriptionUrl": "http://update.miui.com/updates/updateinfo/V13.0.4.0.SGDMIXM/viva_global_0_V13.0.4.0.SGDMIXM_en_75240130879b237825f670c15dba885e.html",
                            "md5": "faf5fff298fc752ae771765adec9a564",
                            "filename": "viva_global_images_V13.0.4.0.SGDMIXM_20220904.0000.00_12.0_global_faf5fff298.tgz",
                            "filesize": "5.7G",
                            "codebase": "12.0",
                            "version": "V13.0.4.0.SGDMIXM",
                            "baseVersion": "V13.0.4.0.SGDMIXM"
                            },
            "MirrorList": ["http://bigota.d.miui.com"]
    }
    */
    public class Fullfirmware
    {
        public Latestfullrom LatestFullRom { get; set; }
        public string[] MirrorList { get; set; }
    }

    public class Latestfullrom
    {
        public string type { get; set; }
        public string device { get; set; }
        public string description { get; set; }
        public string descriptionUrl { get; set; }
        public string md5 { get; set; }
        public string filename { get; set; }
        public string filesize { get; set; }
        public string codebase { get; set; }
        public string version { get; set; }
        public string baseVersion { get; set; }

        public static void startDownload(string url, string filename)
        {
            
            WebClient client = new WebClient();
            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            client.DownloadFileAsync(new Uri(url), AppDomain.CurrentDomain.BaseDirectory+filename);   
        }

        private static void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;
            Console.WriteLine("Downloaded " + e.BytesReceived + " of " + e.TotalBytesToReceive+" %"+int.Parse(Math.Truncate(percentage).ToString()));
            
            //Console.Clear();
        }

        private static void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Console.WriteLine("Completed");
        }

    }

   

}
