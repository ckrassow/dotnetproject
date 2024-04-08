import { useState } from "react";
import Tab from "../../components/Tab";
import "../../styles/Profile.css";
import { Predictions } from "./Predictions";
import { ProfileInfo } from "./ProfileInfo";


const parentTabs = ["Profile", "Predictions", "League"];


export function ProfilePage() {

    const [activeParentTab, setActiveParentTab] = useState(parentTabs[0]);

    return (

        <div className="profile-container">
            <div className="tabs">
                {parentTabs.map(tab => (
                    <Tab
                        key={tab}
                        title={tab}
                        isActive={activeParentTab === tab}
                        onClick={() => setActiveParentTab(tab)}
                    />
                ))}
            </div>

            {activeParentTab === "Predictions" && <Predictions />}        
            {activeParentTab === "Profile" && <ProfileInfo />}
        </div>
    );
}
