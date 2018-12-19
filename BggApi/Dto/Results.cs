using System.Collections.Generic;
using System.Xml.Serialization;

namespace BggApi.Dto
{
    [XmlRoot(ElementName = "results")]
    public class Results
    {
        [XmlElement(ElementName = "result")]
        public List<Result> Result { get; set; }
        [XmlAttribute(AttributeName = "numplayers")]
        public string Numplayers { get; set; }
    }

}
