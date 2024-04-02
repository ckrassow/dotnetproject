import "../styles/Signin.css"
import Card from "../components/Card"

export function SigninPage() {

    const header = <h2>Sign in</h2>;
    const content = (
        <form className="signin-form">
            <label className="signin-label">
                Username:
                <input className="signin-input" type="text" name="username" />
            </label>
            <label className="signin-label">
                Password:
                <input className="signin-input" type="password" name="password" />
            </label>
            <input className="signin-submit" type="submit" value="Sign in" />
        </form>
    );

    return (

        <div className="signin-container">
            <Card header={header} content={content} />
        </div>
    );
}
