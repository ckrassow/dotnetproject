import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";
import Tab from "../../components/Tab";
import "../../styles/Profile.css";
import { Predictions } from "./Predictions";
import { ProfileInfo } from "./ProfileInfo";


const parentTabs = ["Profile", "Predictions", "League"];

export type UserData = {
    Id: number;
    Username: string;
    FirstName: string | null;
    LastName: string | null;
    FavouriteTeam: string | null;
    TeamId: number | null;
    Team: any | null;
    Token: string | null;
};

export type PredictionData = {
    PlayerPredictions: PlayerPrediction[] | null;
    TeamPredictions: TeamPrediction[] | null;
    TournamentPredictions: TournamentPrediction[] | null;
};

export type PlayerPrediction = {
    id: number;
    predictionType: string;
    playerId?: number;
    player?: PlayerData; 
};
  
export type TeamPrediction = {
    id: number;
    predictionType: string;
    nationalTeamId?: number;
    nationalTeam?: TeamData;
};
  
export type TournamentPrediction = {
    id: number;
    predictionType: string;
    predictionValue?: string;
};
  
 
export type PlayerData = {
    name: string;
    imagePath: string;
};
  
export type TeamData = {
    name: string;
    imagePath: string;
};

export function ProfilePage() {
    const { username: viewedUsername } = useParams();
    const [activeParentTab, setActiveParentTab] = useState(parentTabs[0]);
    const isPublicProfile = viewedUsername !== undefined;

    const [userData, setUserData] = useState<UserData>({} as UserData);
    const [predictions, setPredictions] = useState<PredictionData>({} as PredictionData);

    useEffect(() => {
        const token = localStorage.getItem("token");
        const userId = localStorage.getItem("userId");
      
        const fetchUserData = async () => {
          try {
            const response = await axios.get(`http://localhost:5175/api/user/${userId}`, {
              headers: {
                Authorization: `Bearer ${token}`,
              },
            });
            const data = response.data;
            setUserData({
              Id: data.id,
              Username: data.username,
              FirstName: data.firstName,
              LastName: data.lastName,
              FavouriteTeam: data.favouriteTeam,
              TeamId: data.teamId,
              Team: data.team,
              Token: token,
            });
            setPredictions({
                PlayerPredictions: data.playerPredictions,
                TeamPredictions: data.teamPredictions,
                TournamentPredictions: data.tournamentPredictions
            });
          } catch (error) {
            console.error("Error fetching user data", error);
          }
        };
      
        const fetchPlayerData = async () => {
          if (predictions.PlayerPredictions) {
            const updatedPredictions = await Promise.all(
              predictions.PlayerPredictions.map(async (prediction) => {
                if (prediction.playerId) {
                  try {
                    const playerResponse = await axios.get(
                      `http://localhost:5175/api/player/${prediction.playerId}`
                    );
                    return { ...prediction, player: playerResponse.data };
                  } catch (error) {
                    console.error("Error fetching player data", error);
                    return prediction; 
                  }
                } else {
                  return prediction; 
                }
              })
            );
            setPredictions({ ...predictions, PlayerPredictions: updatedPredictions });
          }
        };

        const fetchTeamData = async () => {
            if (predictions.TeamPredictions) {
                const updatedPredictions = await Promise.all(
                    predictions.TeamPredictions.map(async (prediction) => {
                        if (prediction.nationalTeamId) {
                            try {
                                const teamResponse = await axios.get(
                                    `http://localhost:5175/api/nationalteam/${prediction.nationalTeamId}`
                                );
                                return { ...prediction, nationalTeam: teamResponse.data};
                            } catch (error) {
                                console.error("Error fetching team data", error);
                                return prediction;
                            }
                        } else {
                            return prediction;
                        }
                    })
                );
                setPredictions({ ...predictions, TeamPredictions: updatedPredictions})
            }
        };

        /*
        const fetchTournamentData = async () => {
            if (predictions.TournamentPredictions) {
                const updatedPredictions = await Promise.all(
                    predictions.TournamentPredictions.map(async (prediction) => {
                        if (prediction.predictionValue) {
                            try {
                                const tournamentResponse = await axios.get(
                                    `http://localhost:5175/api/tournamentprediction/${prediction.id}`
                                );
                                return { ...prediction, tou}
                            }
                        }
                    })
                )
            }
        }
        */
        fetchUserData().then(() => {
          
          fetchPlayerData();
          fetchTeamData(); 
        });
      }, []);

    return (

        <div className={`profile-container ${isPublicProfile ? "public-profile-container" : ""}`}>
            <div className="tabs">
                {parentTabs.map(tab => (
                    <Tab
                        key={tab}
                        title={tab}
                        isActive={activeParentTab === tab}
                        onClick={() => setActiveParentTab(tab)}
                    />
                ))}
            </div>

            {activeParentTab === "Predictions" && (
            <Predictions isPublicProfile={isPublicProfile} predictionData={predictions} />
            )}        
            {activeParentTab === "Profile" && (
            <ProfileInfo isPublicProfile={isPublicProfile} userData={userData} /> 
            )}
        </div>
    );
}
