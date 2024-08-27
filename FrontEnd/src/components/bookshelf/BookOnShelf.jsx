import { useEffect, useState } from "react";
import axios from "axios";
import "./BookOnShelf.css"

export function BookOnShelf(props)
{
    const [error, setError] = useState("");
    const [success, setSuccess] = useState(false);
    const [title, setTitle] = useState("");
    const [coverPicture, setCoverPicture] = useState("");
    
    useEffect(() => {
        axios.get(`https://localhost:7213/api/Book/GetBookById/${props.bookId}`)
        .then(response => {
            if(response.data.success)
            {
                let body = response.data.payload;
                setTitle(body.title);
                setCoverPicture(body.cover_picture);
                setSuccess(true);
            }else{
                setError(response.data.msg)
            }
        })
        .catch(err => console.error(err))
    }, []);

    return(
        <>
        <div>
            {
                success ?
                (
                    <div id="book-position">
                        <img id="book-cover" src={coverPicture}></img>
                        <h6 id="book-title">{title}</h6>
                    </div>
                )
                :
                (
                    <h1>{error}</h1>
                )
            }
        </div>
        </>
    );
}