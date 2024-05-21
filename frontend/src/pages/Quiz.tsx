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

  const handleStartQuiz = () => {
    if (selectedTeam && quizCategory) {
      setStartQuiz(true);
      setPlayers([
        

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
        const remainingPlayers = players.filter(p => p !== otherPlayer); 
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
            defaultOptionLabel="Select team.."
            selectedOption={selectedTeam}
            setSelectedOption={setSelectedTeam}
          />
          <Dropdown
            options={categories}
            defaultOptionLabel="Select category.."
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