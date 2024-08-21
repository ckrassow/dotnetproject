import { FC } from "react";

interface IDropdownProps {
    options: string[];
    selectedOption: string;
    setSelectedOption: (option: string) => void;
    defaultOptionLabel: string;
    className?: string; 
};

const Dropdown: FC<IDropdownProps> = props => {
    return (
        <select 
            value={props.selectedOption} 
            onChange={(e) => {
                if (e.target.value === props.defaultOptionLabel) {
                    return;
                }
                props.setSelectedOption(e.target.value)
            }}
            className={`form-select ${props.className}`} 
        >
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