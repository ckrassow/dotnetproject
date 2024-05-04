import { useState, useEffect } from "react";
import axiosInstance from "../../utils/Api";
import Tab from "../../components/Tab";
import { Predictions } from "./Predictions";
import { UserProfile } from "../../components/UserProfile";
import { UserData, NationalTeamData, PlayerPrediction, TeamPrediction, TournamentPrediction } from "../../utils/Types";
import { UserSettings } from "./UserSettings";


const parentTabs = ["Profile", "Predictions", "Settings"];
  
export function ProfilePage() {
  
  const [isLoading, setIsLoading] = useState(true);
  const [activeParentTab, setActiveParentTab] = useState(parentTabs[0]);
  const [userData, setUserData] = useState<UserData>({} as UserData);
  const [teamData, setTeamData] = useState<NationalTeamData[]>([]);
  const [playerPredictions, setPlayerPredictions] = useState<PlayerPrediction[]>([]);
  const [teamPredictions, setTeamPredictions] = useState<TeamPrediction[]>([]);
  const [tournamentPredictions, setTournamentPredictions] = useState<TournamentPrediction[]>([]);
  
  const fetchUserData = async () => {

    try {
        const userId = localStorage.getItem("userId");
        const response = await axiosInstance.get(
            `/user/${userId}`
        );
        const data = response.data;
        setUserData({
            username: data.username,
            firstName: data.firstName,
            lastName: data.lastName,
            favouriteTeam: data.favouriteTeam,
            favouriteTeamId: data.favouriteTeamid,
            profilePicRef: data.profilePicRef,
            team: data.team
        });

    } catch (error) {
        console.error("Error fetching user", error);
    }
  };

  const fetchTeamData = async () => {
    try {
        const token = localStorage.getItem("token");
        const response = await axiosInstance.get(
            `/nationalteam`, {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });
        const data = response.data;
        const teamData: NationalTeamData[] = data.map((team: any) => {
            return {
                id: team.id,
                name: team.name,
                playoffAppearences: team.playoffAppearences,
                fifaRanking: team.fifaRanking,
                group: team.group,
                imagePath: team.imagePath,
            };
        });

        setTeamData(teamData);

    } catch (error) {
        console.error("Error when trying to fetch teams", error);
    }
};

  const fetchPlayerPredictions = async () => {

    try {
        const userId = localStorage.getItem("userId");
        const response = await axiosInstance.get(
            `/user/${userId}/playerpredictions`
        );
        const data = response.data;
        setPlayerPredictions([...data]);

    } catch (error) {
        console.error("Error fetching player predictions", error);
    }
  };

  const fetchTeamPredictions = async () => {
    
    try {
        const userId = localStorage.getItem("userId");
        const response = await axiosInstance.get(
            `/user/${userId}/teampredictions`
        );
        const data = response.data;
        setTeamPredictions([...data]);

    } catch (error) {
        console.error("Error fetching team predictions", error);
    }
  };

  const fetchTournamentPredictions = async () => {

    try {
        const userId = localStorage.getItem("userId");
        const response = await axiosInstance.get(
            `/user/${userId}/tournamentpredictions`
        );
        const data = response.data;
        setTournamentPredictions([...data]);

    } catch (error) {
        console.error("Error fetching tournament predictions", error);
    }
  };

  useEffect(() => {
    const fetchData = async () => {
        try {
            await Promise.all([
                fetchUserData(),
                fetchTeamData(),
                fetchPlayerPredictions(),
                fetchTeamPredictions(),
                fetchTournamentPredictions()
            ]);
        } catch (error) {
            console.error("Error fetching data", error);
        } finally {
            setIsLoading(false);
        }
    };
    
    fetchData();
  }, []);

  return (

      <div className="parent-container">
          <div className="flex justify-center space-x-4 mb-4">
              {parentTabs.map(tab => (
                  <Tab
                      key={tab}
                      title={tab}
                      isActive={activeParentTab === tab}
                      onClick={() => setActiveParentTab(tab)}
                  />
              ))}
          </div>
          {isLoading ? (
            <div>Loading...</div> 
          ) : (
            <>
                {activeParentTab === "Predictions" && (
                    <Predictions
                    playerPredictions={playerPredictions}
                    setPlayerPredictions={setPlayerPredictions}
                    teamData={teamData}
                    teamPredictions={teamPredictions}
                    setTeamPredictions={setTeamPredictions}
                    tournamentPredictions={tournamentPredictions}
                    setTournamentPredictions={setTournamentPredictions}  
                    />
                )}

                {activeParentTab === "Profile" && (
                    <UserProfile userData={userData} /> 
                )}

                {activeParentTab === "Settings" && (
                    <UserSettings 
                    teamData={teamData}
                    userData={userData}
                    setUserData={setUserData}
                    />
                )}
            </>
            )}
        </div>
          
  );
}
