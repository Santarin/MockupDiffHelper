using System;
using System.Collections.Generic;
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
                            "//ul[@class='nav nav-primary']",
                            "test1",
                            "test2"
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

        }
    }
}
