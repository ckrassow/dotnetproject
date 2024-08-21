import { useState } from "react";
import Card from "../components/Card";
import axiosInstance from "../utils/Api";
import { useNavigate } from "react-router-dom";

export function SignupPage() {
  const navigate = useNavigate();
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [repeatPassword, setRepeatPassword] = useState("");
  const [showSuccessMessage, setShowSuccessMessage] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (password !== repeatPassword) {
      return;
    }

    try {
      const response = await axiosInstance.post("/user/register", {
        username,
        password,
      });
      if (response.status === 201) {
        setShowSuccessMessage(true);
        setTimeout(() => {
          navigate("/signin");
        }, 3000); 
      } else {
        console.log("User failed to sign up");
      }
    } catch (error) {
      console.error("Error signing in:", error);
    }
  };

  const header = (
    <h2 className="text-3xl font-bold text-white mb-4">Join the Fun!</h2>
  );
  const content = (
    <form className="space-y-6" onSubmit={handleSubmit}>
      <div className="flex justify-between items-center">
        <label className="block pr-2 text-white text-lg font-medium">
          Username:
        </label>
        <input
          autoComplete="on"
          className="sm:w-64 border-2 mt-1 block w-full px-3 py-2 placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
          type="text"
          name="username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
      </div>
      <div className="flex justify-between items-center">
        <label className="block pr-2 text-white text-lg font-medium">
          Password:
        </label>
        <input
          autoComplete="on"
          className="sm:w-64 border-2 mt-1 block w-full px-3 py-2 placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
          type="password"
          name="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </div>
      <div className="flex justify-between items-center">
        <label className="block pr-2 text-white text-lg font-medium">
          Repeat password:
        </label>
        <input
          autoComplete="on"
          className="sm:w-64 border-2 mt-1 block w-full px-3 py-2 placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm"
          type="password"
          name="repeatPassword"
          value={repeatPassword}
          onChange={(e) => setRepeatPassword(e.target.value)}
        />
      </div>
      <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-3 px-6 rounded-md w-full transition duration-300 ease-in-out transform hover:scale-105">
        Create Account
      </button>
      {showSuccessMessage && (
        <p className="text-white mt-2">
          Account successfully created, taking you to signin page...
        </p>
      )}
    </form>
  );

  return (
    <div className="flex flex-col items-center justify-start h-screen bg-gray-100">
      <div className="container mx-auto p-4 mt-20 flex flex-col items-center">
        {" "}
        <Card
          header={header}
          content={content}
          className="bg-gray-800 rounded-lg shadow-2xl p-10 max-w-md"
        />
      </div>
    </div>
  );
}