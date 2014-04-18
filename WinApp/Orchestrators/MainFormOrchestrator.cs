using System.IO;
using System.Xml.Serialization;
using WinApp.Models;

namespace WinApp.Orchestrators
{
    public class MainFormOrchestrator
    {
        private ProjectModel CurrentProject { get; set; }

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
    }
}