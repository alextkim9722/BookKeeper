import { BookOnShelf } from '../BookOnShelf/BookOnShelf';
import { useEffect, useState } from 'react';
import styles from './Shelf.module.css'

export function Shelf(props)
{
    const [books, setBooks] = useState('')

    useEffect(() => {
        setBooks(props.books.map((book) => <BookOnShelf book={book} key={book.id}/>));
    }, [props.books]);

    return(
        <>
            <div style={{width:'100%'}}>
                <img src="/src/assets/shelf-head.png" />
                <div id={styles.bookStackPos}> {books} </div>
            </div>
        </>
    )
}