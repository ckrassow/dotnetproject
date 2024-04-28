import { useState } from "react";
import Tab from "../../components/Tab";
import Card from "../../components/Card";
import Modal from "../../components/Modal";
import Dropdown from "../../components/Dropdown";
import axios from "axios";
import { PlayerData, TournamentPrediction, PlayerPrediction, TeamPrediction, NationalTeamData } from "../../utils/Types";

const childTabs = ["Players", "Teams", "Tournament"];

type PredictionProps = {
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
    const [teamData, setTeamData] = useState<NationalTeamData[]>([]);
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
      
    const height= "300px";
    const width = "400px";
    
    const getTeamData = async () => {
        try {
            const token = localStorage.getItem("token");
            const response = await axios.get(
                `http://localhost:5175/api/nationalteam`, {
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
    
    const handleShowPlayers = async (teamName: string) => {
        try {
            const token = localStorage.getItem("token");
            
            const response = await axios.get(
                `http://localhost:5175/api/nationalteam/${teamName}/players`, {
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
        console.log(props.playerPredictions);

        try {
            const token = localStorage.getItem("token");
            const userId = localStorage.getItem("userId");

            await axios.put(
                `http://localhost:5175/api/user/${userId}/playerprediction/${currentPrediction.id}`,
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
        console.log(updatedTeamPreds);
        props.setTeamPredictions(updatedTeamPreds);

        try {
            const token = localStorage.getItem("token");
            const userId = localStorage.getItem("userId");
            console.log(currentPrediction);

            await axios.put(
                `http://localhost:5175/api/user/${userId}/teamprediction/${currentPrediction.id}`,
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

            await axios.put(
                `http://localhost:5175/api/user/${userId}/tournamentprediction/${currentPrediction.id}`,
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
            <div className="sub-tabs">
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
                <div className="predictions-container">
                    {props.playerPredictions?.map(prediction  => (
                        <Card
                            key={prediction.predictionType}
                            header={<h2>{convertToSpaceSeparated(prediction.predictionTypeString)}</h2>}
                            content={
                                (
                                    <>
                                    {console.log("image path:", prediction.player?.imagePath)}
                                    {prediction.player ? (
                                      <>
                                          <h3>{prediction.player.name}</h3>
                                          <img src={require(`../../assets/${prediction.player.imagePath}`)} alt={prediction.player.name} style={{height: "125px", width: "75px"}} />
                                      </>
                                    ) : (
                                      <>
                                          <h3>Player name</h3>
                                          <img src={require("../../assets/default.jpg")} alt="Player name" style={{height: "125px", width: "75px"}}/>
                                      </>
                                    )}
                                    <button onClick={() => {
                                        setIsModalOpen(true);
                                        setCurrentPrediction(prediction);
                                    }}>Make Prediction</button>
  
                                    </>
                                )
                              }
                            height={height}
                            width={width}
                        />
                    ))}
                </div>
            )}

            {activeTab === "Teams" && (
                <div className="predictions-container">
                    {props.teamPredictions?.map(prediction => (
                        <Card
                            key={prediction.predictionType}
                            header={<h2>{convertToSpaceSeparated(prediction.predictionTypeString)}</h2>}
                            content={
                               (
                                  <>
                                  {console.log("image path:", prediction.nationalTeam?.imagePath)}
                                  {prediction.nationalTeam ? (
                                    <>
                                        <h3>{prediction.nationalTeam.name}</h3>
                                        <img src={require(`../../assets/${prediction.nationalTeam.imagePath}`)} alt={prediction.nationalTeam.name} style={{height: "125px", width: "75px"}} />
                                    </>
                                  ) : (
                                    <>
                                        <h3>Team name</h3>
                                        <img src={require("../../assets/default.jpg")} alt="Team name" style={{height: "125px", width: "75px"}} />
                                    </>
                                  )}
                                  <button onClick={() => {
                                    setIsModalOpen(true);
                                    getTeamData();
                                    setCurrentPrediction(prediction);
                                  }}>Make Prediction</button>

                                  </>
                                )
                              }
                            height={height}
                            width={width}
                        />
                    ))}
                </div>
            )}

            {activeTab === "Tournament" && (
                <div className="predictions-container">
                    {props.tournamentPredictions?.map(prediction => (
                        <Card
                            key={prediction.predictionType}
                            header={<h2>{convertToSpaceSeparated(prediction.predictionTypeString)}</h2>}
                            content={
                                (
                                    <>
                                    {prediction.predictionValue ? (
                                      <>
                                          <h3>{prediction.predictionValue}</h3>
                                      </>
                                    ) : (
                                      <>
                                          <h3>Tournament prediction</h3>
                                      </>
                                    )}
                                    <button onClick={() => {
                                        setIsModalOpen(true);
                                        setCurrentPrediction(prediction);
                                    }}>Make Prediction</button>
  
                                    </>
                                )
                              }
                            height={height}
                            width={width}
                        />
                    ))}
                </div>
            )}
            <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}>
                {activeTab === "Players" && (
                    <>
                        <Dropdown 
                            options={teams} 
                            selectedOption={dropDownTeam}
                            defaultOptionLabel="Select team.." 
                            setSelectedOption={handleShowPlayers} 
                        />
                        {dropDownTeam && (
                            <div className="cards-container">
                                {selectedPlayers.map((player) => (
                                    <div className="modal-card-container" key={player.id}>
                                        <Card
                                            header={<h2>{player.name}</h2>}
                                            content={
                                            <>
                                                <p>Position: {player.pos}</p>
                                                <p>Age: {player.age}</p>
                                                <p>Club: {player.club}</p>
                                                <p>Total NT goals: {player.goals}</p>
                                                <img src={require(`../../assets/${player.imagePath}`)} alt={player.name} style={{height: "125px", width: "75px"}} />
                                                <button onClick={() => {
                                                    handleNewPlayerPrediction(player);
                                                }}>Select</button>
                                            </>
                                            }
                                            height={"300px"}
                                            width={"200px"}
                                        />
                                    </div>
                                ))}
                            </div>
                        )}      
                    </>
                )}

                {activeTab === "Teams" && (
                    <div className="cards-container">
                        {teamData.map((team) => (
                            <div className="modal-card-container" key={team.name}>
                                <Card
                                    header={<h2>{team.name}</h2>}
                                    content={
                                    <>  
                                        <p>Group: {team.group}</p>
                                        <p>Fifa ranking: {team.fifaRanking}</p>
                                        <p>Playoff appearences: {team.playoffAppearences}</p>
                                        <img src={require(`../../assets/${team.imagePath}`)} alt={team.name} style={{height: "125px", width: "75px"}} />
                                        <button onClick={() => {
                                            handleNewTeamPrediction(team);
                                        }}>Select</button>
                                    </>}
                                    height={"300px"}
                                    width={"200px"}
                                />
                            </div>
                        ))}
                    </div>
                )}

                {activeTab === "Tournament" && (
                    <div className="cards-container">
                        {tourPredOptions.map((tourPred) => (
                            <div className="modal-card-container" key={tourPred}>
                                <Card
                                    header={<h2>{tourPred}</h2>}
                                    content={<button onClick={() => {
                                        handleNewTournamentPrediction(tourPred);
                                    }}>Select</button>}
                                    height={"300px"}
                                    width={"200px"}
                                />
                            </div>
                        ))}
                    </div>
                )}
                
            </Modal>

        </div>
    );
}
