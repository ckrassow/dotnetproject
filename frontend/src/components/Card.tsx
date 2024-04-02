import React, { FC } from "react";
import "../styles/Card.css"


interface CardProps {
    header: React.ReactNode;
    content: React.ReactNode;
    height?: number | string;
    width?: number | string;
    maxWidth?: number | string;
};

const Card: FC<CardProps> = props => {

    return (
        <div className="card-container" style={{
            height: props.height,
            width: props.width,
            maxWidth: props.maxWidth,
        }}>
            <div className="card-header">{props.header}</div>
            {props.content}
        </div>
    );
}

export default Card;