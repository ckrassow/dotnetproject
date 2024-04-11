import { FC, useEffect, useState, FormEvent } from "react";

interface ICommentProps {
    author: string;
    text: string;
    timestamp: Date;
}

const Comment: FC<ICommentProps> = ({ author, text, timestamp }) => (
    <div className="comment">
        <h4 className="comment-author">{author}</h4>
        <p className="comment-text">{text}</p>
        <span className="comment-timestamp">{timestamp.toLocaleString()}</span>
    </div>
);

interface ICommentListProps {
    username: string;
    isPublicProfile: boolean;
}

export const CommentList: FC<ICommentListProps> = ({ username, isPublicProfile }) => {
    const [comments, setComments] = useState<ICommentProps[]>([]);
    const [text, setText] = useState("");

    const handleSubmit = (event: FormEvent) => {
        event.preventDefault();
        // Post the comment to your backend
    };

    useEffect(() => {
        // Fetch the comments from your backend
    }, [username]);

    return (
        <div className="comment-list">
            <div className="comments-display">
                {comments.map((comment, index) => (
                    <Comment key={index} {...comment} />
                ))}
            </div>
            {!isPublicProfile && ( 
                <form className="comment-form" onSubmit={handleSubmit}>
                <textarea className="comment-form-textarea resize-none" maxLength={150} value={text} onChange={e => setText(e.target.value)} />
                <button className="comment-form-button" type="submit">Post Comment</button>
                </form>
            )}
        </div>
    );
};