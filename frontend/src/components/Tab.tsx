import { FC } from "react";
import "../styles/Tab.css";

interface ITabProps {
    title: string;
    isActive: boolean;
    onClick: () => void;
    isSubTab?: boolean;
};

const Tab: FC<ITabProps> = props => {

    const tabClass = props.isSubTab ? "sub-tab" : "tab";
    return (
        <button
            className={`tab ${tabClass} ${props.isActive ? "active" : ""}`}
            onClick={props.onClick}
        >
            {props.title}
        </button>
    );
}

export default Tab;