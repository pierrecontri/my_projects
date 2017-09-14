using System;
using System.Collections.Generic;
using System.Xml;

namespace ScreenSaverPierre.Rss
{
    /// <remarks>
    /// Représentation d'un élément Channel dans un document XML RSS 2.0.
    /// Un RssFeed contient un ou plusieurs RssChannels.
    /// </remarks>
    public class RssChannel
    {
        private readonly string title;
        private readonly string link;
        private List<RssItem> items;

        public string Title { get { return title; } }
        public string Link { get { return link; } }
        public IList<RssItem> Items { get { return items.AsReadOnly(); } }

        /// <summary>
        /// Crée un RSSChannel à partir d'un XmlNode représentant un élément Channel dans un document XML RSS 2.0.
        /// </summary>
        /// <param name="channelNode"></param>
        internal RssChannel(XmlNode channelNode)
        {
            items = new List<RssItem>();
            title = channelNode.SelectSingleNode("title").InnerText;
            link = channelNode.SelectSingleNode("link").InnerText;

            XmlNodeList itemNodes = channelNode.SelectNodes("item");
            foreach (XmlNode itemNode in itemNodes)
            {
                items.Add(new RssItem(itemNode));
            }
        }
    }
}
