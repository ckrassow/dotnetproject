import { useState } from "react";
import Tab from "../../components/Tab";
import Card from "../../components/Card";
import Modal from "../../components/Modal";
import Dropdown from "../../components/Dropdown";
import axiosInstance from "../../utils/Api";
import { PlayerData, TournamentPrediction, PlayerPrediction, TeamPrediction, NationalTeamData } from "../../utils/Types";

const childTabs = ["Players", "Teams", "Tournament"];

type PredictionProps = {
    teamData: NationalTeamData[],
    playerPredictions: PlayerPrediction[],
    setPlayerPredictions: React.Dispatch<React.SetStateAction<PlayerPrediction[]>>,
    teamPredictions: TeamPrediction[],
    setTeamPredictions: React.Dispatch<React.SetStateAction<TeamPrediction[]>>,
    tournamentPredictions: TournamentPrediction[],
    setTournamentPredictions: React.Dispatch<React.SetStateAction<TournamentPrediction[]>>
};

type Team = "France" | "England" | "Belgium" | "Portugal" | "Scotland" | "Spain" |
            "Turkey" | "Austria" | "Hungary" | "Slovakia" | "Albania" | "Denmark" |
            "Netherlands" | "Romania" | "Switzerland" | "Serbia" | "Czech Republic" |
            "Italy" | "Slovenia" | "Croatia" | "Georgia" | "Ukraine" | "Poland";

