import { Game } from './Game';

export interface Rating {
	playerId: number;
	game: Game;
	gameId: number;
	rating: number;
}
