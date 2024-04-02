import React, { FC } from "react";
import "../styles/Card.css"


interface CardProps {
    header: React.ReactNode;
    content: React.ReactNode;
};

const Card: FC<CardProps> = props => {

    return (
        <div className="card-container">
            <div className="card-header">{props.header}</div>
            {props.content}
        </div>
    );
}

export default Card;