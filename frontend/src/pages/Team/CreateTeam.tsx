import { FC, useState } from "react";

interface ICreateTeamForm {
    name: string;
    members: string[];
};

interface User {
    id: string;
    name: string;
};

const CreateTeam: FC = () => {
    
    const [formState, setFormState] = useState<ICreateTeamForm>({ 
        name: "",
        members: []
    });
    const [searchTerm, setSearchTerm] = useState("");
    const [searchResults, setSearchResults] = useState<User[]>([]);
    const [showDropdown, setShowDropdown] = useState(false);

    /* WORK ON THIS LATER WHEN I IMPLEMENT BACKEND
    useEffect(() => {
        if (searchTerm) {
          axios.get(`/api/users?search=${searchTerm}`).then(response => {
            setSearchResults(response.data);
          });
        } else {
          setSearchResults([]);
        }
      }, [searchTerm]); */

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setFormState({
            ...formState,
            [e.target.name]: e.target.value,
        });
    };

    const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setSearchTerm(e.target.value);
    };
    
    const handleSearchSubmit = (e: React.KeyboardEvent<HTMLInputElement> | React.MouseEvent) => {
        e.preventDefault();
        setShowDropdown(true);
    };

    const handleAddMember = (user: User) => {
        setFormState({
            ...formState,
            members: [...formState.members, user.name],
        });
        setSearchTerm("");
    };

    const handleDeleteMember = (memberToDelete: string) => {
        setFormState({
            ...formState,
            members: formState.members.filter(member => member !== memberToDelete),
        });
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
    };
    
    return (
        <div className="bg-gray-100 p-4">
            <form className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4">
                <label className="block text-gray-700 text-sm font-bold mb-2">
                    Team name:
                    <input className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-md" type="text" name="name" value={formState.name} onChange={handleInputChange} />
                </label>
                <label className="block text-gray-700 text-sm font-bold mb-2">
                    Search for user to add as member:
                    <input className="shadow appearance-none border rounded w-full py-2 px-3 text-gray-700 leading-tight focus:outline-none focus:shadow-md" type="text" value={searchTerm} onChange={handleSearchChange}
                    onKeyDown={e => e.key === "Enter" && handleSearchSubmit(e)} />
                    <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded" onClick={handleSearchSubmit}>Search</button>
                    {showDropdown && (
                        <div className="dropdown">
                            {searchResults.map(user => (
                                <div key={user.id} onClick={() => handleAddMember(user)}>
                                    {user.name}
                                </div>
                            ))}
                        </div>
                    )}
                </label>
                <ul className="mb-4">
                    {searchResults.map(user => (
                        <li className="mb-2" key={user.id}>
                            {user.name} <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-md" type="button" onClick={() => handleAddMember(user)}>Add member</button>
                        </li>
                    ))}
                </ul>
                <p className="block text-gray-700 text-sm font-bold mb-2">Members:</p>
                <ul className="mb-4">
                    {formState.members.map((member, index) => (
                        <li className="mb-2" key={index}>
                            {member} <button className="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-md" type="button" onClick={() => handleDeleteMember(member)}>Delete member</button>
                        </li>
                    ))}
                </ul>
                <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded focus:outline-none focus:shadow-md" type="submit">Submit</button>
            </form>
        </div>
    );
};

export default CreateTeam;