import { FC, ReactNode } from "react";

interface IModalProps {
    isOpen: boolean;
    onClose: () => void;
    children: ReactNode;
    className?: string;
};

const Modal: FC<IModalProps> = props => {
    if (!props.isOpen) return null;

    return (
        <div className={`fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center`}>
            <div className={`bg-white p-6 rounded-lg shadow-xl ${props.className}`}>
                <button onClick={props.onClose} className="absolute top-2 right-2 text-gray-400 hover:text-gray-500">X</button>
                {props.children}
            </div>
        </div>
    );
}

export default Modal;