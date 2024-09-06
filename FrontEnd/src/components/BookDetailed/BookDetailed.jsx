import { useEffect, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import ReviewCrunched from '../ReviewCrunched/ReviewCrunched';
import axios from 'axios';

import GetRating from '../GetRating';
import ReviewCreate from '../ReviewCreate/ReviewCreate';

import globalStyles from '../../Global.module.css';
import styles from './BookDetailed.module.css';

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
            <div className={`${globalStyles.interactible} ${globalStyles.darkText}`} style={{width:'fit-content'}} onClick={() => navigate('/')}>Return</div>
            <div id={`${styles.content}`}>
                <div id={`${styles.bookFrame}`} className={`${globalStyles.left}`}>
                    <img className={`${globalStyles.bookCover}`}  style={{'--scale':'150px'}} src={book.cover} />
                </div>
                <div id={`${styles.information}`} className={`${globalStyles.leftEnd}`}>
                    <h1>{book.title}</h1>
                    <table className={`${globalStyles.breakOverlap}`}>
                        <tbody>
                            <tr>
                                <td>Rating:</td>
                                <td>{GetRating(book.rating)}</td>
                            </tr>
                            <tr>
                                <td>Authors:</td>
                                <td>{authors}</td>
                            </tr>
                            <tr>
                                <td>Genres:</td>
                                <td>{genres}</td>
                            </tr>
                            <tr>
                                <td>Readers:</td>
                                <td>{Object.keys(users).length}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div id={`${styles.bookReviews}`}>
                <div style={{fontWeight: 'bold', fontSize: '30px'}}>Reviews</div>
                <div className={`${globalStyles.scrollable} ${globalStyles.center}`} style={{height:'150px'}}>{reviews}</div>
                <div className={`${globalStyles.rightJustified} ${globalStyles.interactible}`} onClick={() => setCreatePopup(true)}>Write Review</div>
            </div>

            {createPopup && <ReviewCreate bookId={location.state.id} popupBool={() => setCreatePopup(false)}/>}
        </div>
        </>
    )
}