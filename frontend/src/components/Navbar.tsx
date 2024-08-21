import { FC, useState, useContext } from 'react';
import { Link, NavLink, useNavigate } from 'react-router-dom';
import { FaSearch } from 'react-icons/fa';
import { AuthContext } from '../context/AuthContext';
import SignOut from './SignOut';

const Navbar: FC = () => {

    const [input, setInput] = useState("");
    const navigate = useNavigate();

    const { isLoggedIn } = useContext(AuthContext);
    const handleSearch = async (event: React.FormEvent) => {
        event.preventDefault();
        navigate(`/search?query=${input}`);
    };

    return (
        <div className="w-full bg-gray-800 px-4 sm:px-6 lg:px-8">
            <nav className="text-white py-2 px-4 flex items-center w-full space-x-4">
                
                <Link to="/" className="logo">
                    Home
                </Link>
                <div className="flex flex-grow items-center justify-between">
                    <form onSubmit={handleSearch} className="space-x-2">
                        <div className="flex items-center bg-gray-700 rounded-lg px-3 py-2 focus:outline-none focus:ring-2">
                            <FaSearch id="search-icon" />
                            <input className="flex-grow ml-2 text-black" placeholder="Find user..."
                                value={input}
                                onChange={(e) => setInput(e.target.value)} />
                        </div>
                    </form>
                    {isLoggedIn ? (
                        <>
                            <NavLink className="mx-2" to="/games">Games</NavLink>
                            <NavLink className="mx-2" to={`/user/${localStorage.getItem("username")}`}>Account</NavLink>
                            {/*<NavLink className="mx-2" to="/quiz">Quiz</NavLink>*/}
                            <NavLink className="mx-2" to="/team">Team</NavLink>
                            <SignOut />
                        </>
                    ) : (
                        <NavLink className="mx-2" to="/signin">Sign in</NavLink>
                    )}
                </div>
            </nav>
        </div>


    )
}

export default Navbar;