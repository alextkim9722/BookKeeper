import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import globalStyles from '../../Global.module.css';
import styles from './BookOnShelf.module.css';

export function BookOnShelf(props)
{
    const navigate = useNavigate();

    const [book, setBook] = useState({});
    
    useEffect(() => {
        setBook(props.book);
    }, [props.book]);

    const render = (
        <div className={`${globalStyles.interactible} ${globalStyles.fillContainer}`}>
            <img className={`${globalStyles.fillContainer}`} src={book.cover} onClick={() => navigate(`/bookDetailed`, {state: book})}></img>
            <h6 id={`${styles.bookTitle}`}>{book.title}</h6>
        </div>
    )

    return(
        <>
            <div id={`${styles.bookPosition}`}>
                {render}
            </div>
        </>
    );
}