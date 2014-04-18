using System.Collections.Generic;

namespace WinApp.Models
{
    public class ProjectPageModel
    {
        public string Name { get; set; }
        public string MockupUrl { get; set; }
        public string AppUrl { get; set; }

        public List<string> Filters { get; set; }
    }
}