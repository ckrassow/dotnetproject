import { FC } from "react";
import "../styles/Dropdown.css";

interface IDropdownProps {
    options: string[];
    selectedOption: string;
    setSelectedOption: (option: string) => void;
    defaultOptionLabel: string;
};

const Dropdown: FC<IDropdownProps> = props => {
    return (
        <select value={props.selectedOption} onChange={(e) => {
            if (e.target.value === props.defaultOptionLabel) {
                console.log("Hello");
                return;
            }
            props.setSelectedOption(e.target.value)}}>
            <option value="">{props.defaultOptionLabel}</option>
            {props.options.map((option, index) => (
                <option key={index} value={option}>
                    {option}
                </option>
            ))}
        </select>
    );
}

export default Dropdown;