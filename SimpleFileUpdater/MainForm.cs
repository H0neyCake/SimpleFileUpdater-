using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;


namespace SimpleFileUpdater
{
    public partial class MainForm : Form
    {
        WebClient webClient = new WebClient();
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            VersionChecker verChecker = new VersionChecker();
            string ServerVersion = "http://your_site/version.ini"; //VERSION URL PATH
            FileStream fs = new FileStream(@"version.ini", FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.Default);
            string localVersion = sr.ReadToEnd();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ServerVersion);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                ServerVersion = reader.ReadToEnd();
                if (verChecker.NewVersionExists(localVersion, ServerVersion))
                {
                    label2.Text += "A new version is available";
                }
                else
                    label2.Text += "You have the latest version.";
            }
            fs.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Uri uri = new Uri("http://your_site/FILENAME.xml"); //FILE URL PATH
            webClient.DownloadFileAsync(uri, "FILENAME.xml");
            webClient.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(WebClient_DownloadProgressChanged);
            webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(WebClient_DownloadFileCompleted);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://your_site/default_changelog.txt"); //NEWS URL PATH
        }
        void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            label1.Text = String.Format("Uploaded: {0} Kbytes / {1} Kbytes ", e.BytesReceived - 1024, e.TotalBytesToReceive - 1024);
            progressBar1.Value = e.ProgressPercentage;
        }
        void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            MessageBox.Show(text: "File downloaded!");
            progressBar1.Value = 0;
            label1.Text = "";
            string localVersionFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "version.ini");
            string ServerVersionFile = "http://your_site/version.ini"; //VERSION URL PATH
            if (File.Exists(localVersionFile))
            {
                string localVersion = null;
                string ServerVersion = null;
                using (StreamReader sr = File.OpenText(localVersionFile))
                    localVersion = sr.ReadLine();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ServerVersionFile);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    ServerVersion = reader.ReadToEnd();
                if (!string.IsNullOrWhiteSpace(localVersion))
                {
                    using (StreamWriter sw = new StreamWriter(localVersionFile))
                        sw.Write(ServerVersion);
                }
            }
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Uri uri = new Uri("http:/your_site/FILENAME.xml");//FILE URL PATH
            webClient.DownloadFileAsync(uri, "FILENAME.xml");
            webClient.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(WebClient_DownloadProgressChanged);
            webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(WebClient_DownloadFileCompleted);

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://your_site/default_changelog.txt"); //NEWS URL PATH
           
        }
    }
}
