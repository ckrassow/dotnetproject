import { createContext } from "react";

interface IAuthContext {
    isLoggedIn: boolean;
    setIsLoggedIn: React.Dispatch<React.SetStateAction<boolean>>;
}

export const AuthContext = createContext<IAuthContext>({
    isLoggedIn: false,
    setIsLoggedIn: () => {},
});