import { useEffect, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import ReviewCrunched from '../ReviewCrunched/ReviewCrunched';
import axios from 'axios';

import globalStyles from '../../Global.module.css';
import styles from './BookDetailed.module.css';
import { createPortal } from 'react-dom';
import ReviewCreate from '../ReviewCreate/ReviewCreate';

export default function BookDetailed()
{
    const location = useLocation();
    const navigate = useNavigate();

    const [book, setBook] = useState({});
    const [reviews, setReviews] = useState([]);
    const [authors, setAuthors] = useState("");
    const [genres, setGenres] = useState([]);
    const [users, setUsers] = useState([]);

    const [createPopup, setCreatePopup] = useState(false);

    useEffect(() => {
        var body = location.state;
        setBook(body);

        axios.get(`https://localhost:7213/api/User/GetUsersByBook/${body.id}`)
        .then(response => {
            if(response.data.success)
            {
                var payload = response.data.payload;
                var usersArray = [];
                payload.map(user => {
                    usersArray.push([user.pKey, user.username]);
                });

                setUsers(Object.fromEntries(usersArray.map(([key, value]) => [key, value])));
            }
        })
        .catch(err => console.error(err))

        axios.get(`https://localhost:7213/api/Author/GetAuthorByBook/${body.id}`)
        .then(response => {
            if(response.data.success)
            {
                var payload = response.data.payload;
                setAuthors(payload.map((author) => (author.full_name + ", ")))
            }
        })
        .catch(err => console.error(err))

        axios.get(`https://localhost:7213/api/Genre/GetGenreByBook/${body.id}`)
        .then(response => {
            if(response.data.success)
            {
                var payload = response.data.payload;
                setGenres(payload.map(genre => (genre.genre_name + ", ")))
            }
        })
        .catch(err => console.error(err))

    }, [location.state]);

    useEffect(() =>{
        var body = location.state;
        axios.get(`https://localhost:7213/api/Review/GetReviewByBookId/${body.id}`)
        .then(response => {
            if(response.data.success)
            {
                var payload = response.data.payload;
                setReviews(payload.map(review => (<ReviewCrunched review={review} username={users[`${review.firstKey}`]} key={[review.firstKey, review.secondKey]}/>)))
            }
        })
        .catch(err => console.error(err))
    }, [users]);

    return(
        <>
        <div id={`${styles.detailedWindow}`} className={`${globalStyles.fillContainer} ${globalStyles.pad} ${globalStyles.center}`} style={{'--padding':'50px'}}>
            <div className={`${globalStyles.interactible} ${globalStyles.exit}`} onClick={() => navigate('/')}></div>
            <img className={`${globalStyles.bookCover} ${globalStyles.left}`}  style={{'--scale':'200px'}} src={book.cover} />
            <div id={`${styles.bookInfo}`} className={`${globalStyles.leftEnd} ${globalStyles.breakOverlap}`} style={{height:'600px'}}>
                <div> <h1>{book.title}</h1> </div>
                <div> <h2>{book.rating}/10</h2> </div>
                <div> {authors} </div>
                <div> {genres} </div>
                <div> {Object.keys(users).length} </div>
            </div>
            <div id={`${styles.bookReviews}`}>
                <div>
                    <span style={{fontWeight: 'bold'}}>Reviews</span>
                    <span className={`${globalStyles.rightJustified} ${globalStyles.interactible}`} onClick={() => setCreatePopup(true)}>Write Review</span>
                </div>
                <div className={`${globalStyles.scrollable} ${globalStyles.center}`} style={{height:'150px'}}>{reviews}</div>
            </div>

            {createPopup && <ReviewCreate bookId={location.state.id}/>}
        </div>
        </>
    )
}