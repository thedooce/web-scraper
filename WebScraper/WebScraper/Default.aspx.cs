using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace WebScraper
{
    public partial class _Default : Page
    {
        public int wordCount;
        public Dictionary<string, int> textDict;

        protected void Page_Load(object sender, EventArgs e)
        {
            wordCount = 0;
            textDict = new Dictionary<string, int>();
        }
        
        protected void ScanBtn_Click(Object sender, EventArgs e)
        {
            string url = tbUrl.Text;
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            wordCount = 0;

            ParseTextData(doc);
            ParseImageData(doc);

            RenderTextTable();

        }

        protected void ParseTextData(HtmlDocument doc)
        {
            foreach (var node in doc.DocumentNode.SelectSingleNode("//body").DescendantsAndSelf())
            {
                if (node.NodeType == HtmlNodeType.Text && node.ParentNode.Name != "script")
                {
                    MatchCollection matches = Regex.Matches(node.InnerText, @"\b(?:[a-z]{2,}|[ai])\b", RegexOptions.IgnoreCase);

                    foreach (Match s in matches)
                    {
                        if (textDict.ContainsKey(s.Value))
                        {
                            textDict[s.Value]++;
                        }
                        else
                        {
                            textDict.Add(s.Value, 1);
                        }
                        wordCount++;
                    }
                }

            }
        }

        protected void ParseImageData(HtmlDocument doc)
        {
            var urls = doc.DocumentNode.Descendants("img")
                .Select(x => x.GetAttributeValue("src", null))
                .Where(s => !String.IsNullOrEmpty(s))
                .ToList();
        }

        /// <summary>
        /// Displays the table for text count
        /// </summary>
        protected void RenderTextTable()
        {
            //Convert Dictionary<string, int> to KeyValuePair<string, int> for easier sorting. Sort the pairs by value descending. Take the top 7.
            List<KeyValuePair<string, int>> list = textDict.ToList();
            list.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            var topSevenItems = list.Take(7);

            //Create a TableRow with TableCells for each KeyValuePair. Populate our Table with these new rows.
            foreach (var item in topSevenItems)
            {
                TableRow tr = new TableRow();
                TableCell tc1 = new TableCell { Text = item.Key, HorizontalAlign=HorizontalAlign.Left };
                TableCell tc2 = new TableCell { Text = item.Value.ToString(), HorizontalAlign = HorizontalAlign.Left };
                tr.Cells.Add(tc1);
                tr.Cells.Add(tc2);
                tblData.Rows.Add(tr);
            }

            TableFooterRow trf = new TableFooterRow();
            TableCell tc = new TableCell { Text = String.Format("Total number of words: {0}", wordCount.ToString()), ColumnSpan=2 };
            trf.Cells.Add(tc);
            tblData.Rows.Add(trf);

        }
    }
}