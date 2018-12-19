using System.Xml.Serialization;

namespace BggApi.Dto
{
    [XmlRoot(ElementName = "boardgamefamily")]
    public class BoardgameFamily
    {
        [XmlAttribute(AttributeName = "objectid")]
        public string Objectid { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

}
