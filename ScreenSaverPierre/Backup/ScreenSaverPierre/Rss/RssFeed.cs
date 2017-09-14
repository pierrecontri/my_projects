using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace ScreenSaverPierre.Rss
{
    /// <summary>
    /// Représentation d'un élément RSS dans un document XML RSS 2.0
    /// </summary>
    public class RssFeed
    {
        private List<RssChannel> channels;
        public IList<RssChannel> Channels { get { return channels.AsReadOnly(); } }
        public RssChannel MainChannel { get { return Channels[0]; } }

        /// <summary>
        /// Constructeur privé à utiliser avec un modèle de fabrique.  
        /// </summary>
        /// <param name="xmlNode">Bloc XML dans lequel se trouve le contenu RSSFeed.</param>
        private RssFeed(XmlNode xmlNode)
        {
            channels = new List<RssChannel>();

            // Lire la balise <rss>
            XmlNode rssNode = xmlNode.SelectSingleNode("rss");

            // Pour chaque noeud <channel> du noeud <rss>
            // ajoutez un canal.
            XmlNodeList channelNodes = rssNode.ChildNodes;
            foreach (XmlNode channelNode in channelNodes)
            {
                RssChannel newChannel = new RssChannel(channelNode);
                channels.Add(newChannel);
            }
        }

        /// <summary>
        /// Fabrique qui construit des objets RSSFeed à partir d'un URI pointant vers un fichier XML RSS 2.0 valide.
        /// </summary>
        /// <exception cref="System.Net.WebException">Se produit lorsque l'URI ne peut pas être localisé sur le Web.</exception>
        /// <param name="uri">URL à partir de laquelle lire le flux RSS.</param>
        public static RssFeed FromUri(string uri)
        {
            XmlDocument xmlDoc;
            WebClient webClient = new WebClient();
            using (Stream rssStream = webClient.OpenRead(uri))
            {
                TextReader textReader = new StreamReader(rssStream);
                XmlTextReader reader = new XmlTextReader(textReader);
                xmlDoc = new XmlDocument();
                xmlDoc.Load(reader);
            }
            return new RssFeed(xmlDoc);
        }

        /// <summary>
        /// Fabrique qui construit les objets RssFeed à partir du texte d'un fichier XML RSS 2.0.
        /// </summary>
        /// <param name="rssText">Chaîne contenant le code XML pour le flux RSS.</param>
        public static RssFeed FromText(string rssText)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(rssText);
            return new RssFeed(xmlDoc);
        }
    }
}