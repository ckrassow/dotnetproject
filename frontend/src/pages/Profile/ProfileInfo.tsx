import { useState, useContext } from "react";
import Tab from "../../components/Tab";
import { UserData } from "../../utils/Types";
import { AuthContext } from "../../context/AuthContext";
import { UserProfile } from "../../components/UserProfile";

const childTabs = ["Username", "Settings"];

export function ProfileInfo({ userData }: { userData: UserData }) {

    
    const { isLoggedIn } = useContext(AuthContext);
    const [activeTab, setActiveTab] = useState(childTabs[0]);
    
    return (
        
        <div>
            <div className="flex justify-start space-x-4 mb-4">
                {childTabs.map(tab => (
                    <Tab
                        key={tab}
                        title={tab}
                        isActive={activeTab === tab}
                        onClick={() => setActiveTab(tab)}
                        isSubTab={true}
                    />
                ))}
            </div>

        </div>
    );
}
