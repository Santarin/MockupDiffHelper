using System.Collections.Generic;

namespace WinApp.Models
{
    public class ProjectModel
    {
        public string Name { get; set; }
        
        public List<ProjectPageModel> Pages { get; set; }
    }
}