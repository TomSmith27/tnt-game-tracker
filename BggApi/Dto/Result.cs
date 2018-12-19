using System.Xml.Serialization;

namespace BggApi.Dto
{
    [XmlRoot(ElementName = "result")]
    public class Result
    {
        [XmlAttribute(AttributeName = "value")]
        public string Value { get; set; }
        [XmlAttribute(AttributeName = "numvotes")]
        public int Numvotes { get; set; }
        [XmlAttribute(AttributeName = "level")]
        public string Level { get; set; }
    }

}
