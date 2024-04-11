import { FC, useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { Link } from 'react-router-dom';

interface IUser {
    username: string;
}

const SearchResultPage: FC = () => {
    const [users, setUsers] = useState<IUser[]>([]);
    const location = useLocation();
    const query = new URLSearchParams(location.search).get('query') || '';

    useEffect(() => {
    }, [query]);

    return (
        <div>
            <h1>Search Results for "{query}"</h1>
            {users.map(user => (
                <Link key={user.username} to={`/users/${user.username}`}>
                    {user.username}
                </Link>
            ))}
        </div>
    );
};

export default SearchResultPage;