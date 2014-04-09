using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using TidyManaged;

namespace MockupDiffHelper
{
    public class Formatter
    {
        public void TestEthalonFormatting(string originalFilePath, string fixedDocumentFilePath)
        {
            var html = string.Empty;

            using (var reader = new StreamReader(originalFilePath))
            {
                html = reader.ReadToEnd();
            }

            using (Document tdoc = Document.FromString(html))
            {
                tdoc.ShowWarnings = true;
                tdoc.Quiet = true;
                tdoc.MaximumErrors = int.MaxValue;
                tdoc.ForceOutput = true;
                tdoc.InputCharacterEncoding = EncodingType.Utf8;
                tdoc.OutputCharacterEncoding = EncodingType.Utf8;
                tdoc.OutputXhtml = true;
                tdoc.OutputXml = true;
                tdoc.IndentBlockElements = AutoBool.Yes;
                tdoc.IndentAttributes = false;
                tdoc.IndentCdata = true;
                tdoc.AddVerticalSpace = false;
                tdoc.WrapAt = 120;
                tdoc.AttributeSortType = SortStrategy.Alpha;
                tdoc.WrapScriptLiterals = true;

                tdoc.CleanAndRepair();

                tdoc.Save(fixedDocumentFilePath);
            }
        }
    }
}
