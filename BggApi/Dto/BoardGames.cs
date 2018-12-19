using System.Xml.Serialization;

namespace BggApi.Dto
{
    [XmlRoot(ElementName = "name")]
    public class Name
    {
        [XmlAttribute(AttributeName = "primary")]
        public bool Primary { get; set; }
        [XmlAttribute(AttributeName = "sortindex")]
        public string Sortindex { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "boardgamepublisher")]
    public class Boardgamepublisher
    {
        [XmlAttribute(AttributeName = "objectid")]
        public string Objectid { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "boardgamepodcastepisode")]
    public class Boardgamepodcastepisode
    {
        [XmlAttribute(AttributeName = "objectid")]
        public string Objectid { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "boardgamehonor")]
    public class Boardgamehonor
    {
        [XmlAttribute(AttributeName = "objectid")]
        public string Objectid { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "boardgamemechanic")]
    public class Boardgamemechanic
    {
        [XmlAttribute(AttributeName = "objectid")]
        public string Objectid { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "boardgameexpansion")]
    public class Boardgameexpansion
    {
        [XmlAttribute(AttributeName = "objectid")]
        public string Objectid { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "boardgameaccessory")]
    public class Boardgameaccessory
    {
        [XmlAttribute(AttributeName = "objectid")]
        public string Objectid { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "boardgames")]
    public class BoardGames
    {
        [XmlElement(ElementName = "boardgame")]
        public Boardgame Boardgame { get; set; }
        [XmlAttribute(AttributeName = "termsofuse")]
        public string Termsofuse { get; set; }
    }

}
