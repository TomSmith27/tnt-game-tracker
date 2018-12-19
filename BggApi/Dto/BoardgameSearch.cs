namespace BggApi.Dto
{
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "boardgame")]
    public class BoardgameSearch
    {
        [XmlElement(ElementName = "name")]
        public Name Name { get; set; }
        [XmlElement(ElementName = "yearpublished")]
        public string Yearpublished { get; set; }
        [XmlAttribute(AttributeName = "objectid")]
        public string Objectid { get; set; }
    }

}
