using MockupDiffHelper;
using MockupDiffHelper.Downloader;

namespace ConsoleApp
{
    class Program
    {
        public static App _app;

        static void Main(string[] args)
        {
            _app = new App();

            //_app.TestEthalonDownload();
            //_app.TestApplicantDownload();
            _app.TestEthalonFormatting();
        }
    }

    public class App
    {
        public void TestEthalonDownload()
        {
            var ethalonUrl = "file:///D:/PROJECTS/OSL-BW-Offshore/html-mockup/converted-html/index.html";
            var applicantUrl = string.Empty;
            var dataFolderPath = @"D:\PROJECTS\MockupDiffHelper\Data\";

            var downloadedDocumentPath = @"D:\PROJECTS\MockupDiffHelper\Data\BW.Offshore\FrontPage\Ethalon\Original\index.html";

            var helper = new Downloader(ethalonUrl, applicantUrl, dataFolderPath);

            helper.Download(ethalonUrl, downloadedDocumentPath);
        }

        public void TestApplicantDownload()
        {
            var ethalonUrl = string.Empty;
            var applicantUrl = "http://bw.offshore.local/";
            var dataFolderPath = @"D:\PROJECTS\MockupDiffHelper\Data\";

            var downloadedDocumentPath = @"D:\PROJECTS\MockupDiffHelper\Data\BW.Offshore\FrontPage\Applicant\Original\index.html";

            var helper = new Downloader(ethalonUrl, applicantUrl, dataFolderPath);

            helper.Download(applicantUrl, downloadedDocumentPath);
        }

        public void TestEthalonFormatting()
        {
            var sourceFileName = @"D:\PROJECTS\MockupDiffHelper\Data\BW.Offshore\FrontPage\Ethalon\Original\index.html";
            var destFileName = @"D:\PROJECTS\MockupDiffHelper\Data\BW.Offshore\FrontPage\Ethalon\Fixed\index.html";
            var formatter = new Formatter();

            formatter.TestEthalonFormatting(sourceFileName, destFileName);
        }
    }
}
