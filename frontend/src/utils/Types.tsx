export type UserData = {
    username: string;
    firstName: string | null;
    lastName: string | null;
    favouriteTeam: string | null;
    favouriteTeamId: number | null;
    team: TeamData | null;
    profilePicRef: string | null;
    points: number | null;
};

export type Comment = {
    author: string;
    recipient: string;
    comment: string;
    timestamp: Date;
};

export type TeamData = {
    id: number;
    name: string;
    predictions: PredictionData | null;
};

export type PredictionData = {
    playerPredictions: PlayerPrediction[];
    teamPredictions: TeamPrediction[];
    tournamentPredictions: TournamentPrediction[];
};

export type PlayerData = {
    id: number;
    no: number;
    pos: string;
    name: string;
    age: number;
    caps: number;
    goals: number;
    club: string;
    nationalTeamId: number;
    imagePath: string;
};

export type NationalTeamData = {
    id: number;
    name: string;
    playoffAppearances: string;
    fifaRanking: string;
    group: string;
    imagePath: string;
};

export type PlayerPrediction = {
    id: number;
    predictionType: number;
    predictionTypeString: string;
    playerId: number | null;
    player: PlayerData | null; 
};
  
export type TeamPrediction = {
    id: number;
    predictionType: number;
    predictionTypeString: string;
    nationalTeamId?: number;
    nationalTeam?: NationalTeamData;
};
  
export type TournamentPrediction = {
    id: number;
    predictionType: number;
    predictionTypeString: string;
    predictionValue?: string;
};

export type QualifiedTeams = "France" | "England" | "Belgium" | "Portugal" | "Scotland" | "Spain" |
"Turkey" | "Austria" | "Hungary" | "Slovakia" | "Albania" | "Denmark" |
"Netherlands" | "Romania" | "Switzerland" | "Serbia" | "Czech Republic" |
"Italy" | "Slovenia" | "Croatia" | "Georgia" | "Ukraine" | "Poland";

export type Game = {
    id: number;
    status: string;
    utcDate: Date;
    matchday: number;
    stage: string;
    group: string;
    homeTeam: string;
    awayTeam: string;
    winner: string | null;
    fullTimeScore: { home: number; away: number };
    halfTimeScore: { home: number; away: number };
    lastUpdated: Date;
};
  
export type Group = {
    title: string;
    games: Game[];
};

export type GamePrediction = {
    userId: number;
    gameId: number;
    predictedHomeScore: number | null;
    predictedAwayScore: number | null;
};