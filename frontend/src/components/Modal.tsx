import { FC, ReactNode } from "react";

interface IModalProps {
    isOpen: boolean;
    onClose: () => void;
    children: ReactNode;
};

const Modal: FC<IModalProps> = ({ isOpen, onClose, children }) => {
    if (!isOpen) return null;

    return (
        <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center">
            <div className="bg-white p-5 rounded-lg w-11/12 max-w-6xl h-3/4 overflow-auto">
                <button onClick={onClose}>X</button>
                {children}
            </div>
        </div>
    );
}

export default Modal;