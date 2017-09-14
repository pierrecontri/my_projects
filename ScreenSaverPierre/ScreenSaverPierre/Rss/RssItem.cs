using System;
using System.Xml;
using ScreenSaverPierre.UI;

namespace ScreenSaverPierre.Rss
{
    /// <summary>
    /// Représentation d'un élément Item dans un document XML RSS 2.0.
    /// Un RssChannel contient zéro ou plusieurs RssItems.
    /// </summary>
    public class RssItem : IItem
    {
        private readonly string title;
        private readonly string description;
        private readonly string link;

        public string Title { get { return title; } }
        public string Description { get { return description; } }
        public string Link { get { return link; } }


        /// <summary>
        /// Crée un RSSItem à partir d'un XmlNode représentant un élément Item dans un document XML RSS 2.0.
        /// </summary>
        /// <param name="itemNode"></param>
        internal RssItem(XmlNode itemNode)
        {
            XmlNode selected;
            selected = itemNode.SelectSingleNode("title");
            if (selected != null)
                title = selected.InnerText;

            selected = itemNode.SelectSingleNode("description");
            if (selected != null)
                description = selected.InnerText;

            selected = itemNode.SelectSingleNode("link");
            if (selected != null)
                link = selected.InnerText;
        }
    }
}