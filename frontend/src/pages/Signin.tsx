import "../styles/Signin.css";
import Card from "../components/Card";
import { useNavigate } from "react-router-dom";
import { useState, useContext } from "react";
import { AuthContext } from "../context/AuthContext";
import axios, { AxiosError } from "axios";

export function SigninPage() {

    const navigate = useNavigate();

    const { setIsLoggedIn } = useContext(AuthContext);
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");

    const handleSignIn = async (e: React.FormEvent) => {
        e.preventDefault();

        try {
            console.log(username, password);
            const response = await axios.post("http://localhost:5175/api/user/login", { username, password });
            
            const { token, refreshToken, id } = response.data;
            console.log(response.data);
            localStorage.setItem("token", token);
            localStorage.setItem("refreshToken", refreshToken);
            localStorage.setItem("userId", id)
            setIsLoggedIn(true);
            //navigate("/profile");
        } catch (error) {
            console.error("Error signing in", error);
        }
    };

    const headerSignin = <h2>Sign in</h2>;
    const contentSignin = (
        <form className="form-container-signin" onSubmit={handleSignIn}>
            <label className="form-label-signin">
                Username:
                <input className="form-input-signin" type="text" name="username" value={username} onChange={(e) => setUsername(e.target.value)}/>
            </label>
            <label className="form-label-signin">
                Password:
                <input className="form-input-signin" type="password" name="password" value={password} onChange={(e) => setPassword(e.target.value)}/>
            </label>
            <input className="form-button-signin" type="submit" value="Sign in" />
        </form>
    );

    const headerSignup = <h2>Not a member yet?</h2>
    const contentSignup = (
        <div className="signup-container-1">
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

        <div className="signin-container">
            <Card header={headerSignin} content={contentSignin} maxWidth="600px" />
            <Card header={headerSignup} content={contentSignup} maxWidth="600px" />
        </div>
    );
}
