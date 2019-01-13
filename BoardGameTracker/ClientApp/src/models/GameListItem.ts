export interface GameListItem {
	id: number;
	name: string;
	objectId: number;
	timesPlayed: number;
	lastPlayed?: Date;
	averageRating: number;
	playersAverageRating: number;
	difference: number;
	thumbnail: string;
}
