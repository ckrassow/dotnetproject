import { useState } from "react";
import "../styles/Signup.css";
import Card from "../components/Card";

export function SignupPage() {

    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [repeatPassword, setRepeatPassword] = useState("");

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();

        if (password !== repeatPassword) {
            console.log("Passwords do not match");
            return;
        }

        const response = await fetch("http://localhost:5175/api/user/register", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                username,
                password,
            }),
        });

        const data = await response.json();

        if (response.ok) {
            console.log("Account created successfully");

        } else {

            console.log(data.message);
        }
    };

    const header = <h2>Sign up</h2>;
    const content = (
        <form className="form-container" onSubmit={handleSubmit}>
            <div className="form-group">
                <label className="form-label">
                    Username:
                </label>
                <input className="form-input" type="text" name="username" value={username} onChange={(e) => setUsername(e.target.value)} />
            </div>
            <div className="form-group">
                <label className="form-label">
                    Password:
                </label>
                <input className="form-input" type="password" name="password" value={password} onChange={(e) => setPassword(e.target.value)} />
            </div>
            <div className="form-group">
                <label className="form-label">
                    Repeat password:
                </label>
                <input className="form-input" type="password" name="repeatPassword" value={repeatPassword} onChange={(e) => setRepeatPassword(e.target.value)} />
            </div>
            <input className="form-button" type="submit" value="Create account" />
        </form>
    );

    return (

        <div className="signup-container">
            <Card header={header} content={content} height="500px" width="70%" maxWidth="600px" />
        </div>
    );
}
