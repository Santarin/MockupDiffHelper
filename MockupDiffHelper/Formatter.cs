using HtmlAgilityPack;
using System.Collections.Generic;
using System.IO;
using TidyManaged;

namespace MockupDiffHelper
{
    public class Formatter
    {
        public void ApplyHtmlAgilityPackFormating(string originalFilePath, string fixedDocumentFilePath)
        {
            var html = string.Empty;

            using (var reader = new StreamReader(originalFilePath))
            {
                html = reader.ReadToEnd();
            }

            var document = new HtmlDocument();

            document.LoadHtml(html);

            document.OptionOutputAsXml = true;
            document.OptionFixNestedTags = true;
            document.OptionAutoCloseOnEnd = true;

            document.Save(fixedDocumentFilePath);
        }

        /*public void ApplyXmlFormatting(string originalFilePath, string fixedDocumentFilePath)
        {
            //String Result = "";
            var html = string.Empty;

            using (var reader = new StreamReader(originalFilePath))
            {
                html = reader.ReadToEnd();
            }

            string formattedHtml = string.Empty;

            using (var memoryStream = new MemoryStream())
            {
                using (var xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.Unicode))
                {
                    var xmlDocument = new XmlDocument();

                    /*try
                    {#1#
                        // Load the XmlDocument with the XML.
                        xmlDocument.LoadXml(html);

                        xmlTextWriter.Formatting = Formatting.Indented;

                        // Write the XML into a formatting XmlTextWriter
                        xmlDocument.WriteContentTo(xmlTextWriter);
                        xmlTextWriter.Flush();
                        memoryStream.Flush();

                        // Have to rewind the MemoryStream in order to read
                        // its contents.
                        memoryStream.Position = 0;

                        // Read MemoryStream contents into a StreamReader.
                        var streamReader = new StreamReader(memoryStream);

                        // Extract the text from the StreamReader.
                        formattedHtml = streamReader.ReadToEnd();

                        //Result = FormattedXML;
                    /*}
                    catch (XmlException ex)
                    {
                        Result = ex.ToString();
                    }#1#

                    xmlTextWriter.Close();
                }
                memoryStream.Close();
            }

            using (var sv = new StreamWriter(fixedDocumentFilePath))
            {
                sv.Write(formattedHtml);
            }
            /*Debug.WriteLine(Result);
            return Result;#1#
        }*/

        public void ApplyFormatting(string originalFilePath, string fixedDocumentFilePath)
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
                //tdoc.WrapAt = 120;
                tdoc.WrapAt = 0;
                tdoc.AttributeSortType = SortStrategy.Alpha;
                tdoc.WrapScriptLiterals = true;

                //tdoc.NewInlineTags = "section, aside, header, nav, footer";
                
                tdoc.CleanAndRepair();

                tdoc.Save(fixedDocumentFilePath);
            }
        }

        public void ApplyFilters(string fixedFilePath, List<string> filters, string filteredFilePath)
        {
            var document = new HtmlDocument();

            document.Load(fixedFilePath);

            foreach (var filter in filters)
            {
                var nodes = document.DocumentNode.SelectNodes(filter);

                if (nodes != null)
                {
                    foreach (var node in nodes)
                    {
                        node.Remove();
                    }
                }
            }

            /*document.OptionOutputAsXml = true;
            document.OptionFixNestedTags = true;
            document.OptionAutoCloseOnEnd = true;*/

            document.Save(filteredFilePath);
        }
    }
}
