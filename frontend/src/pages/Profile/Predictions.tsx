import { useState } from "react";
import Tab from "../../components/Tab";
import Card from "../../components/Card";
import Modal from "../../components/Modal";
import Dropdown from "../../components/Dropdown";
import { PredictionData } from "./Profile";


const childTabs = ["Players", "Teams", "Tournament"];

type PredictionProps = {
    isPublicProfile: boolean;
    predictionData: PredictionData;
};

export function Predictions({ isPublicProfile, predictionData }: PredictionProps) {

    const [activeTab, setActiveTab] = useState(childTabs[0]);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [selectedTeam, setSelectedTeam] = useState("");
    const [selectedPlayers, setSelectedPlayers] = useState<string[]>([]);
    type Team = "France" | "England";

    const teams: Team[] = ["France", "England"];
    const players: Record<Team, string[]> = {
        "France": ["Player 1", "Player 2", "Player 3", "Player 4", "Player 5", "Player 6",
                   "Player 7", "Player 8", "Player 9", "Player 10", "Player 11", "Player 12",
                   "Player 13", "Player 14", "Player 15", "Player 16", "Player 17", "Player 18",
                   "Player 19", "Player 20", "Player 21", "Player 22", "Player 23"],
        "England": ["Player 1", "Player 2", "Player 3", "Player 4", "Player 5", "Player 6",
                    "Player 7", "Player 8", "Player 9", "Player 10", "Player 11", "Player 12",
                    "Player 13", "Player 14", "Player 15", "Player 16", "Player 17", "Player 18",
                    "Player 19", "Player 20", "Player 21", "Player 22", "Player 23"],
    };
    const tourPredOptions = [
        "Option A",
        "Option B",
        "Option C",
        "Option D",
      ];
    const height= "300px";
    const width = "400px";
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
                                          <img src={prediction.player.imagePath} alt={prediction.player.name} />
                                      </>
                                    ) : (
                                      <>
                                          <h3>Player name</h3>
                                          <img src="default.jpg" alt="Player name" />
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
                                        <img src={prediction.nationalTeam.imagePath} alt={prediction.nationalTeam.name} />
                                    </>
                                  ) : (
                                    <>
                                        <h3>Team name</h3>
                                        <img src="default.jpg" alt="Team name" />
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
                            setSelectedOption={(team) => {
                            setSelectedTeam(team);
                            setSelectedPlayers(players[team as Team]);
                        }} 
                        />
                        {selectedTeam && (
                            <div className="cards-container">
                                {selectedPlayers.map((player) => (
                                    <div className="modal-card-container" key={player}>
                                        <Card
                                            header={<h2>{player}</h2>}
                                            content={<button>Select</button>}
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
