import { FC, useEffect, useState, FormEvent } from "react";

interface ICommentProps {
    author: string;
    text: string;
    timestamp: Date;
}

const Comment: FC<ICommentProps> = ({ author, text, timestamp }) => (
    <div className="bg-white p-4 rounded shadow-md mb-4">
        <h4 className="font-bold">{author}</h4>
        <p className="mt-2">{text}</p>
        <span className="text-sm text-gray-500 mt-2">{timestamp.toLocaleString()}</span>
    </div>
);

interface ICommentListProps {
    username: string | null;
}

export const CommentList: FC<ICommentListProps> = ({ username }) => {
    const [comments, setComments] = useState<ICommentProps[]>([]);
    const [text, setText] = useState("");

    const handleSubmit = (event: FormEvent) => {
        event.preventDefault();
    };

    useEffect(() => {
    }, [username]);

    return (
        <div className="overflow-auto max-h-[500px]">
            <div className="overflow-auto min-h-[200px] mb-4">
                {comments.map((comment, index) => (
                    <Comment key={index} {...comment} />
                ))}
            </div>
            {( 
                <form className="flex flex-col space-y-2" onSubmit={handleSubmit}>
                <textarea className="resize-none border rounded p-2" maxLength={150} value={text} onChange={e => setText(e.target.value)} />
                <button className="px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600" type="submit">Post Comment</button>
                </form>
            )}
        </div>
    );
};