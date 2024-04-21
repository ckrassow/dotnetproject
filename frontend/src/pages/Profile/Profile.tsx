import { useState, useEffect } from "react";
import { useParams } from "react-router-dom";
import axios from "axios";
import Tab from "../../components/Tab";
import "../../styles/Profile.css";
import { Predictions } from "./Predictions";
import { ProfileInfo, UserData } from "./ProfileInfo";


const parentTabs = ["Profile", "Predictions", "League"];


export function ProfilePage() {
    const { username: viewedUsername } = useParams();
    const [activeParentTab, setActiveParentTab] = useState(parentTabs[0]);
    const isPublicProfile = viewedUsername !== undefined;

    const [userData, setUserData] = useState<UserData>({} as UserData);

    useEffect(() => {
        const token = localStorage.getItem("token");
        const userId = localStorage.getItem("userId");
        axios.get(`http://localhost:5175/api/user/${userId}`, {
            headers: {
                "Authorization": `Bearer ${token}`
            }
        })
        .then(response => {
            const data = response.data;
            setUserData({
                Id: data.id,
                Username: data.username,
                FirstName: data.firstName,
                LastName: data.lastName,
                FavouriteTeam: data.favouriteTeam,
                PlayerPredictions: data.playerPredictions,
                TeamPredictions: data.teamPredictions,
                TournamentPredictions: data.tournamentPredictions,
                TeamId: data.teamId,
                Team: data.team,
                Token: token,
            });
        })
        .catch(error => {
            console.error("Error fetching user data", error);
        });
    }, []);

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
            <ProfileInfo isPublicProfile={isPublicProfile} userData={userData} /> 
            )}
        </div>
    );
}
