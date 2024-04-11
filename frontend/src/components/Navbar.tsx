import { FC, useState } from 'react';
import '../styles/Navbar.css';
import { Link, NavLink, useNavigate } from 'react-router-dom';
import { FaSearch } from 'react-icons/fa';

interface INavbarProps {

    isLoggedIn: boolean;
}

const Navbar: FC<INavbarProps> = ({ isLoggedIn }) => {

    const [input, setInput] = useState("");
    const navigate = useNavigate();

    const handleSearch = (event: React.FormEvent) => {
        event.preventDefault();
        navigate(`search/${input}`);
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
                            <NavLink className="nav-link" to="/quiz">Quiz</NavLink>
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