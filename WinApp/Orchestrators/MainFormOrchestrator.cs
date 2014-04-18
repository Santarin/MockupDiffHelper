using System.IO;
using System.Text;
using System.Xml.Serialization;
using MockupDiffHelper;
using MockupDiffHelper.Downloader;
using WinApp.Models;

namespace WinApp.Orchestrators
{
    public class MainFormOrchestrator
    {
        public ProjectModel CurrentProject { get; set; }

        public Formatter Formatter { get; private set; }

        public string StoragePath
        {
            // {0} - Project Name
            // {1} - Page Name
            // {2} - "Applicant" or "Etalon"
            // {3} - "Fixed" or "Original"
            // {4} - File name
            get
            {
                //return @"D:\PROJECTS\MockupDiffHelper\Data\{0}\{1}\{2}\{3}\{4}";
                return @"D:\PROJECTS\MockupDiffHelper\Data\";
            }
        }

        public MainFormOrchestrator()
        {
            Formatter = new Formatter();
        }

        private const string Applicant = "Applicant";
        private const string Etalon = "Etalon";

        public bool LoadProject(string projectFilePath)
        {
            var serializer = new XmlSerializer(typeof (ProjectModel));

            using (var fileStream = new FileStream(projectFilePath, FileMode.Open))
            {
                CurrentProject = (ProjectModel) serializer.Deserialize(fileStream);
            }
            
            return true;
        }

        public void SaveProject(string projectFilePath, ProjectModel model)
        {
            var serializer = new XmlSerializer(typeof (ProjectModel));

            using (var streamWriter = new StreamWriter(projectFilePath))
            {
                serializer.Serialize(streamWriter, model);
            }
        }

        public string GetPageLocalPath(ProjectPageModel page, PageToCompareType pageType, ModificationType modification, string fileName)
        {
            var path = StoragePath;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = string.Format(@"{0}{1}\", path, CurrentProject.Name);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = string.Format(@"{0}{1}\", path, page.Name);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = string.Format(@"{0}{1}\", path, pageType);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = string.Format(@"{0}{1}\", path, modification);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = string.Format(@"{0}{1}", path, fileName);

            return path;
        }

        public void Download(string url, string downloadedDocumentPath)
        {
            var downloader = new Downloader();

            downloader.Download(url, downloadedDocumentPath);
        }

        public void FixFormatting(ProjectPageModel page, string sourceFilePath, string destinationFilePath)
        {
            //var formatter = new Formatter();

            Formatter.ApplyFormatting(sourceFilePath, destinationFilePath);
        }

        public void ApplyFilters(ProjectPageModel page, string sourceFilePath, string filteredFilePath)
        {
            Formatter.ApplyFilters(sourceFilePath, page.Filters, filteredFilePath);
        }
    }

    public enum PageToCompareType
    {
        Unknown = 0,
        Etalon = 1,
        Applicant = 2
    }

    public enum ModificationType
    {
        Unknown = 0,
        Etalon = 1,
        Fixed = 2
    }
}