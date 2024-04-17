import { Navigate, Routes, Route, BrowserRouter } from 'react-router-dom';
import { HomePage } from './pages/Home';
import LeaderboardPage from './pages/Leaderboard';
import { ProfilePage } from './pages/Profile/Profile';
import { SigninPage } from './pages/Signin';
import Navbar from './components/Navbar';
import { SignupPage } from './pages/Signup';
import { QuizPage } from './pages/Quiz';
import SearchResultPage from './pages/SearchResult';
import "./styles/App.css";
import { TeamPage } from './pages/Team/Team';

function App() {

  const isLoggedIn = false;

  return (
      <BrowserRouter>
      <Navbar isLoggedIn={ isLoggedIn }/>
      <div className='main-div'>
        <Routes>
          <Route path="/" element={<HomePage/>} />
          <Route path="/leaderboard" element={<LeaderboardPage/>} />
          <Route path="/signin" element={isLoggedIn ? <Navigate to="/profile" />: <SigninPage/>} />
          <Route path="/profile" element={isLoggedIn ? <ProfilePage /> : <Navigate to="/signin" /> } />
          <Route path="/signup" element={<SignupPage/>} />
          <Route path="/search/" element={<SearchResultPage/>} />
          <Route path="/team" element={<TeamPage /> } />
          <Route path="/quiz" element={<QuizPage/>} />
        </Routes>
      </div>
      </BrowserRouter>
  );
}

export default App;
