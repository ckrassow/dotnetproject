import { FC, useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { Link } from 'react-router-dom';
import { UserData } from '../utils/Types';
import { fetchUsers } from '../utils/ApiCalls';

const SearchResultPage: FC = () => {
    const [users, setUsers] = useState<UserData[]>([]);
    const location = useLocation();
    const query = new URLSearchParams(location.search).get('query') || '';
    const accountName = process.env.REACT_APP_AZURE_ACC_NAME;
    const sasToken = process.env.REACT_APP_SAS_TOKEN;
    const containerName = process.env.REACT_APP_AZURE_CONTAINER_NAME!;

    const getUsers = async () => {
        try {
            const data = await fetchUsers(query);
            setUsers(data);
        } catch (error) {
            console.error("Error when fetching user search results");
        }
    };

    useEffect(() => {
        getUsers();
    }, [query]);

    return (
        <div className="bg-gray-800 text-white min-h-screen flex flex-col items-center pt-8 px-4 lg:px-0">
            <h1 className="text-3xl lg:text-4xl font-bold mb-6">User Search Results:</h1>
             <div className="w-full max-w-4xl p-4">
                {users.map(user => (
                    <div key={user.username} className="bg-gray-700 shadow-lg rounded-lg p-4 mb-4 transition duration-300 ease-in-out hover:bg-gray-600">
                        <Link 
                            to={`/user/${user.username}`}
                            className="flex flex-col lg:flex-row items-center space-x-0 lg:space-x-4 group"
                        >
                            {user.profilePicRef && (
                                <img 
                                    src={`https://${accountName}.blob.core.windows.net/${containerName}/${user.profilePicRef}?${sasToken}`} 
                                    alt={user.username} 
                                    className="w-24 h-24 lg:w-20 lg:h-20 rounded-full border-2 border-gray-500 group-hover:border-gray-400 mb-4 lg:mb-0" 
                                />
                            )} 
                            <div className="text-center lg:text-left">
                                <p className="text-xl font-semibold">
                                    {user.firstName} {user.lastName} (@{user.username})
                                </p>
                                {user.favouriteTeam && (
                                    <p className="text-gray-400 text-sm mt-1">
                                        Favourite Team: {user.favouriteTeam}
                                    </p>
                                )}
                            </div>
                        </Link>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default SearchResultPage;