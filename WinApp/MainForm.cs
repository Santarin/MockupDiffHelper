using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using WinApp.Models;
using WinApp.Orchestrators;

namespace WinApp
{
    public partial class MainForm : Form
    {
        private MainFormOrchestrator Orchestrator { get; set; }

        public MainForm()
        {
            InitializeComponent();

            Orchestrator = new MainFormOrchestrator();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*var dialogResult = openProjectFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                var filePath = openProjectFileDialog.FileName;

                filePath = @"D:\PROJECTS\MockupDiffHelper\WinApp\Data\ProjectConfig";

                var projectLoaded = Orchestrator.LoadProject(filePath);

                if (projectLoaded)
                {
                    // update UI
                }
            }*/

            var filePath = @"D:\PROJECTS\MockupDiffHelper\WinApp\Data\ProjectConfig1.xml";

            var projectLoaded = Orchestrator.LoadProject(filePath);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var project = new ProjectModel()
            {
                Name = "BW.Offshore",

                Pages = new List<ProjectPageModel>()
                {
                    new ProjectPageModel
                    {
                        AppUrl = "http://bw.offshore.local/",
                        MockupUrl = "file:///D:/PROJECTS/OSL-BW-Offshore/html-mockup/converted-html/index.html",
                        Name = "Front Page",
                        Filters = new List<string>
                        {
                            "//ul[@class='nav nav-primary']"
                        }
                    },
                    new ProjectPageModel
                    {
                        AppUrl = "http://bw.offshore.local/articles-list/article-11/",
                        MockupUrl = "file:///D:/PROJECTS/OSL-BW-Offshore/html-mockup/converted-html/article.html",
                        Name = "Article"
                    }
                }
            };

            var filePath = @"D:\PROJECTS\MockupDiffHelper\WinApp\Data\ProjectConfig1.xml";

            Orchestrator.SaveProject(filePath, project);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var filePath = @"D:\PROJECTS\MockupDiffHelper\WinApp\Data\ProjectConfig1.xml";

            var projectLoaded = Orchestrator.LoadProject(filePath);

            if (projectLoaded)
            {
                // Let's compare [0] page - Front Page
                var page = Orchestrator.CurrentProject.Pages[0];

                // calculate local destination
                var mockupFilePath = Orchestrator.GetPageLocalPath(page, PageToCompareType.Etalon, ModificationType.Etalon, "index.html");
                var applicationFilePath = Orchestrator.GetPageLocalPath(page, PageToCompareType.Applicant, ModificationType.Etalon, "index.html");

                // download files
                Orchestrator.Download(page.MockupUrl, mockupFilePath);
                Orchestrator.Download(page.AppUrl, applicationFilePath);

                // fix formatting
                var fixedMockupFilePath = Orchestrator.GetPageLocalPath(page, PageToCompareType.Etalon, ModificationType.Fixed, "index.html");
                var fixedApplicationFilePath = Orchestrator.GetPageLocalPath(page, PageToCompareType.Applicant, ModificationType.Fixed, "index.html");

                Orchestrator.FixFormatting(page, mockupFilePath, fixedMockupFilePath);
                Orchestrator.FixFormatting(page, applicationFilePath, fixedApplicationFilePath);

                // apply filters
                var filteredMockupFilePath = Orchestrator.GetPageLocalPath(page, PageToCompareType.Etalon, ModificationType.Fixed, "filtered.html");
                var filteredApplicationFilePath = Orchestrator.GetPageLocalPath(page, PageToCompareType.Applicant, ModificationType.Fixed, "filtered.html");

                Orchestrator.ApplyFilters(page, fixedMockupFilePath, filteredMockupFilePath);
                Orchestrator.ApplyFilters(page, fixedApplicationFilePath, filteredApplicationFilePath);

                // run Win Merge
                var winMergeCommandArgs = string.Format("/e \"{0}\" \"{1}\"", filteredApplicationFilePath, filteredMockupFilePath);

                var startInfo = new ProcessStartInfo
                {
                    FileName = @"C:\Program Files (x86)\WinMerge\WinMergeU.exe",
                    Arguments = winMergeCommandArgs
                };

                Process.Start(startInfo);
            }
        }
    }
}
