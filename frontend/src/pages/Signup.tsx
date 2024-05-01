import { useState } from "react";
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
        <form className="space-y-4" onSubmit={handleSubmit}>
            <div className="flex justify-between items-center mb-4">
                <label className="block pr-2">
                    Username:
                </label>
                <input className="sm:w-72 border-2 mt-1 block w-full px-3 py-2 placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm" type="text" name="username" value={username} onChange={(e) => setUsername(e.target.value)} />
            </div>
            <div className="flex justify-between items-center mb-4">
                <label className="block pr-2">
                    Password:
                </label>
                <input className="sm:w-72 border-2 mt-1 block w-full px-3 py-2 placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm" type="password" name="password" value={password} onChange={(e) => setPassword(e.target.value)} />
            </div>
            <div className="flex justify-between items-center mb-4">
                <label className="block pr-2">
                    Repeat password:
                </label>
                <input className="sm:w-72 border-2 mt-1 block w-full px-3 py-2 placeholder-gray-500 text-gray-900 rounded-md focus:outline-none focus:ring-indigo-500 focus:border-indigo-500 sm:text-sm" type="password" name="repeatPassword" value={repeatPassword} onChange={(e) => setRepeatPassword(e.target.value)} />
            </div>
            <input className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded" type="submit" value="Create account" />
        </form>
    );

    return (

        <div className="flex flex-col items-center justify-center h-screen">
            <Card header={header} content={content} height="500px" width="70%" maxWidth="600px" />
        </div>
    );
}
