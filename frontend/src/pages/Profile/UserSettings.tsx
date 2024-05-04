import { useState, useRef, useEffect } from "react";
import Compressor from "compressorjs";
import Tab from "../../components/Tab"; 
import Dropdown from "../../components/Dropdown";
import { NationalTeamData, UserData } from "../../utils/Types";
import { handleUpload } from "../../utils/imageUpload";
import axiosInstance from "../../utils/Api";

type UserSettingsProps = {
    userData: UserData,
    setUserData: React.Dispatch<React.SetStateAction<UserData>>,
    teamData: NationalTeamData[],
};

export function UserSettings(props: UserSettingsProps) {
  const [activeTab, setActiveTab] = useState("profile");
  const [selectedTeam, setSelectedTeam] = useState("");
  const imageFileRef = useRef<Blob | null>(null);
  const [firstName, setFirstName] = useState(props.userData.firstName || "");
  const [lastName, setLastName] = useState(props.userData.lastName || "");
  const [oldPassword, setOldPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [passwordError, setPasswordError] = useState("");
  const [successMessage, setSuccessMessage] = useState("");

  useEffect(() => {

    let timeout: NodeJS.Timeout | null = null;
    if (successMessage) {
      timeout = setTimeout(() => setSuccessMessage(""), 3000);
    }
    return () => clearTimeout(timeout!);
  }, [successMessage]);

  const handleImageUpload = async (e: React.ChangeEvent<HTMLInputElement>) => {
    const image = e.target.files?.[0];
    if (image) {
      try {
        new Compressor(image, {
          quality: 0.8,
          maxWidth: 200,
          maxHeight: 200,
          success(compressedResult) {
            imageFileRef.current = compressedResult;
          },
          error(error) {
            console.error("Error compressing image:", error);
          },
        });
        
      } catch(error) {
        console.error("Error handling image upload:", error);
      }
    }
  };

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    try {

      const formData = {
        firstName: firstName,
        lastName: lastName,
        favoriteTeam: selectedTeam
      };

      props.setUserData((prevUserData) => ({
        ...prevUserData,
        firstName: formData.firstName,
        lastName: formData.lastName,
        favouriteTeam: formData.favoriteTeam
      }))

      const token = localStorage.getItem("token");
      const userId = localStorage.getItem("userId");
      await axiosInstance.put(
        `/user/${userId}/profile`, formData, {
          headers: {
            Authorization: `Bearer ${token}`
          },
        });
        
      setFirstName("");
      setLastName("");
      setSuccessMessage("Profile updated successfully!");

    } catch(error) {
      console.error("Error updating form data:", error);
    }

    const imageFile = imageFileRef.current;
    if (imageFile) {
      try {
        
        const blobName = await handleUpload(imageFile, props.userData);
        console.log(blobName);
        props.setUserData((prevUserData) => ({
          ...prevUserData,
          ...(blobName && { profilePicRef: blobName}),
        }));
        console.log(props.userData);
        
      } catch(error) {
        console.error("Error handling image upload:", error);
      }
    }

  };

  const handlePasswordChange = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (newPassword !== confirmPassword) {
      setPasswordError("Password must be at least 8 characters long.");
      return;
    }

    if (newPassword.length < 8) {
      setPasswordError("New password and confirm password do not match.");
      return;
    }

    try {
      const token = localStorage.getItem("token");
      const userId = localStorage.getItem("userId");
      await axiosInstance.put(
        `/user/${userId}/change-password`,
        { oldPassword, newPassword },
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      setOldPassword("");
      setNewPassword("");
      setConfirmPassword("");
      setPasswordError("");
      setSuccessMessage("Password changed successfully!");

    } catch(error) {
      console.error("Error changing password:", error);
    }
  }

  return (
    <div className="bg-gray-800 text-white p-8">
      <div className="tabs-container flex space-x-4 mb-6">
        <Tab
          title="Profile Settings"
          isActive={activeTab === "profile"}
          onClick={() => setActiveTab("profile")}
        />
        <Tab
          title="Change Password"
          isActive={activeTab === "password"}
          onClick={() => setActiveTab("password")}
        />
      </div>
      <div className="tab-content-container min-h-[300px] md:min-h-[400px]">
        {successMessage && ( 
            <div className="bg-green-500 text-white p-4 rounded-lg mt-4">
              {successMessage}
            </div>
          )}

        {activeTab === "profile" && (
          <div className="bg-gray-900 p-6 rounded-lg shadow-lg w-full max-w-md">
            <form onSubmit={handleSubmit}>
              <label className="block mb-4">
                First name:
                <input
                  type="text"
                  className="form-input mt-1 block w-full text-black"
                  placeholder={props.userData.firstName ? props.userData.firstName : "First name"}
                  value={firstName}
                  onChange={(e) => setFirstName(e.target.value)}
                />
              </label>
              <label className="block mb-4">
                Last name:
                <input
                  type="text"
                  className="form-input mt-1 block w-full text-black"
                  placeholder={props.userData.lastName ? props.userData.lastName : "Last name"}
                  value={lastName}
                  onChange={(e) => setLastName(e.target.value)}
                />
              </label>
              <label className="block mb-4">
                Favourite team:
                <Dropdown
                  options={props.teamData.map((team) => team.name)}
                  selectedOption={selectedTeam}
                  setSelectedOption={setSelectedTeam}
                  defaultOptionLabel="Select a team"
                />
              </label>
              <label className="block mb-4">
                Profile picture:
                <input
                  type="file"
                  accept="image/png, image/jpeg"
                  onChange={handleImageUpload}
                  className="form-input mt-1 block w-full"
                />
              </label>
              <button
                  type="submit"
                  className="bg-blue-500 text-white px-4 py-2 rounded"
              >
                  Save changes
              </button>
            </form>
          </div>
        )}

        {activeTab === "password" && (
          <div className="bg-gray-900 p-6 rounded-lg shadow-lg w-full max-w-md">
            <form onSubmit={handlePasswordChange}>
              <label className="block mb-4">
                Old password:
                <input
                  type="password"
                  className="form-input mt-1 block w-full text-black"
                  value={oldPassword}
                  onChange={(e) => setOldPassword(e.target.value)}
                />
              </label>
              <label className="block mb-4">
                New password:
                <input
                  type="password"
                  className="form-input mt-1 block w-full text-black"
                  value={newPassword}
                  onChange={(e) => setNewPassword(e.target.value)}
                />
              </label>
              <label className="block mb-4">
                Confirm new password:
                <input
                  type="password"
                  className="form-input mt-1 block w-full text-black"
                  value={confirmPassword}
                  onChange={(e) => setConfirmPassword(e.target.value)}
                />
              </label>
              {passwordError && (
                <p className="text-red-500 text-sm">{passwordError}</p>
              )}
              <button
                type="submit"
                className="bg-blue-500 text-white px-4 py-2 rounded"
              >
                Change Password
              </button>
            </form>
          </div>
        )}
      </div>
    </div>
    
  );
}