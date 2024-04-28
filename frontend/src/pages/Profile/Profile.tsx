import { useState, useEffect } from "react";
import axios from "axios";
import Tab from "../../components/Tab";
import "../../styles/Profile.css";
import { Predictions } from "./Predictions";
import { ProfileInfo } from "./ProfileInfo";
import { UserData, PlayerPrediction, TeamPrediction, TournamentPrediction } from "../../utils/Types";


const parentTabs = ["Profile", "Predictions", "League"];
  

export function ProfilePage() {
  
  const [isLoading, setIsLoading] = useState(true);
  const [activeParentTab, setActiveParentTab] = useState(parentTabs[0]);
  const [userData, setUserData] = useState<UserData>({} as UserData);
  const [playerPredictions, setPlayerPredictions] = useState<PlayerPrediction[]>([]);
  const [teamPredictions, setTeamPredictions] = useState<TeamPrediction[]>([]);
  const [tournamentPredictions, setTournamentPredictions] = useState<TournamentPrediction[]>([]);
  
  const fetchUserData = async () => {

    try {
        const userId = localStorage.getItem("userId");
        const response = await axios.get(
            `http://localhost:5175/api/user/${userId}`
        );
        const data = response.data;
        setUserData({
            Id: data.id,
            Username: data.username,
            FirstName: data.firstname,
            LastName: data.lastname,
            FavouriteTeam: data.favouriteteam,
            Team: data.team
        });

    } catch (error) {
        console.error("Error fetching user", error);
    }
  };

  const fetchPlayerPredictions = async () => {

    try {
        const userId = localStorage.getItem("userId");
        const response = await axios.get(
            `http://localhost:5175/api/user/${userId}/playerpredictions`
        );
        const data = response.data;
        console.log(data);
        setPlayerPredictions([...data]);

    } catch (error) {
        console.error("Error fetching player predictions", error);
    }
  };

  const fetchTeamPredictions = async () => {
    
    try {
        const userId = localStorage.getItem("userId");
        const response = await axios.get(
            `http://localhost:5175/api/user/${userId}/teampredictions`
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
        const response = await axios.get(
            `http://localhost:5175/api/user/${userId}/tournamentpredictions`
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
          {isLoading ? (
            <div>Loading...</div> 
          ) : (
            <>
                {activeParentTab === "Predictions" && (
                    <Predictions
                    playerPredictions={playerPredictions}
                    setPlayerPredictions={setPlayerPredictions}
                    teamPredictions={teamPredictions}
                    setTeamPredictions={setTeamPredictions}
                    tournamentPredictions={tournamentPredictions}
                    setTournamentPredictions={setTournamentPredictions}  
                    />
                )}

                {activeParentTab === "Profile" && (
                    <ProfileInfo userData={userData} /> 
                )}
            </>
            )}
        </div>
          
  );
}
