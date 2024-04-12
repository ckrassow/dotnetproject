import { FC, useState } from "react";
import "../../styles/CreateTeam.css";

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
        console.log("handleSearchSubmit");
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
        console.log(formState);
    };
    
    return (
        <div className="create-team-wrapper">
            <form className="create-team-form">
                <label className="label-create-team">
                    Team name:
                    <input className="input-create-team" type="text" name="name" value={formState.name} onChange={handleInputChange} />
                </label>
                <label className="label-create-team">
                    Search for user to add as member:
                    <input className="input-create-team" type="text" value={searchTerm} onChange={handleSearchChange}
                    onKeyDown={e => e.key === "Enter" && handleSearchSubmit(e)} />
                    <button className="search-user-button" onClick={handleSearchSubmit}>Search</button>
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
                <ul className="ul-create-team">
                    {searchResults.map(user => (
                        <li className="li-create-team" key={user.id}>
                            {user.name} <button className="add-member-button" type="button" onClick={() => handleAddMember(user)}>Add member</button>
                        </li>
                    ))}
                </ul>
                <p className="p-create-team">Members:</p>
                <ul className="ul-create-team">
                    {formState.members.map((member, index) => (
                        <li className="li-create-team" key={index}>
                            {member} <button className="delete-member-button" type="button" onClick={() => handleDeleteMember(member)}>Delete member</button>
                        </li>
                    ))}
                </ul>
                <button className="submit-form-button" type="submit">Submit</button>
            </form>
        </div>
    );
};

export default CreateTeam;