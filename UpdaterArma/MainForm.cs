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

namespace UpdaterArma
{
    public partial class MainForm : Form
    {
        WebClient webClient = new WebClient();
        public MainForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            webBrowser1.Navigate("http://95.213.182.75/default_changelog.txt");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Uri uri = new Uri("http://95.213.182.75/addon_tto/patchlist.xml");
            webClient.DownloadFileAsync(uri, "patchlist.xml");
            webClient.DownloadProgressChanged += new System.Net.DownloadProgressChangedEventHandler(WebClient_DownloadProgressChanged);
            webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(WebClient_DownloadFileCompleted);

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
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
