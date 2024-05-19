import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { fetchUserData, fetchUserComments, fetchTeamData, fetchPlayerPredictions, fetchTeamPredictions, fetchTournamentPredictions } from "../../utils/ApiCalls";
import Tab from "../../components/Tab";
import { Predictions } from "./Predictions";
import { UserProfile } from "./UserProfile";
import { UserData, NationalTeamData, PlayerPrediction, TeamPrediction, TournamentPrediction, Comment } from "../../utils/Types";
import { UserSettings } from "./UserSettings";


  
export function ProfilePage() {
  
  const [isLoading, setIsLoading] = useState(true);
  const [isPrivateView, setIsPrivateView] = useState(false);
  const parentTabs = isPrivateView
  ? ["Profile", "Predictions", "Settings"]
  : ["Profile", "Predictions"];
  const [activeParentTab, setActiveParentTab] = useState(parentTabs[0]);
  const [userData, setUserData] = useState<UserData>({} as UserData);
  const [teamData, setTeamData] = useState<NationalTeamData[]>([]);
  const [comments, setComments] = useState<Comment[]>([]);
  const [playerPredictions, setPlayerPredictions] = useState<PlayerPrediction[]>([]);
  const [teamPredictions, setTeamPredictions] = useState<TeamPrediction[]>([]);
  const [tournamentPredictions, setTournamentPredictions] = useState<TournamentPrediction[]>([]);
  const loggedInUsername = localStorage.getItem("username");
  const { username } = useParams();
  const navigate = useNavigate();

  const getUserData = async () => {

    try {

        if (!username) {
            console.error("Username not found in URL parameters");
            navigate("/");
            return;
        } 

        const data = await fetchUserData(username);
        setUserData({
            username: data.username,
            firstName: data.firstName,
            lastName: data.lastName,
            favouriteTeam: data.favouriteTeam,
            favouriteTeamId: data.favouriteTeamid,
            profilePicRef: data.profilePicRef,
            team: data.team,
        });

    } catch (error) {
        console.error("Error fetching user", error);
    }
  };

  const getComments = async () => {

    if (!username) {
      console.error("Username not found in URL parameters");
      navigate("/");
      return;
    }
    try {
      const data = await fetchUserComments(username);
      const comments: Comment[] = data.reverse().map((comment: Comment) => {
        console.log(comment);
        return {
            author: comment.author,
            recipient: comment.recipient,
            comment: comment.comment,
            timestamp: comment.timestamp
        };
      });
      setComments(comments);

    } catch(error) {
      console.error("Error fetching comment data", error);
    }
    
  };

  const getTeamData = async () => {
    try {
        const data = await fetchTeamData();
        const teamData: NationalTeamData[] = data.map((team: NationalTeamData) => {
            return {
                id: team.id,
                name: team.name,
                playoffAppearances: team.playoffAppearances,
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

  const getPlayerPredictions = async () => {
    try {
        const data = await fetchPlayerPredictions(userData.username);
        setPlayerPredictions([...data]);

    } catch (error) {
        console.error("Error fetching player predictions", error);
    }
  };

  const getTeamPredictions = async () => {
    try {
        const data = await fetchTeamPredictions(userData.username);
        setTeamPredictions([...data]);

    } catch (error) {
        console.error("Error fetching team predictions", error);
    }
  };

  const getTournamentPredictions = async () => {
    try {
        const data = await fetchTournamentPredictions(userData.username);
        setTournamentPredictions([...data]);

    } catch (error) {
        console.error("Error fetching tournament predictions", error);
    }
  };

  useEffect(() => {

    setIsPrivateView(loggedInUsername === username);
  }, [loggedInUsername, username])

  useEffect(() => {
    const fetchData = async () => {
      try {
        await getUserData();
        await getComments();
        if (loggedInUsername === username) {
            await getTeamData();
        }
        
      } catch (error) {
        console.error("Error fetching user", error);
      }
    };
  
    fetchData();
  }, [username]);

  useEffect(() => {
    const fetchPredictions = async () => {
      try {
        await getPlayerPredictions();
        await getTeamPredictions();
        await getTournamentPredictions();
      } catch (error) {
        console.error("Error fetching predictions", error);
      } finally {
        setIsLoading(false);
      }
    };
  
    if (userData.username) { 
      fetchPredictions();
    }
  }, [userData]);

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
                    isPrivateView={isPrivateView}
                    />
                )}

                {activeParentTab === "Profile" && (
                    <UserProfile 
                    userData={userData} 
                    comments={comments}
                    setComments={setComments} /> 
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
