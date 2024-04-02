import { Navigate, Routes, Route, BrowserRouter } from 'react-router-dom';
import { HomePage } from './pages/Home';
import { LeaderboardPage } from './pages/Leaderboard';
import { ProfilePage } from './pages/Profile';
import { SigninPage } from './pages/Signin';
import Navbar from './components/Navbar';
import { SignupPage } from './pages/Signup';

function App() {

  const isLoggedIn = false;

  return (
    <div className='mainDiv'>
      <BrowserRouter>
      <Navbar isLoggedIn={ isLoggedIn }/>
        <Routes>
          <Route path="/" element={<HomePage/>} />
          <Route path="/leaderboard" element={<LeaderboardPage/>} />
          <Route path="/signin" element={isLoggedIn ? <Navigate to="/profile" />: <SigninPage/>} />
          <Route path="/profile" element={isLoggedIn ? <ProfilePage /> : <Navigate to="/signin" /> } />
          <Route path="/signup" element={<SignupPage/>} />
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
