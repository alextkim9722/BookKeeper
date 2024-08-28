import { BookOnShelf } from "./BookOnShelf"
import { useEffect, useState } from "react";
import "./Shelf.css"

export function Shelf(props)
{
    const [books, setBooks] = useState('')

    useEffect(() => {
        setBooks(props.bookIds.map((id, index) => <BookOnShelf bookId={id} key={index}/>));
    }, [books]);

    return(
        <>
            <div>
                <img src="/src/assets/shelf-head.png"></img>
            </div>
            <div id="shelf-position">
                {books}
            </div>
        </>
    )
}