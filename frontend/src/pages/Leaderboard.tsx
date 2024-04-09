import "../styles/Leaderboard.css";

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
      <div className="leaderboard-container">
        <div>
          <h2 className="h2-leaderboard">Global Leaderboard</h2>
          <ul className="ul-leaderboard">
            {globalLeaderboard.map((user, index) => (
              <li key={index}>
                {user.name} - {user.score}
              </li>
            ))}
          </ul>
        </div>
  
        <div>
          <h2 className="h2-leaderboard">Friends Leaderboard</h2>
          <ul className="ul-leaderboard">
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