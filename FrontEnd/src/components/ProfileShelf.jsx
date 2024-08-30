import { useState, useEffect } from "react";
import { Profile } from "./Profile";
import { Bookshelf } from "./Bookshelf";
import axios from "axios";

export default function ProfileShelf()
{
    const [books, setBooks] = useState([]);
    const [user, setUser] = useState({});

    useEffect(() => {
        axios.get(`https://localhost:7213/api/User/GetUserById/${1}`)
        .then(response => {
            if(response.data.success)
            {
                let body = response.data.payload;
                setUser({
                    username: body.username,
                    description: body.description,
                    dateJoined: body.date_joined,
                    picture: body.profile_picture,
                    booksRead: body.booksRead,
                    pagesRead: body.pagesRead
                });
            }
        })
        .catch(err => console.error(err))

        axios.get(`https://localhost:7213/api/Book/GetBooksByUser/${1}`)
        .then(response => {
            if(response.data.success)
            {
                let body = response.data.payload;
                setBooks(body.map(book => ({
                    id: book.pKey,
                    title: book.title,
                    isbn: book.isbn,
                    rating: book.rating,
                    pages: book.pages,
                    cover: book.cover_picture
                })))
            }
        })
        .catch(err => console.error(err))
    }, [])

    return (
        <>
        <div id="app">
            <div id="profile-column"><Profile user={user}/></div>
            <div id="shelf-column"><Bookshelf books={books}/></div>
        </div>
        </>
    )
}