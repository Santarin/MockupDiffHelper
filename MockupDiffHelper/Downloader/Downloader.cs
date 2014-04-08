using System;
using System.IO;
using System.Net;

namespace MockupDiffHelper.Downloader
{
    public class Downloader
    {
        private string _ethalonUrl;
        private string _applicantUrl;
        
        private string _dataFolderPath;
        private int _timeout = 60000;

        public string EthalonUrl
        {
            get { return _ethalonUrl; }
        }

        public string ApplicantUrl
        {
            get { return _applicantUrl; }
        }

        public string DataFolderPath
        {
            get { return _dataFolderPath; }
        }

        public Downloader(string ethalonUrl, string applicantUrl, string dataFolderPath)
        {
            // TODO: these paths should be configurable from UI
            _ethalonUrl = ethalonUrl;
            _applicantUrl = applicantUrl;
            _dataFolderPath = dataFolderPath;

            //_downloadedDocumentPath = Path.Combine(_dataFolderPath, Path.GetFileName(new Uri(_ethalonUrl).LocalPath));
        }

        public void Download(string url, string downloadedDocumentPath)
        {
            //CreateRepository();

            using (var client = GetWebClient())
            {
                client.Headers.Add(HttpRequestHeader.Accept, "*/*");
                client.Headers.Add(HttpRequestHeader.UserAgent, "MockupDiffChecker");

                client.DownloadFile(url, downloadedDocumentPath);
            }
        }
        
        private WebClient GetWebClient()
        {
            return new ExtendedWebClient(_timeout);
        }

        /*private void CreateRepository()
        {
            if (!Directory.Exists(_dataFolderPath))
            {
                Directory.CreateDirectory(_dataFolderPath);
            }
        }*/
    }
}
