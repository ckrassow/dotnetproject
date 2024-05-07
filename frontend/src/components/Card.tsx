import { FC, ReactNode } from "react";

interface ICardProps {
    header: ReactNode;
    content: ReactNode;
    className?: string;
};

const Card: FC<ICardProps> = props => {
    return (
        <div className={`shadow-md rounded px-4 py-6 mb-4 flex-grow ${props.className}`}>
            <div className="font-bold text-xl mb-4">{props.header}</div>
            <div className="flex flex-col">{props.content}</div>
        </div>
    );
}

export default Card;