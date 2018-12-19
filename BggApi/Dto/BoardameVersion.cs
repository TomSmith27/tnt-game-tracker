using System.Xml.Serialization;

namespace BggApi.Dto
{
    [XmlRoot(ElementName = "boardgameversion")]
    public class BoardameVersion
    {
        [XmlAttribute(AttributeName = "objectid")]
        public string Objectid { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

}
