import { FC, useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { Link } from 'react-router-dom';
import { UserData } from '../utils/Types';
import axiosInstance from '../utils/Api';

const SearchResultPage: FC = () => {
    const [users, setUsers] = useState<UserData[]>([]);
    const location = useLocation();
    const query = new URLSearchParams(location.search).get('query') || '';

    useEffect(() => {
        console.log("use effect in searchresults");
        const fetchUsers = async () => {
            try {
                const response = await axiosInstance.get(
                    `/user/search?query=${query}`
                );
                console.log(response.data);
                setUsers(response.data);
    
            } catch(error) {
                console.error("Error searching users:", error);
            }
        };

        fetchUsers();
    }, [query]);

    return (
        <div className="bg-gray-900 text-white min-h-screen">
             <div className="p-4">
                {users.map(user => (
                    <Link 
                        key={user.username} 
                        to={`/users/${user.username}`}
                        className="flex items-center space-x-4 hover:bg-gray-800 p-4 rounded-lg mb-4"
                    >
                        {user.profilePicRef && (
                            <img 
                                src={user.profilePicRef} 
                                alt={user.username} 
                                className="w-16 h-16 rounded-full" 
                            />
                        )} 
                        <div>
                            <p className="text-lg font-medium">
                                {user.firstName} {user.lastName} (@{user.username})
                            </p>
                            {user.favouriteTeam && (
                                <p className="text-gray-400 text-sm">
                                    Favourite Team: {user.favouriteTeam}
                                </p>
                            )}
                        </div>
                    </Link>
                ))}
            </div>
        </div>
    );
};

export default SearchResultPage;