export interface Game {
  id: number;
  name: string;
  description: string;
  objectId: number;
  yearPublished: Date;
  minPlayers: number;
  maxPlayers: number;
  playingTime: number;
  minPlaytime: number;
  maxPlaytime: number;
  age: number;
  image: string;
  thumbnail: string;
  averageRating: number;
  usersRated: number;
  categories: any[];
  bestPlayerCount: string;
  playerRatings: any[];
  havePlayed: Boolean;
}
