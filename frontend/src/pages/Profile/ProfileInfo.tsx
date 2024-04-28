import { useState, useContext } from "react";
import Tab from "../../components/Tab";
import Card from "../../components/Card";
import { CommentList } from "./CommentComponents";
import { UserData } from "../../utils/Types";
import { AuthContext } from "../../context/AuthContext";

const childTabs = ["Username", "Settings"];

export function ProfileInfo({ userData }: { userData: UserData }) {

    
    const { isLoggedIn } = useContext(AuthContext);
    const [activeTab, setActiveTab] = useState(childTabs[0]);
    
    return (
        
        <div>
            <div className="sub-tabs">
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
                                <p><strong>Username:</strong> {userData?.Username}</p>
                                <p><strong>First name:</strong> {userData?.FirstName}</p>
                                <p><strong>Last name:</strong> {userData?.LastName}</p>
                                <p><strong>Favourite team:</strong> {userData?.FavouriteTeam}</p>
                            </div>
                        }
                    />
                    <Card
                        header="Comment wall"
                        content={
                            <div className="comment-wall">
                                <CommentList username={userData?.Username} />
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
