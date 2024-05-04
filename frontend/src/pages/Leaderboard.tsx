
const globalLeaderboard = [
  { name: "Alice", score: 1200 },
  { name: "Bob", score: 1150 },
  { name: "Charlie", score: 1080 },
];

const friendsLeaderboard = [
  { name: "David", score: 950 },
  { name: "Emily", score: 880 },
  { name: "Frank", score: 820 },
];

const LeaderboardPage = () => {
    return (
      <div className="container mx-auto space-y-8">
        <div>
          <h2 className="text-center font-bold text-xl">Global Leaderboard</h2>
          <ul className="list-disc list-inside text-lg">
            {globalLeaderboard.map((user, index) => (
              <li key={index}>
                {user.name} - {user.score}
              </li>
            ))}
          </ul>
        </div>
  
        <div>
          <h2 className="text-center font-bold text-xl">Friends Leaderboard</h2>
          <ul className="list-disc list-inside text-lg">
            {friendsLeaderboard.map((friend, index) => (
              <li key={index}>
                {friend.name} - {friend.score}
              </li>
            ))}
          </ul>
        </div>
      </div>
    );
  };

export default LeaderboardPage;