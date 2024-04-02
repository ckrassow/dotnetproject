import "../styles/Signup.css";
import Card from "../components/Card";

export function SignupPage() {

    const header = <h2>Sign up</h2>;
    const content = (
        <form className="form-container">
            <div className="form-group">
                <label className="form-label">
                    Username:
                </label>
                <input className="form-input" type="text" name="username" />
            </div>
            <div className="form-group">
                <label className="form-label">
                    Password:
                </label>
                <input className="form-input" type="password" name="password" />
            </div>
            <div className="form-group">
                <label className="form-label">
                    Repeat password:
                </label>
                <input className="form-input" type="password" name="repeatPassword" />
            </div>
            <div className="form-group">
                <label className="form-label">
                    Email:
                </label>
                <input className="form-input" type="email" name="email" />
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
