using System.Collections.Generic;
using System.Xml.Serialization;

namespace BggApi.Dto
{
    [XmlRoot(ElementName = "boardgame")]
    public class Boardgame
    {
        [XmlElement(ElementName = "yearpublished")]
        public int Yearpublished { get; set; }
        [XmlElement(ElementName = "minplayers")]
        public int Minplayers { get; set; }
        [XmlElement(ElementName = "maxplayers")]
        public int Maxplayers { get; set; }
        [XmlElement(ElementName = "playingtime")]
        public string Playingtime { get; set; }
        [XmlElement(ElementName = "minplaytime")]
        public int Minplaytime { get; set; }
        [XmlElement(ElementName = "maxplaytime")]
        public int Maxplaytime { get; set; }
        [XmlElement(ElementName = "age")]
        public int Age { get; set; }
        [XmlElement(ElementName = "name")]
        public List<Name> Name { get; set; }
        [XmlElement(ElementName = "description")]
        public string Description { get; set; }
        [XmlElement(ElementName = "thumbnail")]
        public string Thumbnail { get; set; }
        [XmlElement(ElementName = "image")]
        public string Image { get; set; }
        [XmlElement(ElementName = "boardgamepublisher")]
        public List<Boardgamepublisher> Boardgamepublisher { get; set; }
        [XmlElement(ElementName = "boardgamepodcastepisode")]
        public List<Boardgamepodcastepisode> Boardgamepodcastepisode { get; set; }
        [XmlElement(ElementName = "boardgamehonor")]
        public List<Boardgamehonor> Boardgamehonor { get; set; }
        [XmlElement(ElementName = "boardgamemechanic")]
        public List<Boardgamemechanic> Boardgamemechanic { get; set; }
        [XmlElement(ElementName = "boardgameexpansion")]
        public List<Boardgameexpansion> Boardgameexpansion { get; set; }
        [XmlElement(ElementName = "boardgameaccessory")]
        public List<Boardgameaccessory> Boardgameaccessory { get; set; }
        [XmlElement(ElementName = "boardgameversion")]
        public List<BoardameVersion> Boardgameversion { get; set; }
        [XmlElement(ElementName = "boardgamefamily")]
        public List<BoardgameFamily> Boardgamefamily { get; set; }
        [XmlElement(ElementName = "boardgamecategory")]
        public List<BoardgameCategory> Boardgamecategory { get; set; }
        [XmlElement(ElementName = "boardgamedesigner")]
        public BoardgameDesigner Boardgamedesigner { get; set; }
        [XmlElement(ElementName = "boardgameartist")]
        public List<BoardgameArtist> Boardgameartist { get; set; }
        [XmlElement(ElementName = "boardgamesubdomain")]
        public List<BoardgameSubdomain> Boardgamesubdomain { get; set; }
        [XmlElement(ElementName = "boardgameimplementation")]
        public BoardgameImplementation Boardgameimplementation { get; set; }
        [XmlElement(ElementName = "poll")]
        public List<Poll> Poll { get; set; }
        [XmlElement(ElementName = "statistics")]
        public Statistics Statistics { get; set; }
        [XmlAttribute(AttributeName = "objectid")]
        public int Objectid { get; set; }
    }

}
