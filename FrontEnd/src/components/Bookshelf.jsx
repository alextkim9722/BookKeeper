import { Shelf } from './Shelf';
import { useEffect, useState } from 'react'
import './Bookshelf.css';

export function Bookshelf(props)
{
    const [books, setBooks] = useState([])

    useEffect(() => {
        setBooks(props.books)
    }, [props.books]);

    return(
        <>
            <div id="bookshelf-root">
                <Shelf books={books.slice(0, 5)}/>
            </div>
        </>
    )
}