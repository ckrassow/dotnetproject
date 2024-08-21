import { FC } from "react";

interface ITabProps {
  title: string;
  isActive: boolean;
  onClick: () => void;
  isSubTab?: boolean;
  className?: string; 
}

const Tab: FC<ITabProps> = (props) => {

  const tabStyles = `inline-flex items-center justify-center w-24 py-2 font-medium text-center rounded-md ${
    props.isActive ? "border-b-2 border-indigo-600 text-gray-700 bg-white" : "hover:bg-gray-50"
  } ${props.className || ''}`; 

  return (
    <button className={tabStyles} onClick={props.onClick}>
      {props.title}
    </button>
  );
};

export default Tab;