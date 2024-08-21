import { useNavigate } from 'react-router-dom'; 

const HomePage = () => {
  const navigate = useNavigate(); 
  const handleGetStarted = () => {
    navigate('/signin');
  };

  return (
    <div className="bg-gray-100 min-h-screen flex items-center justify-center">
      <div className="container mx-auto text-center">
        <h1 className="text-4xl font-bold text-gray-800 mb-4">Welcome to my Euro 2024 predictions website!</h1>
        <p className="text-lg text-gray-600 mb-8">
          Predict the outcome of the tournament and compete with your friends! 
        </p>
        <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded" onClick={handleGetStarted}>
          Get Started
        </button>
      </div>
    </div>
  );
};

export default HomePage;