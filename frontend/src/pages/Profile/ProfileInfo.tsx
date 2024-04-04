import { useState } from "react";
import Tab from "../../components/Tab";
import Card from "../../components/Card";

const childTabs = ["Username", "Settings"];

export function ProfileInfo() {

    const [activeTab, setActiveTab] = useState(childTabs[0]);
    const dummyData = {
        username: "User",
        email: "email",
        favouriteTeam: "Germany",
        predPoints: 1000,
    };

    return (
        
        <div>
            <div className="tabs">
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

            {activeTab === "Username" && (
                <div className="profile-info-container">
                    <Card
                        header="Profile information"
                        content={
                            <div className="profile-p-div">
                                <p><strong>Username:</strong> {dummyData.username}</p>
                                <p><strong>Email:</strong> {dummyData.email}</p>
                                <p><strong>Favourite team:</strong> {dummyData.favouriteTeam}</p>
                                <p><strong>Points:</strong> {dummyData.predPoints}</p>
                            </div>
                        }
                    />
                </div>
            )}

            {activeTab === "Settings" && (
                <div className="profile-info-container">
                    <Card
                        header="Settings information"
                        content={
                            <p>Just some settings things I guess?</p>
                        }
                    />
                </div>
            )}

        </div>
    );
}
