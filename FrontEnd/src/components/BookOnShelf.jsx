import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './BookOnShelf.css';

export function BookOnShelf(props)
{
    const navigate = useNavigate();

    const [book, setBook] = useState({});
    
    useEffect(() => {
        setBook(props.book);
    }, [props.book]);

    const render = (
        <div id='book'>
            <img className='book-cover' src={book.cover} onClick={() => navigate(`/bookDetailed`, {state: book})}></img>
            <h6 id='book-title'>{book.title}</h6>
        </div>
    )

    return(
        <>
            <div id='book-position'>
                {render}
            </div>
        </>
    );
}