import "../styles/Signin.css";
import Card from "../components/Card";
import { useNavigate } from "react-router-dom";

export function SigninPage() {

    const navigate = useNavigate();

    const headerSignin = <h2>Sign in</h2>;
    const contentSignin = (
        <form className="form-container">
            <label className="form-label">
                Username:
                <input className="form-input" type="text" name="username" />
            </label>
            <label className="form-label">
                Password:
                <input className="form-input" type="password" name="password" />
            </label>
            <input className="form-button" type="submit" value="Sign in" />
        </form>
    );

    const headerSignup = <h2>Not a member yet?</h2>
    const contentSignup = (
        <div className="signup-container">
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
            <Card header={headerSignin} content={contentSignin} height="300px" width="50%" maxWidth="600px" />
            <Card header={headerSignup} content={contentSignup} width="50%" maxWidth="600px" />
        </div>
    );
}
