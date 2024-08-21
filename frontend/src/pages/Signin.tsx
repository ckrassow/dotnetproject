import Card from "../components/Card";
import { useNavigate } from "react-router-dom";
import { useState, useContext, useRef, useEffect } from "react";
import { AuthContext } from "../context/AuthContext";
import axiosInstance from "../utils/Api";
export function SigninPage() {

    const navigate = useNavigate();

    const { setIsLoggedIn } = useContext(AuthContext);
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [showFeedback, setShowFeedback] = useState(false);
    const [feedbackMessage, setFeedbackMessage] = useState("");
    const feedbackRef = useRef<HTMLDivElement>(null);

    const handleSignIn = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            const response = await axiosInstance.post("/user/login", { username, password });
            const data = response.data;
            localStorage.setItem("token", data.token);
            localStorage.setItem("refreshToken", data.refreshToken);
            localStorage.setItem("userId", data.userId);
            localStorage.setItem("username", data.username);
            setIsLoggedIn(true);
            navigate(`/user/${data.username}`);
        } catch (error: any) {
            if (error.response?.status === 401) {
                setFeedbackMessage("Wrong password or username");
            } else {
                setFeedbackMessage("Something went wrong, try again");
            }
            setShowFeedback(true);
        };
    };

    useEffect(() => {
        if (showFeedback) {
            const timeoutId = setTimeout(() => {
                setShowFeedback(false);
            }, 3000);

            return () => clearTimeout(timeoutId);
        }
    }, [showFeedback]);

    const headerSignin = <h2 className="text-3xl font-bold text-white mb-4">Welcome Back!</h2>;
    const contentSignin = (
        <form className="space-y-4" onSubmit={handleSignIn}>
            <label className="block">
                <span className="text-white text-lg font-medium">Username:</span>
                <input className="border-2 mt-1 block w-full px-3 py-2 placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm" type="text" name="username" value={username} onChange={(e) => setUsername(e.target.value)}/>
            </label>
            <label className="block">
                <span className="text-white text-lg font-medium">Password:</span>
                <input className="border-2 mt-1 block w-full px-3 py-2 placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm" type="password" name="password" value={password} onChange={(e) => setPassword(e.target.value)}/>
            </label>
            <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-md w-full transition duration-300 ease-in-out transform">
                Sign in
            </button>
        </form>
    );

    const headerSignup = <h2 className="text-3xl font-bold text-white mb-4">New to the Game?</h2>
    const contentSignup = (
        <div className="flex flex-col items-center justify-center">
            <p className="text-white text-lg">Join our community and start making predictions for the Euro 2024 tournament! You can:</p>
            <ul className="text-white text-lg mt-4 list-disc ml-8">
                <li>Showcase your football knowledge!</li>
                <li>Compete with friends!</li>
                <li>Test your knowledge with quizzes!</li>
            </ul>
            <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded-md w-full transition duration-300 ease-in-out transform mt-4" onClick={() => navigate("/signup")}>
                Sign up now!
            </button>
        </div>
    );

    return (

        <div className="flex flex-col items-center justify-center h-screen bg-gray-100">
            <div className="container mx-auto p-4">
                <Card header={headerSignin} content={contentSignin} className="bg-gray-800 rounded-lg shadow-md p-8" />
                <Card header={headerSignup} content={contentSignup} className="bg-gray-800 rounded-lg shadow-md p-8 mt-4" />
                {showFeedback && (
                    <div ref={feedbackRef} className="absolute bottom-10 left-1/2 transform -translate-x-1/2 bg-red-500 text-white px-4 py-2 rounded-md">
                        {feedbackMessage}
                    </div>
        )}
            </div>
        </div>
    );
}