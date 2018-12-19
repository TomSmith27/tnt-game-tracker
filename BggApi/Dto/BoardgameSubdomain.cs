using System.Xml.Serialization;

namespace BggApi.Dto
{
    [XmlRoot(ElementName = "boardgamesubdomain")]
    public class BoardgameSubdomain
    {
        [XmlAttribute(AttributeName = "objectid")]
        public string Objectid { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

}
