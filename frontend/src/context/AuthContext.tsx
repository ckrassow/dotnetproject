import { FC, useState, createContext, ReactNode } from "react";

interface IAuthContext {
    isLoggedIn: boolean;
    setIsLoggedIn: React.Dispatch<React.SetStateAction<boolean>>;
    signOut: () => void;
}

export const AuthContext = createContext<IAuthContext>({
    isLoggedIn: false,
    setIsLoggedIn: () => {},
    signOut: () => {},
});

const AuthProvider: FC<{ children: ReactNode }> = ({ children }) => {
    const [isLoggedIn, setIsLoggedIn] = useState(
        () => !!(localStorage.getItem("token") && localStorage.getItem("userId")) 
      );

    const signOut = () => {
        localStorage.removeItem("token");
        localStorage.removeItem("userId");
        setIsLoggedIn(false); 
    };

    const value = { isLoggedIn, setIsLoggedIn, signOut };

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    );
};

export default AuthProvider;