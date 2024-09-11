import { useState, useEffect, createContext } from "react";
import { Profile } from "./Profile/Profile";
import { Bookshelf } from "./Bookshelf/Bookshelf";
import globalStyles from '../Global.module.css';
import axios from "axios";

export const UserContext = createContext(1);

export default function ProfileShelf()
{
    const [books, setBooks] = useState([]);

    useEffect(() => {
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
        <div className={`${globalStyles.pad} ${globalStyles.center}`} style={{display:'flex',flexDirection:'row',alignItems:'center',justifyContent:'center','--padding':'50px', '--width':'100%'}}>
            <UserContext.Provider value='1'>
                <Profile />
                <Bookshelf books={books}/>
            </UserContext.Provider>
        </div>
        </>
    )
}