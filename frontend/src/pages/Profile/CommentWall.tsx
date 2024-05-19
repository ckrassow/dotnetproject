import { useState } from "react";
import { useParams } from "react-router-dom";
import axiosInstance from "../../utils/Api";
import {  Comment } from "../../utils/Types";
import { Link } from 'react-router-dom';


interface CommentWallProps {
    comments: Comment[];
    setComments: React.Dispatch<React.SetStateAction<Comment[]>> 
};

const CommentWall = ({ comments, setComments }: CommentWallProps) => {
  const [comment, setComment] = useState("");
  const { username } = useParams();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
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
      setComments([response.data, ...comments]);

    } catch(error) {
      console.error("Error submitting comment:", error);
    }
  };

  return (
    <>
      <div className="space-y-4 overflow-y-auto max-h-96 p-2 min-h-[400px]">
      {comments.length === 0 ? (
          <div className="bg-gray-800 text-gray-200 p-4 rounded-md shadow-md mb-2">
            <p className="text-gray-400">No comments yet. Be the first to comment!</p> 
          </div>
        ) : (
          comments.map((commentData: Comment, index: number) => (
            <div key={index} className="bg-gray-800 text-gray-200 p-4 rounded-md shadow-md mb-2">
              <p className="font-bold">
                <Link to={`/user/${commentData.author}`} className="text-blue-400 hover:text-blue-300">
                  {commentData.author}
                </Link>
                <span className="text-gray-400 text-sm">
                  {new Date(commentData.timestamp).toISOString().slice(0, 16).replace("T", " ")}:
                </span>
              </p>
              <p className="text-gray-300">{commentData.comment}</p>
            </div>
          )))}
      </div>
      <form onSubmit={handleSubmit} className="flex flex-col items-stretch mt-4">
      <textarea
        value={comment}
        onChange={(e) => setComment(e.target.value)}
        placeholder="Write a comment..."
        className="w-full h-24 px-3 py-2 bg-gray-900 text-gray-200 border border-gray-700 rounded-md shadow-md resize-none focus-within:ring-2 focus-within:ring-blue-500 focus-within:ring-offset-2"
        rows={4}
      />
        <button type="submit" className="px-4 py-2 mt-2 bg-blue-600 hover:bg-blue-700 text-gray-100 font-bold rounded-md shadow-md">
         Post
        </button>
      </form>
    </>
  );
};

export default CommentWall;