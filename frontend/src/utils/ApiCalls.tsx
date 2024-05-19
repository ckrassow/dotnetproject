import axiosInstance from "./Api";

export const fetchUserData = async (username: string) => {
    try {
      const response = await axiosInstance.get(`user/${username}`);
      return response.data; 
    } catch (error) {
      console.error("Error fetching user", error);
      throw error; 
    }
};

export const fetchUserComments = async (username: string) => {
  try {
    const response = await axiosInstance.get(`/user/${username}/comments`);
    return response.data;
  } catch(error) {
    console.error("Error fetching user comments", error);
    throw error;
  }
}

export const fetchTeamData = async () => {
    try {
      const token = localStorage.getItem("token");
      const response = await axiosInstance.get("/nationalteam", {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      });
      return response.data; 
    } catch (error) {
      console.error("Error when trying to fetch teams", error);
      throw error; 
    }
};

export const fetchPlayerPredictions = async (username: string) => {
    try {
      const response = await axiosInstance.get(`/user/${username}/playerpredictions`);
      return response.data; 
    } catch (error) {
      console.error("Error fetching player predictions", error);
      throw error; 
    }
};

export const fetchTeamPredictions = async (username: string) => {
    try {
      const response = await axiosInstance.get(`/user/${username}/teampredictions`);
      return response.data; 
    } catch (error) {
      console.error("Error fetching player predictions", error);
      throw error; 
    }
};

export const fetchTournamentPredictions = async (username: string) => {
    try {
      const response = await axiosInstance.get(`/user/${username}/tournamentpredictions`);
      return response.data; 
    } catch (error) {
      console.error("Error fetching player predictions", error);
      throw error; 
    }
};