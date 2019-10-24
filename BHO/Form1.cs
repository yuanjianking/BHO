using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BHO
{
    public partial class Form1 : Form
    {
        //public string csrf = "";
        //public string jds = "";
        //public string jns = "";
        //public string jsd = "";
        //public string jdd = "";
        //public string cms = "";

        public string url = @"https://www-r1.e-ninsho.ne.jp/en-uw/v1/SSO";
        public string sfn = "";
        public string ric = "";
        public string ked = "";
        public string epr = "";
        public string itv = "";
        public string hav = "";
        public string spm = "";

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = textBox1.Text;

            Func<string, string> ReadCard = t =>
            {
                //    string p = string.Format("_csrf={0}&jds={1}&jns={2}&jsd{3}&jdd={4}&cms={5}", csrf, jds, jns, jsd, jdd, cms); 
                string p = @"sfn=test.e-ninsho.co.jp&ric=ENM101011A&ked=MDFmOTYxND&epr=JemdEcKUk4NnMlb0Ri43XwLyF9Dhh6-ACVCdikWaG9jqmA3KXeCTxUjjHENiFkz5jj_iOH-VuKMzb7bx-J-c8IA1SzXs4cn-32aHIR7AhsEEc76RgeGRZt9V0oH1qynoK6dBhJkT0BegG3cgeyo4yqsGUlaefG2URSM56y1KFeo5uhdHbyhxgY9qAzN6VSru6z9yJ0H-ppylnI4I2hqxLeCqYgoenT6W3A8uTW580KNEDfZan6WiqNKfGSUSRqCDsk337VqJzVpMClhgQxZuhwITCBX3yxbGzH1wvSD8t5AHr1tsWlbhMb4WDrxfHBzr&itv=ZZIJ1Zac5f3BhAVYxkjfLQ&hav=9419B0A5BADF3DFD9F31356341A9A5C9634C27FC6BD43B83681004ACD0D84190&spm=";
                return p;
            };

            Func<Func<string, string>, string> Send = r =>
             {
                 string msg = r(text);
                 string sso = "";
                 try
                 {
                     sso = HttpPost(url, msg);
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message);
                 }
                 return sso;
             };
            MessageBox.Show(Send(ReadCard));
        }

        private string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            request.AllowAutoRedirect = false;
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        public string HttpGet(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }
    }
}