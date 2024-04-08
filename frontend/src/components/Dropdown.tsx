import { FC } from "react";
import "../styles/Dropdown.css";

interface IDropdownProps {
    options: string[];
    selectedOption: string;
    setSelectedOption: (option: string) => void;
};

const Dropdown: FC<IDropdownProps> = ({ options, selectedOption, setSelectedOption }) => {
    return (
        <select value={selectedOption} onChange={(e) => setSelectedOption(e.target.value)}>
            {options.map((option, index) => (
                <option key={index} value={option}>
                    {option}
                </option>
            ))}
        </select>
    );
}

export default Dropdown;