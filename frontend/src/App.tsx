import { Navigate, Routes, Route, BrowserRouter } from 'react-router-dom';
import { useContext } from 'react';
import { HomePage } from './pages/Home';
import LeaderboardPage from './pages/Leaderboard';
import { ProfilePage } from './pages/Profile/Profile';
import { SigninPage } from './pages/Signin';
import Navbar from './components/Navbar';
import { SignupPage } from './pages/Signup';
import { QuizPage } from './pages/Quiz';
import SearchResultPage from './pages/SearchResult';
import { TeamPage } from './pages/Team/Team';
import { AuthContext }  from './context/AuthContext';
import Wrapper from './components/Wrapper';

function App() {

  const { isLoggedIn } = useContext(AuthContext);
  return (
      <BrowserRouter>
        <Wrapper className="bg-gray-100">
          <Navbar />
          <div className="container mx-auto p-3 sm:p-6 lg:p-9">
            <Routes>
              <Route path="/" element={<HomePage/>} />
              <Route path="/leaderboard" element={<LeaderboardPage/>} />
              <Route path="/signin" element={isLoggedIn ? <Navigate to={`/user/${localStorage.getItem("username")}`} />: <SigninPage/>} />
              <Route path="/user/:username" element={<ProfilePage />} />
              <Route path="/signup" element={<SignupPage/>} />
              <Route path="/search/" element={<SearchResultPage/>} />
              <Route path="/team" element={<TeamPage /> } />
              <Route path="/quiz" element={<QuizPage/>} />
            </Routes>
          </div>
        </Wrapper>
      </BrowserRouter>
  );
}

export default App;
