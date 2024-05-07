import { UserData, Comment } from "../../utils/Types";
import CommentWall from "./CommentWall";

type UserProfileProps = {
  userData: UserData,
  comments: Comment[],
  setComments: React.Dispatch<React.SetStateAction<Comment[]>>
};

export function UserProfile(props: UserProfileProps) {

  const accountName = process.env.REACT_APP_AZURE_ACC_NAME;
  const sasToken = process.env.REACT_APP_SAS_TOKEN;
  const containerName = process.env.REACT_APP_AZURE_CONTAINER_NAME!;

  const blobUrl = props.userData.profilePicRef
    ? `https://${accountName}.blob.core.windows.net/${containerName}/${props.userData.profilePicRef}?${sasToken}`
    : null; 

    return (
      <div className="flex flex-col md:flex-row md:space-x-8 p-4 md:p-0 h-screen md:h-5/6">
        <div className="bg-gradient-to-br from-gray-800 to-blue-900 shadow-lg rounded-lg px-8 py-12 mb-4 flex-grow md:w-1/2 md:max-w-lg">
          <h2 className="text-white font-bold text-2xl mb-6">Profile Information</h2>
          <div className="space-y-4 text-gray-300">
            {blobUrl ? (
              <img src={blobUrl} alt="Profile" className="w-48 h-48 rounded-full mx-auto border-4 border-blue-500 shadow-md" />
            ) : (
              <div className="w-24 h-24 rounded-full bg-gray-600 mx-auto animate-pulse" />
            )}
  
            <p className="text-lg font-semibold">Username: <span className="font-light italic">{props.userData.username}</span></p>
            <p className="text-lg font-semibold">First name: <span className="font-light italic">{props.userData.firstName}</span></p>
            <p className="text-lg font-semibold">Last name: <span className="font-light italic">{props.userData.lastName}</span></p>
            <p className="text-lg font-semibold">Favourite team: <span className="font-light italic">{props.userData.favouriteTeam}</span></p>
          </div>
        </div>
        <div className="bg-gradient-to-br from-gray-800 to-blue-900 shadow-lg rounded-lg px-8 py-12 flex-grow md:w-1/2 md:max-w-lg">
          <h2 className="text-white font-bold text-2xl mb-6">Comment Wall</h2>
          <CommentWall comments={props.comments} setComments={props.setComments} />
        </div>
      </div>
    );
}