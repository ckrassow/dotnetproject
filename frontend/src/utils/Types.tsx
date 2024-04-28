export type UserData = {
    Id: number;
    Username: string;
    FirstName: string | null;
    LastName: string | null;
    FavouriteTeam: string | null;
    Team: TeamData | null;
};

export type TeamData = {
    Id: number;
    Name: string;
    Predictions: PredictionData | null;
};

export type PredictionData = {
    PlayerPredictions: PlayerPrediction[];
    TeamPredictions: TeamPrediction[];
    TournamentPredictions: TournamentPrediction[];
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
  playoffAppearences: string;
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

