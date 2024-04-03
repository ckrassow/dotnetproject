import { useState } from "react";
import Tab from "../../components/Tab";
import Card from "../../components/Card";

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
                            content={<button>Make Prediction</button>}
                            height="300px"
                            width="350px"
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
                            height="300px"
                            width="350px"
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
                            height="300px"
                            width="350px"
                        />
                    ))}
                </div>
            )}

        </div>
    );
}
