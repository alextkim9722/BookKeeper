import { Shelf } from '../Shelf/Shelf';
import { BookOnShelf } from '../BookOnShelf/BookOnShelf';
import { useEffect, useState } from 'react'
import globalStyles from '../../Global.module.css'

export function Bookshelf(props)
{
    const [books, setBooks] = useState('')

    useEffect(() => {
        setBooks(props.books.map((book) => <BookOnShelf book={book} key={book.id}/>));
    }, [props.books]);

    return(
        <>
            <div className={`${globalStyles.pad} ${globalStyles.scrollable}`} style={{marginLeft:'50px', width:'55%', height:'80%', backgroundColor:'rgb(240, 230, 211)','--padding':'25px'}}>
                {books}
            </div>
        </>
    )
}