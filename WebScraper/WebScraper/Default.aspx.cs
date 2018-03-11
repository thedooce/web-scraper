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
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        
        protected void ScanBtn_Click(Object sender, EventArgs e)
        {
            string url = tbUrl.Text;
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            int wordCount = 0;

            Dictionary<string, int> dict = new Dictionary<string, int>();

            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//text()"))
            {
                //string[] text = node.InnerText.Split(' ');
                //wordCount += text.Length;
                MatchCollection matches = Regex.Matches(node.InnerText, @"\b(?:[a-z]{2,}|[ai])\b", RegexOptions.IgnoreCase);

                foreach (Match s in matches)
                {
                    if (dict.ContainsKey(s.Value))
                    {
                        dict[s.Value]++;
                    }
                    else
                    {
                        dict.Add(s.Value, 1);
                    }
                }
                
            }
            lblCount.Text = wordCount.ToString();

            List<KeyValuePair<string, int>> list = dict.ToList();

            list.Sort((pair1, pair2) => pair2.Value.CompareTo(pair1.Value));
            var trs = list.Select(a => String.Format("<tr><td>{0}</td><td>{1}</td></tr>", a.Key, a.Value));

            var tableContents = String.Concat(trs);

            var table = "<table>" + tableContents + "</table>";
            
            litWords.Text = table;


            var urls = doc.DocumentNode.Descendants("img")
                .Select(x => x.GetAttributeValue("src", null))
                .Where(s => !String.IsNullOrEmpty(s));
            

        }
    }
}