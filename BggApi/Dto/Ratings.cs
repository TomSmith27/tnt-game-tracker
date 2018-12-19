using System.Xml.Serialization;

namespace BggApi.Dto
{
    [XmlRoot(ElementName = "ratings")]
    public class Ratings
    {
        [XmlElement(ElementName = "usersrated")]
        public int Usersrated { get; set; }
        [XmlElement(ElementName = "average")]
        public double Average { get; set; }
        [XmlElement(ElementName = "bayesaverage")]
        public string Bayesaverage { get; set; }
        [XmlElement(ElementName = "ranks")]
        public Ranks Ranks { get; set; }
        [XmlElement(ElementName = "stddev")]
        public string Stddev { get; set; }
        [XmlElement(ElementName = "median")]
        public string Median { get; set; }
        [XmlElement(ElementName = "owned")]
        public string Owned { get; set; }
        [XmlElement(ElementName = "trading")]
        public string Trading { get; set; }
        [XmlElement(ElementName = "wanting")]
        public string Wanting { get; set; }
        [XmlElement(ElementName = "wishing")]
        public string Wishing { get; set; }
        [XmlElement(ElementName = "numcomments")]
        public string Numcomments { get; set; }
        [XmlElement(ElementName = "numweights")]
        public string Numweights { get; set; }
        [XmlElement(ElementName = "averageweight")]
        public string Averageweight { get; set; }
    }

}
