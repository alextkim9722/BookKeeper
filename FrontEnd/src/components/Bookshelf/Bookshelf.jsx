import { Shelf } from '../Shelf/Shelf';
import { useEffect, useState } from 'react'
import globalStyles from '../../Global.module.css'

export function Bookshelf(props)
{
    const [books, setBooks] = useState([])

    useEffect(() => {
        setBooks(props.books)
    }, [props.books]);

    return(
        <>
            <div className={`${globalStyles.leftEnd}`} style={{width:'70%'}}>
                <Shelf books={books.slice(0, 5)}/>
            </div>
        </>
    )
}