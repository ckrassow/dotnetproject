import { useState } from "react";
import Dropdown from '../components/Dropdown';
import Card from '../components/Card';

type Player = {
    name: string;
    caps: number;
    goals: number;
    imageUrl: string;
  };

const Quiz = () => {
  const [selectedTeam, setSelectedTeam] = useState('');
  const [quizCategory, setQuizCategory] = useState('');
  const [startQuiz, setStartQuiz] = useState(false);
  const [players, setPlayers] = useState<Player[]>([]);
  const [currentPlayerIndex, setCurrentPlayerIndex] = useState(0);
  const [score, setScore] = useState(0);


  const teams = ['Germany', 'France', 'Spain'];
  const categories = ['Most caps', 'Most goals'];
  const imagesFolder = require.context("../assets/images", true);

  const handleStartQuiz = () => {
    if (selectedTeam && quizCategory) {
      setStartQuiz(true);
      // dummy data
      setPlayers([
        { name: "Thomas Müller", caps: 120, goals: 40, imageUrl: imagesFolder("./Thomas_Müller_Werner100359_CC_BY-SA_4.0.jpg")},
        { name: "Toni Kroos", caps: 110, goals: 8, imageUrl: imagesFolder("./Toni_Kroos_Unknown_CC_BY-SA_4.0.jpg")},
        { name: "Antonio Rüdiger", caps: 50, goals: 1, imageUrl: imagesFolder("./Antonio_Rüdiger_Granada_CC_BY-SA_4.0.jpg")},
        { name: "Pascal Gros", caps: 5, goals: 0, imageUrl: imagesFolder("./Pascal_Groß_FlickreviewR_2_CC_BY-SA_4.0.jpg")},
        { name: "Florian Wirtz", caps: 11, goals: 4, imageUrl: imagesFolder("./Florian_Wirtz_Pyaet_CC_BY-SA_4.0.jpg")},
        { name: "Oliver Baumann", caps: 2, goals: 0, imageUrl: imagesFolder("./Oliver_Baumann_Silesia711_CC_BY-SA_4.0.jpg")},
        { name: "Niclas Füllkrug", caps: 16, goals: 3, imageUrl: imagesFolder("./Niclas_Füllkrug_Silesia711_CC_BY-SA_4.0.jpg")},
        { name: "Christoph Baumgartner", caps: 2, goals: 0, imageUrl: imagesFolder("./Christoph_Baumgartner_Steindy_CC_BY-SA_4.0.jpg")},

      ]);
    }
  };

  const handlePlayerSelection = (selectedPlayer: Player) => {
    const otherPlayer = players.find(p => p.name !== selectedPlayer.name);
    console.log("Other player:", otherPlayer);

    if (!otherPlayer) {
        console.log("Could not find other player");
        return;
    }

    const isCorrect = 
        (quizCategory === "Most caps" && selectedPlayer.caps > otherPlayer.caps) ||
        (quizCategory === "Most goals" && selectedPlayer.goals > otherPlayer.goals);

    if (isCorrect) {
        console.log("Players:", players);
        const remainingPlayers = players.filter(p => p !== otherPlayer); // Only remove the losing player
        const newPlayerIndex = Math.floor(Math.random() * remainingPlayers.length);
        const newPlayer = remainingPlayers[newPlayerIndex];
        console.log("Remaining players:", remainingPlayers);
        console.log("New player:", newPlayer);

        setPlayers([...remainingPlayers]);
        setCurrentPlayerIndex(0);
        setScore(score + 1);

    } else {
        console.log("Game over!");
    }
  };

  return (
    <div className="container mx-auto p-4">
      {!startQuiz ? (
        <>
          <Dropdown
            options={teams}
            selectedOption={selectedTeam}
            setSelectedOption={setSelectedTeam}
          />
          <Dropdown
            options={categories}
            selectedOption={quizCategory}
            setSelectedOption={setQuizCategory}
          />
          <button
            className={`mt-4 p-2 text-white ${selectedTeam && quizCategory ? 'bg-blue-500' : 'bg-gray-300'}`}
            onClick={handleStartQuiz}
            disabled={!selectedTeam || !quizCategory}
          >
            Start Quiz
          </button>
        </>
      ) : (
        // Quiz UI goes here
        <div className="flex justify-center items-center space-x-4">
          {players.slice(currentPlayerIndex, currentPlayerIndex + 2).map((player, index) => (
            
            <Card
              key={index}
              header={<img src={player.imageUrl} alt={player.name} />}
              content={
                <div className="text-center">
                  <h3 className="text-lg font-bold">{player.name}</h3>
                  <button
                    className="mt-2 p-2 bg-blue-500 text-white rounded"
                    onClick={() => handlePlayerSelection(player)}
                  >
                    Select
                  </button>
                </div>
              }
            />
          ))}
        </div>
      )}
    </div>
  );
};

export { Quiz as QuizPage};