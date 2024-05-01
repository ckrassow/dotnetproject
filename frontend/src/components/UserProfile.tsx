import Card from "./Card";
import { CommentList } from "../pages/Profile/CommentComponents";
import { UserData } from "../utils/Types";

type UserProfileProps = {
  userData: UserData,
};

export function UserProfile(props: UserProfileProps) {

  const accountName = process.env.REACT_APP_AZURE_ACC_NAME;
  const sasToken = process.env.REACT_APP_SAS_TOKEN;
  const containerName = process.env.REACT_APP_AZURE_CONTAINER_NAME!;

  const blobUrl = props.userData.profilePicRef
    ? `https://${accountName}.blob.core.windows.net/${containerName}/${props.userData.profilePicRef}?${sasToken}`
    : null; 

  return (
    <div className="flex justify-start space-x-4">
      <Card
        header="Profile information"
        content={
          <div className="space-y-2">
            {blobUrl ? (
              <img src={blobUrl} alt="Profile" className="w-20 h-20 rounded-full" />
            ) : (
              <div className="w-20 h-20 rounded-full bg-gray-300" />
            )}

            <p><strong>Username:</strong> {props.userData.username}</p>
            <p><strong>First name:</strong> {props.userData.firstName}</p>
            <p><strong>Last name:</strong> {props.userData.lastName}</p>
            <p><strong>Favourite team:</strong> {props.userData.favouriteTeam}</p>
          </div>
        }
      />
      <Card
        header="Comment wall"
        content={
          <div className="w-full max-w-md p-4 bg-white rounded shadow-md resize-none">
            <CommentList username={props.userData.username} />
          </div>
        }
      />
    </div>
  );
}