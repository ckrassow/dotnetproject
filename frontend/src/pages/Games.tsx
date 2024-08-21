import { useState, useEffect } from "react";
import { fetchGames } from "../utils/ApiCalls";
import Card from "../components/Card";
import Tab from "../components/Tab";
import { Game, Group } from "../utils/Types";
import axiosInstance from "../utils/Api";
import { AxiosError } from "axios";

const Games = () => {
  const [games, setGames] = useState<Game[]>([]);
  const [activeTab, setActiveTab] = useState("Group A");
  const [groups, setGroups] = useState<Group[]>([]);
  const [predictions, setPredictions] = useState<Record<number, { homeScore: number; awayScore: number }>>({});
  const [homeScoreError, setHomeScoreError] = useState<boolean>(false);
  const [awayScoreError, setAwayScoreError] = useState<boolean>(false); 
  const [predictionMessage, setPredictionMessage] = useState<string | null>(null);
  const [homeScores, setHomeScores] = useState<Record<number, number>>({});
  const [awayScores, setAwayScores] = useState<Record<number, number>>({});
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const userId = localStorage.getItem("userId");
  const token = localStorage.getItem("token");

  useEffect(() => {
    const fetchAllGames = async () => {
      try {
        const data = await fetchGames();
        const games: Game[] = data.map((game: Game) => {
          return {
            id: game.id,
            status: game.status,
            utcDate: game.utcDate,
            matchDay: game.matchday,
            stage: game.stage,
            winner: game.winner,
            homeTeam: game.homeTeam,
            awayTeam: game.awayTeam,
            lastUpdated: game.lastUpdated,
            fullTimeScore: { home: game.fullTimeScore.home, away: game.fullTimeScore.away },
            halfTimeScore: { home: game.halfTimeScore.home, away: game.halfTimeScore.away },
            group: game.group.startsWith("GROUP_") ? game.group.replace("GROUP_", "") : game.group
          };
        });
        setGames(games);

        const groupedGames: Group[] = data.reduce((acc: Record<string, Group>, game: Game) => {
          if (!acc[game.group]) {
            acc[game.group] = { title: game.group.startsWith("GROUP_") ? game.group.replace("GROUP_", "Group ") : game.group, games: [] };
          }
          acc[game.group].games.push(game);
          return acc;
        }, {} as Record<string, Group>);
        setGroups(Object.values(groupedGames));

        fetchUserGamePredictions();

      } catch (error) {
        console.error("Error fetching games:", error);
      }
    };

    const fetchUserGamePredictions = async () => {
      try {
        const response = await axiosInstance.get(`user/${userId}/gameprediction`, {
          headers: {
            Authorization: `Bearer ${token}`
          }
        });
        setPredictions(response.data.reduce((acc: Record<number, { homeScore: number; awayScore: number }>, prediction: any) => {
          acc[prediction.gameId] = {
            homeScore: prediction.homeScore,
            awayScore: prediction.awayScore
          };
          return acc;
        }, {}));

        setHomeScores(response.data.reduce((acc: Record<number, number>, prediction: any) => {
          acc[prediction.gameId] = prediction.homeScore;
          return acc;
        }, {}));
      
        setAwayScores(response.data.reduce((acc: Record<number, number>, prediction: any) => {
          acc[prediction.gameId] = prediction.awayScore;
          return acc;
        }, {}));

      } catch (error) {
        console.error("Error fetching user game predictions", error);
      }
    };

    fetchAllGames();
  }, []);


  const handleTabClick = (title: string) => {
    setActiveTab(title);
  };

  const filteredGames = groups.find((group) => group.title === activeTab)?.games || [];

  const createGamePrediction = async (gameId: number, homeScore: number, awayScore: number) => {
    try {

      if (homeScore < 0 || awayScore < 0) {
        setHomeScoreError(true);
        setAwayScoreError(true);
        return;
      }

      await axiosInstance.post(`user/${userId}/gameprediction`, {
        gameId,
        homeScore,
        awayScore
      }, {
        headers: {
          Authorization: `Bearer ${token}`
        }
      });
      setPredictionMessage("Prediction created successfully!"); 
      setTimeout(() => {
        setPredictionMessage(null); 
      }, 3000); 
    } catch (error) {
      console.error("Error creating new game prediction", error);
      if (error instanceof AxiosError) {
        switch (error.response?.status) {
          case 401:
            setErrorMessage("Unauthorized. Please log in again.");
            break;
          case 403:
            setErrorMessage("Forbidden. You are not authorized to perform this action.");
            break;
          case 404:
            setErrorMessage("Game or user not found.");
            break;
          case 400:
            setErrorMessage("A prediction for this game and user already exists.");
            break;
          case 500:
            setErrorMessage("Passed deadline for prediction");
            break;
          default:
            setErrorMessage("An unexpected error occurred. Please try again later.");
            break;
        }
      } else {
        setErrorMessage("An unexpected error occurred. Please try again later.");
      }
    }
  };

  const updateGamePrediction = async (gameId: number, homeScore: number, awayScore: number) => {
    try {
      if (homeScore < 0 || awayScore < 0) {
        setHomeScoreError(true);
        setAwayScoreError(true);
        return;
      }

      await axiosInstance.put(`user/${userId}/gameprediction`, {
        gameId,
        homeScore,
        awayScore
      }, {
        headers: {
          Authorization: `Bearer ${token}`
        }
      });
      setPredictionMessage("Prediction updated successfully!"); 
      setTimeout(() => {
        setPredictionMessage(null); 
      }, 3000); 
    } catch (error) {
      console.error("Error updating game prediction", error);
      if (error instanceof AxiosError) {
        switch (error.response?.status) {
          case 401:
            setErrorMessage("Unauthorized. Please log in again.");
            break;
          case 403:
            setErrorMessage("Forbidden. You are not authorized to perform this action.");
            break;
          case 404:
            setErrorMessage("Prediction not found.");
            break;
          case 400:
            setErrorMessage("Deadline for prediction has already passed.");
            break;
          default:
            setErrorMessage("An unexpected error occurred. Please try again later.");
            break;
        }
      } else {
        setErrorMessage("An unexpected error occurred. Please try again later.");
      }
    }
  };


  return (
    <div className="bg-gray-100 min-h-screen">
      <div className="container mx-auto px-4 py-8">
        <div className="flex justify-between items-center mb-4">
          <h1 className="text-3xl font-bold text-gray-800">Games</h1>
        </div>

        {predictionMessage && (
          <div className="fixed top-10 left-1/2 transform -translate-x-1/2 bg-green-500 text-white p-3 rounded-md shadow-md">
            {predictionMessage}
          </div>
        )}
        {errorMessage && (
          <div className="fixed top-10 left-1/2 transform -translate-x-1/2 bg-red-500 text-white p-3 rounded-md shadow-md">
            {errorMessage}
            <button onClick={() => setErrorMessage(null)} className="ml-2 text-white font-bold">Close</button>
          </div>
        )}

        <div className="mb-4">
          <Tab title="Group A" isActive={activeTab === "Group A"} onClick={() => handleTabClick("Group A")} />
          <Tab title="Group B" isActive={activeTab === "Group B"} onClick={() => handleTabClick("Group B")} />
          <Tab title="Group C" isActive={activeTab === "Group C"} onClick={() => handleTabClick("Group C")} />
          <Tab title="Group D" isActive={activeTab === "Group D"} onClick={() => handleTabClick("Group D")} />
          <Tab title="Group E" isActive={activeTab === "Group E"} onClick={() => handleTabClick("Group E")} />
          <Tab title="Group F" isActive={activeTab === "Group F"} onClick={() => handleTabClick("Group F")} />
          <Tab title="Playoff" isActive={false} isSubTab onClick={() => handleTabClick("Playoff")} />
        </div>

        <div className="grid grid-cols-1 md:grid-cols-3 gap-4">
          {filteredGames.map((game, index) => (
            <Card
              key={index}
              header={`Matchday ${game.matchday}`}
              content={
                <div className="p-4 bg-gray-800 rounded-md">
                  <div className="text-lg font-bold text-white mb-2">
                    {game.homeTeam} vs {game.awayTeam}
                  </div>
                  <div className="text-gray-400 mb-2">
                    Halftime: {game.halfTimeScore.home} - {game.halfTimeScore.away}
                  </div>
                  <div className="text-gray-400">
                    Fulltime: {game.fullTimeScore.home} - {game.fullTimeScore.away}
                  </div>              

                  <div className="mb-2">
                    <label htmlFor={`homeScore-${game.id}`} className="block text-white text-sm font-bold mb-2">Home Score:</label>
                    <input 
                      type="number" 
                      id={`homeScore-${game.id}`} 
                      value={homeScores[game.id] || 0}
                      onChange={(e) => setHomeScores({ ...homeScores, [game.id]: parseInt(e.target.value) })} 
                      className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" 
                      min="0" 
                    />
                    {homeScoreError && <span className="text-red-500">Scores cannot be negative.</span>}
                  </div>
                  <div className="mb-2">
                    <label htmlFor={`awayScore-${game.id}`} className="block text-white text-sm font-bold mb-2">Away Score:</label>
                    <input 
                      type="number" 
                      id={`awayScore-${game.id}`} 
                      value={awayScores[game.id]|| 0} 
                      onChange={(e) => setAwayScores({ ...awayScores, [game.id]: parseInt(e.target.value) })}
                      className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-outline" 
                      min="0" 
                    />
                    {awayScoreError && <span className="text-red-500">Scores cannot be negative.</span>}
                  </div>

                  <button
                    onClick={() => {
                      const homeScoreInput = document.getElementById(`homeScore-${game.id}`) as HTMLInputElement;
                      const awayScoreInput = document.getElementById(`awayScore-${game.id}`) as HTMLInputElement;

                      const homeScore = parseInt(homeScoreInput.value || "0");
                      const awayScore = parseInt(awayScoreInput.value || "0");

                      const existingPrediction = predictions[game.id];

                      if (homeScore < 0 || awayScore < 0) {
                        setHomeScoreError(true);
                        setAwayScoreError(true);
                        return; 
                      } else {
                        setHomeScoreError(false);
                        setAwayScoreError(false);
                      }

                      if (existingPrediction) {
                        updateGamePrediction(game.id, homeScore, awayScore);
                      } else {
                        createGamePrediction(game.id, homeScore, awayScore);
                      }
                    }}
                    className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus-shadow-outline"
                  >
                    {predictions[game.id] ? "Update Prediction" : "Submit Prediction"}
                  </button>
                </div>
              }
            />
          ))}
        </div>
      </div>
    </div>
  );
};

export default Games;