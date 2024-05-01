import React, { FC } from "react";

interface ICardProps {
    header: React.ReactNode;
    content: React.ReactNode;
    height?: number | string;
    width?: number | string;
    maxWidth?: number | string;
};

const Card: FC<ICardProps> = props => {

    return (
        <div className="bg-white shadow-md rounded px-8 pt-6 pb-8 mb-4" style={{
            height: props.height,
            width: props.width,
            maxWidth: props.maxWidth,
        }}>
            <div className="font-bold text-xl mb-4">{props.header}</div>
            {props.content}
        </div>
    );
}

export default Card;