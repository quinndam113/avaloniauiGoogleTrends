using GoogleTrendViewer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace GoogleTrendViewer.Services
{
    public class GoogleTrendService
    {
        //Url: https://trends.google.com.vn/trends/trendingsearches/daily/rss?geo=VN
        public List<TrendItem> GetTodayTrends(string url)
        {
            var result = new List<TrendItem>();
            try
            {
                var rssXmlDoc = new XmlDocument();
                rssXmlDoc.Load(url);

                var rssNodes = rssXmlDoc.SelectNodes("rss/channel/item");

                foreach (XmlNode rssNode in rssNodes)
                {
                    var childNodes = rssNode.ChildNodes.AsQueryable();

                    var item = new TrendItem
                    {
                        Title = childNodes.FirstOrDefault(x => x.Name == "title")?.InnerText,
                        Description = childNodes.FirstOrDefault(x => x.Name == "description")?.InnerText,
                        Link = childNodes.FirstOrDefault(x => x.Name == "link")?.InnerText,
                        Traffic = childNodes.FirstOrDefault(x => x.Name == "ht:approx_traffic")?.InnerText,
                        Picture = childNodes.FirstOrDefault(x => x.Name == "ht:picture")?.InnerText,
                        PictureSource = childNodes.FirstOrDefault(x => x.Name == "ht:picture_source")?.InnerText
                    };

                    var pubDateNode = childNodes.FirstOrDefault(x => x.Name == "pubDate");
                    if (pubDateNode != null)
                    {
                        item.PubDate = Convert.ToDateTime(pubDateNode.InnerText);
                    }

                    var newsNodes = childNodes.Where(x => x.Name == "ht:news_item");
                    foreach (XmlNode newsNode in newsNodes)
                    {
                        var newsChildNodes = newsNode.ChildNodes.AsQueryable();

                        var newsItem = new NewsItem
                        {
                            Title = newsChildNodes.FirstOrDefault(x => x.Name == "ht:news_item_title")?.InnerText,
                            Description = newsChildNodes.FirstOrDefault(x => x.Name == "ht:news_item_snippet")?.InnerText,
                            Source = newsChildNodes.FirstOrDefault(x => x.Name == "ht:news_item_source")?.InnerText,
                            Url = newsChildNodes.FirstOrDefault(x => x.Name == "ht:news_item_url")?.InnerText
                        };

                        item.NewsItems.Add(newsItem);
                    }

                    result.Add(item);
                }
            }
            catch { }

            return result;
        }
    }

    public static class XmlExtensions
    {
        public static IEnumerable<XmlNode> AsQueryable(this XmlNodeList list)
        {
            if (list == null) return new List<XmlNode>();
            return list.Cast<XmlNode>();
        }
    }
}
