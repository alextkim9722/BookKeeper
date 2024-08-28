import { useState } from 'react'

export function BookDetailed(props)
{
    const[title, setTitle] = useState(props.book.title);
    const[pages, setPages] = useState(props.book.pages);
    const[isbn, setIsbn] = useState(props.book.isbn);
    const[cover, setCover] = useState(props.book.cover_picture);

    return(
        <>
        <div id='detailed-window'>
            <></>
        </div>
        </>
    )
}