import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import Tab from "../../components/Tab";
import "../../styles/Profile.css";
import { Predictions } from "./Predictions";
import { ProfileInfo } from "./ProfileInfo";


const parentTabs = ["Profile", "Predictions", "League"];


export function ProfilePage() {
    const { username: viewedUsername } = useParams();
    const [activeParentTab, setActiveParentTab] = useState(parentTabs[0]);
    const isPublicProfile = viewedUsername !== undefined;

    return (

        <div className={`profile-container ${isPublicProfile ? "public-profile-container" : ""}`}>
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

            {activeParentTab === "Predictions" && (
            <Predictions isPublicProfile={isPublicProfile} />
            )}        
            {activeParentTab === "Profile" && (
            <ProfileInfo isPublicProfile={isPublicProfile} /> 
            )}
        </div>
    );
}
