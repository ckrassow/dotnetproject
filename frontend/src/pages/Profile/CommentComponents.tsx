import { FC, useEffect, useState, FormEvent } from "react";

interface CommentProps {
    author: string;
    text: string;
    timestamp: Date;
}

const Comment: FC<CommentProps> = ({ author, text, timestamp }) => (
    <div className="comment">
        <h4 className="comment-author">{author}</h4>
        <p className="comment-text">{text}</p>
        <span className="comment-timestamp">{timestamp.toLocaleString()}</span>
    </div>
);

interface CommentListProps {
    username: string;
}

export const CommentList: FC<CommentListProps> = ({ username }) => {
    const [comments, setComments] = useState<CommentProps[]>([]);
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
            <form className="comment-form" onSubmit={handleSubmit}>
                <textarea className="comment-form-textarea resize-none" maxLength={150} value={text} onChange={e => setText(e.target.value)} />
                <button className="comment-form-button" type="submit">Post Comment</button>
            </form>
        </div>
    );
};