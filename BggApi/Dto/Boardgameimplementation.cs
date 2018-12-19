using System.Xml.Serialization;

namespace BggApi.Dto
{
    [XmlRoot(ElementName = "boardgameimplementation")]
    public class BoardgameImplementation
    {
        [XmlAttribute(AttributeName = "objectid")]
        public string Objectid { get; set; }
        [XmlAttribute(AttributeName = "inbound")]
        public string Inbound { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

}
