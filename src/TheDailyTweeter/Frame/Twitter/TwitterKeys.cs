﻿using System.Xml.Serialization;

namespace Membus.WpfTwitterClient.Frame.Twitter
{
    [XmlRoot]
    public class TwitterKeys
    {
        public string ApiKey { get; set; }
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        public string RequestTokenUrl { get; set; }
        public string AccessTokenUrl { get; set; }
        public string AuthorizeUrl { get; set; }
    }
}