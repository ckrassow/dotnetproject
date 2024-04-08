import { FC, ReactNode } from "react";
import "../styles/Modal.css";

interface IModalProps {
    isOpen: boolean;
    onClose: () => void;
    children: ReactNode;
};

const Modal: FC<IModalProps> = ({ isOpen, onClose, children }) => {
    if (!isOpen) return null;

    return (
        <div className="modal-backdrop">
            <div className="modal-content">
                <button onClick={onClose}>X</button>
                {children}
            </div>
        </div>
    );
}

export default Modal;