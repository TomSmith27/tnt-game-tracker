using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BggApi.Dto
{
    [Serializable]
    [XmlRoot(ElementName = "boardgames", Namespace = "")]
    public class BoardgameSearchResult
    {
        [XmlElement(ElementName = "boardgame")]
        public List<Boardgame> Boardgames { get; set; }
    }
}
