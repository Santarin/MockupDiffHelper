using System;
using System.Diagnostics;
using System.Windows.Forms;
using WinApp.Models;
using WinApp.Orchestrators;

namespace WinApp
{
    public partial class MainForm : Form
    {
        private MainFormOrchestrator Orchestrator { get; set; }

        private bool _projectIsLoaded = false;

        public MainForm()
        {
            InitializeComponent();

            Orchestrator = new MainFormOrchestrator();

            ModifyUI();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openProjectFileDialog.ShowDialog() == DialogResult.OK)
            {
                _projectIsLoaded = Orchestrator.LoadProject(openProjectFileDialog.FileName);
            }

            ModifyUI();
        }

        private void ModifyUI()
        {
            button2.Enabled = _projectIsLoaded;
            chkDoNotReloadApp.Enabled = _projectIsLoaded;
            chkDoNotReloadMockup.Enabled = _projectIsLoaded;

            if (_projectIsLoaded)
            {
                Text = Orchestrator.CurrentProject.Name;
            }
            else
            {
                Text = "Please Load project";
            }
        }

        private string TryApplyModificationsAndGetFileToMergePath(ProjectPageModel page, PageTypeToCompare pageCompareType, bool applyModifications)
        {
            var fileWithAppliedFiltersPath = Orchestrator.GetPageLocalPath(page, pageCompareType,
                ModificationType.Fixed, "filtered.html");

            if (applyModifications)
            {
                var ethalonFilePath = Orchestrator.GetPageLocalPath(page, pageCompareType,
                    ModificationType.Original, "index.html");

                Orchestrator.Download(page.GetUrl(pageCompareType), ethalonFilePath);

                var fileWithFixedFormattingPath = Orchestrator.GetPageLocalPath(page, pageCompareType,
                    ModificationType.Fixed, "index.html");

                Orchestrator.FixFormatting(page, ethalonFilePath, fileWithFixedFormattingPath);

                Orchestrator.ApplyFilters(page, fileWithFixedFormattingPath, fileWithAppliedFiltersPath);
            }

            return fileWithAppliedFiltersPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (_projectIsLoaded)
            {
                var page = Orchestrator.CurrentProject.Pages[0];

                var rightFilePath = TryApplyModificationsAndGetFileToMergePath(page, PageTypeToCompare.Mockup, !chkDoNotReloadMockup.Checked);
                var leftFilePath = TryApplyModificationsAndGetFileToMergePath(page, PageTypeToCompare.App, !chkDoNotReloadApp.Checked);

                // run Win Merge
                TryShowDifference(leftFilePath, rightFilePath);
            }
            else
            {
                MessageBox.Show("Please load project file first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TryShowDifference(string leftFilePath, string rightFilePath)
        {
            if (!string.IsNullOrEmpty(leftFilePath) && !string.IsNullOrEmpty(rightFilePath))
            {
                var winMergeCommandArgs = string.Format("/e \"{0}\" \"{1}\"", leftFilePath, rightFilePath);

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
