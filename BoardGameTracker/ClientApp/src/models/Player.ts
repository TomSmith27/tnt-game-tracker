export interface RatedGame {
	rating: number;
	id: number;
	name: string;
	description: string;
	objectId: number;
	thumbnail: string;
	image: string;
}

export interface PlayedGame {
	plays: number;
	id: number;
	name: string;
	description: string;
	objectId: number;
	thumbnail: string;
	image: string;
}

export interface RatingsDistribution {
	1: number;
	2: number;
	3: number;
	4: number;
	5: number;
	6: number;
	7: number;
	8: number;
	9: number;
	10: number;
}

export interface Player {
	id: number;
	name: string;
	uniqueGamesPlayed: number;
	totalGamesPlayed: number;
	highestRatedGames: RatedGame[];
	lowestRatedGames: RatedGame[];
	mostPlayedGame: PlayedGame;
	leastPlayedGame: PlayedGame;
	averageRating: number;
	colour: string;
	ratingsDistribution: RatingsDistribution;
}
