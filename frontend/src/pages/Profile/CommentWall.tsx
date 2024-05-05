import { useState } from "react";
import { useParams } from "react-router-dom";
import axiosInstance from "../../utils/Api";
import {  Comment } from "../../utils/Types";

interface CommentWallProps {
    comments: Comment[];
    setComments: React.Dispatch<React.SetStateAction<Comment[]>> 
};

const CommentWall = ({ comments, setComments }: CommentWallProps) => {
  const [comment, setComment] = useState("");
  const { username } = useParams();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    const oldComments = [...comments];
    
    try {
      const authorId = localStorage.getItem("userId");
      const token = localStorage.getItem("token");
      const response = await axiosInstance.post(
        `/user/addcomment`,
        {
          recipient: username,
          authorId: authorId,
          text: comment
        },
        {
          headers: {
            Authorization: `Bearer ${token}`
          }
        }
      );
      
      setComments([...comments, response.data]);


    } catch(error) {
      console.error("Error submitting comment:", error);
    }
  };

  return (
    <div className="space-y-4">
      {comments.map((commentData: Comment, index: number) => (
        <div key={index} className="bg-gray-100 p-4 rounded-md">
          <p className="font-bold">{commentData.author}:</p>
          <p>{commentData.comment}</p>
        </div>
      ))}

      <form onSubmit={handleSubmit} className="flex items-center">
        <input
          type="text"
          value={comment}
          onChange={(e) => setComment(e.target.value)}
          placeholder="Write a comment..."
          className="w-full px-3 py-2 border rounded-md shadow-sm"
        />
        <button type="submit" className="px-4 py-2 bg-blue-500 text-white rounded-md">
          Post
        </button>
      </form>
    </div>
  );
};

export default CommentWall;