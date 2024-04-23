import { useState } from "react";
import Tab from "../../components/Tab";
import Card from "../../components/Card";
import Modal from "../../components/Modal";
import Dropdown from "../../components/Dropdown";
import axios from "axios";
import { PredictionData, TeamData, PlayerData } from "./Profile";

const DATA_PATH = process.env.REACT_APP_DATA_PATH;
const childTabs = ["Players", "Teams", "Tournament"];

type PredictionProps = {
    isPublicProfile: boolean;
    predictionData: PredictionData;
};

export function Predictions({ isPublicProfile, predictionData }: PredictionProps) {

    const [activeTab, setActiveTab] = useState(childTabs[0]);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [selectedTeam, setSelectedTeam] = useState("");
    const [selectedPlayers, setSelectedPlayers] = useState<PlayerData[]>([]);
    
    type Team = "France" | "England";
    console.log(DATA_PATH);

    const teams: Team[] = ["France", "England"];
    const tourPredOptions = [
        "Option A",
        "Option B",
        "Option C",
        "Option D",
      ];
    const height= "300px";
    const width = "400px";
    
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
                console.log(player.imagePath);
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

            setSelectedTeam(teamName);
            setSelectedPlayers(playerData);            

        } catch (error) {
            console.error("Error when trying to show players", error);
        }
    };

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
                    {predictionData.PlayerPredictions?.map(prediction => (
                        <Card
                            key={prediction.predictionType}
                            header={<h2>{prediction.predictionType}</h2>}
                            content={
                                isPublicProfile ? (
                                  <p>Public profile</p> 
                                ) : (
                                    <>
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
                                    <button onClick={() => setIsModalOpen(true)}>Make Prediction</button>
  
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
                    {predictionData.TeamPredictions?.map(prediction => (
                        <Card
                            key={prediction.predictionType}
                            header={<h2>{prediction.predictionType}</h2>}
                            content={
                                isPublicProfile ? (
                                  <p>Public profile</p> 
                                ) : (
                                  <>
                                  {prediction.nationalTeam ? (
                                    <>
                                        <h3>{prediction.nationalTeam.name}</h3>
                                        <img src={`../assets/${prediction.nationalTeam.imagePath}`} alt={prediction.nationalTeam.name} style={{height: "125px", width: "75px"}} />
                                    </>
                                  ) : (
                                    <>
                                        <h3>Team name</h3>
                                        <img src={"../../assets/default.jpg"} alt="Team name" style={{height: "125px", width: "75px"}} />
                                    </>
                                  )}
                                  <button onClick={() => setIsModalOpen(true)}>Make Prediction</button>

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
                    {predictionData.TournamentPredictions?.map(prediction => (
                        <Card
                            key={prediction.predictionType}
                            header={<h2>{prediction.predictionType}</h2>}
                            content={
                                isPublicProfile ? (
                                  <p>Public profile</p> 
                                ) : (
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
                                    <button onClick={() => setIsModalOpen(true)}>Make Prediction</button>
  
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
                            selectedOption={selectedTeam}
                            defaultOptionLabel="Select team.." 
                            setSelectedOption={handleShowPlayers} 
                        />
                        {selectedTeam && (
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
                                                <button>Select</button>
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
                        {teams.map((team) => (
                            <div className="modal-card-container" key={team}>
                                <Card
                                    header={<h2>{team}</h2>}
                                    content={<button>Select</button>}
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
                                    content={<button>Select</button>}
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
