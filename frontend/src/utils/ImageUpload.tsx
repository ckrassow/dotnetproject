import { v4 as uuid4 } from "uuid";
import { BlobServiceClient } from "@azure/storage-blob";
import axios from "axios";
import { UserData } from "./Types";

const accountName = process.env.REACT_APP_AZURE_ACC_NAME;
const sasToken = process.env.REACT_APP_SAS_TOKEN;
const containerName = process.env.REACT_APP_AZURE_CONTAINER_NAME!;
const blobServiceClient = new BlobServiceClient(`https://${accountName}.blob.core.windows.net/?${sasToken}`);
const containerClient = blobServiceClient.getContainerClient(containerName);

export async function handleUpload(file: Blob, user: UserData): Promise<string | null> {

    const blobName = uuid4() + ".jpg";
    const token = localStorage.getItem("token");
    const userId = localStorage.getItem("userId");

    try {

        const response = await axios.put(
            `http://localhost:5175/api/user/${userId}/profile-picture`,
            { ProfilePicRef: blobName},
            {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            }
        );

        if (response.status === 204) {

            //const blobClient = containerClient.getBlockBlobClient(blobName);

            if (user.profilePicRef) {

                const delBlobClient = containerClient.getBlockBlobClient(user.profilePicRef);
                await delBlobClient.delete();
            }

            const blobClient = containerClient.getBlockBlobClient(blobName);
            await blobClient.uploadData(file);
            return blobName;

        } else {
            console.error("Error updating profile picture reference ELSE STATEMENT:", response.data);
            return null;
        }

    } catch(error) {
        console.error("Error during profile picture upload CATCH STATEMENT", error);

        try {
            await axios.delete(
                `http://localhost:5175/api/user/${userId}/profile-picture`, {
                    headers: { Authorization: `Bearer ${token}`},
                });
            return null;

        } catch(cleanUpError) {
            console.error("Error during cleanup", cleanUpError);
            return null;
        }
    }
};

export async function getImageURL(profilePicRef: string): Promise<string | undefined> {
    
    try {
        const blobClient = containerClient.getBlobClient(profilePicRef);
        const blobExists = await blobClient.exists();
        if (!blobExists) {
            return undefined;
        }

        const blobUrl = blobClient.url;
        return blobUrl;

    } catch(error) {
        console.error("Error retrieving image URL:", error);
        return undefined;
    }
}
