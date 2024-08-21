import React, { useState, useContext } from 'react';
import { AuthContext } from '../context/AuthContext';
import Modal from './Modal'; 

const SignOut: React.FC = () => {
  const [showModal, setShowModal] = useState(false);
  const { signOut } = useContext(AuthContext); 

  const handleSignOut = () => {

    localStorage.removeItem("token");
    localStorage.removeItem("refreshToken");
    localStorage.removeItem("userId");
    localStorage.removeItem("username");
    localStorage.removeItem("teamId");
    signOut(); 
    setShowModal(false);
  };

  return (
    <>
      <button className="mx-2" onClick={() => setShowModal(true)}>
        Sign Out
      </button>
      <Modal className="bg-gray-900" isOpen={showModal} onClose={() => setShowModal(false)}>
        <div className="flex flex-col items-center space-y-4 p-8"> 
          <h2 className="text-xl font-bold">Confirm Sign Out</h2>
          <p className="text-gray-300">Are you sure you want to sign out?</p>
          <div className="flex space-x-4"> 
            <button 
              className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded"
              onClick={handleSignOut}
            >
              Yes, Sign Out
            </button>
            <button 
              className="bg-gray-500 hover:bg-gray-700 text-white font-bold py-2 px-4 rounded"
              onClick={() => setShowModal(false)}
            >
              Cancel
            </button>
          </div>
        </div>
      </Modal>
    </>
  );
};

export default SignOut;