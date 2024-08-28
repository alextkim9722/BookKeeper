import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { JsonRender } from '../JsonRender';
import axios from 'axios';
import './BookOnShelf.css';

export function BookOnShelf(props)
{
    const [success, setSuccess] = useState(false);
    const [error, setError] = useState('');
    const [title, setTitle] = useState('');
    const [coverPicture, setCoverPicture] = useState('');
    
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
    }, [title, coverPicture]);

    const render = (
        <div id='book'>
            <img className='book-cover' src={coverPicture}></img>
            <h6 id='book-title'>{title}</h6>
        </div>
    )

    return(
        <>
            <div id='book-position'>
                <JsonRender success={success} content={render} error={error} />
            </div>
        </>
    );
}