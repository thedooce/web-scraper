﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace WebScraper
{
    public partial class _Default : Page
    {
        public Uri inputUrl;
        public int wordCount;
        public Dictionary<string, int> textDict;
        public List<string> imgUrls;

        protected void Page_Load(object sender, EventArgs e)
        {
            wordCount = 0;
            textDict = new Dictionary<string, int>();
            imgUrls = new List<string>();
        }
        
        protected void ScanBtn_Click(Object sender, EventArgs e)
        {
            inputUrl = new Uri(tbUrl.Text);
            wordCount = 0;

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(inputUrl);

            ParseTextData(doc);
            ParseImageData(doc);

            RenderTextTable();
            RenderCarousel();
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
            imgUrls = doc.DocumentNode.Descendants("img")
                .Select(x => x.GetAttributeValue("src", null))
                .Where(s => !String.IsNullOrEmpty(s))
                .ToList();
        }

        protected void RenderCarousel()
        { 

            if (imgUrls.Count > 0)
            {
                var carouselInnerHtml = new StringBuilder();
                var indicatorsHtml = new StringBuilder(@"<ol class='carousel-indicators'>");

                //loop through and build up the html for indicators + images
                for (int i = 0; i < imgUrls.Count; i++)
                {
                    Uri imageUri = new Uri(inputUrl, imgUrls[i]);
                    carouselInnerHtml.AppendLine(i == 0 ? "<div class='item active'>" : "<div class='item'>");
                    carouselInnerHtml.AppendLine("<img src='" + imageUri + "' alt='Slide #" + (i + 1) + "'>");
                    carouselInnerHtml.AppendLine("</div>");
                    indicatorsHtml.AppendLine(i == 0 ? @"<li data-target='#myCarousel' data-slide-to='" + i + "' class='active'></li>" : @"<li data-target='#myCarousel' data-slide-to='" + i + "' class=''></li>");
                }
                //close tag
                indicatorsHtml.AppendLine("</ol>");
                //stick the html in the literal tags and the cache
                litCarouselImages.Text = carouselInnerHtml.ToString();
                litCarouselIndicators.Text = indicatorsHtml.ToString();

                myCarousel.Visible = true;
            }
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

            tblData.Visible = true;
        }
    }
}