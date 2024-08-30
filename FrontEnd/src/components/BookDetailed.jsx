import { useEffect, useState } from 'react'
import { useLocation } from 'react-router-dom';
import axios from "axios";
import './BookDetailed.css'

export default function BookDetailed()
{
    const location = useLocation();

    const [book, setBook] = useState({});
    const [reviews, setReviews] = useState([]);
    const [authors, setAuthors] = useState("");
    const [genres, setGenres] = useState([]);
    const [users, setUsers] = useState([]);

    useEffect(() => {
        var body = location.state;
        setBook(body);

        axios.get(`https://localhost:7213/api/Review/GetReviewByBookId/${body.id}`)
        .then(response => {
            if(response.data.success)
            {
                var payload = response.data.payload;
                setReviews(payload.map(review => ({
                    userId: review.firstKey,
                    bookId: review.secondKey,
                    description: review.description,
                    rating: review.rating,
                    date: review.date_submitted
                })))
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

        axios.get(`https://localhost:7213/api/User/GetUsersByBook/${body.id}`)
        .then(response => {
            if(response.data.success)
            {
                var payload = response.data.payload;
                setUsers(payload.map(user => ({
                    id: user.pKey,
                    userName: user.username
                })))
            }
        })
        .catch(err => console.error(err))

    }, [location.state]);

    return(
        <>
        <div id="detailed-window">
            <div id="detailed-padding">
                <img id="big-cover" src={book.cover} />
                <div id="book-info">
                    <div> <h1>{book.title}</h1> </div>
                    <div> <h2>{book.rating}/10</h2> </div>
                    <div> <p>{authors}</p> </div>
                    <div> <p>{genres}</p> </div>
                    <div> <p>{users.length}</p> </div>
                </div>
                <div>hello</div>
            </div>
        </div>
        </>
    )
}