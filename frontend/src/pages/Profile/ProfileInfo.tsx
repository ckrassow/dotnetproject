import { useState } from "react";
import Tab from "../../components/Tab";
import { UserData } from "../../utils/Types";

const childTabs = ["Username", "Settings"];

export function ProfileInfo({  }: { userData: UserData }) {

    
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
