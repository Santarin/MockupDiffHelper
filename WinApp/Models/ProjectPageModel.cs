using System;
using System.Collections.Generic;
using WinApp.Orchestrators;

namespace WinApp.Models
{
    public class ProjectPageModel
    {
        public string Name { get; set; }
        public string MockupUrl { get; set; }
        public string AppUrl { get; set; }

        public List<string> Filters { get; set; }

        public string GetUrl(PageTypeToCompare compareType)
        {
            switch (compareType)
            {
                case PageTypeToCompare.Mockup:
                    return MockupUrl;
                case PageTypeToCompare.App:
                    return AppUrl;
                default: throw new NotSupportedException();
            }
        }
    }
}