export function Predictions(props: PredictionProps) {

    const [activeTab, setActiveTab] = useState(childTabs[0]);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [dropDownTeam, setDropDownTeam] = useState("");
    const [currentPrediction, setCurrentPrediction] = useState<PlayerPrediction | TeamPrediction | TournamentPrediction>({} as PlayerPrediction);
    const [selectedPlayers, setSelectedPlayers] = useState<PlayerData[]>([]);
    
    
    const teams: Team[] = ["France", "England", "Belgium", "Portugal", "Scotland",
                           "Spain", "Turkey", "Austria", "Hungary", "Slovakia",
                           "Albania", "Denmark", "Netherlands", "Romania",
                           "Switzerland", "Serbia", "Czech Republic", "Italy",
                           "Slovenia", "Croatia", "Georgia", "Ukraine","Poland"
    ];

    const tourPredOptions = [
        "Option A",
        "Option B",
        "Option C",
        "Option D",
      ];
        
    const handleShowPlayers = async (teamName: string) => {
        try {
            const token = localStorage.getItem("token");
            
            const response = await axiosInstance.get(
                `/nationalteam/${teamName}/players`, {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                });

            const data = response.data;
            const playerData: PlayerData[] = data.map((player: any) => {
                return {
                    id: player.id,
                    no: player.no,
                    pos: player.pos,
                    name: player.name,
                    age: player.age,
                    caps: player.caps,
                    goals: player.goals,
                    club: player.club,
                    nationalTeamId: player.nationalTeamId,
                    imagePath: player.imagePath
                };
            });

            setDropDownTeam(teamName);
            setSelectedPlayers(playerData);            

        } catch (error) {
            console.error("Error when trying to show players", error);
        }
    };

    const handleNewPlayerPrediction = async (player: PlayerData) => {

        const oldPlayerPred = [...props.playerPredictions];
        console.log(player);

        const updatedPlayerPreds = props.playerPredictions.map(prediction => {
            if (prediction.id === currentPrediction.id) {
                return {
                    ...prediction,
                    playerId: player.id,
                    player: player
                };
            }
            return prediction;
        });

        props.setPlayerPredictions(updatedPlayerPreds);

        try {
            const token = localStorage.getItem("token");
            const userId = localStorage.getItem("userId");

            await axiosInstance.put(
                `/user/${userId}/playerprediction/${currentPrediction.id}`,
                {
                    playerId: player.id
                },
                {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            );

        } catch (error) {
            console.error("Error handling new player prediction", error);
            props.setPlayerPredictions(oldPlayerPred);
        }
    };

    const handleNewTeamPrediction = async (team: NationalTeamData) => {

        const oldTeamPred = [...props.teamPredictions];

        const updatedTeamPreds = props.teamPredictions.map(prediction => {
            if (prediction.id === currentPrediction.id) {
                return {
                    ...prediction,
                    nationalTeamId: team.id,
                    nationalTeam: team
                };
            }
            return prediction;
        });
        
        props.setTeamPredictions(updatedTeamPreds);

        try {
            const token = localStorage.getItem("token");
            const userId = localStorage.getItem("userId");
            console.log(currentPrediction);

            await axiosInstance.put(
                `/user/${userId}/teamprediction/${currentPrediction.id}`,
                {
                    nationalTeamId: team.id
                },
                {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            );

        } catch (error) {
            console.error("Error handling new team prediction", error);
            props.setTeamPredictions(oldTeamPred);
        }
    };

    const handleNewTournamentPrediction = async (predValue: string) => {

        const oldTourPred = [...props.tournamentPredictions];

        const updatedTourPreds = props.tournamentPredictions.map(prediction => {
            if (prediction.id === currentPrediction.id) {
                return {
                    ...prediction,
                    predictionValue: predValue,
                };
            }
            return prediction;
        });

        props.setTournamentPredictions(updatedTourPreds);

        try {
            const token = localStorage.getItem("token");
            const userId = localStorage.getItem("userId");

            await axiosInstance.put(
                `/user/${userId}/tournamentprediction/${currentPrediction.id}`,
                {
                    predictionValue: predValue
                },
                {
                    headers: {
                        Authorization: `Bearer ${token}`
                    }
                }
            );

        } catch (error) {
            console.error("Error handling new tournament prediction", error);
            props.setTournamentPredictions(oldTourPred);
        }
    };

    function convertToSpaceSeparated(input: string): string {
        let result = "";
        for (let i = 0; i < input.length; i++) {
            const char = input[i];

            if (i > 0 && char === char.toUpperCase() && input[i - 1] !== ' ') {
                result += ' ';
            }
            
            result += char;
        }
        return result;
    }

    return (
        <div>
            <div className="flex justify-start space-x-4 mb-4">
                {childTabs.map(tab => (
                    <Tab
                        key={tab}
                        title={tab}
                        isActive={activeTab === tab}
                        onClick={() => setActiveTab(tab)}
                        isSubTab={true}
                    />
                ))}
            </div>

            {activeTab === "Players" && (
                <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-4">
                    {props.playerPredictions?.map(prediction  => (
                        <Card
                            key={prediction.predictionType}
                            className="bg-gray-800 rounded-lg shadow-md text-blue-200"
                            header={<div className="flex justify-between items-center mb-2">
                            <h2 className="text-xl font-bold">{convertToSpaceSeparated(prediction.predictionTypeString)}</h2>
                            </div>}
                            content={
                                (   
                                    <div className="flex flex-col items-center">
                                    {prediction.player ? (
                                      <>
                                          <h3 className="text-lg font-semibold mb-2">{prediction.player.name}</h3>
                                          <img src={require(`../../assets/${prediction.player.imagePath}`)} alt={prediction.player.name} className="w-32 h-32 object-cover" />
                                      </>
                                    ) : (
                                      <>
                                          <h3 className="text-lg font-semibold mb-2">No selection</h3>
                                          <img src={require("../../assets/default.jpg")} alt="No selection" className="w-32 h-32 object-cover"/>
                                      </>
                                    )}
                                    <button onClick={() => {
                                        setIsModalOpen(true);
                                        setCurrentPrediction(prediction);
                                    }} className="bg-blue-500 text-white px-4 py-2 rounded mt-4 hover:bg-blue-600 transition duration-300 ease-in-out">
                                    Make Prediction</button>
  
                                    </div>
                                )
                              }
                        />
                    ))}
                </div>
            )}

            {activeTab === "Teams" && (
                <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-4">
                    {props.teamPredictions?.map(prediction => (
                        <Card
                            key={prediction.predictionType}
                            className="bg-gray-800 rounded-lg shadow-md text-blue-200"
                            header={<div className="flex justify-between items-center mb-2">
                            <h2 className="text-xl font-bold">{convertToSpaceSeparated(prediction.predictionTypeString)}</h2>
                            </div>}
                            content={
                               (
                                  <div className="flex flex-col items-center">
                                  {prediction.nationalTeam ? (
                                    <>
                                        <h3 className="text-lg font-semibold mb-2">{prediction.nationalTeam.name}</h3>
                                        <img src={require(`../../assets/${prediction.nationalTeam.imagePath}`)} alt={prediction.nationalTeam.name} className="w-32 h-32 object-cover"/>
                                    </>
                                  ) : (
                                    <>
                                        <h3 className="text-lg font-semibold mb-2">No selection</h3>
                                        <img src={require("../../assets/default.jpg")} alt="No selection" className="w-32 h-32 object-cover"/>
                                    </>
                                  )}
                                  <button onClick={() => {
                                    setIsModalOpen(true);
                                    setCurrentPrediction(prediction);
                                  }} className="bg-blue-500 text-white px-4 py-2 rounded mt-4 hover:bg-blue-600 transition duration-300 ease-in-out">Make Prediction</button>

                                  </div>
                                )
                              }
                        />
                    ))}
                </div>
            )}

            {activeTab === "Tournament" && (
                <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 gap-4">
                    {props.tournamentPredictions?.map(prediction => (
                        <Card
                            key={prediction.predictionType}
                            className="bg-gray-800 rounded-lg shadow-md text-blue-200"
                            header={<div className="flex justify-between items-center mb-2">
                            <h2 className="text-xl font-bold">{convertToSpaceSeparated(prediction.predictionTypeString)}</h2>
                            </div>}
                            content={
                                (
                                    <div className="flex flex-col items-center">
                                    {prediction.predictionValue ? (
                                      <>
                                          <h3 className="text-lg font-semibold mb-2">{prediction.predictionValue}</h3>
                                      </>
                                    ) : (
                                      <>
                                          <h3 className="text-lg font-semibold mb-2">Tournament prediction</h3>
                                      </>
                                    )}
                                    <button onClick={() => {
                                        setIsModalOpen(true);
                                        setCurrentPrediction(prediction);
                                    }} className="bg-blue-500 text-white px-4 py-2 rounded mt-4 hover:bg-blue-600 transition duration-300 ease-in-out">Make Prediction</button>
  
                                    </div>
                                )
                              }
                        />
                    ))}
                </div>
            )}
            <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)} className="bg-gray-900 w-4/5 h-4/5">
                {activeTab === "Players" && (
                    <>
                        <Dropdown 
                            options={teams} 
                            selectedOption={dropDownTeam}
                            defaultOptionLabel="Select team.." 
                            setSelectedOption={handleShowPlayers} 
                        />
                        {dropDownTeam && (
                            <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4 overflow-y-auto max-h-[650px] p-4">
                                {selectedPlayers.map((player) => (
                                    <div className="flex flex-col items-center m-2" key={player.id}>
                                        <Card
                                            key={player.id}
                                            className="w-full h-full bg-gray-100 rounded-lg shadow-md hover:bg-gray-200 transition duration-300 ease-in-out"
                                            header={<div className="flex justify-between items-center mb-2">
                                            <h2 className="text-lg font-bold">{player.name}</h2>
                                          </div>}
                                            content={
                                            <div className="flex flex-col items-center">
                                                <img src={require(`../../assets/${player.imagePath}`)} alt={player.name} className="w-32 h-32 object-cover"/>
                                                <p className="text-sm text-gray-600">Position: {player.pos}</p>
                                                <p className="text-sm text-gray-600">Age: {player.age}</p>
                                                <p className="text-sm text-gray-600">Club: {player.club}</p>
                                                <p className="text-sm text-gray-600">Total NT goals: {player.goals}</p>
                                                <button onClick={() => {
                                                    handleNewPlayerPrediction(player);
                                                }} className="bg-blue-500 text-white px-4 py-2 rounded mt-4 hover:bg-blue-600 transition duration-300 ease-in-out w-full">
                                                    Select
                                                </button>
                                            </div>
                                            }
                                        />
                                    </div>
                                ))}
                            </div>
                        )}      
                    </>
                )}

                {activeTab === "Teams" && (
                    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4 overflow-y-auto max-h-[650px] p-4">
                        {props.teamData.map((team) => (
                            <div className="flex flex-col items-center m-2" key={team.name}>
                                <Card
                                    key={team.id}
                                    header={<div className="flex justify-between items-center mb-2">
                                    <h2 className="text-lg font-bold">{team.name}</h2>
                                  </div>}
                                    className="w-full h-full bg-gray-100 rounded-lg shadow-md hover:bg-gray-200 transition duration-300 ease-in-out"
                                    content={
                                    <div className="flex flex-col items-center">
                                        <img src={require(`../../assets/${team.imagePath}`)} alt={team.name} className="w-32 h-32 object-cover"/>
                                        <p className="text-sm text-gray-600">Group: {team.group}</p>
                                        <p className="text-sm text-gray-600">Fifa ranking: {team.fifaRanking}</p>
                                        <p className="text-sm text-gray-600">Playoff appearences: {team.playoffAppearances}</p>
                                        <button onClick={() => {
                                            handleNewTeamPrediction(team);
                                        }} className="bg-blue-500 text-white px-4 py-2 rounded mt-4 hover:bg-blue-600 transition duration-300 ease-in-out w-full">
                                            Select
                                        </button>
                                    </div>}
                                />
                            </div>
                        ))}
                    </div>
                )}

                {activeTab === "Tournament" && (
                    <div className="grid grid-cols-1 sm:grid-cols-2 md:grid-cols-3 lg:grid-cols-4 gap-4 overflow-y-auto max-h-[650px] p-4">
                        {tourPredOptions.map((tourPred) => (
                            <div className="flex flex-col items-center m-2" key={tourPred}>
                                <Card
                                    header={<div className="flex justify-between items-center mb-2">
                                    <h2 className="text-lg font-bold">{tourPred}</h2>
                                  </div>}
                                    className="w-full h-full bg-gray-100 rounded-lg shadow-md hover:bg-gray-200 transition duration-300 ease-in-out"
                                    content={<button onClick={() => {
                                        handleNewTournamentPrediction(tourPred);
                                    }} className="bg-blue-500 text-white px-4 py-2 rounded mt-4 hover:bg-blue-600 transition duration-300 ease-in-out w-full">
                                        Select
                                    </button>}
                                />
                            </div>
                        ))}
                    </div>
                )}
                
            </Modal>

        </div>
    );
}
