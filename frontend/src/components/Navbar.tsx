import { FC, useState, useContext } from 'react';
import '../styles/Navbar.css';
import { Link, NavLink, useNavigate } from 'react-router-dom';
import { FaSearch } from 'react-icons/fa';
import { AuthContext } from '../context/AuthContext';



const Navbar: FC = () => {

    const [input, setInput] = useState("");
    const navigate = useNavigate();

    const { isLoggedIn } = useContext(AuthContext);
    const handleSearch = (event: React.FormEvent) => {
        event.preventDefault();
        navigate(`/search?query=${input}`);
    };

    return (
        <div className="navbar-container">
            <nav className="navbar">
                
                <Link to="/" className="logo">
                    Some name
                </Link>
                <div className="nav-div">
                    <NavLink className="nav-link" to="/leaderboard">Leaderboard</NavLink>
                    <form onSubmit={handleSearch}>
                        <div className="search-field">
                            <FaSearch id="search-icon" />
                            <input placeholder="Find user..."
                                value={input}
                                onChange={(e) => setInput(e.target.value)} />
                        </div>
                    </form>
                    {isLoggedIn ? (
                        <>
                            <NavLink className="nav-link" to="/profile">Profile</NavLink>
                            {/*<NavLink className="nav-link" to="/quiz">Quiz</NavLink>*/}
                            <NavLink className="nav-link" to="/team">Team</NavLink>
                        </>
                    ) : (
                        <NavLink className="nav-link" to="/signin">Sign in</NavLink>
                    )}
                </div>
            </nav>
        </div>


    )
}

export default Navbar;