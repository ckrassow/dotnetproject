import { useState } from "react";
import Tab from "../../components/Tab";
import Card from "../../components/Card";
import Modal from "../../components/Modal";
import Dropdown from "../../components/Dropdown";

const childTabs = ["Players", "Teams", "Tournament"];
const predictionsPlayers = [
    "PlayerPrediction 1",
    "PlayerPrediction 2",
    "PlayerPrediction 3",
    "PlayerPrediction 4",
    "PlayerPrediction 5",
    "PlayerPrediction 6",
    "PlayerPrediction 7",
    "PlayerPrediction 8"
];

const predictionsTeams = [
    "TeamPrediction 1",
    "TeamPrediction 2",
    "TeamPrediction 3",
    "TeamPrediction 4",
    "TeamPrediction 5",
    "TeamPrediction 6",
    "TeamPrediction 7",
    "TeamPrediction 8"
];

const predictionsTournament = [
    "TournamentPrediction 1",
    "TournamentPrediction 2",
    "TournamentPrediction 3",
    "TournamentPrediction 4",
    "TournamentPrediction 5",
    "TournamentPrediction 6",
    "TournamentPrediction 7",
    "TournamentPrediction 8"
];

export function Predictions() {
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
    const height= "300px";
    const width = "400px";
    return (
        <div>
            <div className="tabs">
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
                    {predictionsPlayers.map(prediction => (
                        <Card
                            key={prediction}
                            header={<h2>{prediction}</h2>}
                            content={<button onClick={() => setIsModalOpen(true)}>Make Prediction</button>}
                            height={height}
                            width={width}
                        />
                    ))}
                </div>
            )}

            {activeTab === "Teams" && (
                <div className="predictions-container">
                    {predictionsTeams.map(prediction => (
                        <Card
                            key={prediction}
                            header={<h2>{prediction}</h2>}
                            content={<button>Make Prediction</button>}
                            height={height}
                            width={width}
                        />
                    ))}
                </div>
            )}

            {activeTab === "Tournament" && (
                <div className="predictions-container">
                    {predictionsTournament.map(prediction => (
                        <Card
                            key={prediction}
                            header={<h2>{prediction}</h2>}
                            content={<button>Make Prediction</button>}
                            height={height}
                            width={width}
                        />
                    ))}
                </div>
            )}
            <Modal isOpen={isModalOpen} onClose={() => setIsModalOpen(false)}>
                <Dropdown 
                    options={teams} 
                    selectedOption={selectedTeam} 
                    setSelectedOption={(team) => {
                        setSelectedTeam(team);
                        setSelectedPlayers(players[team as Team]);
                    }} 
                />
                <div className="player-cards-container">
                    {selectedPlayers.map(player => (
                        <div className="player-card-container">
                            <Card
                                key={player}
                                header={<h2>{player}</h2>}
                                content={<button>Select</button>}
                                height={"300px"}
                                width={"200px"}
                            />
                        </div>
                    ))}
                </div>
            </Modal>

        </div>
    );
}
