 using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Logic.Managers
{
    public class ReadManager
    {
        /// <summary>
        /// Read xml by the standard library.
        /// </summary>
        /// <returns>List<Item></returns>
        public static List<Item> ReadXml()
        {
            var items = new List<Item>();
            XmlDocument doc = new();
            doc.Load("data.xml");
            XmlElement? elements = doc.DocumentElement;
            if (elements != null)
            {
                foreach (XmlElement el in elements)
                {
                    Item item = new Item();
                    foreach (XmlNode childnode in el.ChildNodes)
                    {
                        if (childnode.Name == "title")
                            item.Title = childnode.InnerText;
                        if (childnode.Name == "link")
                            item.Link = childnode.InnerText;
                        if (childnode.Name == "description")
                            item.Description = childnode.InnerText.Trim();
                        if (childnode.Name == "category")
                            item.Category = childnode.InnerText;
                        if (childnode.Name == "pubDate")
                            item.PubDate = DateTime.Parse(childnode.InnerText);
                    }
                    items.Add(item);
                }
            }
            return items;
        }
        /// <summary>
        /// Read xml with XPath.
        /// </summary>
        /// <returns>List<Item></returns>
        public static List<Item> ReadXmlXPath()
        {
            var items = new List<Item>();
            XmlDocument doc = new XmlDocument();
            doc.Load("data.xml");
            XmlElement? xRoot = doc.DocumentElement;
            XmlNodeList? nodes = xRoot?.SelectNodes("*");
            if (nodes is not null)
            {
                foreach (XmlNode node in nodes)
                {
                    Item item = new Item();
                    item.Title = node.SelectSingleNode("title")?.InnerText;
                    item.Link = node.SelectSingleNode("link")?.InnerText;
                    item.Description = node.SelectSingleNode("description")?.InnerText.Trim();
                    item.Category = node.SelectSingleNode("category")?.InnerText;
                    item.PubDate = DateTime.Parse(node.SelectSingleNode("pubDate")!.InnerText);
                    items.Add(item);
                }
            }
            return items;
        }
        /// <summary>
        /// Read xml with regex.
        /// </summary>
        /// <returns>List<Item></returns>
        public static async Task<List<Item>> ReadXmlRegexModelsAsync()
        {
            var items = new List<Item>();
            using (StreamReader reader = new StreamReader("data.xml"))
            {
                string? line = await reader.ReadToEndAsync();
                if (line is not null)
                {
                    string pattern = "(<item>)(.*?)(</item>)";
                    MatchCollection matches = Regex.Matches(line, pattern, 
                        RegexOptions.Singleline);
                    foreach (Match match in matches)
                    {
                        Item item = new Item();
                        item.Title = Regex.Match(match.Value, "(<title>)(.*?)(</title>)",
                            RegexOptions.Singleline).Groups[2].Value;
                        item.Link = Regex.Match(match.Value, "(<link>)(.*?)(</link>)", 
                            RegexOptions.Singleline).Groups[2].Value;
                        item.Description = Regex.Match(match.Value, "(<description>)(.*?)(</description>)",
                            RegexOptions.Singleline).Groups[2].Value.Trim();
                        item.Category = Regex.Match(match.Value, "(<category>)(.*?)(</category>)", 
                            RegexOptions.Singleline).Groups[2].Value;
                        item.PubDate = DateTime.Parse(Regex.Match(match.Value, "(<pubDate>)(.*?)(</pubDate>)", 
                            RegexOptions.Singleline).Groups[2].Value);
                        items.Add(item);
                    }
                }
            }
            return items;
        }
        /// <summary>
        /// Read xml with regex.
        /// </summary>
        /// <returns>string</returns>
        public static async Task<string> ReadXmlRegexStringAsync()
        {
            string result = string.Empty;
            var items = new List<Item>();
            using (StreamReader reader = new StreamReader("data.xml"))
            {
                string? line = await reader.ReadToEndAsync();
                if (line is not null)
                {
                    string pattern = "(<item>)(.*?)(</item>)";
                    MatchCollection matches = Regex.Matches(line, pattern, 
                        RegexOptions.Singleline);
                    foreach (Match match in matches)
                    {
                        result += Regex.Match(match.Value, "(<title>)(.*?)(</title>)",
                            RegexOptions.Singleline).Groups[2].Value + "\n";
                        result += Regex.Match(match.Value, "(<link>)(.*?)(</link>)",
                            RegexOptions.Singleline).Groups[2].Value + "\n";
                        result += (Regex.Match(match.Value, "(<description>)(.*?)(</description>)", 
                            RegexOptions.Singleline).Groups[2].Value).Trim()+"\n";
                        result += Regex.Match(match.Value, "(<category>)(.*?)(</category>)", 
                            RegexOptions.Singleline).Groups[2].Value + "\n";
                        result += Regex.Match(match.Value, "(<pubDate>)(.*?)(</pubDate>)", 
                            RegexOptions.Singleline).Groups[2].Value + "\n\n";
                    }
                }
            }
            return result;
        }
    }
}
