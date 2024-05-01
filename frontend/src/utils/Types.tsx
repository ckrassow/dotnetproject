export type UserData = {
    id: number;
    username: string;
    firstName: string | null;
    lastName: string | null;
    favouriteTeam: string | null;
    favouriteTeamId: number | null;
    team: TeamData | null;
    profilePicRef: string | null;
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
