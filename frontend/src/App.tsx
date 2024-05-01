import { Navigate, Routes, Route, BrowserRouter } from 'react-router-dom';
import { useState, useEffect } from 'react';
import { HomePage } from './pages/Home';
import LeaderboardPage from './pages/Leaderboard';
import { ProfilePage } from './pages/Profile/Profile';
import { SigninPage } from './pages/Signin';
import Navbar from './components/Navbar';
import { SignupPage } from './pages/Signup';
import { QuizPage } from './pages/Quiz';
import SearchResultPage from './pages/SearchResult';
import { TeamPage } from './pages/Team/Team';
import { AuthContext } from './context/AuthContext';

function App() {

  const [isLoggedIn, setIsLoggedIn] = useState(false);
  useEffect(() => {
    const token = localStorage.getItem("token");
    if (token) {
      setIsLoggedIn(true);
    }
  }, []);

  return (
      <BrowserRouter>
        <AuthContext.Provider value={{ isLoggedIn, setIsLoggedIn }}>
          <Navbar />
          <div className="px-10 sm:px-20 lg:px-28">
            <Routes>
              <Route path="/" element={<HomePage/>} />
              <Route path="/leaderboard" element={<LeaderboardPage/>} />
              <Route path="/signin" element={isLoggedIn ? <Navigate to="/account" />: <SigninPage/>} />
              <Route path="/account" element={isLoggedIn ? <ProfilePage /> : <Navigate to="/signin" /> } />
              <Route path="/signup" element={<SignupPage/>} />
              <Route path="/search/" element={<SearchResultPage/>} />
              <Route path="/team" element={<TeamPage /> } />
              <Route path="/quiz" element={<QuizPage/>} />
            </Routes>
          </div>
        </AuthContext.Provider>
      </BrowserRouter>
  );
}

export default App;
