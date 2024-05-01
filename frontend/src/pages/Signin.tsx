import Card from "../components/Card";
import { useNavigate } from "react-router-dom";
import { useState, useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import axios from "axios";

export function SigninPage() {

    const navigate = useNavigate();

    const { setIsLoggedIn } = useContext(AuthContext);
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");

    const handleSignIn = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            console.log(username, password);
            const response = await axios.post("http://localhost:5175/api/user/login", { username, password });
            const data = response.data;
            const token = data.token;
            const refreshToken = data.refreshToken;
            const id = data.userId;
            console.log(response.data);
            localStorage.setItem("token", token);
            localStorage.setItem("refreshToken", refreshToken);
            localStorage.setItem("userId", id);
            setIsLoggedIn(true);
            navigate("/profile");
        } catch (error) {
            console.error("Error signing in", error);
        }
    };

    const headerSignin = <h2>Sign in</h2>;
    const contentSignin = (
        <form className="space-y-4" onSubmit={handleSignIn}>
            <label className="block">
                Username:
                <input className="border-2 mt-1 block w-full px-3 py-2 placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm" type="text" name="username" value={username} onChange={(e) => setUsername(e.target.value)}/>
            </label>
            <label className="block">
                Password:
                <input className="border-2 mt-1 block w-full px-3 py-2 placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm" type="password" name="password" value={password} onChange={(e) => setPassword(e.target.value)}/>
            </label>
            <input className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded" type="submit" value="Sign in" />
        </form>
    );

    const headerSignup = <h2>Not a member yet?</h2>
    const contentSignup = (
        <div className="flex flex-col items-center justify-center">
            <p>With a WhateverThisWebsiteWillBeCalled account you can:<br />
               - Showcase your football knowledge by participating in the
               predictions for the Euro 2024 tournament. <br />
               - Create leagues and compare your predictions to your friends and family. <br />
               - Perhaps do some quizzes or whatever I add if this stuff isn't enough stuff.
            </p>
            <button className="form-button" onClick={() => navigate("/signup")}>
                Sign up now!
            </button>
        </div>
    );

    return (

        <div className="flex flex-col items-center justify-center h-screen">
            <Card header={headerSignin} content={contentSignin} maxWidth="600px" />
            <Card header={headerSignup} content={contentSignup} maxWidth="600px" />
        </div>
    );
}
