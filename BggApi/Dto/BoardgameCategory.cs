using System.Xml.Serialization;

namespace BggApi.Dto
{
    [XmlRoot(ElementName = "boardgamecategory")]
    public class BoardgameCategory
    {
        [XmlAttribute(AttributeName = "objectid")]
        public int Objectid { get; set; }
        [XmlText]
        public string Text { get; set; }
    }

}
